using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmbcUIEditory.SkinParts.Controls
{
    public class Button : Control
    {
        String TextureNoFocus
        {
            get
            {
                return Path.Combine(
                                Control.SkinDir,
                                "media",
                                "Textures",
                                GetInnerText("texturenofocus"));
            }
        }
        String TextureFocus
        {
            get
            {
                return Path.Combine(
                                Control.SkinDir,
                                "media",
                                "Textures",
                                GetInnerText("texturefocus"));
            }
        }
        int Border
        {
            get
            {
                int val = 0;
                String txt = GetAttribute("texturefocus", "border");
                int.TryParse(txt, out val);
                return val;
            }
        }
        String OnClick
        {
            get
            {
                String evt = "";
                evt = GetInnerText("onclick");
                return evt;
            }
        }

        public Button(XmlNode node)
            : base(node)
        {
        }

        public override void render(Graphics g)
        {
            base.render(g);
            if (File.Exists(TextureNoFocus) &&
                    Width > 0 &&
                    Height > 0)
            {
                //Bitmap img = new Bitmap(Width, Height);
                //Graphics g2 = Graphics.FromImage(img);
                System.Drawing.Image txt = Bitmap.FromFile(TextureNoFocus);
                int x = PosX;
                int y = PosY;
                if (PosX < 0)
                    x = (int)g.VisibleClipBounds.Width + PosX;
                if (PosY < 0)
                    y = (int)g.VisibleClipBounds.Height + PosY;
                Rectangle destinationRect = new Rectangle(
                    x + Border,
                    y + Border,
                    Width - Border,
                    Height - Border);
                Rectangle sourceRect = new Rectangle(0, 0, txt.Width, txt.Height);
                g.DrawImage(txt,
                            destinationRect,
                            sourceRect,
                            GraphicsUnit.Pixel);
                txt.Dispose();
                //img.Dispose();
                g.Flush();
            }
        }
    }
}
