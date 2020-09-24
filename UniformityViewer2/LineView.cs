using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniformityViewer2
{
    public abstract class LineView
    {
        protected const int MOUSE_GAP = 3;
        protected const int TAB_GAP = 4;

        public bool ShowLine { get; set; } = true;

        protected Controls.LinePictureBox mParent;

        public enum LineType { Vertical, Horizon }
        public LineType lineType = LineType.Vertical;

        protected LineType TargetLine = LineType.Horizon;

        private bool isMouseButtonDown = false;

        protected Rectangle prevImageRect = Rectangle.Empty;

        protected void RaiseLineMove(LineType lineType, int position)
        {
            mParent.LineMove(lineType, position);
        }

        public LineView(Controls.LinePictureBox parent, LineType type) { mParent = parent; this.lineType = type; }

        public void OnMouseMove(MouseEventArgs e)
        {
            CheckBounds(e);

            if (isMouseButtonDown)
            {
                if (TargetLine == GetLineType())
                {
                    int pos = IsLineMove(e);
                }
            }
            else
            {
                IsHover(e);
            }
        }

        protected abstract LineType GetLineType();

        protected abstract void CheckBounds(MouseEventArgs e);

        protected abstract int IsLineMove(MouseEventArgs e);

        protected abstract bool IsHover(MouseEventArgs e);


        public void OnMouseDown(MouseEventArgs e)
        {
            if (IsClicked(e)) isMouseButtonDown = true;
        }

        protected abstract bool IsClicked(MouseEventArgs e);

        public void OnMouseUp(MouseEventArgs e)
        {
            isMouseButtonDown = false;
        }

        public void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (TargetLine == GetLineType())
            {
                int pos = SetLineCenter(e);
                RaiseLineMove(GetLineType(), pos);
            }
        }

        protected abstract int SetLineCenter(MouseEventArgs e);

        public abstract int GetLineCoordinate();

        public void Draw(Graphics g)
        {
            UpdateCenter();

            DrawLines(g);

            if (prevImageRect.Width != mParent.Image.Width || prevImageRect.Height != mParent.Image.Height)
                ReplaceStuff();

        }

        protected abstract void DrawLines(Graphics g);

        public void Resize()
        {
            UpdateCenter();

            ReplaceStuff();
        }

        protected abstract void UpdateCenter();

        private void ReplaceStuff()
        {
            if (prevImageRect.IsEmpty) prevImageRect = mParent.GetImageRect();

            Rectangle curImageRect = mParent.GetImageRect();

            Replace(curImageRect);

            prevImageRect = curImageRect;
        }

        protected abstract void Replace(Rectangle curImageRect);

        public void TabLine(LineType type, double gap, int count, int length, double pos, int num)
        {
            if (type == GetLineType())
            {
                int position = Tab(mParent.GetImageRect(), gap, length, pos, num);

                RaiseLineMove(type, position);
            }
        }

        protected abstract int Tab(Rectangle curImageRect, double gap, int length, double pos, int num);
    }

    public class LineHorizon : LineView
    {
        int Position = -1;

        public LineHorizon(Controls.LinePictureBox parent) : base(parent, LineType.Horizon) { }

        public override int GetLineCoordinate()
        {
            return Position;
        }

        protected override void CheckBounds(MouseEventArgs e)
        {
            if (Position >= mParent.Height) Position = mParent.Height;
            if (Position < 0) Position = 0;
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
            else return false;
        }

        protected override bool IsHover(MouseEventArgs e)
        {
            if (Math.Abs(Position - e.Y) <= MOUSE_GAP && ShowLine)
            {
                mParent.Cursor = Cursors.HSplit;
                return true;
            }
            if (mParent.Cursor == Cursors.HSplit)
                mParent.Cursor = Cursors.Arrow;
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

            if (h == TAB_GAP + 1) h = 0;

            return Position;
        }

        protected override void UpdateCenter()
        {
            if (Position == -1) Position = mParent.Height / 2;
        }

        protected override int SetLineCenter(MouseEventArgs e)
        {
            if (IsClicked(e))
                return this.Position = mParent.Height / 2;
            return Position;
        }
    }

    public class LineVertical : LineView
    {
        int Position = -1;

        public LineVertical(Controls.LinePictureBox parent) : base(parent, LineType.Vertical) { }

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
