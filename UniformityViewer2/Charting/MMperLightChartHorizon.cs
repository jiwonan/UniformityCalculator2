using OpenCvSharp;
using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace UniformityViewer2.Charting
{
    public class MMperLightChartHorizon : MMperLightChartRenderer
    {
        public MMperLightChartHorizon(Chart chart, Mat lightImage) : base(chart, lightImage, Controls.Lines.LineView.LineType.Horizon) { }

        protected override void GetDetailData(int searchPos, Controls.Lines.LineView.LineType type)
        {
            if (type == mLineType)
            {
                for (int col = 0; col < mLightImage.Cols; col++)
                {
                    Console.WriteLine($"{col * UniformityCalculator2.Data.CalcValues.MMperPixel}mm : {mLightImage.Get<byte>(searchPos, col)}");
                }
            }
        }

        protected override int GetLength()
        {
            return mLightImage.Cols;
        }

        protected override Series GetSeriesData(Series data, int searchPos)
        {
            for (int col = 0; col < mLightImage.Cols; col++)
            {
                data.Points.AddXY(col * UniformityCalculator2.Data.CalcValues.MMperPixel, mLightImage.Get<byte>(searchPos, col));
            }

            return data;
        }
    }
}
