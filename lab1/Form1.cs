using lab1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Bitmap originalImage;
        Stack<Bitmap> filterHistory = new Stack<Bitmap>();

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int newWidth = this.ClientSize.Width;
            int newHeight = this.ClientSize.Height;


            pictureBox1.Width = newWidth / 2;
            pictureBox1.Height = newHeight / 2;
            pictureBox1.Location = new Point((newWidth - pictureBox1.Width) / 2, (newHeight - pictureBox1.Height) / 2);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            progressBar1.Width = newWidth / 2;
            progressBar1.Location = new Point((newWidth - progressBar1.Width) / 2, newHeight - progressBar1.Height - 20);

            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.Width = newWidth / 8;
                    button.Location = new Point((newWidth - button.Width), newHeight - button.Height - 20);
                }
            }

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files| *.png; *.jpg;* .bmp|All files(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
                originalImage = (Bitmap)image.Clone();
                filterHistory.Push((Bitmap)image.Clone());
            }
        }


        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new InvertFilter());
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
                filterHistory.Push((Bitmap)image.Clone());
            }
            progressBar1.Value = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void UndoLastFilter()
        {
            if (filterHistory.Count > 1)
            {
                filterHistory.Pop();
                image = (Bitmap)filterHistory.Peek().Clone();
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoLastFilter();
        }

        private void ApplyFilter(Filters filter)
        {
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new MedianFilter());
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new BlurFilter());
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new GaussianFilter());
        }

        private void вОттенкахСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new GrayScaleFilters());
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new SepiaFilter());
        }

        private void увеличитьЯркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new BrightnessFilter());
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                ApplyFilter(new SobelFilter());
            }
            else
            {
                MessageBox.Show("Загрузите изображение сначала.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void увеличитьРезкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new SharpnessFilter());
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new EmbossFilter());
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new TranslateFilter());
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double angle = Math.PI / 4;

            int centerX = image.Width / 2;
            int centerY = image.Height / 2;

            ApplyFilter(new RotateFilter(angle, centerX, centerY));
        }

        private void волны1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int amplitude = 20;
            ApplyFilter(new WavesFilter1(amplitude));
        }

        private void волны2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int amplitude = 20;
            ApplyFilter(new WavesFilter2(amplitude));
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new GlassEffectFilter());
        }

        private void блюрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new MotionBlurFilter(10));
        }

        private void линейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new AutoLevelsFilter());
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
    }
}
