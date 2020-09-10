using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniformityCalculator2
{
    public class ProcessManager
    {
        Stopwatch sw = new Stopwatch();

        CheckBox[] checkBoxes;

        DBMaster dbMaster;
        ImageManager imageManager;

        public ProcessManager(params CheckBox[] checkBoxes)
        {
            this.checkBoxes = checkBoxes;

            dbMaster = new DBMaster();

            imageManager = new ImageManager();

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

        //Queue<Mat> psfMatList = new Queue<Mat>();

        private void ReadyforWork()
        {

            ProgressManager.SetProps(GetCheckCount());

            int masterIdx = dbMaster.CreateMaster(GetUseType());


            if (masterIdx == -1)
            {
                Stop();
                return;
            }

            unsafe
            {
                for (decimal pupilSize = (decimal)MasterInputValue.PupilSizeStart; pupilSize <= (decimal)MasterInputValue.PupilSizeEnd; pupilSize += (decimal)MasterInputValue.PupilSizeGap)
                {

                    for (decimal lightEffi = (decimal)MasterInputValue.LightEfficiencyStart; lightEffi <= (decimal)MasterInputValue.LightEfficiencyEnd; lightEffi += (decimal)MasterInputValue.LightEfficiencyGap)
                    {
                        DataInput obj = new DataInput(masterIdx, (double)lightEffi, (double)pupilSize
                            , (double)MasterInputValue.PinMirrorHeightStart, (double)MasterInputValue.PinMirrorHeightEnd, (double)MasterInputValue.PinMirrorHeightGap
                            , (double)MasterInputValue.PinMirrorWidthStart, (double)MasterInputValue.PinMirrorWidthEnd, (double)MasterInputValue.PinMirrorWidthGap
                            , PinMirrorShape.Circle, (double)MasterInputValue.InnerPercent, MasterInputValue.PinmirrorLines);

                        SetThreads(obj);

                    }
                }
            }
        }

        private void SetThreads(DataInput obj)
        {
            if (checkBoxes[0].Checked)
            {
                obj.MirrorShape = PinMirrorShape.Circle;
                if (!STOP_PROCESS)
                    ThreadPool.QueueUserWorkItem(imageManager.ProcessMirror, obj); //쓰레드풀에 넣고 돌림
            }

            if (checkBoxes[1].Checked)
            {
                obj.MirrorShape = PinMirrorShape.Hexa;
                if (!STOP_PROCESS)
                    ThreadPool.QueueUserWorkItem(imageManager.ProcessMirror, obj); //쓰레드풀에 넣고 돌림
            }

            if (checkBoxes[2].Checked)
            {
                obj.MirrorShape = PinMirrorShape.Circle_Circle;
                if (!STOP_PROCESS)
                    ThreadPool.QueueUserWorkItem(imageManager.ProcessMirror, obj); //쓰레드풀에 넣고 돌림
            }

            if (checkBoxes[3].Checked)
            {
                obj.MirrorShape = PinMirrorShape.Hexa_Circle;
                if (!STOP_PROCESS)
                    ThreadPool.QueueUserWorkItem(imageManager.ProcessMirror, obj); //쓰레드풀에 넣고 돌림
            }
        }

        private int GetCheckCount()
        {
            int ret = 0;

            for (int i = 0; i < checkBoxes.Length; i++)
            {
                if (checkBoxes[i].Checked) ret++;
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
                    ret += 1;
                
                if (i != checkBoxes.Length)
                    ret <<= 1;
            }

            return ret;
        }

        public void Stop()
        {
            STOP_PROCESS = true;
            sw.Stop();
        }

    }
}
