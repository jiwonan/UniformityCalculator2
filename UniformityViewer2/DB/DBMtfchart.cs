using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Linq;
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

            double value = 0;

            // string qry = $"SELECT CPD, value FROM uniform_mtfchart WHERE mtf_d = {Math.Round(mtfdistance * 10, 2) / 10} AND mtf_r = {Math.Round(mtfradius * 10, 2) / 10} AND mtftype = '{mtfType}' ORDER BY CPD";
            string qry = $"SELECT CPD, value FROM uniform_mtfchart WHERE mtf_d = {mtfdistance} AND mtf_r = {mtfradius} AND mtftype = '{mtfType}' ORDER BY CPD";

            using (MySqlCommand cmd = new MySqlCommand(qry, con))
            {
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    targetSeries.Points.AddXY(reader.GetDouble(0), reader.GetDouble(1));

                    value = reader.GetDouble(0);
                    
                }
                //targetChart.ChartAreas[0].AxisX.Maximum = value;
                targetChart.ChartAreas[0].AxisX.IsMarginVisible = false;
            }
            


            con.Close();
        }

        public string DrawChart(Chart chartWidth, SizeF pinmirrorSize, double pinmirrorGap)
        {
            DrawChart(chartWidth, pinmirrorGap, Math.Truncate(pinmirrorSize.Width * 100)/100, "S");
            DrawChart(chartWidth, pinmirrorGap, Math.Truncate(pinmirrorSize.Height * 100) / 100, "T");

            /*double mtf_d = Math.Round(pinmirrorGap * 10, 2) / 10;

            double mtf_r_w = Math.Round(pinmirrorSize.Width * 10, 2) / 10;
            double mtf_r_h = Math.Truncate(pinmirrorSize.Height * 10) / 10;
*/
            return string.Join(",", pinmirrorGap, pinmirrorSize.Width);

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
