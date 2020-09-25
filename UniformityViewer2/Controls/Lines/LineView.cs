using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniformityViewer2.Controls.Lines
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

}
