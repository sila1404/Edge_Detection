using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace Edge_Detection
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> inputImage;
        Image<Gray, byte> outputImage, cannyImage;
        Image<Gray, float> sobelImage, laplacianImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                inputImage = new Image<Bgr, byte>(opf.FileName);
                imgInput.Image = inputImage;
            }
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputImage != null)
            {
                cannyImage = inputImage.Convert<Gray, byte>().Canny(150, 10);
                imgOutput.Image = cannyImage;

                outputImage = cannyImage;
            }
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputImage != null)
            {
                sobelImage = inputImage.Convert<Gray, float>().Sobel(1, 1, 5);
                CvInvoke.Normalize(sobelImage, sobelImage, 0, 255, NormType.MinMax, DepthType.Cv8U);
                imgOutput.Image = sobelImage;

                outputImage = sobelImage.Convert<Gray, byte>();
            }
        }

        private void laplacianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputImage != null)
            {
                laplacianImage = inputImage.Convert<Gray, byte>().Laplace(5);
                imgOutput.Image = laplacianImage.Convert<Gray, byte>();

                outputImage = laplacianImage.Convert<Gray, byte>();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.Title = "Save Capture Image";
            svf.DefaultExt = "*.jpg";
            svf.Filter = "Jpeg Files (*.jpg)|*.jpg";

            if (svf.ShowDialog() == DialogResult.OK)
            {
                if (outputImage != null)
                {
                    outputImage.Save(svf.FileName);
                }
                else
                {
                    MessageBox.Show("No image in the ImageBox to save.");
                }
            }
        }
    }
}