using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace UniformityViewer2.Controls
{
    public partial class ctlHistLegend : UserControl
    {
        public Mat HistMat { get; set; }
        private double mMaxval, mMinval;
        public double MaxVal {
            get {
                return mMaxval;
            }
            set {
                if (IsCustomMinMaxValue) return;
                mMaxval = value;
            }
        }
        public double MinVal {
            get {
                return mMinval;
            }
            set {
                if (IsCustomMinMaxValue) return;
                mMinval = value;
            }
        }

        public ctlHistLegend()
        {
            this.DoubleBuffered = true;

            InitializeComponent();

            toolTip1.SetToolTip(this, "");
            this.MouseMove += CtlHistLegend_MouseMove;
        }

        private void CtlHistLegend_MouseMove(object sender, MouseEventArgs e)
        {
            //if (histData.Count == 0) return;

            //double x = HistMat.Height * e.Y / this.ClientSize.Height;

            //var data = histData.ElementAt((int)x);

            ////Console.WriteLine($"Value : {data.Key} / Count : {data.Value} / Rate : {(double)data.Value / totalCount * 100: 0.00}%");
            ////toolTip1.Hide(this);
            //toolTip1.Show($"Value : {data.Key : 0.00} / Count : {data.Value} / Rate : {(double)data.Value / totalCount * 100 : 0.00}%", this, int.MaxValue);

            if (e.Y < 10 || e.Y > this.ClientRectangle.Height - 10) return;

            mousePt = e.Location;
            this.Invalidate();
        }

        System.Drawing.Point mousePt = System.Drawing.Point.Empty;
        int totalCount = 0;
        Dictionary<double, int> histData = new Dictionary<double, int>();

        //OpenCvSharp.Window showWin = new OpenCvSharp.Window();
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (HistMat == null)
            {
                base.OnPaintBackground(e);
                return;
            }

            histData.Clear();

            Graphics g = e.Graphics;
            g.Clear(Color.White);


            for (int row = 0; row < HistMat.Rows; row++)
            {
                double val = row * (MaxVal - MinVal) / HistMat.Rows + MinVal;
                int mydata = (int)(HistMat.At<float>(0, row));

                if (histData.ContainsKey(val)) continue;
                histData.Add(val, mydata);
            }

            totalCount = histData.Sum(x => x.Value);

            //차트영역 그리기
            Mat newMat = new Mat(histData.Count, histData.Max(x => x.Value), MatType.CV_8U);

            newMat.SetTo(Scalar.Black);

            for (int i = 0; i < histData.Count; i++)
            {
                var kv = histData.ElementAt(i);

                if (kv.Value > 0)
                {
                    newMat.Line(new OpenCvSharp.Point(0, i), new OpenCvSharp.Point(kv.Value, i), Scalar.All(i));
                }
            }

            newMat = newMat.Normalize(255, 0, NormTypes.MinMax);

            Mat mask = newMat.Clone();
            if (newMat.Empty()) return;

            Cv2.ApplyColorMap(newMat, newMat, ColormapTypes.Jet);

            Mat tmp = newMat.EmptyClone();
            tmp.SetTo(Scalar.White);
            newMat.CopyTo(tmp, mask);
            newMat = tmp;

            newMat.Line(new OpenCvSharp.Point(0, 0), new OpenCvSharp.Point(histData.First().Value, 0), new Scalar(128, 0, 0));

            SizeF s = g.MeasureString("- 0.9999", this.Font);

            //showWin.ShowImage(newMat);

            if (IsCustomMinMaxValue)
            {
                newMat = newMat[new Rect(0, 0, newMat.Width, newMat.Height - 1)];
            }
            newMat = newMat.Resize(new OpenCvSharp.Size(this.ClientSize.Width - s.Width, this.ClientSize.Height - 20), 0, 0, InterpolationFlags.Area);

            g.DrawImage(newMat.ToBitmap(), new PointF(0, 10));

            var data = histData.First();

            g.DrawString($"- {data.Key: 0.00}", this.Font, Brushes.Black, new PointF(this.ClientSize.Width - s.Width, 10 - s.Height / 2));

            data = histData.Last();

            g.DrawString($"- {data.Key: 0.00}", this.Font, Brushes.Black, new PointF(this.ClientSize.Width - s.Width, this.ClientSize.Height - s.Height / 2 - 10));

            if (mousePt != System.Drawing.Point.Empty)
            {
                double x = HistMat.Height * (mousePt.Y - 10) / (this.ClientSize.Height - 20);
                if (x >= HistMat.Rows) x = HistMat.Rows - 1;
                data = histData.ElementAt((int)x);
                g.DrawString($"- {data.Key: 0.00}", this.Font, Brushes.Black, new PointF(this.ClientSize.Width - s.Width, mousePt.Y - s.Height / 2));
            }

            //newMat.Line()

        }

        public delegate void OnMinMaxValueChangedDelegate();
        public event OnMinMaxValueChangedDelegate OnMinMaxValueChanged;

        public bool IsCustomMinMaxValue { get; private set; } = false;

        private void 최소값설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = Microsoft.VisualBasic.Interaction.InputBox("변경할값을 입력하세요", "도비는 자유에요", "0");

            double ret = -1;

            if (double.TryParse(result, out ret))
            {
                IsCustomMinMaxValue = false;
                this.MinVal = ret;
                IsCustomMinMaxValue = true;

                OnMinMaxValueChanged.Invoke();
            }
            else
            {
                MessageBox.Show("숫자가 아니네?");
            }
        }

        private void 최대값설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = Microsoft.VisualBasic.Interaction.InputBox("변경할값을 입력하세요", "도비는 자유에요", "0");

            double ret = -1;

            if (double.TryParse(result, out ret))
            {
                IsCustomMinMaxValue = false;
                this.MaxVal = ret;
                IsCustomMinMaxValue = true;

                OnMinMaxValueChanged.Invoke();
            }
            else
            {
                MessageBox.Show("숫자가 아니네?");
            }
        }
    }
}
