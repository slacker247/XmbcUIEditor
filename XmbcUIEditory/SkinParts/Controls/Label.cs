using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmbcUIEditory.SkinParts.Controls
{
    public class Label : Control
    {
        String Text
        {
            get
            {
                return GetInnerText("label");
            }
        }
        String AlignY
        {
            get
            {
                String align = "Center";
                if (!String.IsNullOrEmpty(GetInnerText("aligny")))
                    align = GetInnerText("aligny");
                return align;
            }
        }
        String AlignX
        {
            get
            {
                String align = "Center";
                if (!String.IsNullOrEmpty(GetInnerText("alignx")))
                    align = GetInnerText("alignx");
                return align;
            }
        }
        String Font
        {
            get
            {
                return GetInnerText("font");
            }
        }
        Color TextColor
        {
            get
            {
                Color color = Color.Black;
                if (!String.IsNullOrEmpty(GetInnerText("textcolor")))
                {
                    try
                    {
                        color = ColorTranslator.FromHtml("0x" + GetInnerText("textcolor"));
                    }
                    catch(Exception ex)
                    {
                        color = Color.FromName(GetInnerText("textcolor"));
                    }
                }
                return color;
            }
        }

        public Label(XmlNode node)
            : base(node)
        {
            ;
        }

        public override void render(Graphics g)
        {
            base.render(g);
            if (Width > 0 &&
                Height > 0)
            {
                Bitmap img = new Bitmap(Width, Height);
                Graphics g2 = Graphics.FromImage(img);

                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), AlignX, true);
                strFormat.LineAlignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), AlignY, true);

                g2.DrawString(Text,
                             new Font("Title", 20), new SolidBrush(TextColor),
                             new RectangleF(0, 0, Width, Height),
                             strFormat);
                g2.Flush();
                int x = PosX;
                int y = PosY;
                if (PosX < 0)
                    x = (int)g.VisibleClipBounds.Width + PosX;
                if (PosY < 0)
                    y = (int)g.VisibleClipBounds.Height + PosY;
                g.DrawImage(img, x, y);
                img.Dispose();
                g.Flush();
            }
        }
    }
}
