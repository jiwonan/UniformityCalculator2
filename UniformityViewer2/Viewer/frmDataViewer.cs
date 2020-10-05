using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UniformityViewer2.Controls.Lines;
using Point = OpenCvSharp.Point;

namespace UniformityViewer2.Viewer
{
    public partial class frmDataViewer : Form
    {
        private static frmDataViewer mInstance = null;

        public static bool HasInstance => mInstance != null;

        DB.DBMtfchart mtfchart = new DB.DBMtfchart();
        List<Charting.ChartRenderer> charts;

        public static frmDataViewer GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new frmDataViewer();
            }

            return mInstance;
        }

        public frmDataViewer()
        {
            InitializeComponent();

            this.FormClosed += FrmDataViewer_FormClosed;

            this.Resize += FrmDataViewer_Resize;

            //4분할 화면 유지
            ResizeSpliter();

            charts = new List<Charting.ChartRenderer>();

            picPsf.OnLineMove += PicPsf_OnLineMove;
        }


        private void PicPsf_OnLineMove(object sender, LineView.LineType lineType, int position)
        {
            foreach (var chart in charts)
            {
                chart.DrawChart(sender, position, lineType);
            }

            ShowLinePosition(sender, lineType, position);
        }

        private void ShowLinePosition(object sender, LineView.LineType lineType, int position)
        {
            if (lineType == LineView.LineType.Horizon)
                posH = charts[0].CalcSearchPos(sender, position);
            else
                posV = charts[1].CalcSearchPos(sender, position);

            linePosToolStripLabel.Text = $"H : {Math.Round(posH * UniformityCalculator2.Data.CalcValues.MMperPixel, 2)}mm, V : {Math.Round(posV * UniformityCalculator2.Data.CalcValues.MMperPixel, 2)}mm";
        }

        private void CreateCharts(Mat lightImage)
        {
            charts.Clear();

            charts.Add(new Charting.MMperLightChartHorizon(MMperLightChartHorizon, lightImage));
            charts.Add(new Charting.MMperLightChartVertical(MMperLightChartVertical, lightImage));

            charts[0].CreateChart();
            charts[1].CreateChart();

            charts[0].DrawChart(picPsf, picPsf.Height / 2, LineView.LineType.Horizon);
            charts[1].DrawChart(picPsf, picPsf.Width / 2, LineView.LineType.Vertical);

            posH = lightImage.Height / 2;
            posV = lightImage.Width / 2;

            linePosToolStripLabel.Text = $"H : {Math.Round(posH * UniformityCalculator2.Data.CalcValues.MMperPixel, 2)}mm, V : {Math.Round(posV * UniformityCalculator2.Data.CalcValues.MMperPixel, 2)}mm";
        }

        private void FrmDataViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mInstance == this)
            {
                mInstance = null;
            }
        }

        private void FrmDataViewer_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            ResizeSpliter();
        }

        private void ResizeSpliter()
        {
            splitContainer1.SplitterDistance = splitContainer1.Width / 2;
            splitContainer2.SplitterDistance = splitContainer2.Height / 2;
            splitContainer3.SplitterDistance = splitContainer3.Height / 2;
        }

        private double CalcImageHeight(Mat m)
        {
            return (double)m.Height / 600;
        }

        private string GetShapeType(int idx)
        {
            string type = "";
            switch (idx)
            {
                case 0:
                    type = "Circle";
                    break;
                case 1:
                    type = "Circle_Circle";
                    break;
                case 2:
                    type = "Hexa";
                    break;
                case 3:
                    type = "Hexa_Circle";
                    break;
            }

            return type;
        }

        public void LoadResultData(DB.DetailInfo detailInfo, Tuple<int, double> lineAndInnerPercent, int shapeType)
        {
            int lines = lineAndInnerPercent.Item1;
            double innerPercent = lineAndInnerPercent.Item2;

            if (lines == -1)
            {
                MessageBox.Show("오류!!");
                return;
            }

            double pinmirrorGap = UniformityCalculator2.Data.CalcValues.GetPinMirrorGap(detailInfo.Light, detailInfo.pinMirrorSize);

            frmTester.PinInfo info = new frmTester.PinInfo(detailInfo.Light, detailInfo.Pupil, detailInfo.pinMirrorSize.Width, detailInfo.pinMirrorSize.Height, pinmirrorGap);

            Mat kernel = UniformityCalculator2.Image.KernelManager.GetKernel(info.pinmrSize, (decimal)innerPercent, detailInfo.ShapeType);

            var ret = UniformityCalculator2.Image.ImageManager.GetInstance().ProcessImage(lines, info.pinmrGap, info.pupil, kernel, -1, true);

            double zoom = 600d / ret.Result.Width * 100;

            Rect roi = new Rect(Math.Max(0, ret.Result.Width / 2 - 500), Math.Max(0, ret.Result.Height / 2 - 500), Math.Min(1000, ret.Result.Width), Math.Min(1000, ret.Result.Height));
            double ratio = CalcImageHeight(ret.MirrorImage[roi]);

            Mat show = UniformityCalculator2.Image.ImageManager.GetInstance().DrawHexagon(ret.Result.Clone())[roi].Resize(new OpenCvSharp.Size(ret.Result[roi].Width / ratio, ret.Result[roi].Height / ratio));

            show.PutText($"Zoom : {zoom:0.00}%", new Point(0, 20), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);

            LoadResultData(ret.MirrorImage[roi], ret.Result[roi], ret, info, show, shapeType, 20);
        }

        private Bitmap GetMirrorImage(Mat mirrorMat, double ratio)
        {
            mirrorMat.ConvertTo(mirrorMat, MatType.CV_8U);
            mirrorMat = mirrorMat.Resize(new OpenCvSharp.Size(mirrorMat.Width / ratio, mirrorMat.Height / ratio));
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mirrorMat);
        }

        private void SetChart(Mat resultMat, SizeF pinmrSize, double pinmrGap, double ratio)
        {
            Mat m = resultMat.Split()[0];
            m *= 255;
            m.ConvertTo(resultMat, MatType.CV_8U);

            string str = mtfchart.DrawChart(chartWidth, pinmrSize, Math.Truncate(pinmrGap * 100) / 100);
            picPsf.Image = resultMat.Resize(new OpenCvSharp.Size(resultMat.Width / ratio, resultMat.Height / ratio));

            string[] mtfValues = str.Split(',');

            mtf_d.Text = "간격 : " + mtfValues[0];
            mtf_r.Text = "크기 : " + mtfValues[1]; // 소수점 2자리

            CreateCharts(resultMat.Clone());
        }

        private Mat GetResultImage(Mat show, frmTester.PinInfo info, UniformityCalculator2.Image.ImageManager.ProcessImageResult ret, int shapeType, int pos)
        {
            show.PutText($"ShapeType : {GetShapeType(shapeType)}", new Point(0, 20+pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"Efficiency : {info.light}% pupilSize : {info.pupil}mm", new Point(0, 40 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"pinmirrorSize : {info.pinmrSize}mm", new Point(0, 60 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"pinmirror_Gap : {info.pinmrGap}mm", new Point(0, 80 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"Max-Avg : {ret.MaxAvg}", new Point(0, 100 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"Min-Avg : {ret.MinAvg}", new Point(0, 120 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"MeanDev : {ret.MeanDev}", new Point(0, 140 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"LumperDegree(Max) : {ret.LumDegreeMax}", new Point(0, 160 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"LumperDegree(Avg) : {ret.LumDegreeAvg}", new Point(0, 180 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            show.PutText($"Real Efficiency : {info.light * (info.pinmrWidth / info.pinmrHeight)}%", new Point(0, 200 + pos), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);

            return show;
        }

        public void LoadResultData(Mat mirrorMat, Mat resultMat, UniformityCalculator2.Image.ImageManager.ProcessImageResult ret, frmTester.PinInfo info, Mat show, int shapeType, int pos = 0)
        {
            double ratio = CalcImageHeight(mirrorMat);

            picPinmirror.Image = GetMirrorImage(mirrorMat, ratio);

            SetChart(resultMat, info.pinmrSize, info.pinmrGap, ratio);

            Mat m = show.Split()[0];
            m *= 255;
            m.ConvertTo(show, MatType.CV_8U);

            picPsf.Image = GetResultImage(show, info, ret, shapeType,pos);

            resultMat.Dispose();
            show.Dispose();
            m.Dispose();
            mirrorMat.Dispose();
        }

        private int posV, posH;

        private void picPsf_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(picPsf, e.X, e.Y);
            }
        }

        private void horizonValueToolStripMenuItem_Click(object sender, EventArgs e) // 0
        {
            SavePosData(picPsf, LineView.LineType.Horizon, charts[0], posH);
        }

        private void verticalValueToolStripMenuItem_Click(object sender, EventArgs e) // 1
        {
            SavePosData(picPsf, LineView.LineType.Vertical, charts[1], posV);
        }

        private void SavePosData(object sender, LineView.LineType type, Charting.ChartRenderer chart, int position)
        {
            string data = chart.GetFileData(sender, type, position);

            if(data == null)
            {
                MessageBox.Show("파일 저장 실패");
            }

            using (SaveFileDialog dig = new SaveFileDialog())
            {
                dig.Filter = "txt파일|*.txt";
                if(dig.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetFullPath(dig.FileName);
                    File.WriteAllText(path, data);

                    System.Diagnostics.Process.Start(dig.FileName, path);
                }
            }
        }
    }
}
