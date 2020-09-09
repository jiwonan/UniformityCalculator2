using System.Windows.Forms;

namespace UniformityCalculator2
{
    public static class ProgressManager
    {
        private static ProgressBar mProgressBar;
        private static Form mForm;

        public static void SetProgressBar(Form form, ProgressBar pb)
        {
            mProgressBar = pb;
            mForm = form;
        }

        public static ProgressBar GetProgressBar()
        {
            return mProgressBar;
        }

        public static void AddProgress(int amt)
        {
            if (mProgressBar.InvokeRequired)
            {
                mForm.Invoke(new MethodInvoker(() =>
                {
                    mProgressBar.Value += amt;

                    if (mProgressBar.Value == mProgressBar.Maximum)
                    {
                        LogManager.SetLog("작업이 완료되었습니다");
                    }

                }));
            }
            else
            {
                mProgressBar.Value += amt;

                if (mProgressBar.Value == mProgressBar.Maximum)
                {
                    LogManager.SetLog("작업이 완료되었습니다");
                }
            }
        }

        public static void SetProps(int checkCount)
        {
            mProgressBar.Maximum = (int)((((MasterInputValue.PupilSizeEnd - MasterInputValue.PupilSizeStart) / MasterInputValue.PupilSizeGap) + 1) *
                (((MasterInputValue.LightEfficiencyEnd - MasterInputValue.LightEfficiencyStart) / MasterInputValue.LightEfficiencyGap) + 1) *
                (((MasterInputValue.PinMirrorWidthEnd - MasterInputValue.PinMirrorWidthStart) / MasterInputValue.PinMirrorWidthGap) + 1) *
                (((MasterInputValue.PinMirrorHeightEnd - MasterInputValue.PinMirrorHeightStart) / MasterInputValue.PinMirrorHeightGap) + 1) *
                checkCount); // 작업 개수.
            mProgressBar.Maximum += 1;
            mProgressBar.Value = 0;
        }
    }
}
