using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmbcUIEditory
{
    public class Skin
    {
        protected XmlDocument Addon { get; set; }
        public String Folder
        {
            get
            {
                return Addon["addon"]["extension"]["res"].Attributes["folder"].Value;
            }
        }
        public int Width
        {
            get
            {
                int width = -1;
                string val = Addon["addon"]["extension"]["res"].Attributes["width"].Value;
                int.TryParse(val, out width);
                return width;
            }
        }
        public int Height
        {
            get
            {
                int height = -1;
                string val = Addon["addon"]["extension"]["res"].Attributes["height"].Value;
                int.TryParse(val, out height);
                return height;
            }
        }
        public SkinParts.Window Window { get; set; }

        public Skin(String fileName)
        {
            Addon = new XmlDocument();
            Addon.Load(fileName);
            FileInfo fi = new FileInfo(fileName);
            SkinParts.Includes.WorkingDir = Path.Combine(fi.DirectoryName, Folder);
            SkinParts.Includes.Load("includes.xml");
            Window = new SkinParts.Window(Path.Combine(fi.DirectoryName, Folder).ToString() + "/Home.xml");
        }

        public Image render()
        {
            Bitmap img = new Bitmap(Width, Height);
            Window.render(Graphics.FromImage(img));
            return img;
        }
    }
}
