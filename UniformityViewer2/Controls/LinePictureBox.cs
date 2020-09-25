using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniformityViewer2.Controls
{
    public class LinePictureBox : PictureBox
    {
        private Mat mImage;
        public new Mat Image {
            get { return mImage; }
            set {
                if (value == null) return;
                mImage = value.Clone();

                using (Mat m = new Mat())
                {
                    mImage.ConvertTo(m, MatType.CV_8UC4);

                    base.Image = BitmapConverter.ToBitmap(m);
                }
            }
        }

        List<Lines.LineView> controls;

        public delegate void OnLineMoveDelegate(object sender, Lines.LineView.LineType lineType, int position);
        public event OnLineMoveDelegate OnLineMove;

        public LinePictureBox()
        {
            this.MouseMove += LinePictureBox_MouseMove;
            this.MouseDown += LinePictureBox_MouseDown;
            this.MouseUp += LinePictureBox_MouseUp;
            this.MouseDoubleClick += LinePictureBox_MouseDoubleClick;

            controls = new List<Lines.LineView>();
            controls.Add(new Lines.LineVertical(this));
            controls.Add(new Lines.LineHorizon(this));
        }

        private void LinePictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (var control in controls)
            {
                control.OnMouseDoubleClick(e);
            }
        }

        private void LinePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.Image == null) return;

            foreach (var control in controls)
            {
                control.OnMouseUp(e);
            }
        }

        private void LinePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Image == null) return;

            foreach (var control in controls)
            {
                control.OnMouseDown(e);
            }
        }

        private void LinePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Image == null) return;

            foreach (var control in controls)
            {
                control.OnMouseMove(e);
            }
        }

        public Rectangle GetImageRect()
        {
            Rectangle rect = new Rectangle(0, 0, 0, 0);

            double ratio = Math.Min(this.ClientRectangle.Width / (double)this.Image.Width, this.ClientRectangle.Height / (double)this.Image.Height);

            rect.Width = (int)(this.Image.Width * ratio);
            rect.Height = (int)(this.Image.Height * ratio);

            rect.X = (this.ClientRectangle.Width - rect.Width) / 2;
            rect.Y = (this.ClientRectangle.Height - rect.Height) / 2;

            return rect;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (this.Image == null) return;

            Graphics g = pe.Graphics;

            foreach (var control in controls)
            {
                control.Draw(g);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.Image == null) return;

            foreach (var control in controls)
            {
                control.Resize();
            }
        }

        public void LineMove(Lines.LineView.LineType lineType, int position)
        {
            OnLineMove?.Invoke(this, lineType, position);

            this.Invalidate();
        }

        public void TabLine(Lines.LineView.LineType type, double gap, int count, int length, double pos, int num)
        {
            foreach (var control in controls)
            {
                control.TabLine(type, gap, count, length, pos, num);
            }
        }

        public string GetLineCoordinates()
        {
            Rectangle rect = GetImageRect();

            int lineV = ((Lines.LineVertical)controls[0]).GetLineCoordinate() - rect.X;
            int lineH = ((Lines.LineHorizon)controls[1]).GetLineCoordinate() - rect.Y;

            return $"V : {Math.Round(lineV * UniformityCalculator2.Data.CalcValues.MMperPixel, 2)}mm, H : {Math.Round(lineH * UniformityCalculator2.Data.CalcValues.MMperPixel, 2)}mm |";
        }
    }
}
