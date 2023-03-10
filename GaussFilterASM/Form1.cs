using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System;
using System.Windows.Forms;

namespace GaussFilterASM
{
    public partial class Form1 : Form
    {
        private Bitmap beforeBitmap { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Select bmp file",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "bmp",
                Filter = "bmp files (*.bmp)|*.bmp",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                beforeBmpBox.Image = new Bitmap(fileDialog.FileName);
                beforeBitmap = new Bitmap(fileDialog.FileName);
                runButton.Enabled = true;

            }

        }


        [DllImport("C:\\Users\\Pawe? Skorupa\\Desktop\\semestr V\\JA\\GaussFilterASM\\x64\\Release\\GaussAsmDLL.dll")]
        static extern void gaussBlurAsm(byte[] inputPixels, int size, int imageWidht, int startHeight, int endHeight);

        [LibraryImport("C:\\Users\\Pawe? Skorupa\\Desktop\\semestr V\\JA\\GaussFilterASM\\x64\\Release\\GaussCppDLL.dll")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        public static partial void gaussBlur(byte[] inputPixels, int size, int imageWidth, int startHeight, int endHeight);
        private void runButton_Click(object sender, EventArgs e)
        {

            runButton.Enabled = false;

            if (!testCheckbox.Checked)
            {
                runBlurNormally();
            }
            else
            {
                runBlurSpeedtest();
            }

            runButton.Enabled = true;
            saveButton.Enabled = true;
        }

        private void runBlurNormally()
        {

            int width = beforeBitmap.Width;
            int height = beforeBitmap.Height;

            var timer = new System.Diagnostics.Stopwatch();

            Rectangle rect = new Rectangle(0, 0, beforeBitmap.Width, beforeBitmap.Height);
            BitmapData bmpData = beforeBitmap.LockBits(rect, ImageLockMode.ReadWrite, beforeBitmap.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * beforeBitmap.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            int numberOfThreads = threadsTrackbar.Value;

            int dividedPart = height / numberOfThreads;

            Thread[] threads = new Thread[numberOfThreads];

            if (cppButton.Checked)
            {

                for (int i = 0; i < (numberOfThreads - 1); i++)
                {
                    int localIndex = i;
                    threads[i] = new Thread(() => gaussBlur(rgbValues, bytes, width, localIndex * dividedPart, (localIndex + 1) * dividedPart));
                }
                threads[numberOfThreads - 1] = new Thread(() =>
                gaussBlur(rgbValues, bytes, width, (numberOfThreads - 1) * dividedPart, height - 1));

            }
            else
            {

                for (int i = 0; i < (numberOfThreads - 1); i++)
                {
                    int localIndex = i;
                    threads[i] = new Thread(() => gaussBlurAsm(rgbValues, bytes, width /** height*/, localIndex * dividedPart, (localIndex + 1) * dividedPart));
                }
                threads[numberOfThreads - 1] = new Thread(() =>
                gaussBlurAsm(rgbValues, bytes, width, (numberOfThreads - 1) * dividedPart, height - 1));

            }

            timer.Start();
            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            timer.Stop();

            Marshal.Copy(rgbValues, 0, ptr, bytes);

            beforeBitmap.UnlockBits(bmpData);

            afterBmpBox.Image = beforeBitmap;

            timeValueLabel.Text = timer.ElapsedTicks.ToString() + " ticks";


        }

        private void runBlurSpeedtest()
        {

            String outputPath = "C:\\Users\\Pawe? Skorupa\\Desktop\\semestr V\\JA\\GaussFilterASM\\SpeedtestResults\\"
                + DateTime.Now.ToString("yyyyMMdd-hhmmssss") + ".csv";

            StreamWriter writer = new StreamWriter(outputPath);
            writer.WriteLine("threads,C++,Asm");

            int width = beforeBitmap.Width;
            int height = beforeBitmap.Height;

            Rectangle rect = new Rectangle(0, 0, beforeBitmap.Width, beforeBitmap.Height);
            BitmapData bmpData = beforeBitmap.LockBits(rect, ImageLockMode.ReadWrite, beforeBitmap.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * beforeBitmap.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int numberOfThreads = 1; numberOfThreads <= 64; numberOfThreads++)
            {

                var timer = new System.Diagnostics.Stopwatch();

                int dividedPart = height / numberOfThreads;

                Thread[] threads = new Thread[numberOfThreads];

                for (int i = 0; i < (numberOfThreads - 1); i++)
                {
                    int localIndex = i;
                    threads[i] = new Thread(() =>
                    {
                        for (int i = 0; i < 10; i++) {
                            gaussBlur(rgbValues, bytes, width, localIndex * dividedPart, (localIndex + 1) * dividedPart);
                        }
                    });
                }
                threads[numberOfThreads - 1] = new Thread(() =>
                {
                    for (int i = 0; i < 10; i++) {
                        gaussBlur(rgbValues, bytes, width, (numberOfThreads - 1) * dividedPart, height - 1);
                    }

                });

                timer.Start();
                foreach (Thread thread in threads)
                {
                    thread.Start();
                }

                foreach (Thread thread in threads)
                {
                    thread.Join();
                }
                timer.Stop();

                writer.Write(numberOfThreads + "," + (timer.ElapsedTicks / 10).ToString() + ",");
                writer.Flush();

                for (int i = 0; i < (numberOfThreads - 1); i++)
                {
                    int localIndex = i;
                    threads[i] = new Thread(() => {
                        for (int i = 0; i <= 10; i++) {
                            gaussBlurAsm(rgbValues, bytes, width, localIndex * dividedPart, (localIndex + 1) * dividedPart);
                            }
                        }
                    );
                }
                threads[numberOfThreads - 1] = new Thread(() => {
                    for (int i = 0; i <= 10; i++) {
                        gaussBlurAsm(rgbValues, bytes, width, (numberOfThreads - 1) * dividedPart, height - 1);
                        }
                    }
                );

                timer.Restart();
                //for (int i = 0; i < 10; i++)
                //{
                    foreach (Thread thread in threads)
                    {
                        thread.Start();
                    }

                    foreach (Thread thread in threads)
                    {
                        thread.Join();
                    }
                //}
                timer.Stop();

                writer.Write((timer.ElapsedTicks / 10).ToString() + "\n");
                writer.Flush();

                //Marshal.Copy(rgbValues, 0, ptr, bytes);

                //beforeBitmap.UnlockBits(bmpData);

                //afterBmpBox.Image = beforeBitmap;

            }

            writer.Close();

        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "bmp files (*.bmp)|*.bmp";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Length > 0)
            {
                FileStream fs = (FileStream)saveFileDialog.OpenFile();
                afterBmpBox.Image.Save(fs, ImageFormat.Bmp);
                fs.Close();
            }
        }
    }
}