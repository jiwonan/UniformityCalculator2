using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;

namespace UniformityViewer2.Viewer
{
    public partial class frmDataViewer : Form
    {
        private static frmDataViewer mInstance = null;

        public static bool HasInstance => mInstance != null;

        DB.DBMtfchart mtfchart = new DB.DBMtfchart();

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
        }
        private void FrmDataViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mInstance == this) mInstance = null;
        }

        private void FrmDataViewer_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) return;

            ResizeSpliter();
        }

        private void ResizeSpliter()
        {

            splitContainer1.SplitterDistance = splitContainer1.Width / 2;
            splitContainer2.SplitterDistance = splitContainer2.Height / 2;
            splitContainer3.SplitterDistance = splitContainer3.Height / 2;
        }

        public void LoadResultData(DB.DetailInfo detailInfo, Tuple<int, double> lineAndInnerPercent, string shapeType)
        {

            double light = 0, pupil = 0;
            SizeF pinmirrorSize;

            light = detailInfo.Light;
            pupil = detailInfo.Pupil;
            pinmirrorSize = detailInfo.pinMirrorSize;

            int lines = lineAndInnerPercent.Item1;
            double innerPercent = lineAndInnerPercent.Item2;

            if (lines == -1)
            {
                MessageBox.Show("오류!!");
                return;
            }

            double pinmirrorGap = Data.CalcValues.GetPinMirrorGap(light, pinmirrorSize);



            Mat kernel = Image.KernelManager.GetKernel(pinmirrorSize, (decimal)innerPercent, detailInfo.ShapeType);

            var ret = Image.ImageManager.GetInstance().ProcessImage(lines, pinmirrorGap, pupil, kernel, -1, true);

            double zoom = 600d / ret.Result.Width * 100;

            OpenCvSharp.Rect roi = new OpenCvSharp.Rect(Math.Max(0, ret.Result.Width / 2 - 500), Math.Max(0, ret.Result.Height / 2 - 500), Math.Min(1000, ret.Result.Width), Math.Min(1000, ret.Result.Height));

            Mat resultMat = ret.Result[roi].Resize(new OpenCvSharp.Size(600, 600));
            Mat mirrorMat = ret.MirrorImage[roi].Resize(new OpenCvSharp.Size(600, 600));

            resultMat.PutText($"Zoom : {zoom:0.00}%", new Point(0, 20), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"ShapeType : {shapeType}", new Point(0, 40), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"Efficiency : {light}% pupilSize : {pupil}mm", new Point(0, 60), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"pinmirrorSize : {pinmirrorSize}mm", new Point(0, 80), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"pinmirror_Gap : {pinmirrorGap}mm", new Point(0, 100), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"Max-Avg : {ret.MaxAvg}", new Point(0, 120), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"Min-Avg : {ret.MinAvg}", new Point(0, 140), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"MeanDev : {ret.MeanDev}", new Point(0, 160), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"LumperDegree(Max) : {ret.LumDegreeMax}", new Point(0, 180), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"LumperDegree(Avg) : {ret.LumDegreeAvg}", new Point(0, 200), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"Real Efficiency : {light * (pinmirrorSize.Width / pinmirrorSize.Height)}%", new Point(0, 220), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);

            mirrorMat.ConvertTo(mirrorMat, MatType.CV_8U);
            picPinmirror.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mirrorMat);

            Mat m = resultMat.Split()[0];
            m *= 255;
            m.ConvertTo(resultMat, MatType.CV_8U);
            picPsf.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resultMat);

            mtfchart.DrawChart(chartWidth, pinmirrorSize, Math.Truncate(pinmirrorGap * 10) / 10);

            resultMat.Dispose();
            m.Dispose();
            mirrorMat.Dispose();
        }
    }
}
