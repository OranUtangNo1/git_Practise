using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProccessingApp
{
    public enum Processing
    {
        Gray,
        Gauss,
        Contrast,
        Saturation
    }

    public static class ProcessConst
    {
    }
    public interface IImageProcessor
    {
        Bitmap ProcessExecute(Bitmap inputImage);
    }

    public class ImageProcessor: IImageProcessor
    {
        private readonly List<IProcess> processors;
        private readonly ImageProcessSetting setting;

        public ImageProcessor(IEnumerable<Processing> processingMethods, ImageProcessSetting setting)
        {
            this.setting = setting;
            processors = new List<IProcess>();

            foreach (var method in processingMethods)
            {
                switch (method)
                {
                    case Processing.Gray:
                        processors.Add(new GrayProcessor());
                        break;
                    case Processing.Gauss:
                        processors.Add(new GaussProcessor());
                        break;
                    case Processing.Contrast:
                        processors.Add(new ContrastProcessor());
                        break;
                    case Processing.Saturation:
                        processors.Add(new SaturationProcessor());
                        break;
                }
            }
        }

        public Bitmap ProcessExecute(Bitmap inputImage)
        {
            Bitmap resultImage = inputImage;
            foreach (var processor in processors)
            {
                resultImage = processor.Process(resultImage, this.setting);
            }
            return resultImage;
        }
    }
    public interface IProcess
    {
        Bitmap Process(Bitmap inputImage, ImageProcessSetting setting);
    }

    public class GrayProcessor : IProcess
    {
        public Bitmap Process(Bitmap inputImage, ImageProcessSetting setting)
        {
            return OpenCVRapper.GrayScale(inputImage);
        }
    }

    public class GaussProcessor : IProcess
    {
        public Bitmap Process(Bitmap inputImage, ImageProcessSetting setting)
        {
            return OpenCVRapper.GaussianBlur(inputImage, setting.GaussianKernel);
        }
    }

    public class ContrastProcessor : IProcess
    {
        public Bitmap Process(Bitmap inputImage, ImageProcessSetting setting)
        {
            // 明るさ調整処理の実装
            return inputImage;
        }
    }

    public class SaturationProcessor : IProcess
    {
        public Bitmap Process(Bitmap inputImage, ImageProcessSetting setting)
        {
            return OpenCVRapper.Saturation(inputImage, setting.SaturationLevel);
        }
    }

    public class ImageProcessSetting
    {
        public int CntrastLevel { get; set; } = 25;
        public int SaturationLevel { get; set; } = 25;
        public int GaussianKernel { get; set; } = 3;

        public ImageProcessSetting(int contrastLevel,int saturationLevel,int gaussianKernel)
        {
            this.CntrastLevel = contrastLevel;
            this.SaturationLevel = saturationLevel;
            this.GaussianKernel = gaussianKernel;
        }
    }
}
