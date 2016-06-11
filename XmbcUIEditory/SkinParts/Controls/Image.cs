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
    public class Image : Control
    {
        String Texture
        {
            get
            {
                String path = GetInnerText("texture");
                if(path.Contains("special://skin/"))
                {
                    path = Path.Combine(
                                Control.SkinDir,
                                path.Replace("special://skin/", "").Replace("/", "\\")
                                );
                }
                else
                {
                    path = Path.Combine(
                                Control.SkinDir,
                                "media",
                                "Textures",
                                path
                                );
                }
                return path;
            }
        }
        String Fallback
        {
            get
            {
                String path = GetAttribute("texture", "fallback");
                if (path.Contains("special://skin/"))
                {
                    path = Path.Combine(
                                Control.SkinDir,
                                path.Replace("special://skin/", "").Replace("/", "\\")
                                );
                }
                else
                {
                    path = Path.Combine(
                                Control.SkinDir,
                                "media",
                                "Textures",
                                path
                                );
                }
                return path;
            }
        }
        int BorderSize
        {
            get
            {
                int val = 0;
                String txt = GetInnerText("bordersize");
                int.TryParse(txt, out val);
                return val;
            }
        }

        public Image(XmlNode node)
            : base(node)
        {
        }

        public override void render(Graphics g)
        {
            base.render(g);
            String textureFile = "";
            if (File.Exists(Texture))
                textureFile = Texture;
            else if (File.Exists(Fallback))
                textureFile = Fallback;
            if (!String.IsNullOrEmpty(textureFile))
            {
                if (this.Width > 0 &&
                    this.Height > 0)
                {
                    //Bitmap img = new Bitmap(Width, Height);
                    //Graphics g2 = Graphics.FromImage(img);
                    System.Drawing.Image txt = Bitmap.FromFile(textureFile);
                    int x = PosX;
                    int y = PosY;
                    if (PosX < 0)
                        x = (int)g.VisibleClipBounds.Width + PosX;
                    if (PosY < 0)
                        y = (int)g.VisibleClipBounds.Height + PosY;
                    Rectangle destinationRect = new Rectangle(
                        x + BorderSize, 
                        y + BorderSize,
                        Width - BorderSize,
                        Height - BorderSize);
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
            else
            {
                Console.WriteLine("Can't find file.");
            }
        }
    }
}
