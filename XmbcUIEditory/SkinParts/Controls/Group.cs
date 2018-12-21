using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmbcUIEditory.SkinParts.Controls
{
    public class Group : Control
    {
        int MaxWidth = 0;
        int MaxHeight = 0;

        protected List<Control> m_Controls = new List<Control>();

        public Group(XmlNode node)
            : base(node)
        {
            foreach (XmlNode cNode in node.ChildNodes)
            {
                if (cNode.Name == "control")
                {
                    Control ctrl = null;
                    String type = cNode.Attributes["type"].Value;
                    switch (type)
                    {
                        case "image":
                            ctrl = new Image(cNode);
                            break;
                        case "button":
                            ctrl = new Button(cNode);
                            break;
                        case "label":
                            ctrl = new Label(cNode);
                            break;
                    }
                    if (ctrl != null)
                    {
                        m_Controls.Add(ctrl);
                        if (ctrl.Width > this.MaxWidth)
                            this.MaxWidth = ctrl.Width;
                        if (ctrl.Height > this.MaxHeight)
                            this.MaxHeight = ctrl.Height;
                    }
                }
                else if (cNode.Name == "include")
                {

                }
            }
        }

        public override void render(Graphics g)
        {
            base.render(g);
            if (m_Controls.Count > 0 &&
                MaxWidth > 0 &&
                MaxHeight > 0)
            {
                Bitmap img = new Bitmap(MaxWidth, MaxHeight);
                Graphics g2 = Graphics.FromImage(img);
                foreach (Control ctrl in m_Controls)
                    ctrl.render(g2);
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
