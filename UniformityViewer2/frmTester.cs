using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// using UniformityCalculator2;
using Point = OpenCvSharp.Point;

namespace UniformityViewer2
{
    public partial class frmTester : Form
    {
        private static frmTester mInstance = null;

        public static frmTester Instance {
            get {
                if (mInstance == null || mInstance.IsDisposed) mInstance = new frmTester();
                return mInstance;
            }
        }

        public frmTester()
        {
            InitializeComponent();
            this.FormClosed += FrmTester_FormClosed;
            leS_ValueChanged(leS, null);
            mirrorShapeCB.SelectedIndex = 0;

            doubleNumericTextBox2_ValueChanged(null, new EventArgs());
        }

        private void FrmTester_FormClosed(object sender, FormClosedEventArgs e)
        {
            mInstance = null;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow")]

        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        private Window mViewer = null;
        private Window mViewer2 = null;
        private Window mViewer3 = null;

        public Window Viewer {
            get {
                if (mViewer == null)
                {
                    mViewer = new Window("Original Data", WindowMode.KeepRatio);
                }

                return mViewer;
            }
        }

        public Window Viewer2 {
            get {
                if (mViewer2 == null)
                {
                    mViewer2 = new Window("Mirror Image", WindowMode.KeepRatio);
                }

                return mViewer2;
            }
        }

        public Window Viewer3 {
            get {
                if (mViewer3 == null)
                {
                    mViewer3 = new Window("Blended", WindowMode.KeepRatio);
                }

                return mViewer3;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Mat resultMat, mirrorMat;
            GetImageMat(out resultMat, out mirrorMat);

            Console.WriteLine((double)resultMat.Height / resultMat.Width);

            Viewer.Resize(resultMat.Width, resultMat.Height);
            Viewer.ShowImage(resultMat);
            Viewer2.Resize(mirrorMat.Width, mirrorMat.Height);
            Viewer2.ShowImage(mirrorMat);
        }


        private void doubleNumericTextBox2_ValueChanged(object sender, EventArgs e)
        {
            doubleNumericTextBox4.Value = (decimal)Math.Sqrt(Math.Pow((double)horizonFOVTextBox.Value, 2) + Math.Pow((double)verticalFOVTextBox.Value, 2));
        }

        internal void SetData(int mirrorType, double lightEffi, int lines, double pupil, SizeF pinSize)
        {
            mirrorShapeCB.SelectedIndex = mirrorType;
            radioButton1.Checked = true;
            leS.Value = (decimal)lightEffi;
            pinLines.Value = lines;
            psS.Value = (decimal)pupil;
            pisS.Value = (decimal)pinSize.Width;
            pisE.Value = (decimal)pinSize.Height;
        }

        private void btnImageOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = dlg.FileName;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Mat img;
            bool isImgExist = SaveImage(out img);
            if (isImgExist)
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "png파일|*.png";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        img.SaveImage(dlg.FileName);
                        MessageBox.Show("저장됨");
                    }
                }
            }
            else
            {
                MessageBox.Show("저장 실패!");
            }
        }

        private void GetImageMat(out Mat resultMat, out Mat mirrorMat)
        {
            double light = (double)leS.Value;
            double pupil = (double)psS.Value;
            double pinmrWidth = (double)pisS.Value;
            double pinmrHeight = (double)pisE.Value;
            double pinmirrorGap = (double)lg.Value;

            Mat kernel = KernelManager.GetKernel((decimal)pinmrWidth, (decimal)pinmrHeight, 33, mirrorShapeCB.SelectedIndex);

            ImageManager.ProcessImageResult ret = ImageManager.GetInstance().ProcessImage((int)pinLines.Value, pinmirrorGap, pupil, kernel, 0, false, chkInvert.Checked);
            //Viewer.ShowImage(ret.Result);

            resultMat = ImageProcessor.Instance.GetResultMat(ret, (double)eyeReliefTextBox.Value, (double)horizonFOVTextBox.Value, (double)verticalFOVTextBox.Value);

            mirrorMat = ImageProcessor.Instance.GetMirrorMat(ret);

            if (string.IsNullOrWhiteSpace(txtImagePath.Text) == false && File.Exists(txtImagePath.Text))
            {
                Mat img = ImageProcessor.Instance.GetImageMat(resultMat, txtImagePath.Text);

                Viewer3.Resize(img.Width, img.Height);
                Viewer3.ShowImage(img);
            } // 이미지 파일 여부, 이미지 파일 Show.

            resultMat.PutText($"Efficiency : {light}%", new Point(0, 20), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"pupilSize : {pupil}mm", new Point(0, 40), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"pinmirrorSize : {pinmrWidth}mm", new Point(0, 60), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"pinmirror_Gap : {pinmirrorGap}mm", new Point(0, 80), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"Max-Avg : {ret.MaxAvg}", new Point(0, 100), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"Min-Avg : {ret.MinAvg}", new Point(0, 120), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"MeanDev : {ret.MeanDev}", new Point(0, 140), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"LumperDegree(Max) : {ret.LumDegreeMax}", new Point(0, 160), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"LumperDegree(Avg) : {ret.LumDegreeAvg}", new Point(0, 180), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
        }

        private bool SaveImage(out Mat img)
        {
            double pupil = (double)psS.Value;
            double pinmrWidth = (double)pisS.Value;
            double pinmrHeight = (double)pisE.Value;
            double pinmirrorGap = (double)lg.Value;

            Mat kernel = KernelManager.GetKernel((decimal)pinmrWidth, (decimal)pinmrHeight, 33, mirrorShapeCB.SelectedIndex);

            var ret = ImageManager.GetInstance().ProcessImage((int)pinLines.Value, pinmirrorGap, pupil, kernel, 0, false);

            Viewer.ShowImage(ret.Result); // Original Data

            Mat resultMat = ImageProcessor.Instance.GetResultMat(ret, (double)eyeReliefTextBox.Value, (double)horizonFOVTextBox.Value, (double)verticalFOVTextBox.Value);

            if (string.IsNullOrWhiteSpace(txtImagePath.Text) == false && File.Exists(txtImagePath.Text))
            {
                img = ImageProcessor.Instance.GetImageMat(resultMat, txtImagePath.Text); // 사진 선택.
                return true;
            }
            else
            {
                img = null; // 사진 미선택.
                return false;
            }
        }

        private void pisS_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                leS_ValueChanged(leS, null);
            }
            else if (radioButton2.Checked)
            {
                lg_ValueChanged(lg, null);
            }
        }

        private void leS_ValueChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked) return;

            double light = (double)leS.Value;
            double pinmr = (double)pisS.Value;

            lg.Value = (decimal)CalcValues.GetPinMirrorGap(light, pinmr);
        }

        private void lg_ValueChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked) return;

            double pingap = (double)lg.Value;
            double pinmr = (double)pisS.Value;

            leS.Value = (decimal)CalcValues.GetLightEffi(pinmr, pingap);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lg.Enabled = false;
            leS.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            lg.Enabled = true;
            leS.Enabled = false;
        }
    }
}
