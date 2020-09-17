using System;
using System.Windows.Forms;

namespace UniformityCalculator2
{
    public partial class frmViewer : Form
    {
        /*#region Values

        public double LightEfficiencyStart => (double)leS.Value;
        public double LightEfficiencyEnd => (double)leE.Value;
        public double LightEfficiencyGap => (double)leG.Value;

        public double PupilSizeStart => (double)psS.Value;
        public double PupilSizeEnd => (double)psE.Value;
        public double PupilSizeGap => (double)psG.Value;

        public double PinMirrorWidthStart => (double)piwS.Value;
        public double PinMirrorWidthEnd => (double)piwE.Value;
        public double PinMirrorWidthGap => (double)piwG.Value;

        public double PinMirrorHeightStart => (double)pihS.Value;
        public double PinMirrorHeightEnd => (double)pihE.Value;
        public double PinMirrorHeightGap => (double)pihG.Value;

        public int pinmirrorLines => (int)pinLines.Value;

        #endregion*/

        Process.ProcessManager processManager;

        public frmViewer()
        {
            InitializeComponent();

            Psf.PsfDataManager.LoadPsfData();

            Process.LogManager.setTextBox(LogBox);
            Process.ProgressManager.SetProgressBar(this, progressBar1);

            processManager = new Process.ProcessManager(chkCircle, chkHexa, chkCircleCircle, chkHexacircle);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "작업시작")
            {
                btnStart.Text = "작업중지";

                Data.MasterInputValue.SetValues((double)leS.Value, (double)leE.Value, (double)leG.Value, (double)psS.Value, (double)psE.Value, (double)psG.Value
                , (double)piwS.Value, (double)piwE.Value, (double)piwG.Value, (double)pihS.Value, (double)pihE.Value, (double)pihG.Value
                , (int)pinLines.Value, (double)innerPercent.Value);

                processManager.Start();
            }
            else
            {
                processManager.Stop();
                btnStart.Text = "작업시작";
            }
        }

    }
}
