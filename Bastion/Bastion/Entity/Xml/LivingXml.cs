using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using Bastion.Entity.Map;
using Bastion.Entity.Alive;

namespace Bastion.Entity.Xml
{
    class LivingXml
    {
        static public void ListToXml(List<Living> lst, string filePathName)
        {
            string workFile;

            workFile = Path.GetTempFileName();

            using (FileStream stream = File.Create(workFile))
            {
                XmlWriterSettings settings;

                settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    writer.WriteStartDocument(true);

                    writer.WriteStartElement("Livings");
                    foreach (Living bt in lst)
                    {
                        ToXml(bt, writer);
                    }
                    writer.WriteEndElement();

                    writer.WriteEndDocument();
                }
            }

            File.Copy(workFile, filePathName, true);
            File.Delete(workFile);
        }

        static private void ToXml(Living living, XmlWriter writer)
        {
            throw new NotImplementedException();
            //writer.WriteAttributeString("Name", living.Name);
            //writer.WriteElementString("Walkable", living.Walkable.ToString());
            //writer.WriteEndElement();
            //writer.WriteElementString("Texture", living.TextureName);
            //writer.WriteEndElement();
        }

        static public List<Living> ListFromXml(string FilePathName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FilePathName);

            XmlNodeList nodes = doc.SelectNodes("Livings/Living");
            List<Living> lst = new List<Living>();

            foreach (XmlNode n in nodes)
            {
                Living bt = FromXml(n);
                if (bt != null)
                {
                    lst.Add(bt);
                }
            }
            return lst;            
        }

        static private Living FromXml(XmlNode node)
        {
            if (node != null)
            {
                Living living = new Living();
                if (node.Attributes["name"] == null)
                {
                    throw new Exception("Failed to find name attribute in Living");
                }
                living.Name = node.Attributes["name"].Value;
                XmlNode canWalkXml = node.SelectSingleNode("CanWalk");
                living.CanWalk = (canWalkXml == null || canWalkXml.InnerText == null) ? false : bool.Parse(canWalkXml.InnerText);

                XmlNode texture = node.SelectSingleNode("Texture");
                if (texture == null)
                {
                    throw new Exception("Failed to find texture element in Living");
                }
                living.TextureName = texture.InnerText;

                XmlNode startPosXml = node.SelectSingleNode("StartPosition");
                if (startPosXml == null)
                {
                    throw new Exception("Failed to find StartPosition element in Living Element");
                }
                if (startPosXml.Attributes["X"] == null ||
                    startPosXml.Attributes["Y"] == null ||
                    startPosXml.Attributes["Z"] == null)
                {
                    throw new Exception("Failed to find X,Y, or Z attribute in StartPosition Element");
                }
                Position3 startPosition = new Position3(int.Parse(startPosXml.Attributes["X"].InnerText),
                                                        int.Parse(startPosXml.Attributes["Y"].InnerText),
                                                        int.Parse(startPosXml.Attributes["Z"].InnerText));
                living.Position.Pos = startPosition;
                
                return living;
            }
            return null;
        }
    }
}
