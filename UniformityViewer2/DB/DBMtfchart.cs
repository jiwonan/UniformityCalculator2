using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace UniformityViewer2.DB
{
    public class DBMtfchart : UniformityCalculator2.DB.DBManager
    {
        public void DrawChart(Chart targetChart, double mtfdistance, double mtfradius, string mtfType)
        {
            var con = GetConnection();

            var targetSeries = targetChart.Series["MTF_" + mtfType];
            targetSeries.Points.Clear();

            string qry = $"SELECT CPD, value FROM uniform_mtfchart WHERE mtf_d = {Math.Truncate(mtfdistance * 10) / 10} AND mtf_r = {Math.Truncate(mtfradius * 10) / 10} AND mtftype = '{mtfType}' ORDER BY CPD";

            using (MySqlCommand cmd = new MySqlCommand(qry, con))
            {
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    targetSeries.Points.AddXY(reader.GetDouble(0), reader.GetDouble(1));
                }
            }
            con.Close();
        }

        public string DrawChart(Chart chartWidth, SizeF pinmirrorSize, double pinmirrorGap)
        {
            DrawChart(chartWidth, pinmirrorGap, pinmirrorSize.Width, "S");
            DrawChart(chartWidth, pinmirrorGap, pinmirrorSize.Height, "T");

            double mtf_d = Math.Truncate(pinmirrorGap * 10) / 10;

            double mtf_r_w = Math.Truncate(pinmirrorSize.Width * 10) / 10;
            double mtf_r_h = Math.Truncate(pinmirrorSize.Height * 10) / 10;

            return string.Join(",", mtf_d, mtf_r_w, mtf_r_h);

            //drawChart(chartHeight, pinmirrorGap, pinmirrorSize.Height, "S");
            //drawChart(chartHeight, pinmirrorGap, pinmirrorSize.Height, "T");
        }


        public string GetMtfData(string mtf)
        {
            try
            {
                var con = GetConnection();

                double min = 0, max = 0, gap = 0;

                string MTFCHART_MINMAX = $"SELECT MAX({mtf}), MIN({mtf}) FROM uniform_mtfchart;";
                using (MySqlCommand cmd = new MySqlCommand(MTFCHART_MINMAX, con))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        max = reader.GetDouble(0);
                        min = reader.GetDouble(1);
                    }

                    reader.Close();
                }

                string MTFCHART_GAP = $"SELECT MIN({mtf}) FROM uniform_mtfchart WHERE {mtf} <> {min};";
                using (MySqlCommand cmd = new MySqlCommand(MTFCHART_GAP, con))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        gap = (reader.GetDouble(0) - min);
                    }
                    reader.Close();
                }

                con.Close();
                return $"{min} ~ {max}, {gap}간격";
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

    }
}
