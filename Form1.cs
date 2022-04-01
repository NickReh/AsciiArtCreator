using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AsciiArt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 0;
            tbHeight.Text = "10";
            tbWidth.Text = "8";
            RestoreDefaultChars();
            tbFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = false;
            DialogResult result = op1.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                op1.Filter = "allfiles|*.png";
                string[] FName;
                string newFilePath, newFileName;
                int height = int.Parse(tbHeight.Text);
                int width = int.Parse(tbWidth.Text);
                foreach (string s in op1.FileNames)
                {
                    Image image1 = Image.FromFile(s);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        FName = s.Split('\\');
                        newFilePath = tbFolder.Text.Trim().Length > 0 ? tbFolder.Text : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        newFileName = tbTitle.Text.Trim().Length > 0 ? tbTitle.Text.Replace(".txt", "") + ".txt" : FName[FName.Length - 1].Replace(".png", ".txt").Replace(".jpg", ".txt").Replace(".jpeg", ".txt");
                        using (StreamWriter file = new StreamWriter(newFilePath + "\\" + newFileName))
                        {
                            image1.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                            Bitmap bm = new Bitmap(stream);
                            Color origColor;
                            int colorAve;
                            for (int i = 0; i < bm.Height / height; i++)
                            {
                                for (int j = 0; j < bm.Width / width; j++)
                                {
                                    colorAve = 0;
                                    for (int k = 0; k < height; k++)
                                    {
                                        for (int l = 0; l < width; l++)
                                        {
                                            origColor = bm.GetPixel(j * width + l, i * height + k);
                                            int grayScale = (int)((origColor.R * .3) + (origColor.G * .59) + (origColor.B * .11));
                                            colorAve += grayScale;
                                        }
                                    }
                                    colorAve = Convert.ToInt32(colorAve / (height * width));
                                    if (j == (bm.Width / width) - 1)
                                        file.WriteLine(GetAsciiChar(colorAve));
                                    else
                                        file.Write(GetAsciiChar(colorAve));
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Image has been artified!");
            }
        }

        private string GetAsciiChar(int grayScale)
        {
            if (grayScale < 10)
                return tb12.Text.Trim().Length > 0 ? tb12.Text.Substring(0, 1) : " ";
            else if (grayScale < 25)
                return tb11.Text.Trim().Length > 0 ? tb11.Text.Substring(0, 1) : " ";
            else if (grayScale < 40)
                return tb10.Text.Trim().Length > 0 ? tb10.Text.Substring(0, 1) : " ";
            else if (grayScale < 60)
                return tb9.Text.Trim().Length > 0 ? tb9.Text.Substring(0, 1) : " ";
            else if (grayScale < 80)
                return tb8.Text.Trim().Length > 0 ? tb8.Text.Substring(0, 1) : " ";
            else if (grayScale < 100)
                return tb7.Text.Trim().Length > 0 ? tb7.Text.Substring(0, 1) : " ";
            else if (grayScale < 125)
                return tb6.Text.Trim().Length > 0 ? tb6.Text.Substring(0, 1) : " ";
            else if (grayScale < 150)
                return tb5.Text.Trim().Length > 0 ? tb5.Text.Substring(0, 1) : " ";
            else if (grayScale < 175)
                return tb4.Text.Trim().Length > 0 ? tb4.Text.Substring(0, 1) : " ";
            else if (grayScale < 200)
                return tb3.Text.Trim().Length > 0 ? tb3.Text.Substring(0, 1) : " ";
            else if (grayScale < 225)
                return tb2.Text.Trim().Length > 0 ? tb2.Text.Substring(0, 1) : " ";
            else
                return tb1.Text.Trim().Length > 0 ? tb1.Text.Substring(0, 1) : " ";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                tbHeight.Text = "10";
                tbWidth.Text = "8";
            }
            else if (listBox1.SelectedIndex == 1)
            {
                tbHeight.Text = "6";
                tbWidth.Text = "4";
            }
            else
            {
                tbHeight.Text = "3";
                tbWidth.Text = "2";
            }
        }

        private void tbHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if (!int.TryParse(tbHeight.Text, out height))
                tbHeight.Text = "8";
        }

        private void tbWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (!int.TryParse(tbWidth.Text, out width))
                tbWidth.Text = "6";
        }

        private void tbFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Select folder to save ascii art to.";
            folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowser.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if(folderBrowser.ShowDialog() != DialogResult.Cancel)
                tbFolder.Text = folderBrowser.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RestoreDefaultChars();
        }

        private void RestoreDefaultChars()
        {
            tb12.Text = "K";
            tb11.Text = "N";
            tb10.Text = "#";
            tb9.Text = "@";
            tb8.Text = "&";
            tb7.Text = "%";
            tb6.Text = "i";
            tb5.Text = "c";
            tb4.Text = ":";
            tb3.Text = "*";
            tb2.Text = ".";
            tb1.Text = " ";
        }
    }
}
