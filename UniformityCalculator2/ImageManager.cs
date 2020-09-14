using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;

namespace UniformityCalculator2
{
    public class ImageManager
    {

        private static ImageManager mInstance = null;

        public static ImageManager GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new ImageManager();
            }

            return mInstance;
        }

        public void ProcessMirror(object obj)
        {

            DBMaster dbMaster = new DBMaster();
            DBDetail dbDetail = new DBDetail();

            DataInput dataInput = (DataInput)obj;

            double lightEffi = dataInput.LightEffi;
            double pupilSize = dataInput.PupilSize;
            double innerArea = dataInput.InnerArea;
            PinMirrorShape kernelType = dataInput.MirrorShape;
            int lineCount = dataInput.PinLines;
            int masterIdx = dataInput.MasterIdx;

            using (Mat psfMat = PsfDataManager.LoadPsfData((double)pupilSize))
            {
                int cnt = 0;
                StringBuilder sb = new StringBuilder();

                for (decimal pinmirrorHeight = (decimal)dataInput.PinHeightStart; pinmirrorHeight <= (decimal)dataInput.PinHeightEnd; pinmirrorHeight += (decimal)dataInput.PinHeightGap)
                {
                    for (decimal pinmirrorWidth = pinmirrorHeight + (decimal)dataInput.PinWidthStart; pinmirrorWidth <= pinmirrorHeight + (decimal)dataInput.PinWidthEnd; pinmirrorWidth += (decimal)dataInput.PinWidthGap)
                    {
                        double pinmirrorGap = CalcValues.GetPinMirrorGap((double)lightEffi, new SizeF((float)pinmirrorWidth, (float)pinmirrorHeight));

                        using (Mat kernel = KernelManager.GetKernel((decimal)pinmirrorWidth, (decimal)pinmirrorHeight, (decimal)innerArea, kernelType))
                        {
                            ProcessImage(kernel, psfMat, lineCount, pinmirrorGap, ref dataInput);
                        }

                        dbDetail.InsertDetail(dataInput, (double)pinmirrorWidth, (double)pinmirrorHeight, false, ref cnt, ref sb);

                        if (ProgressManager.GetProgressBar().Value == ProgressManager.GetProgressBar().Maximum) // 실행 X.
                        {
                            dbDetail.InsertDetail(dataInput, (double)pinmirrorWidth, (double)pinmirrorHeight, true, ref cnt, ref sb);
                            dbMaster.FinishMaster(masterIdx);
                        }

                        if(ProgressManager.GetProgressBar().Maximum - ProgressManager.GetProgressBar().Value < 10)
                        {

                        }
                    }
                    GC.Collect();
                }

                dbDetail.InsertDetail(cnt, sb);
                sb.Clear();

            }
        }

        /// <summary>
        /// resultMat, mirrorImage, psfMat, maxavg, minavg, dev
        /// </summary>
        /// <param name="pinMirrorSize"></param>
        /// <param name="lineCount"></param>
        /// <param name="pinGap"></param>
        /// <param name="pupilSize"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public void ProcessImage(Mat kernel, Mat psf, int lineCount, double pinmirrorGap, ref DataInput dataInput)
        {
            // SetLog("Start Processing");

            List<Point> tmpPts = new List<Point>();
            using (Mat mirrorImage = CreateMirrorImage(lineCount, pinmirrorGap, kernel, ref tmpPts))
            {
                if (tmpPts.Count != 6)
                {
                    //이상한데
                    LogManager.SetLog("pts Error");
                    throw new Exception("pts Error");
                }

                //개선필요함
                //pts의 순서를 0, 1, 2, 3, 4, 5 에서
                // 0, 1, 3, 5, 4, 2 순으로
                Point[] pts = new Point[6];
                pts[0] = tmpPts[0];
                pts[1] = tmpPts[1];
                pts[2] = tmpPts[3];
                pts[3] = tmpPts[5];
                pts[4] = tmpPts[4];
                pts[5] = tmpPts[2];

                Mat resultMat = 0.01 * mirrorImage;
                Cv2.Filter2D(resultMat, resultMat, -1, psf);

                resultMat = resultMat.Normalize(0, 10, NormTypes.MinMax);

                //Mat tmp = new Mat();
                //Cv2.Merge(new Mat[3] { resultMat, resultMat, resultMat }, tmp);


                //(new Window()).ShowImage(tmp);
                double pixelPitch = CalcValues.MMperPixel; //mm
                double EyeRelief = 18; //mm

                using (Mat mask = new Mat(resultMat.Rows, resultMat.Cols, MatType.CV_8U))
                using (Mat mean = new Mat())
                using (Mat stddev = new Mat())
                using (Mat calcResult = Cv2.Abs(resultMat[new Rect(0, 0, resultMat.Width - 1, resultMat.Height)] - resultMat[new Rect(1, 0, resultMat.Width - 1, resultMat.Height)]).ToMat() / Math.Atan(pixelPitch / EyeRelief))
                {

                    mask.SetTo(Scalar.Black);
                    mask.FillPoly(new Point[1][] { pts }, Scalar.White, LineTypes.AntiAlias); //마스크 영역 생성

                    double min, max;
                    Point minPt, maxPt;
                    resultMat.MinMaxLoc(out min, out max, out minPt, out maxPt, mask);

                    Cv2.MeanStdDev(resultMat, mean, stddev, mask);
                    double avg = mean.At<double>(0);
                    double dev = stddev.At<double>(0);

                    double maxavg, minavg;

                    maxavg = (max - avg) / (max + avg);
                    minavg = (avg - min) / (min + avg);

                    calcResult.MinMaxLoc(out min, out max, out minPt, out maxPt, mask[new Rect(0, 0, mask.Width - 1, mask.Height)]);

                    Cv2.MeanStdDev(calcResult, mean, stddev, mask[new Rect(0, 0, mask.Width - 1, mask.Height)]);
                    avg = mean.At<double>(0);

                    dataInput.MinAvg = minavg;
                    dataInput.MaxAvg = maxavg;
                    dataInput.MeanDev = dev;
                    dataInput.LumperDegree = max;
                    dataInput.LumperDegree_Avg = avg;
                }


                //resultMat.PutText($"ShapeType : {dataInput.MirrorShape}", new Point(0, 40), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                //resultMat.PutText($"Efficiency : {dataInput.LightEffi}% pupilSize : {dataInput.PupilSize}mm", new Point(0, 60), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                //resultMat.PutText($"pinmirrorSize : {0.65}mm", new Point(0, 80), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                //resultMat.PutText($"pinmirror_Gap : {pinmirrorGap}mm", new Point(0, 100), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                //resultMat.PutText($"Max-Avg : {dataInput.MaxAvg}", new Point(0, 120), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                //resultMat.PutText($"Min-Avg : {dataInput.MinAvg}", new Point(0, 140), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                //resultMat.PutText($"MeanDev : {dataInput.MeanDev}", new Point(0, 160), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                //resultMat.PutText($"LumperDegree(Max) : {dataInput.LumperDegree}", new Point(0, 180), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                //resultMat.PutText($"LumperDegree(Avg) : {dataInput.LumperDegree_Avg}", new Point(0, 200), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);
                ////resultMat.PutText($"Real Efficiency : {light * (pinmirrorSize.Width / pinmirrorSize.Height)}%", new Point(0, 220), HersheyFonts.HersheyDuplex, 0.5, Scalar.Red);

                //resultMat.SaveImage("new_resultMat.png");

                resultMat.Dispose();

                //SetLog("Process completed");
            }
        }



        public ProcessImageResult ProcessImage(int lineCount, double pinGap, double pupilSize, Mat kernel, int jobId = 0, bool drawCenterArea = true, bool invertMirror = false)
        {
            // LogManager.SetLog("Start Processing");

            List<Point> tmpPts = new List<Point>();
            Mat mirrorImage = CreateMirrorImage(lineCount, pinGap, kernel, ref tmpPts);


            if (invertMirror)
            {
                double min, max;
                mirrorImage.MinMaxLoc(out min, out max);
                var gap = max - min;

                mirrorImage -= gap;
                mirrorImage = mirrorImage.Abs().ToMat();
            }
            //mirrorMat.MinMaxLoc(out min, out max);


            //SaveImage(mirrorImage, "Mirror", jobId);

            if (tmpPts.Count != 6)
            {
                //이상한데
                LogManager.SetLog("pts Error");
                throw new Exception("pts Error");
            }

            //개선필요함
            //pts의 순서를 0, 1, 2, 3, 4, 5 에서
            // 0, 1, 3, 5, 4, 2 순으로
            Point[] pts = new Point[6];
            pts[0] = tmpPts[0];
            pts[1] = tmpPts[1];
            pts[2] = tmpPts[3];
            pts[3] = tmpPts[5];
            pts[4] = tmpPts[4];
            pts[5] = tmpPts[2];

            Mat downAlphaMirrorImage = null;
            using (Mat psfMat = PsfDataManager.LoadPsfData(pupilSize))
            {

                downAlphaMirrorImage = 0.01 * mirrorImage;

                Cv2.Filter2D(downAlphaMirrorImage, downAlphaMirrorImage, -1, psfMat);
            }
            downAlphaMirrorImage = downAlphaMirrorImage.Normalize(1, 0, NormTypes.MinMax);

            //Viewer.ShowImage(downAlphaMirrorImage);

            Mat resultMat;

            if (!drawCenterArea)
                resultMat = downAlphaMirrorImage[new Rect(pts[0].X, pts[0].Y, pts[1].X - pts[0].X, pts[4].Y - pts[0].Y)].Clone();
            else
                resultMat = downAlphaMirrorImage.Clone();

            //Mat resultMat = downAlphaMirrorImage.Clone();

            //Mat samplingArea = downAlphaMirrorImage[new Rect(pts[0].X, pts[0].Y, pts[1].X - pts[0].X, pts[4].Y - pts[0].Y)].Clone();

            Cv2.Merge(new Mat[3] { resultMat, resultMat, resultMat }, resultMat);

            if (drawCenterArea)
            {
                resultMat.Polylines(new Point[1][] { pts }, true, Scalar.Red, 1, LineTypes.AntiAlias); //결과값
                LogManager.SetLog("Get result image complete");
            }

            //관심영역으로 자르기
            //int minX, maxX, minY, maxY;
            //minX = pts[5].X;
            //maxX = pts[2].X;
            //minY = pts[0].Y;
            //maxY = pts[3].Y;

            //downAlphaMirrorImage = downAlphaMirrorImage[new Rect(minX, minY, maxX - minX, maxY - minY)];

            //for (int i = 0; i < 6; i++)
            //{
            //    pts[i].X -= minX;
            //    pts[i].Y -= minY;
            //}

            using (Mat mask = new Mat(downAlphaMirrorImage.Rows, downAlphaMirrorImage.Cols, MatType.CV_8U))
            {
                mask.SetTo(Scalar.Black);
                mask.FillPoly(new Point[1][] { pts }, Scalar.White, LineTypes.AntiAlias); //마스크 영역 생성

                double min, max;
                Point minPt, maxPt;
                downAlphaMirrorImage.MinMaxLoc(out min, out max, out minPt, out maxPt, mask);

                Mat mean = new Mat(), stddev = new Mat();
                Cv2.MeanStdDev(downAlphaMirrorImage, mean, stddev, mask);
                double avg = mean.At<double>(0);
                double dev = stddev.At<double>(0);

                double maxavg, minavg;

                maxavg = (max - avg) / (max + avg);
                minavg = (avg - min) / (min + avg);

                //순기님 추가(각도당 휘도 변화량)

                double pixelPitch = CalcValues.MMperPixel; //mm
                double EyeRelief = 18; //mm

                Mat matCurrent; //Current
                Mat matRight; //Current + X축1px

                matCurrent = new Mat(downAlphaMirrorImage, new Rect(0, 0, downAlphaMirrorImage.Width - 1, downAlphaMirrorImage.Height));

                matRight = new Mat(downAlphaMirrorImage, new Rect(1, 0, downAlphaMirrorImage.Width - 1, downAlphaMirrorImage.Height));

                Mat calcResult = Cv2.Abs(matCurrent - matRight).ToMat() / Math.Atan(pixelPitch / EyeRelief);

                calcResult.MinMaxLoc(out min, out max, out minPt, out maxPt, mask[new Rect(0, 0, mask.Width - 1, mask.Height)]);

                Cv2.MeanStdDev(calcResult, mean, stddev, mask[new Rect(0, 0, mask.Width - 1, mask.Height)]);

                avg = mean.At<double>(0);

                downAlphaMirrorImage.Dispose();
                mean.Dispose();
                stddev.Dispose();
                matCurrent.Dispose();
                matRight.Dispose();
                calcResult.Dispose();

                LogManager.SetLog("Process completed");
                return new ProcessImageResult(resultMat, mirrorImage, maxavg, minavg, dev, max, avg);
            }
        }

        public struct ProcessImageResult : IDisposable
        {
            public Mat Result;
            public Mat MirrorImage;

            public double MaxAvg;
            public double MinAvg;
            public double MeanDev;
            public double LumDegreeMax;
            public double LumDegreeAvg;


            public ProcessImageResult(Mat resultMat, Mat mirrorImage, double maxavg, double minavg, double dev, double max, double avg) : this()
            {
                Result = resultMat;
                MirrorImage = mirrorImage;
                MaxAvg = maxavg;
                MinAvg = minavg;
                MeanDev = dev;
                LumDegreeMax = max;
                LumDegreeAvg = avg;
            }

            public void Dispose()
            {
                Result?.Dispose();
                MirrorImage?.Dispose();
            }
        }

        private Mat CreateMirrorImage(int lineCount, double pinGap, Mat kernel, ref List<Point> pts)
        {
            double pinGapHeight = pinGap * Math.Sqrt(3) / 2; //mm값
            double matWidth = CalcValues.MMtoPixel((lineCount + 1) * pinGap); //픽셀값
            double matHeigt = CalcValues.MMtoPixel((lineCount + 1) * pinGapHeight); //픽셀값

            int pinGapWidthInPixel = CalcValues.MMtoPixel(pinGap);
            int pinGapHeightInPixel = CalcValues.MMtoPixel(pinGapHeight);


            using (Mat m = new Mat((int)matWidth, (int)matHeigt, MatType.CV_64F))
            {
                m.SetTo(Scalar.Black);

                Point centerPoint = new Point(m.Width / 2, m.Height / 2);

                Point drawingPoint = new Point();

                int centerLine = (int)lineCount / 2 + 1;

                for (int row = 1; row <= lineCount; row++)
                {
                    //row는 행번호
                    int mirrorCount = lineCount + Math.Min(row, centerLine) - Math.Max(row, centerLine);

                    drawingPoint.Y = centerPoint.Y - (centerLine - row) * pinGapHeightInPixel;
                    drawingPoint.X = centerPoint.X;

                    //맨왼쪽점 찾기
                    //현재열의 총 갯수는 row개다
                    int totalRow = mirrorCount;
                    if (mirrorCount % 2 == 1) //홀수면 중앙미러가 있다
                    {
                        totalRow--;
                        drawingPoint.X -= pinGapWidthInPixel / 2;
                    }

                    drawingPoint.X -= pinGapWidthInPixel / 2;
                    drawingPoint.X -= ((totalRow / 2) - 1) * pinGapWidthInPixel;

                    for (int col = 1; col <= mirrorCount; col++)
                    {
                        if (row == centerLine - 1 && (col == mirrorCount / 2 || col == mirrorCount / 2 + 1))
                        {
                            //위에 두개
                            pts.Add(drawingPoint);
                        }
                        else if (row == centerLine && (col == mirrorCount / 2 || col == mirrorCount / 2 + 2))
                        {
                            //중간 두개
                            pts.Add(drawingPoint);
                        }
                        else if (row == centerLine + 1 && (col == mirrorCount / 2 || col == mirrorCount / 2 + 1))
                        {
                            //아래 두개
                            pts.Add(drawingPoint);
                        }


                        m.Line(drawingPoint, drawingPoint, Scalar.White);
                        //Cv2.Circle(m, drawingPoint, pinRadiusInPixel, Scalar.White, -1, LineTypes.AntiAlias, 0);

                        drawingPoint.X += pinGapWidthInPixel;
                    }
                }

                Cv2.Filter2D(m, m, -1, kernel);

                return m.Clone();
            }
        }        

    }
}
