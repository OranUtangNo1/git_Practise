using System.Net.NetworkInformation;
using OpenCvSharp;


namespace ImageProccessingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 選択画像
        /// </summary>
        private Bitmap selectedImage;
        Bitmap SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                // picBoxに画像を設定
                this.PicBox_SelectedImage.Image = ImageFileManager.ResizeImage(this.PicBox_SelectedImage.Size, selectedImage);
            }
        }


        /// <summary>
        /// 処理済み画像
        /// </summary>
        private Image processedImage;
        Image ProcessedImage
        {
            get { return processedImage; }
            set
            {
                processedImage = value;
                // picBoxに画像を設定
                this.PicBox_ProcessedImage.Image = ImageFileManager.ResizeImage(this.PicBox_ProcessedImage.Size, processedImage);
            }
        }

        /// <summary>
        /// Select Imageﾎﾞﾀﾝ押下時処理
        /// </summary>
        /// <param name="sender">発行元情報</param>
        /// <param name="e">イベント情報</param>

        private void Btn_SelectImage_Click(object sender, EventArgs e)
        {
            // ファイル選択
            var selectFilepath = ImageFileManager.ImageFileSelect();
            if (!string.IsNullOrEmpty(selectFilepath))
            {
                // 選択ファイルを表示
                this.SelectedImage = new Bitmap (selectFilepath);
            }
            else
            {
                //MessageBox.Show("画像選択エラー");
            }
        }

        /// <summary>
        /// Clearﾎﾞﾀﾝ押下時処理
        /// </summary>
        /// <param name="sender">発行元情報</param>
        /// <param name="e">イベント情報</param>
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            if(this.SelectedImage != null)
            {
                this.SelectedImage.Dispose();
                this.SelectedImage = null;
            }
            this.PicBox_SelectedImage.Image = null;

            if(this.ProcessedImage != null)
            {
                this.ProcessedImage.Dispose();
                this.ProcessedImage = null;
            }
            this.PicBox_ProcessedImage.Image = null;
        }


        /// <summary>
        /// ProcessImageﾎﾞﾀﾝ押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ProcessImage_Click(object sender, EventArgs e)
        {
            var targetProcesses = this.GetSelectedProcessingMethods();
            var setting = new ImageProcessSetting(this.ScBar_Contrast.Value, this.ScBar_Saturation.Value,this.ScBar_Gauss.Value*2+1);
            var processer = new ImageProcessor(targetProcesses, setting);
            var processedBitmap = processer.ProcessExecute(SelectedImage);

            this.ProcessedImage = processedBitmap;
        }

        /// <summary>
        /// 適用する処理項目取得
        /// </summary>
        /// <returns>適用項目のリスト</returns>
        private IEnumerable<Processing> GetSelectedProcessingMethods()
        {
            var selectedMethods = new List<Processing>();
            
            if (this.ChkBox_GrayScale.Checked)
            {
                selectedMethods.Add(Processing.Gray);
            }
            if (this.ChkBox_Gauss.Checked)
            {
                selectedMethods.Add(Processing.Gauss);
            }

            return selectedMethods;
        }
    }
}
