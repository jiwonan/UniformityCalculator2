using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniformityCalculator2;
using Point = OpenCvSharp.Point;

namespace UniformityViewer
{
    public partial class frmTester : Form
    {
        private static frmTester mInstance = null;
        public static frmTester Instance
        {
            get
            {
                if (mInstance == null || mInstance.IsDisposed) mInstance = new frmTester();
                return mInstance;
            }
        }
        private frmTester()
        {
            InitializeComponent();
            this.FormClosed += FrmTester_FormClosed;
            leS_ValueChanged(leS, null);
            comboBox3.SelectedIndex = 0;

            doubleNumericTextBox2_ValueChanged(null, new EventArgs());
        }

        private void FrmTester_FormClosed(object sender, FormClosedEventArgs e)
        {
            mInstance = null;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            double light = (double)leS.Value;
            double pupil = (double)psS.Value;
            double pinmrWidth = (double)pisS.Value;
            double pinmrHeight = (double)piSE.Value;
            double pinmirrorGap = (double)lg.Value;

            

            Mat kernel = KernelManager.GetKernel((decimal)pinmrWidth, (decimal)pinmrHeight, 33, comboBox3.SelectedIndex);

            var ret = ImageManager.Instance.ProcessImage((int)pinLines.Value, pinmirrorGap, pupil, kernel, 0, false, chkInvert.Checked);
            //Viewer.ShowImage(ret.Result);

            Mat resultMat = CreateAlphaChannel(ret.Result, (double)doubleNumericTextBox1.Value, (double)doubleNumericTextBox2.Value, (double)doubleNumericTextBox3.Value);
            //Mat resultMat = ret.Result;
            Mat mirrorMat = ret.MirrorImage;

            double multi = 1.0d;

            if (resultMat.Width > resultMat.Height)
            {
                multi = 600d / resultMat.Width;
            }
            else if(resultMat.Height > resultMat.Width)
            {
                multi = 600d / resultMat.Height;
            }
            else
            {
                multi = 600d / resultMat.Width;
            }

            resultMat = resultMat.Resize(new OpenCvSharp.Size(resultMat.Width * multi, resultMat.Height * multi));


            if (mirrorMat.Width > mirrorMat.Height)
            {
                multi = 600d / resultMat.Width;
            }
            else if (mirrorMat.Height > mirrorMat.Width)
            {
                multi = 600d / mirrorMat.Height;
            }
            else
            {
                multi = 600d / mirrorMat.Width;
            }

            mirrorMat = mirrorMat.Resize(new OpenCvSharp.Size(mirrorMat.Width * multi, mirrorMat.Height * multi));

            if(string.IsNullOrWhiteSpace(txtImagePath.Text) == false && File.Exists(txtImagePath.Text))
            {
                Mat img = Cv2.ImRead(txtImagePath.Text, ImreadModes.Color);
                img = img.Resize(new OpenCvSharp.Size(resultMat.Width, resultMat.Height));

                Mat m = resultMat.ExtractChannel(0);

                Mat[] channels = img.Split();
                for(int i =0; i  < channels.Length; i++)
                {
                    Mat target = new Mat();
                    
                    channels[i].ConvertTo(target, MatType.CV_64FC1);

                    target = target.Mul(m).ToMat();

                    target.ConvertTo(target, MatType.CV_8UC1);

                    channels[i] = target;
                }

                Cv2.Merge(channels, img);

                Viewer3.Resize(img.Width, img.Height);
                Viewer3.ShowImage(img);
            }

            resultMat.PutText($"Efficiency : {light}%", new Point(0, 20), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"pupilSize : {pupil}mm", new Point(0, 40), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"pinmirrorSize : {pinmrWidth}mm", new Point(0, 60), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"pinmirror_Gap : {pinmirrorGap}mm", new Point(0, 80), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"Max-Avg : {ret.MaxAvg}", new Point(0, 100), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"Min-Avg : {ret.MinAvg}", new Point(0, 120), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"MeanDev : {ret.MeanDev}", new Point(0, 140), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"LumperDegree(Max) : {ret.LumDegreeMax}", new Point(0, 160), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
            resultMat.PutText($"LumperDegree(Avg) : {ret.LumDegreeAvg}", new Point(0, 180), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);

            Console.WriteLine((double)resultMat.Height / resultMat.Width);

            Viewer.Resize(resultMat.Width, resultMat.Height);
            Viewer.ShowImage(resultMat);
            Viewer2.Resize(mirrorMat.Width, mirrorMat.Height);
            Viewer2.ShowImage(mirrorMat);
        }

        //Viewer

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
        public Window Viewer
        {
            get
            {
                if (mViewer == null)
                {
                    mViewer = new Window("Original Data", WindowMode.KeepRatio);
                }

                return mViewer;
            }
        }
        public Window Viewer3
        {
            get
            {
                if (mViewer3 == null)
                {
                    mViewer3 = new Window("Blended", WindowMode.KeepRatio);
                }

                return mViewer3;
            }
        }

        //private void CSVExport_Click(object sender, EventArgs e)
        //{
        //    Window win = (Window)contextMenuStrip1.Tag;

        //    if (win.Image == null)
        //    {
        //        MessageBox.Show("이미지가 없습니다");
        //        return;
        //    }


        //    Mat resultMat = win.Image.Clone();

        //    using (SaveFileDialog dlg = new SaveFileDialog())
        //    {
        //        dlg.Filter = "CSV파일|*.csv";
        //        if (dlg.ShowDialog() == DialogResult.OK)
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            for (int row = 0; row < resultMat.Rows; row++)
        //            {
        //                for (int col = 0; col < resultMat.Cols; col++)
        //                {
        //                    decimal a = (decimal)resultMat.Get<double>(row, col);
        //                    sb.Append(a.ToString());
        //                    sb.Append(",");
        //                }
        //                sb.Remove(sb.Length - 1, 1);
        //                sb.AppendLine("");
        //            }

        //            File.WriteAllText(dlg.FileName, sb.ToString());
        //            MessageBox.Show("저장이 완료되었습니다");
        //        }
        //    }

        //}

        public Window Viewer2
        {
            get
            {
                if (mViewer2 == null)
                {
                    mViewer2 = new Window("Mirror Image", WindowMode.KeepRatio);
                }

                return mViewer2;
            }
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

        private void leS_ValueChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked) return;

            double light = (double)leS.Value;
            double pinmr = (double)pisS.Value;

            lg.Value = (decimal)ImageManager.Instance.GetPinMirrorGap(light, pinmr);
        }

