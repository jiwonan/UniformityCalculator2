using MySql.Data.MySqlClient;
using System;
using System.Text;

namespace UniformityCalculator2.DB
{
    public class DBDetail : DBManager
    {
        private static string INSERT_DETAIL = "INSERT INTO uniform_detail VALUES ";

        static int QRY_MAX_COUNT = 10;

        public void InsertDetail(int cnt, StringBuilder sb)
        {
            string qry = sb.ToString();

            qry = qry.Substring(0, qry.Length - 1) + ";";

            Insert(qry, cnt);
        }

        public void InsertDetail(Data.DataInput dataInput, double width, double height, bool lastJob, ref int cnt, ref StringBuilder sb) 
        {
            LogManager.SetLog("Detail Insert Start");

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
                if (cnt < QRY_MAX_COUNT && lastJob == false)
                {
                    sb.Append(",");
                }
                else
                {
                    sb.Append(";");
                }
            }

            if (cnt == QRY_MAX_COUNT || lastJob)
            {
                Insert(sb.ToString(), cnt);
                cnt = 0;
            }
        }

        private void Insert(string qry, int cnt)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(SERVER_PATH))
                using (MySqlCommand cmd = new MySqlCommand(qry, con))
                {
                    con.Open();
                    // con.BeginTransaction();
                    cmd.ExecuteNonQueryAsync();
                }
                LogManager.SetLog($"Detail 입력됨({cnt}건)");
                ProgressManager.AddProgress(cnt);
            }
            catch (Exception err)
            {
                LogManager.SetLog("Detail Insert오류");
                LogManager.SetLog(err.Message);
            }
        }

        private static string DELETE_DETAIL = "DELETE FROM uniform_detail WHERE master_idx = @masterIdx";
        private static string ALTER_IDX_DETAIL = "ALTER TABLE uniform_detail AUTO_INCREMENT = @idx;";
        private static string DETAIL_COUNT = "SELECT COUNT(master_idx) FROM uniform_detail;";

        public void DeleteDetailData(int masterIdx)
        {
            try
            {
                using (var con = GetConnection())
                {
                    int count = -1;

                    using (MySqlCommand cmd = new MySqlCommand(DELETE_DETAIL, con))
                    {
                        cmd.Parameters.AddWithValue("@masterIdx", masterIdx);
                        cmd.ExecuteNonQueryAsync();
                    }
                    using (MySqlCommand cmd = new MySqlCommand(DETAIL_COUNT, con))
                    {
                        var reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            count = reader.GetInt32(0);
                        }

                        reader.Close();
                    }
                    using (MySqlCommand cmd = new MySqlCommand(ALTER_IDX_DETAIL, con))
                    {
                        cmd.Parameters.AddWithValue("@idx", count + 1);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.SetLog(e.Message);
            }
        }

    }
}
