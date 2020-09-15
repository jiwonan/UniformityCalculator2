using System;
using System.Windows.Forms;

namespace UniformityCalculator2
{
    public partial class Form1 : Form
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

        ProcessManager processManager;
        DBDetail detail = new DBDetail();
        DBMaster master = new DBMaster();

        public Form1()
        {
            InitializeComponent();

            PsfDataManager.LoadPsfData();

            LogManager.setTextBox(LogBox);
            ProgressManager.SetProgressBar(this, progressBar1);

            processManager = new ProcessManager(chkCircle, chkHexa, chkCircleCircle, chkHexacircle);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "작업시작")
            {
                btnStart.Text = "작업중지";

                MasterInputValue.SetValues((double)leS.Value, (double)leE.Value, (double)leG.Value, (double)psS.Value, (double)psE.Value, (double)psG.Value
                , (double)piwS.Value, (double)piwE.Value, (double)piwG.Value, (double)pihS.Value, (double)pihE.Value, (double)pihG.Value
                , (int)pinLines.Value, (double)innerPercent.Value);

                masterIdx = processManager.Start();

                if (masterIdx != -1) timer1.Enabled = true;
                else return;
            }
            else
            {
                processManager.Stop();
                btnStart.Text = "작업시작";

                timer1.Enabled = false;
            }
        }

        int masterIdx;

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Get Count From DBDetail.

            int cnt = detail.GetDetailCount(masterIdx);

            Console.WriteLine(cnt);

            if(ProgressManager.GetProgressBar().Maximum == cnt)
            {
                master.FinishMaster(masterIdx);
            }
        }
    }
}
