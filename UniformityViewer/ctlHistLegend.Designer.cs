namespace UniformityViewer
{
    partial class ctlHistLegend
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.최소값설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.최대값설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.최소값설정ToolStripMenuItem,
            this.최대값설정ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // 최소값설정ToolStripMenuItem
            // 
            this.최소값설정ToolStripMenuItem.Name = "최소값설정ToolStripMenuItem";
            this.최소값설정ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.최소값설정ToolStripMenuItem.Text = "최소값설정";
            this.최소값설정ToolStripMenuItem.Click += new System.EventHandler(this.최소값설정ToolStripMenuItem_Click);
            // 
            // 최대값설정ToolStripMenuItem
            // 
            this.최대값설정ToolStripMenuItem.Name = "최대값설정ToolStripMenuItem";
            this.최대값설정ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.최대값설정ToolStripMenuItem.Text = "최대값설정";
            this.최대값설정ToolStripMenuItem.Click += new System.EventHandler(this.최대값설정ToolStripMenuItem_Click);
            // 
            // ctlHistLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "ctlHistLegend";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 최소값설정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 최대값설정ToolStripMenuItem;
    }
}
