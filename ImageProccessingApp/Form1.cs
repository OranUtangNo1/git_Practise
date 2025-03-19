using System.Net.NetworkInformation;
using OpenCvSharp;


namespace ImageProccessingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.imageProcessingData = new ImageProcessingData();
            this.imageFileManager = new ImageFileManager();
        }

        ImageProcessingData imageProcessingData { get; set; }
        ImageFileManager imageFileManager { get; set; }

        private Bitmap selectedImage;
        Bitmap SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                // picBox�ɉ摜��ݒ�
                this.PicBox_SelectedImage.Image = ImageFileManager.ResizeImage(this.PicBox_SelectedImage.Size, selectedImage);
            }
        }

        private Image processedImage;
        Image ProcessedImage
        {
            get { return processedImage; }
            set
            {
                processedImage = value;
                // picBox�ɉ摜��ݒ�
                this.PicBox_ProcessedImage.Image = ImageFileManager.ResizeImage(this.PicBox_ProcessedImage.Size, processedImage);
            }
        }

        private void Btn_SelectImage_Click(object sender, EventArgs e)
        {
            // �t�@�C���I��
            var selectFilepath = ImageFileManager.ImageFileSelect();
            if (!string.IsNullOrEmpty(selectFilepath))
            {
                // �I���t�@�C����\��
                this.SelectedImage = new Bitmap (selectFilepath);
            }
            else
            {
                //MessageBox.Show("�摜�I���G���[");
            }
        }

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

        private void Btn_ProcessImage_Click(object sender, EventArgs e)
        {
            var targetProcesses = this.GetSelectedProcessingMethods();
            var setting = new ImageProcessSetting(this.ScBar_Contrast.Value, this.ScBar_Saturation.Value,this.ScBar_Gauss.Value*2+1);
            var processer = new ImageProcessor(targetProcesses, setting);
            var processedBitmap = processer.ProcessExecute(SelectedImage);

            this.ProcessedImage = processedBitmap;
        }

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

    /// <summary>
    /// �摜�����f�[�^�N���X
    /// </summary>
    public class ImageProcessingData
    {
        int Contrast {  get; set; }
        int Satulation { get; set; }

        public ImageProcessingData() 
        {
            this.Contrast = 0;
            this.Satulation = 0;
        }
    }
}
