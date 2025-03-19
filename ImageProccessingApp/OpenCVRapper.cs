using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ImageProccessingApp
{
    internal static class OpenCVRapper
    {
        public static Bitmap GrayScale(Bitmap bitmap)
        {

            using (Mat mat = BitmapToMat(bitmap))
            {
                var grayMat = new Mat();
                // グレースケール
                Cv2.CvtColor(mat, grayMat, ColorConversionCodes.BGR2GRAY);

                return MatToBitmap(grayMat);
            }
        }

        public static Bitmap GaussianBlur(Bitmap bitmap, int kernelSize)
        {
            using (Mat mat = BitmapToMat(bitmap))
            {
                Mat blurredImage = new Mat();
                // ガウシアンブラー
                Cv2.GaussianBlur(mat, blurredImage, new OpenCvSharp.Size(kernelSize, kernelSize), 0);

                return MatToBitmap(blurredImage);
            }
        }

        public static Bitmap Saturation(Bitmap bitmap, int saturationLevel)
        {
            // 0〜50の範囲の値を0〜2の範囲にスケールします。
            float scale = saturationLevel / 50.0f;  // 0〜1の範囲

            // BitmapをMatに変換
            using (Mat mat = BitmapToMat(bitmap))
            {
                // BGRからHSVに変換
                Mat hsvImage = new Mat();
                Cv2.CvtColor(mat, hsvImage, ColorConversionCodes.BGR2HSV);

                // HSVの各チャンネルを分割
                Mat[] hsvChannels = new Mat[3];
                Cv2.Split(hsvImage, out hsvChannels);

                // 彩度(S)のチャンネルを取得
                Mat saturationChannel = hsvChannels[1];

                // 彩度の変更: scaleに基づいて彩度を変更します。
                saturationChannel.ConvertTo(saturationChannel, MatType.CV_8U, 1 + scale);

                // チャンネルを再結合
                Cv2.Merge(hsvChannels, hsvImage);

                // HSVからBGRに戻す
                Mat resultImage = new Mat();
                Cv2.CvtColor(hsvImage, resultImage, ColorConversionCodes.HSV2BGR);

                // 変換したMatをBitmapに戻す
                return MatToBitmap(resultImage);
            }
        }

        public static Bitmap Contrast(Bitmap bitmap, int contrastLevel)
        {

        }

        #region converter

        public static Mat BitmapToMat(Bitmap bitmap)
        {
            Mat mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);
            return mat;
        }

        public static Bitmap MatToBitmap(Mat mat)
        {
            Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
            return bitmap;
        }

        #endregion converter
    }
}



