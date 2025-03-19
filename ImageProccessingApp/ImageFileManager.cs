using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProccessingApp
{
    public class ImageFileManager()
    {
        public enum FileFilter
        {
            png,
            jpg,
            jpeg,
            bmp,
        }

        // Const
        const string AstDot = "*";
        const string Dot = ".";
        const string SemiColon = ";";
        const string Colon = ":";
        const string Pipe = "｜";
        const string FileFilterHeader = "Image Files";

        /// <summary>
        /// 画像選択
        /// </summary>
        /// <returns>ファイルパス</returns>
        public static string ImageFileSelect()
        {
            var filePath = String.Empty;
            // ファイル選択
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = CreateFileFilter(new FileFilter[] { FileFilter.png, FileFilter.bmp, FileFilter.jpg });
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }

            return filePath;
        }

        /// <summary>
        /// 画像リサイズ
        /// </summary>
        /// <param name="size">希望サイズ</param>
        /// <param name="originalImage">元画像</param>
        /// <returns>リサイズ後画像</returns>
        public static Image ResizeImage(Size size, Image originalImage)
        {
            // Gurd
            if (originalImage == null) { return new Bitmap(size.Width, size.Height); }

            var newImage = new Bitmap(size.Width, size.Height);

            // Graphicsオブジェクトを作成して、新しい画像に描画
            using (Graphics g = Graphics.FromImage(newImage))
            {
                // 描画モード設定（アンチエイリアスなど）
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // newImageをoldImageのサイズに合わせて描画
                g.DrawImage(originalImage, 0, 0, size.Width, size.Height);
            }

            return newImage;
        }

        /// <summary>
        /// ファイルフィルター設定文字列生成
        /// </summary>
        /// <param name="filerTypes">選択可能拡張子</param>
        /// <returns>フィルター文字列</returns>
        private static string CreateFileFilter(IEnumerable<FileFilter> filerTypes)
        {
            var extentions = filerTypes.
                Select(filertype => $"*.{filertype.ToString()};")
                .ToList();
            var filterString = string.Join("", extentions);

            return $"{FileFilterHeader}|{filterString}";
        }
    }
}
