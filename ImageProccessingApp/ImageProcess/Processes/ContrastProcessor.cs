using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProccessingApp.Processes
{
    /// <summary>
    /// 明るさ変更
    /// </summary>
    public class ContrastProcessor : IProcess
    {
        public Bitmap Process(Bitmap inputImage, ImageProcessSetting setting)
        {
            // 明るさ調整処理の実装
            return inputImage;
        }
    }
}
