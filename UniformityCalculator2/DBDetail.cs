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
            string qry = sb.ToString();

            qry = qry.Substring(0, qry.Length - 1) + ";";

            using (MySqlConnection con = new MySqlConnection(SERVER_PATH))
            using (MySqlCommand cmd = new MySqlCommand(qry, con))
            {
                con.Open();
                cmd.ExecuteNonQueryAsync();
            }
            LogManager.SetLog($"Detail 입력됨({cnt}건)");
            ProgressManager.AddProgress(cnt);
        }

        public void InsertDetail(DataInput dataInput, double width, double height, bool lastJob, ref int cnt, ref StringBuilder sb)
        {
            InsertDetail(dataInput, width, height, lastJob, ref cnt, ref sb, QRY_MAX_COUNT);
        }

        public void InsertDetail(DataInput dataInput, double width, double height, bool lastJob, ref int cnt, ref StringBuilder sb, int max_cnt) // 
        {
            try
            {
                if (cnt == 0)
                {
                    sb.Clear();
                    sb.AppendLine(INSERT_DETAIL);
                }

                //SetLog($"Detail 준비({cnt})");
                if (cnt < max_cnt)
                {
                    //cmd.Parameters.Clear();
                    sb.AppendLine($" (NULL, {dataInput.MasterIdx}, " +
                        $"{dataInput.LightEffi}, {dataInput.PupilSize}, {height}, " +
                        $"{dataInput.MaxAvg}, {dataInput.MinAvg}, {dataInput.MeanDev}, " +
                        $"{dataInput.LumperDegree}, {dataInput.LumperDegree_Avg}, " +
                        $"{(int)dataInput.MirrorShape}, {width})");

                    cnt++;
                    if (cnt < max_cnt && lastJob == false) sb.Append(",");
                    else sb.Append(";");
                }

                if (cnt == max_cnt || lastJob)
                {

                    // InsertDetail(cnt, sb);

                    using (MySqlConnection con = new MySqlConnection(SERVER_PATH))
                    using (MySqlCommand cmd = new MySqlCommand(sb.ToString(), con))
                    {
                        con.Open();
                        cmd.ExecuteNonQueryAsync();
                    }
                    ProgressManager.AddProgress(max_cnt);
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

        private static string GET_DETAIL_COUNT = "SELECT COUNT(master_idx) FROM uniform_detail WHERE master_idx = @selectedMaster";

        public int GetDetailCount(int selectedMaster)
        {
            int count;

            var con = GetConnection();
            using (MySqlCommand cmd = new MySqlCommand(GET_DETAIL_COUNT, con))
            {
                cmd.Parameters.AddWithValue("@selectedMaster", selectedMaster);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                    count = reader.GetInt32(0);
                else count = -1;
            }

            con.Close();

            return count;
        }


    }
}
