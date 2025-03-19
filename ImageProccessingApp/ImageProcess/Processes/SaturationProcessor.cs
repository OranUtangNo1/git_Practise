using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProccessingApp.Processes
{
    /// <summary>
    /// 彩度変更
    /// </summary>
    public class SaturationProcessor : IProcess
    {
        public Bitmap Process(Bitmap inputImage, ImageProcessSetting setting)
        {
            return OpenCVRapper.Saturation(inputImage, setting.SaturationLevel);
        }
    }
}
