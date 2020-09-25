using System;
using System.Drawing;
using System.Windows.Forms;

namespace UniformityViewer2.Controls.Lines
{
    public class LineHorizon : LineView
    {
        int Position = -1;

        public LineHorizon(LinePictureBox parent) : base(parent, LineType.Horizon) { }

        public override int GetLineCoordinate()
        {
            return Position;
        }

        protected override void CheckBounds(MouseEventArgs e)
        {
            if (Position >= mParent.Height)
            {
                Position = mParent.Height;
            }

            if (Position < 0)
            {
                Position = 0;
            }
        }

        protected override void DrawLines(Graphics g)
        {
            g.DrawLine(Pens.Red, new System.Drawing.Point(0, Position), new System.Drawing.Point(mParent.Width, Position));
        }

        protected override LineType GetLineType()
        {
            return this.lineType;
        }

        protected override bool IsClicked(MouseEventArgs e)
        {
            if (Math.Abs(Position - e.Y) <= MOUSE_GAP && ShowLine)
            {
                TargetLine = this.lineType;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override bool IsHover(MouseEventArgs e)
        {
            if (Math.Abs(Position - e.Y) <= MOUSE_GAP && ShowLine)
            {
                mParent.Cursor = Cursors.HSplit;
                return true;
            }
            if (mParent.Cursor == Cursors.HSplit)
            {
                mParent.Cursor = Cursors.Arrow;
            }

            return false;
        }

        protected override int IsLineMove(MouseEventArgs e)
        {
            Rectangle rect = mParent.GetImageRect();

            if (e.Y >= rect.Y && e.Y <= rect.Bottom)
            {
                Position = e.Y;
                mParent.Cursor = Cursors.HSplit;
            }

            this.RaiseLineMove(lineType, Position);

            return Position;
        }

        protected override void Replace(Rectangle curImageRect)
        {
            float horizonRatio = (float)prevImageRect.Height / curImageRect.Height;
            Position = (int)(Math.Round((Position - prevImageRect.Y) / horizonRatio + curImageRect.Y));
        }

        int h = 0;

        protected override int Tab(Rectangle curImageRect, double gap, int length, double pos, int num)
        {
            float hRatio = (float)length / curImageRect.Height;

            Position = (int)(curImageRect.Top + pos / hRatio + gap / TAB_GAP / hRatio * (num * TAB_GAP + h++));

            if (h == TAB_GAP + 1)
            {
                h = 0;
            }

            return Position;
        }

        protected override void UpdateCenter()
        {
            if (Position == -1)
            {
                Position = mParent.Height / 2;
            }
        }

        protected override int SetLineCenter(MouseEventArgs e)
        {
            if (IsClicked(e))
            {
                return this.Position = mParent.Height / 2;
            }

            return Position;
        }
    }

}
