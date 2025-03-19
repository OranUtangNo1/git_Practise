using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProccessingApp.Processes
{
    /// <summary>
    /// Process用インターフェース
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Process
        /// </summary>
        /// <param name="inputImage">入力画像(bmp)</param>
        /// <param name="setting">画像処理設定クラス</param>
        /// <returns>出力画像(bmp)</returns>
        Bitmap Process(Bitmap inputImage, ImageProcessSetting setting);
    }
}
