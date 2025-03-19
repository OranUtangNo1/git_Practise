using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProccessingApp.Processes
{
    /// <summary>
    /// ガアウシアンフィルター(ぼかし)処理
    /// </summary>
    public class GaussProcessor : IProcess
    {
        public Bitmap Process(Bitmap inputImage, ImageProcessSetting setting)
        {
            return OpenCVRapper.GaussianBlur(inputImage, setting.GaussianKernel);
        }
    }
}
