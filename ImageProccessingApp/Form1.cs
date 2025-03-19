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
        /// �I���摜
        /// </summary>
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


        /// <summary>
        /// �����ς݉摜
        /// </summary>
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

        /// <summary>
        /// Select Image���݉���������
        /// </summary>
        /// <param name="sender">���s�����</param>
        /// <param name="e">�C�x���g���</param>

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

        /// <summary>
        /// Clear���݉���������
        /// </summary>
        /// <param name="sender">���s�����</param>
        /// <param name="e">�C�x���g���</param>
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
        /// ProcessImage���݉���������
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
        /// �K�p���鏈�����ڎ擾
        /// </summary>
        /// <returns>�K�p���ڂ̃��X�g</returns>
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
