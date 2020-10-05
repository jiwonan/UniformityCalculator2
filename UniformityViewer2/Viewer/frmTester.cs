using OpenCvSharp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace UniformityViewer2.Viewer
{
    public partial class frmTester : Form
    {
        private static frmTester mInstance = null;

        public static frmTester Instance {
            get {
                if (mInstance == null || mInstance.IsDisposed)
                {
                    mInstance = new frmTester();
                }

                return mInstance;
            }
        }

        private bool isNew = false;

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

        /*[DllImport("user32.dll", EntryPoint = "FindWindow")]

        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);*/

        // [StructLayout(LayoutKind.Sequential)]

        /*public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
*/
        // [DllImport("user32.dll", SetLastError = true)]
        // static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

       /* private Window mViewer = null;
        private Window mViewer2 = null;*/
        private Window mViewer3 = null;

/*        public Window Viewer {
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
*/
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
            ShowDataViewer();
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

        public struct PinInfo
        {
            public double light;
            public double pupil;
            public double pinmrWidth;
            public double pinmrHeight;
            public double pinmrGap;
            public SizeF pinmrSize;

            public PinInfo(double light, double pupil, double width, double height, double gap)
            {
                this.light = light;
                this.pupil = pupil;
                pinmrWidth = width;
                pinmrHeight = height;
                pinmrGap = gap;
                pinmrSize = new SizeF((float)pinmrWidth, (float)pinmrHeight);
            }
        }

        private void ShowDataViewer()
        {
            Mat resultMat, mirrorMat;
            PinInfo info;
            UniformityCalculator2.Image.ImageManager.ProcessImageResult ret;
            GetImageMat(out mirrorMat, out resultMat, out info, out ret);

            if (string.IsNullOrWhiteSpace(txtImagePath.Text) == false && File.Exists(txtImagePath.Text))
            {
                Mat img = ImageProcessor.Instance.GetImageMat(resultMat, txtImagePath.Text);

                Viewer3.Resize(img.Width, img.Height);
                Viewer3.ShowImage(img);
            } // 이미지 파일 여부, 이미지 파일 Show.

            frmDataViewer viewer;

            if (isNew)
            {
                viewer = frmDataViewer.GetInstance();
            }
            else
            {
                viewer = new frmDataViewer();
            }

            viewer.LoadResultData(mirrorMat.Clone(), resultMat.Clone(), ret, info, resultMat.Clone(), mirrorShapeCB.SelectedIndex);
            viewer.Show();
        }

        private void GetImageMat(out Mat resultMat)
        {
            Mat m;
            PinInfo info;
            UniformityCalculator2.Image.ImageManager.ProcessImageResult ret;
            GetImageMat(out m, out resultMat, out info, out ret);

            m.Dispose();
            ret.Dispose();
        }

        private void GetImageMat(out Mat mirrorMat, out Mat resultMat, out PinInfo info, out UniformityCalculator2.Image.ImageManager.ProcessImageResult ret)
        {
            info = new PinInfo(0, (double)psS.Value, (double)pisS.Value, (double)pisE.Value, (double)lg.Value);

            Mat kernel = UniformityCalculator2.Image.KernelManager.GetKernel((decimal)info.pinmrWidth, (decimal)info.pinmrHeight, 33, mirrorShapeCB.SelectedIndex);

            ret = UniformityCalculator2.Image.ImageManager.GetInstance().ProcessImage((int)pinLines.Value, info.pinmrGap, info.pupil, kernel, 0, false);

            mirrorMat = ImageProcessor.Instance.GetMirrorMat(ret);

            resultMat = ImageProcessor.Instance.GetResultMat(ret, (double)eyeReliefTextBox.Value, (double)horizonFOVTextBox.Value, (double)verticalFOVTextBox.Value);
        }

        private bool SaveImage(out Mat img)
        {
            Mat resultMat;

            GetImageMat(out resultMat);

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
            if (!radioButton1.Checked)
            {
                return;
            }

            double light = (double)leS.Value;
            double pinmr = (double)pisS.Value;

            lg.Value = (decimal)UniformityCalculator2.Data.CalcValues.GetPinMirrorGap(light, pinmr);
        }

        private void lg_ValueChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked)
            {
                return;
            }

            double pingap = (double)lg.Value;
            double pinmr = (double)pisS.Value;

            leS.Value = (decimal)UniformityCalculator2.Data.CalcValues.GetLightEffi(pinmr, pingap);
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
