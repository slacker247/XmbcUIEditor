using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmbcUIEditory.SkinParts
{
    public class Includes
    {
        static XmlDocument Docu;
        public static Dictionary<String, XmlNode> Controls = new Dictionary<String, XmlNode>();
        static bool Loaded = false;

        public static String WorkingDir { get; set; }

        public static void Load(String fileName)
        {
            if (Loaded)
                return;
            Loaded = true;

            Docu = new XmlDocument();
            Docu.Load(Path.Combine(WorkingDir, fileName));

            foreach (XmlNode cNode in Docu["includes"].ChildNodes)
            {
                if(cNode.Name == "include")
                {
                    if (cNode.Attributes["file"] != null)
                        Load(cNode.Attributes["file"].Value);
                    else if(cNode.Attributes["name"] != null)
                    {
                        String name = cNode.Attributes["name"].Value;
                        if(!Controls.ContainsKey(name))
                            Controls.Add(name, cNode.FirstChild);
                    }
                }
            }
        }
    }
}
