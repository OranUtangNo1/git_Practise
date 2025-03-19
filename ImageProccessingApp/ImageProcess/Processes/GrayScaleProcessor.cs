using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProccessingApp.Processes
{
    /// <summary>
    /// グレースケール処理
    /// </summary>
    public class GrayProcessor : IProcess
    {
        public Bitmap Process(Bitmap inputImage, ImageProcessSetting setting)
        {
            return OpenCVRapper.GrayScale(inputImage);
        }

    }
}
