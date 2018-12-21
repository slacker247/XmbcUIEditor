using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmbcUIEditory.SkinParts
{
    public class Control : Renderable
    {
        static public String SkinDir { get; set; }
        protected XmlNode Node { get; set; }

        public int Id
        {
            get
            {
                int id = -1;
                string val = "";
                if(Node.Attributes["id"] != null)
                    val = Node.Attributes["id"].Value;
                int.TryParse(val, out id);
                return id;
            }
        }
        public int PosX
        {
            get
            {
                int id = -1;
                bool rev = false;
                string val = GetInnerText("posx");
                if (val.EndsWith("r"))
                {
                    val = val.Substring(0, val.Length - 1);
                    rev = true;
                }
                int.TryParse(val, out id);
                if (rev)
                    id *= -1;
                return id;
            }
        }
        public int PosY
        {
            get
            {
                int id = -1;
                bool rev = false;
                string val = GetInnerText("posy");
                if (val.EndsWith("r"))
                {
                    val = val.Substring(0, val.Length - 1);
                    rev = true;
                }
                int.TryParse(val, out id);
                if (rev)
                    id *= -1;
                return id;
            }
        }
        public int Width
        {
            get
            {
                int id = -1;
                string val = GetInnerText("width");
                int.TryParse(val, out id);
                return id;
            }
        }
        public int Height
        {
            get
            {
                int id = -1;
                string val = GetInnerText("height");
                int.TryParse(val, out id);
                return id;
            }
        }
        public String Description
        {
            get
            {
                string val = GetInnerText("description");
                return val;
            }
        }
        public bool Visible
        {
            get
            {
                bool val = false;
                String txt = GetInnerText("visible");

                return val;
            }
        }

        public Control(XmlNode node)
        {
            Node = node;
        }

        protected String GetValue(String property)
        {
            return GetValue(Node, property);
        }

        public static String GetValue(XmlNode node, String property)
        {
            String value = "";
            if (node[property] != null)
                value = node[property].Value;
            return value;
        }

        protected String GetInnerText(String property)
        {
            return GetInnerText(Node, property);
        }

        public static String GetInnerText(XmlNode node, String property)
        {
            String value = "";
            if (node[property] != null)
                value = node[property].InnerText;
            return value;
        }

        protected String GetAttribute(String childName, String attribute)
        {
            return GetAttribute(Node, childName, attribute);
        }

        public static String GetAttribute(XmlNode node, String childName, String attribute)
        {
            String value = "";
            if (node[childName] != null &&
                node[childName].Attributes[attribute] != null)
                value = node[childName].Attributes[attribute].Value;
            return value;
        }

        public virtual void render(Graphics gr)
        {

        }
    }
}
