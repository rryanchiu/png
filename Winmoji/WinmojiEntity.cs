using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmoji
{
    public class WinmojiEntity
    {
        public WinmojiEntity()
        {
            this.fontStyle = "Arial";
            this.text = "";
            this.fontSize = 15;
            this.width = 400;
            this.height = 400;
            this.fontColor = Color.White;
            this.backColor = Color.Black;
        }

        public WinmojiEntity(string text,int fontSize, int width, int height, Color fontColor,Color backColor)
        {
            this.text = text;
            this.fontSize = fontSize;
            this.width = width;
            this.height = height;
            this.fontColor = fontColor;
            this.backColor = backColor;
        }

        public String text { get; set; }

        public Color fontColor { get; set; }

        public Color backColor { get; set; }

        public int fontSize { get; set; }

        public String fontStyle { get; set; }

        public int width { get; set; }
        public int height { get; set; }

        public Image CreateImage()
        {
            Font font = new Font(fontStyle, fontSize, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(this.fontColor);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            Bitmap image = new Bitmap(this.width, this.height);
            Graphics g = Graphics.FromImage(image);
            SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);
            g = Graphics.FromImage(image);
            g.Clear(this.backColor);

            RectangleF rf = new Rectangle(Point.Empty, new Size(this.width, this.height));
            g.DrawString(text, font, brush, rf, format);
            g.Dispose();
            return image;
        }

        public Image CreateFullSizeImage(int width,int height)
        {
            Font font = new Font(fontStyle, fontSize, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(this.fontColor);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);
            g = Graphics.FromImage(image);
            g.Clear(this.backColor);

            RectangleF rf = new Rectangle(Point.Empty, new Size(width, height));
            g.DrawString(text, font, brush, rf, format);
            g.Dispose();
            return image;
        }


    }
}
