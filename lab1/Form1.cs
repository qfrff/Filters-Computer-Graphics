using lab1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace lab1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files| *.png; *.jpg;* .bmp|All files(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                image = new Bitmap(dialog.FileName);
            }

            pictureBox1.Image = image;
            pictureBox1.Refresh();
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //InvertFilter filter = new InvertFilter();
            //Bitmap resultImage = filter.processImage(image);
            //pictureBox1.Image = resultImage;
            //pictureBox1.Refresh();

            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filters = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filters);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new GaussianFilter();

            backgroundWorker1.RunWorkerAsync(filter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void вОттенкахСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                GrayScaleFilters grayScaleFilter = new GrayScaleFilters();
                backgroundWorker1.RunWorkerAsync(grayScaleFilter);
                originalImage = pictureBox1.Image.Clone() as Bitmap;
            }
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SepiaFilter sepiaFilter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(sepiaFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void увеличитьЯркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrightnessFilter brightnessFilter = new BrightnessFilter();
            backgroundWorker1.RunWorkerAsync(brightnessFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                SobelFilter sobelFilter = new SobelFilter();
                backgroundWorker1.RunWorkerAsync(sobelFilter);
                originalImage = pictureBox1.Image.Clone() as Bitmap;
            }
            else
            {
                MessageBox.Show("Загрузите изображение сначала.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void увеличитьРезкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpnessFilter sharpnessFilter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(sharpnessFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmbossFilter embossFilter = new EmbossFilter();
            backgroundWorker1.RunWorkerAsync(embossFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TranslateFilter translateFilter = new TranslateFilter();
            backgroundWorker1.RunWorkerAsync(translateFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double angle = Math.PI / 4;

            int centerX = image.Width / 2;
            int centerY = image.Height / 2;

            RotateFilter rotateFilter = new RotateFilter(angle, centerX, centerY);
            backgroundWorker1.RunWorkerAsync(rotateFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void волны1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int amplitude = 20;

            WavesFilter1 wavesFilter1 = new WavesFilter1(amplitude);
            backgroundWorker1.RunWorkerAsync(wavesFilter1);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void волны2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int amplitude = 20;

            WavesFilter2 wavesFilter2 = new WavesFilter2(amplitude);
            backgroundWorker1.RunWorkerAsync(wavesFilter2);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlassEffectFilter glassEffectFilter = new GlassEffectFilter();
            backgroundWorker1.RunWorkerAsync(glassEffectFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        private void блюрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MotionBlurFilter motionBlurFilter = new MotionBlurFilter(10);
            backgroundWorker1.RunWorkerAsync(motionBlurFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }

        ImageSaver imageSaver = new ImageSaver();

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                imageSaver.SaveImage((Bitmap)pictureBox1.Image);
                MessageBox.Show("Фотография сохранена");
            }
            else
            {
                MessageBox.Show("Фотография не сохранена");
            }
        }

        private Bitmap originalImage;

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                {
                    if (originalImage != null)
                    {
                        pictureBox1.Image = originalImage.Clone() as Bitmap;
                    }
                }
            }
        }

        private void линейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoLevelsFilter linearStretchFilter = new AutoLevelsFilter();
            backgroundWorker1.RunWorkerAsync(linearStretchFilter);
            originalImage = pictureBox1.Image.Clone() as Bitmap;
        }
    }
}