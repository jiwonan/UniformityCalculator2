using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;

namespace UniformityViewer2.Charting
{
    public class CtlChartRenderer
    {
        private Controls.ctlHistLegend mChart;
        public CtlChartRenderer(Controls.ctlHistLegend chart)
        {
            this.mChart = chart;
        }

        public Mat LoadChart(Mat m, int gubun, Controls.TempPictureBox pictureBox)
        {
            if (m == null) { return null; }

            Mat[] channels = m.Split();

            Mat origin = new Mat();

            Mat targetMat = new Mat(); // = channels[idx];

            if (gubun == 0)
            {
                targetMat = channels[DataParser.CHANNEL_MAX_AVG];
            }
            else if (gubun == 1)
            {
                targetMat = channels[DataParser.CHANNEL_MIN_AVG];
            }
            else if (gubun == 2)
            {
                targetMat = channels[DataParser.CHANNEL_MEAN_DEV];
            }
            else if (gubun == 3)
            {
                Mat value1 = channels[DataParser.CHANNEL_MAX_AVG];
                Mat value2 = channels[DataParser.CHANNEL_MEAN_DEV];

                targetMat = (value1 + value2) / 2;
            }
            else if (gubun == 4)
            {
                Mat value1 = channels[DataParser.CHANNEL_MAX_AVG];
                Mat value2 = channels[DataParser.CHANNEL_MEAN_DEV];

                Cv2.Sqrt(value1.Mul(value2), targetMat);
            }
            else if (gubun == 5)
            {
                targetMat = channels[DataParser.CHANNEL_LUMPER_MAX];
            }
            else if (gubun == 6)
            {
                targetMat = channels[DataParser.CHANNEL_LUMPER_AVG];
            }

            double minVal, maxVal;

            int orginHeight = targetMat.Height;

            if (mChart.IsCustomMinMaxValue)
            {
                targetMat = targetMat.Threshold(mChart.MaxVal, mChart.MaxVal, ThresholdTypes.Trunc);
                targetMat = targetMat.Threshold(mChart.MinVal, mChart.MinVal, ThresholdTypes.Tozero);

                //CustomValue를 사용하는경우
                //Colormap을 적용했을때 색상 일관성을 위해
                //맨아래 한줄을 추가하고 MaxValue값을 지정해둔다
                targetMat = targetMat.Resize(new OpenCvSharp.Size(targetMat.Width, targetMat.Height + 1));
                targetMat[new Rect(0, orginHeight, targetMat.Width, 1)].SetTo(new Scalar(mChart.MaxVal));
            }

            targetMat.Normalize(255, 0, NormTypes.MinMax);
            targetMat.ConvertTo(origin, MatType.CV_32FC1);

            Cv2.MinMaxLoc(targetMat, out minVal, out maxVal);

            if (minVal == 0 && maxVal == 0)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
                pictureBox.Invalidate();

                return null;
            }

            mChart.MaxVal = maxVal;
            mChart.MinVal = minVal;

            Mat hist = new Mat();
            int[] histSize = { 100 };

            minVal = Math.Min(mChart.MinVal, mChart.MaxVal);
            maxVal = Math.Max(mChart.MinVal, mChart.MaxVal);

            Rangef[] ranges = { new Rangef((float)minVal, (float)maxVal + 0.001f) };

            Cv2.CalcHist(new Mat[] { origin }, new int[] { 0 }, null, hist, 1, histSize, ranges);

            mChart.HistMat = hist;

            mChart.Invalidate();

            Mat target = new Mat();

            targetMat.Normalize(255, 0, NormTypes.MinMax).ConvertTo(origin, MatType.CV_8U);

            Cv2.ApplyColorMap(origin, target, ColormapTypes.Jet);

            target = target[new Rect(0, 0, target.Width, orginHeight)].Resize(new OpenCvSharp.Size(pictureBox.Width, pictureBox.Height), 0, 0, InterpolationFlags.Area);

            pictureBox.Image = target.ToBitmap();
            return target.Clone();
        }

        public void ClearChart()
        {
            mChart.HistMat = new Mat();
            mChart.Invalidate();
        }
    }
}
