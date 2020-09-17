using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace UniformityCalculator2
{
    public class ProcessManager
    {
        Stopwatch sw = new Stopwatch();

        CheckBox[] checkBoxes;

        Image.ImageManager imageManager;

        public ProcessManager(params CheckBox[] checkBoxes)
        {
            this.checkBoxes = checkBoxes;


            imageManager = new Image.ImageManager();

            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(Environment.ProcessorCount / 2, Environment.ProcessorCount / 2);
        }

        public static bool STOP_PROCESS { get; private set; }

        public void Start()
        {
            STOP_PROCESS = false;
            sw.Start();
            ReadyforWork();
        }

        private int masterIdx;

        private void ReadyforWork()
        {
            DB.DBMaster dbMaster = new DB.DBMaster();

            ProgressManager.SetProps(GetCheckCount());

            masterIdx = dbMaster.CreateMaster(GetUseType());

            if (masterIdx == -1)
            {
                Stop();
                return;
            }

            imageManager.endThread = false;

            unsafe
            {
                for (decimal pupilSize = (decimal)Data.MasterInputValue.PupilSizeStart; pupilSize <= (decimal)Data.MasterInputValue.PupilSizeEnd; pupilSize += (decimal)Data.MasterInputValue.PupilSizeGap)
                {

                    for (decimal lightEffi = (decimal)Data.MasterInputValue.LightEfficiencyStart; lightEffi <= (decimal)Data.MasterInputValue.LightEfficiencyEnd; lightEffi += (decimal)Data.MasterInputValue.LightEfficiencyGap)
                    {
                        Data.DataInput obj = new Data.DataInput(masterIdx, (double)lightEffi, (double)pupilSize
                            , Data.MasterInputValue.PinMirrorHeightStart, Data.MasterInputValue.PinMirrorHeightEnd, Data.MasterInputValue.PinMirrorHeightGap
                            , Data.MasterInputValue.PinMirrorWidthStart, Data.MasterInputValue.PinMirrorWidthEnd, Data.MasterInputValue.PinMirrorWidthGap
                            , Data.PinMirrorShape.Circle, Data.MasterInputValue.InnerPercent, Data.MasterInputValue.PinmirrorLines);

                        SetThreads(obj);
                    }
                }
            }
        }

        private void SetThreads(Data.DataInput obj)
        {
            if (checkBoxes[0].Checked)
            {
                obj.MirrorShape = Data.PinMirrorShape.Circle;
                if (!STOP_PROCESS)
                {
                    ThreadPool.QueueUserWorkItem(imageManager.ProcessMirror, obj); //쓰레드풀에 넣고 돌림
                }
            }

            if (checkBoxes[1].Checked)
            {
                obj.MirrorShape = Data.PinMirrorShape.Hexa;
                if (!STOP_PROCESS)
                {
                    ThreadPool.QueueUserWorkItem(imageManager.ProcessMirror, obj); //쓰레드풀에 넣고 돌림
                }
            }

            if (checkBoxes[2].Checked)
            {
                obj.MirrorShape = Data.PinMirrorShape.Circle_Circle;
                if (!STOP_PROCESS)
                {
                    ThreadPool.QueueUserWorkItem(imageManager.ProcessMirror, obj); //쓰레드풀에 넣고 돌림
                }
            }

            if (checkBoxes[3].Checked)
            {
                obj.MirrorShape = Data.PinMirrorShape.Hexa_Circle;
                if (!STOP_PROCESS)
                {
                    ThreadPool.QueueUserWorkItem(imageManager.ProcessMirror, obj); //쓰레드풀에 넣고 돌림
                }
            }
        }

        private int GetCheckCount()
        {
            int ret = 0;

            for (int i = 0; i < checkBoxes.Length; i++)
            {
                if (checkBoxes[i].Checked)
                {
                    ret++;
                }
            }

            return ret;
        }

        private int GetUseType()
        {
            int ret = 0;

            int i;
            for (i = 0; i < checkBoxes.Length; i++)
            {
                if (checkBoxes[i].Checked)
                {
                    ret += 1;
                }

                if (i != checkBoxes.Length)
                {
                    ret <<= 1;
                }
            }

            return ret;
        }

        public void Stop()
        {
            STOP_PROCESS = true;
            sw.Stop();

            imageManager.endThread = true;

            if (!(ProgressManager.GetProgressBar().Maximum == ProgressManager.GetProgressBar().Value))
            {
                DB.DBMaster dbMaster = new DB.DBMaster();

                dbMaster.DeleteMaster(masterIdx);
                LogManager.SetLog("작업이 중단되었습니다.");
            }

            ProgressManager.GetProgressBar().Value = 0;
        }

    }
}
