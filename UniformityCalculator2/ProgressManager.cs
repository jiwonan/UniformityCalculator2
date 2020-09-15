using System;
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
                    Add(amt);

                }));
            }
            else
            {
                Add(amt);
            }
        }

        static readonly object obj = new object();

        private static void Add(int amt)
        {
            lock(obj)
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
            mProgressBar.Maximum = (int)(((((decimal)MasterInputValue.PupilSizeEnd - (decimal)MasterInputValue.PupilSizeStart) / (decimal)MasterInputValue.PupilSizeGap) + 1) *
                ((((decimal)MasterInputValue.LightEfficiencyEnd - (decimal)MasterInputValue.LightEfficiencyStart) / (decimal)MasterInputValue.LightEfficiencyGap) + 1) *
                ((((decimal)MasterInputValue.PinMirrorWidthEnd - (decimal)MasterInputValue.PinMirrorWidthStart) / (decimal)MasterInputValue.PinMirrorWidthGap) + 1) *
                ((((decimal)MasterInputValue.PinMirrorHeightEnd - (decimal)MasterInputValue.PinMirrorHeightStart) / (decimal)MasterInputValue.PinMirrorHeightGap) + 1) *
                checkCount); // 작업 개수.

            mProgressBar.Value = 0;
        }
    }
}
