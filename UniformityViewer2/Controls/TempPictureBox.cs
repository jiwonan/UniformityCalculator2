﻿using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Windows.Forms;

namespace UniformityViewer2.Controls
{
    public class TempPictureBox : PictureBox
    {
        public void GetXYLocation(Mat m, System.Drawing.Point clicked, out double xLoc, out double yLoc)
        {
            if (m == null)
            {
                throw new Exception("GetXYLocation 오류");
            }

            double widthRatio, heightRatio;

            GetImageRatio(m, clicked, out widthRatio, out heightRatio, out xLoc, out yLoc);
        }

        public void GetImageRatio(Mat m, int x, int y, out double widthValue, out double heightValue)
        {
            double xLoc, yLoc;
            GetImageRatio(m, new System.Drawing.Point(x, y), out widthValue, out heightValue, out xLoc, out yLoc);
        }

        public void GetImageRatio(Mat m, System.Drawing.Point lastClickedPoint, out double widthValue, out double heightValue, out double xLoc, out double yLoc)
        {
            widthValue = (double)m.Width / this.ClientRectangle.Width;
            heightValue = (double)m.Height / this.ClientRectangle.Height;

            xLoc = lastClickedPoint.X * widthValue;
            yLoc = lastClickedPoint.Y * heightValue;
        }

        public void DrawSelectedRect(Mat m, int xLoc, int yLoc, double widthValue, double heightValue, Mat selectedMat)
        {
            if (selectedMat == null)
            {
                return;
            }

            Mat tmpMat = selectedMat.Clone();

            tmpMat.Line(new Point(0, yLoc / heightValue), new Point((xLoc + 1) / widthValue, yLoc / heightValue), Scalar.Red, 2);
            tmpMat.Line(new Point(0, (yLoc + 1) / heightValue), new Point((xLoc + 1) / widthValue, (yLoc + 1) / heightValue), Scalar.Red, 2);

            tmpMat.Line(new Point(xLoc / widthValue, yLoc / heightValue), new Point(xLoc / widthValue, this.Height), Scalar.Red, 2);
            tmpMat.Line(new Point((xLoc + 1) / widthValue, yLoc / heightValue), new Point((xLoc + 1) / widthValue, this.Height), Scalar.Red, 2);

            this.Image = tmpMat.ToBitmap();
            tmpMat.Dispose();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            base.OnPaint(pe);
        }

    }
}
