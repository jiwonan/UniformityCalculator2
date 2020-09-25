using System;
using System.Drawing;
using System.Windows.Forms;

namespace UniformityViewer2.Controls.Lines
{
    public class LineVertical : LineView
    {
        int Position = -1;

        public LineVertical(LinePictureBox parent) : base(parent, LineType.Vertical) { }

        public override int GetLineCoordinate()
        {
            return Position;
        }

        protected override void CheckBounds(MouseEventArgs e)
        {
            if (Position >= mParent.Width) Position = mParent.Width;
            if (Position < 0) Position = 0;
        }

        protected override void DrawLines(Graphics g)
        {
            g.DrawLine(Pens.Red, new System.Drawing.Point(Position, 0), new System.Drawing.Point(Position, mParent.Height));
        }

        protected override LineType GetLineType()
        {
            return this.lineType;
        }

        protected override bool IsClicked(MouseEventArgs e)
        {
            if (Math.Abs(Position - e.X) <= MOUSE_GAP && ShowLine)
            {
                TargetLine = this.lineType;

                return true;
            }
            else return false;
        }

        protected override bool IsHover(MouseEventArgs e)
        {
            if (Math.Abs(Position - e.X) <= MOUSE_GAP && ShowLine)
            {
                mParent.Cursor = Cursors.VSplit;
                return true;
            }
            if (mParent.Cursor == Cursors.VSplit)
                mParent.Cursor = Cursors.Arrow;
            return false;
        }

        protected override int IsLineMove(MouseEventArgs e)
        {
            Rectangle rect = mParent.GetImageRect();

            if (e.X >= rect.X && e.X <= rect.Right)
            {
                Position = e.X;
                mParent.Cursor = Cursors.VSplit;
            }

            this.RaiseLineMove(lineType, Position);

            return Position;
        }

        protected override void Replace(Rectangle curImageRect)
        {
            float verticalRatio = (float)prevImageRect.Width / curImageRect.Width;
            Position = (int)(Math.Round((Position - prevImageRect.X) / verticalRatio + curImageRect.X));
        }

        int v = 0;

        protected override int Tab(Rectangle curImageRect, double gap, int length, double pos, int num)
        {
            float wRatio = (float)length / curImageRect.Width;

            Position = (int)(curImageRect.Left + pos / wRatio + gap / (TAB_GAP * 2) / wRatio * (num * TAB_GAP + v++));

            if (v == TAB_GAP + 1) v = 0;

            return Position;
        }

        protected override void UpdateCenter()
        {
            if (Position == -1) Position = mParent.Width / 2;
        }

        protected override int SetLineCenter(MouseEventArgs e)
        {
            if (IsClicked(e))
                return this.Position = mParent.Width / 2;
            return Position;
        }
    }

}