        private void lg_ValueChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked) return;

            double pingap = (double)lg.Value;
            double pinmr = (double)pisS.Value;

            leS.Value = (decimal)ImageManager.Instance.GetLightEffi(pinmr, pingap);
        }

        private void pisS_ValueChanged(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            double light = (double)leS.Value;
            double pupil = (double)psS.Value;
            double pinmrWidth = (double)pisS.Value;
            double pinmrHeight = (double)piSE.Value;
            double pinmirrorGap = (double)lg.Value;

            Mat kernel = UniformityCalculator2.KernelManager.GetKernel((decimal)pinmrWidth, (decimal)pinmrHeight, 33, comboBox3.SelectedIndex);

            var ret = ImageManager.Instance.ProcessImage((int)pinLines.Value, pinmirrorGap, pupil, kernel, 0, false);
            Viewer.ShowImage(ret.Result);

            Mat resultMat = CreateAlphaChannel(ret.Result, (double)doubleNumericTextBox1.Value, (double)doubleNumericTextBox2.Value, (double)doubleNumericTextBox3.Value);
            
            double multi = 1.0d;

            if (resultMat.Width > resultMat.Height)
            {
                multi = 600d / resultMat.Width;
            }
            else if (resultMat.Height > resultMat.Width)
            {
                multi = 600d / resultMat.Height;
            }
            else
            {
                multi = 600d / resultMat.Width;
            }

            resultMat = resultMat.Resize(new OpenCvSharp.Size(resultMat.Width * multi, resultMat.Height * multi));

            if (string.IsNullOrWhiteSpace(txtImagePath.Text) == false && File.Exists(txtImagePath.Text))
            {
                Mat img = Cv2.ImRead(txtImagePath.Text, ImreadModes.Color);
                img = img.Resize(new OpenCvSharp.Size(resultMat.Width, resultMat.Height));

                Mat m = resultMat.ExtractChannel(0);

                Mat[] channels = img.Split();
                for (int i = 0; i < channels.Length; i++)
                {
                    Mat target = new Mat();

                    channels[i].ConvertTo(target, MatType.CV_64FC1);

                    target = target.Mul(m).ToMat();

                    target.ConvertTo(target, MatType.CV_8UC1);

                    channels[i] = target;
                }

                Cv2.Merge(channels, img);

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "png파일|*.png";
                    if(dlg.ShowDialog() == DialogResult.OK)
                    {
                        img.SaveImage(dlg.FileName);
                        MessageBox.Show("저장됨");
                    }
                }
            }
        }
        public Mat CreateAlphaChannel(Mat samplingMat, double eyeRelief, double h_fov, double v_fov)
        {
            //이거는 결과화면으로 나갈 Mat의 크기다
            double TotalWidth = 2 * (Math.Tan((h_fov / 2) * Math.PI / 180) * eyeRelief) / CalcValues.MMperPixel;
            double TotalHeight = 2 * (Math.Tan((v_fov / 2) * Math.PI / 180) * eyeRelief) / CalcValues.MMperPixel;

            //ret보다 크면서 가로/세로가 samplingMat의 홀수배인 Mat를 만든다
            int widthMultiplier = (int)(TotalWidth / samplingMat.Width) + 1;
            if (widthMultiplier % 2 == 0) widthMultiplier++;

            int heightMultiplier = (int)(TotalHeight / samplingMat.Height) + 1;
            if (heightMultiplier % 2 == 0) heightMultiplier++;

            using (Mat tmp = new Mat(samplingMat.Height * heightMultiplier, samplingMat.Width * widthMultiplier, samplingMat.Type()))
            {

                Rect r = new Rect(0, 0, samplingMat.Width, samplingMat.Height);
                for (int row = 0; row < heightMultiplier; row++)
                {
                    r.Y = samplingMat.Height * row;
                    for (int col = 0; col < widthMultiplier; col++)
                    {
                        r.X = samplingMat.Width * col;
                        Cv2.CopyTo(samplingMat, tmp[r]);
                    }
                }

                r = new Rect((int)((tmp.Width - TotalWidth) / 2), (int)((tmp.Height - TotalHeight) / 2), (int)TotalWidth, (int)TotalHeight);

                return tmp[r].Clone();
            }
        }

        private void btnImageOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = dlg.FileName;
                }
            }
        }

        internal void SetData(int mirrorType, double lightEffi, int lines, double pupil, SizeF pinSize)
        {
            comboBox3.SelectedIndex = mirrorType;
            radioButton1.Checked = true;
            leS.Value = (decimal)lightEffi;
            pinLines.Value = lines;
            psS.Value = (decimal)pupil;
            pisS.Value = (decimal)pinSize.Width;
            piSE.Value = (decimal)pinSize.Height;
        }

        private void doubleNumericTextBox2_ValueChanged(object sender, EventArgs e)
        {
            doubleNumericTextBox4.Value = (decimal)Math.Sqrt(Math.Pow((double)doubleNumericTextBox2.Value, 2) + Math.Pow((double)doubleNumericTextBox3.Value, 2));
        }
    }
}
