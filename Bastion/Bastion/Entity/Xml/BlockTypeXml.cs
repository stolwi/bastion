using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Bastion.Entity.Xml
{
    class BlockTypeXml
    {
        static public void ListToXml(List<BlockType> lst, string filePathName)
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

                    writer.WriteStartElement("BlockTypes");
                    foreach (BlockType bt in lst)
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

        static private void ToXml(BlockType bt, XmlWriter writer)
        {
            writer.WriteAttributeString("Name", bt.Name);
            writer.WriteElementString("Walkable", bt.Walkable.ToString());
            writer.WriteEndElement();
            writer.WriteElementString("Texture", bt.TextureName);
            writer.WriteEndElement();
        }

        static public List<BlockType> ListFromXml(string FilePathName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FilePathName);

            XmlNodeList nodes = doc.SelectNodes("BlockTypes/BlockType");
            List<BlockType> lst = new List<BlockType>();

            foreach (XmlNode n in nodes)
            {
                BlockType bt = FromXml(n);
                if (bt != null)
                {
                    lst.Add(bt);
                }
            }
            return lst;            
        }

        static private BlockType FromXml(XmlNode node)
        {
            if (node != null)
            {
                BlockType bt = new BlockType();
                if (node.Attributes["name"] == null)
                {
                    throw new Exception("Failed to find name attribute in BlockType");
                }
                bt.Name = node.Attributes["name"].Value;
                XmlNode walkable = node.SelectSingleNode("Walkable");
                bt.Walkable = (walkable == null || walkable.InnerText == null) ? false : bool.Parse(walkable.InnerText);

                XmlNode texture = node.SelectSingleNode("Texture");
                if (texture == null)
                {
                    throw new Exception("Failed to find texture element in BlockType");
                }
                bt.TextureName = texture.InnerText;
                return bt;
            }
            return null;
        }
    }
}
