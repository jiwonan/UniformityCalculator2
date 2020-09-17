using System;
using System.Windows.Forms;

namespace UniformityCalculator2.Process
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
                try
                {
                    mProgressBar.Value += amt;

                    if (mProgressBar.Value == mProgressBar.Maximum)
                    {
                        LogManager.SetLog("작업이 완료되었습니다");
                    }
                }
                catch
                {
                    mProgressBar.Value = mProgressBar.Maximum;
                }
            }
        }

        public static void SetProps(int checkCount)
        {
            mProgressBar.Maximum = (int)(((((decimal)Data.MasterInputValue.PupilSizeEnd - (decimal)Data.MasterInputValue.PupilSizeStart) / (decimal)Data.MasterInputValue.PupilSizeGap) + 1) *
                ((((decimal)Data.MasterInputValue.LightEfficiencyEnd - (decimal)Data.MasterInputValue.LightEfficiencyStart) / (decimal)Data.MasterInputValue.LightEfficiencyGap) + 1) *
                ((((decimal)Data.MasterInputValue.PinMirrorWidthEnd - (decimal)Data.MasterInputValue.PinMirrorWidthStart) / (decimal)Data.MasterInputValue.PinMirrorWidthGap) + 1) *
                ((((decimal)Data.MasterInputValue.PinMirrorHeightEnd - (decimal)Data.MasterInputValue.PinMirrorHeightStart) / (decimal)Data.MasterInputValue.PinMirrorHeightGap) + 1) *
                checkCount); // 작업 개수.

            mProgressBar.Value = 0;
        }
    }
}
