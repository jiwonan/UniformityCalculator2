using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformityViewer2.Image
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
            using (Mat psfMat = PsfManager.LoadPsfData(pupilSize))
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

                double pixelPitch = Data.CalcValues.MMperPixel; //mm
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
            double matWidth = Data.CalcValues.MMtoPixel((lineCount + 1) * pinGap); //픽셀값
            double matHeigt = Data.CalcValues.MMtoPixel((lineCount + 1) * pinGapHeight); //픽셀값

            int pinGapWidthInPixel = Data.CalcValues.MMtoPixel(pinGap);
            int pinGapHeightInPixel = Data.CalcValues.MMtoPixel(pinGapHeight);


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
