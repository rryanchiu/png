
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace Winmoji
{

    public partial class Winmoji : SkinMain
    {

        private WinmojiEntity winmoji = new WinmojiEntity();

        public Winmoji()
        {
            InitializeComponent();
        }

        private void Winmoji_Load(object sender, EventArgs e)
        {
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            this.widthTextBox.Text = Convert.ToString(screenWidth);
            this.heightTextBox.Text = Convert.ToString(screenHeight);

            winmoji = new WinmojiEntity(this.ContentTextBox.Text,
                Convert.ToInt32(this.fontSizeComboBox.Text),
                this.pictureBox1.Width,
                this.pictureBox1.Height,
                Color.White,
                Color.Black
                );
            this.pictureBox1.Image = winmoji.CreateImage();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = this.colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                winmoji.fontColor = this.colorDialog1.Color;
                this.fontColorButton.BackColor = this.colorDialog1.Color;
                this.pictureBox1.Image = winmoji.CreateImage();
            }
        }

        private void BackColorButton_Click(object sender, EventArgs e)
        {
            DialogResult result = this.colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                winmoji.backColor = this.colorDialog1.Color;
                this.backColorButton.BackColor = this.colorDialog1.Color;
                this.pictureBox1.Image = winmoji.CreateImage();
            }
        }

        private void ContentTextBox_TextChanged(object sender, EventArgs e)
        {
            winmoji.text = this.ContentTextBox.Text;
            this.pictureBox1.Image = winmoji.CreateImage();
        }

        private void FontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.fontSizeComboBox.Text))
            {
                return;
            }
            winmoji.fontSize = Convert.ToInt32(this.fontSizeComboBox.Text);
            this.pictureBox1.Image = winmoji.CreateImage();
        }

        private void FontSizeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.fontSizeComboBox.Text))
            {
                return;
            }
            winmoji.fontSize = Convert.ToInt32(this.fontSizeComboBox.Text);
            this.pictureBox1.Image = winmoji.CreateImage();
        }

        private void ScreenWallButton_Click(object sender, EventArgs e)
        {
            int height = Convert.ToInt32(this.heightTextBox.Text);
            int widht = Convert.ToInt32(this.widthTextBox.Text);
            if (SystemUtil.setScreenWallPaper(winmoji.CreateFullSizeImage(widht, height)))
            {
                MessageBox.Show("设置成功");
            }
            else
            {
                MessageBox.Show("设置失败");
            }
        }

        private void SaveImgButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.heightTextBox.Text) || String.IsNullOrEmpty(this.widthTextBox.Text))
            {
                MessageBox.Show("图片尺寸未设置");
                return;
            }
            int height = Convert.ToInt32(this.heightTextBox.Text);
            int widht = Convert.ToInt32(this.widthTextBox.Text);
            if (SystemUtil.SaveImage(winmoji.CreateFullSizeImage(widht,height)))
            {
                MessageBox.Show("图片保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }

        private void CloseButton_MouseMove(object sender, MouseEventArgs e)
        {
            this.CloseButton.BackColor = Color.DarkRed;
            this.CloseButton.ForeColor = Color.White;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            this.CloseButton.BackColor = Color.White;
            this.CloseButton.ForeColor = Color.Black;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MinButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}