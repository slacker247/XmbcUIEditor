using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmbcUIEditory.SkinParts
{
    public class Window : Renderable
    {
        protected XmlDocument Docu { get; set; }
        public int Id
        {
            get
            {
                int id = -1;
                string val = Docu["window"].Attributes["id"].Value;
                int.TryParse(val, out id);
                return id;
            }
        }
        public String DefaultControl
        {
            get
            {
                return Docu["window"]["defaultcontrol"].InnerText;
            }
        }

        protected List<Control> m_Controls = new List<Control>();

        public Window(String fileName)
        {
            Docu = new XmlDocument();
            Docu.Load(fileName);
            FileInfo fi = new FileInfo(fileName);
            Control.SkinDir = fi.Directory.Parent.FullName;

            foreach (XmlNode cNode in Docu["window"]["controls"].ChildNodes)
            {
                XmlNode lNode = cNode;
                if (lNode.Name == "include" &&
                    Includes.Controls.ContainsKey(lNode.InnerText))
                {
                    lNode = Includes.Controls[lNode.InnerText];
                }
                if (lNode != null && lNode.Name == "control")
                {
                    Control ctrl = null;
                    String type = lNode.Attributes["type"].Value;
                    switch (type)
                    {
                        case "image":
                            ctrl = new Controls.Image(lNode);
                            break;
                        case "button":
                            ctrl = new Controls.Button(lNode);
                            break;
                        case "label":
                            ctrl = new Controls.Label(lNode);
                            break;
                        case "group":
                            ctrl = new Controls.Group(lNode);
                            break;
                    }
                    if (ctrl != null)
                    {
                        m_Controls.Add(ctrl);
                    }
                }
            }
        }

        public void render(Graphics gr)
        {
            foreach (Control ctrl in m_Controls)
                ctrl.render(gr);
            gr.Flush();
        }
    }
}
