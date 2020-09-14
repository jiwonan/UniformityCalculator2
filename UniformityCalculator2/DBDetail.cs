using MySql.Data.MySqlClient;
using System;
using System.Text;

namespace UniformityCalculator2
{
    public class DBDetail : DBManager
    {
        private static string INSERT_DETAIL = "INSERT INTO uniform_detail VALUES ";
        
        static int QRY_MAX_COUNT = 10;

        public void InsertDetail(int cnt, StringBuilder sb)
        {
            using (MySqlConnection con = new MySqlConnection(SERVER_PATH))
            using (MySqlCommand cmd = new MySqlCommand(sb.ToString(), con))
            {
                con.Open();
                cmd.ExecuteNonQueryAsync();
            }
            ProgressManager.AddProgress(cnt);
            LogManager.SetLog($"Detail 입력됨({cnt}건)");
        }

        public void InsertDetail(DataInput dataInput, double width, double height, bool lastJob, ref int cnt, ref StringBuilder sb)
        {
            InsertDetail(dataInput, width, height, lastJob, ref cnt, ref sb, QRY_MAX_COUNT);
        }

        public void InsertDetail(DataInput dataInput, double width, double height, bool lastJob, ref int cnt, ref StringBuilder sb, int max_cnt)
        {
            try
            {
                if (cnt == 0)
                {
                    sb.Clear();
                    sb.AppendLine(INSERT_DETAIL);
                }

                //SetLog($"Detail 준비({cnt})");
                if (cnt < QRY_MAX_COUNT)
                {
                    //cmd.Parameters.Clear();
                    sb.AppendLine($" (NULL, {dataInput.MasterIdx}, " +
                        $"{dataInput.LightEffi}, {dataInput.PupilSize}, {height}, " +
                        $"{dataInput.MaxAvg}, {dataInput.MinAvg}, {dataInput.MeanDev}, " +
                        $"{dataInput.LumperDegree}, {dataInput.LumperDegree_Avg}, " +
                        $"{(int)dataInput.MirrorShape}, {width})");

                    cnt++;
                    if (cnt < QRY_MAX_COUNT && lastJob == false) sb.Append(",");
                    else sb.Append(";");
                }

                if (cnt == QRY_MAX_COUNT || lastJob)
                {
                    using (MySqlConnection con = new MySqlConnection(SERVER_PATH))
                    using (MySqlCommand cmd = new MySqlCommand(sb.ToString(), con))
                    {
                        con.Open();
                        cmd.ExecuteNonQueryAsync();
                    }
                    ProgressManager.AddProgress(QRY_MAX_COUNT);
                    LogManager.SetLog($"Detail 입력됨({cnt}건)");
                    cnt = 0;
                }
            }
            catch (Exception err)
            {
                LogManager.SetLog("Detail Insert오류");
                LogManager.SetLog(err.Message);
            }
        }


    }
}
