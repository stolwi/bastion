using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using Bastion.Entity.Map;

namespace Bastion.Entity.Xml
{
    class StartMapXml
    {

        static public List<BlockType> blockTypes { get; set; }
        
        static public void ListToXml(List<StartMap> lst, string filePathName)
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

                    writer.WriteStartElement("StartMaps");
                    foreach (StartMap bt in lst)
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

        static private void ToXml(StartMap bt, XmlWriter writer)
        {
            throw new NotImplementedException();
            //writer.WriteAttributeString("Name", bt.Name);
            //writer.WriteElementString("Walkable", bt.Walkable.ToString());
            //writer.WriteEndElement();
        }

        static public List<StartMap> ListFromXml(string FilePathName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FilePathName);

            XmlNodeList nodes = doc.SelectNodes("StartMaps/StartMap");
            List<StartMap> lst = new List<StartMap>();

            foreach (XmlNode n in nodes)
            {
                StartMap bt = FromXml(n);
                if (bt != null)
                {
                    lst.Add(bt);
                }
            }
            return lst;            
        }

        static private StartMap FromXml(XmlNode node)
        {
            if (blockTypes == null)
            {
                throw new Exception("Must set the blockTypes in StartMapXml before calling FromXml");
            }
            if (node != null)
            {
                StartMap sm = new StartMap();
                if (node.Attributes["name"] == null)
                {
                    throw new Exception("Failed to find name attribute in StartMap");
                }
                sm.Name = node.Attributes["name"].Value;

                if (node.Attributes["xsize"] == null)
                {
                    throw new Exception("Failed to find xsize attribute in StartMap");
                }
                sm.XSize = int.Parse(node.Attributes["xsize"].Value);

                if (node.Attributes["ysize"] == null)
                {
                    throw new Exception("Failed to find ysize attribute in StartMap");
                }
                sm.YSize = int.Parse(node.Attributes["ysize"].Value);

                if (node.Attributes["zsize"] == null)
                {
                    throw new Exception("Failed to find zsize attribute in StartMap");
                }
                sm.ZSize = int.Parse(node.Attributes["zsize"].Value);
                
                sm.FillCommands = MapFillListFromXml(node.SelectNodes("MapFill"));

                return sm;
            }
            return null;
        }

        static private List<MapFillCommand> MapFillListFromXml(XmlNodeList lst)
        {
            List<MapFillCommand> fillList = new List<MapFillCommand>();
            foreach (XmlNode n in lst)
            {
                fillList.Add(MapFillFromXml(n));
            }
            return fillList;
        }

        static private MapFillCommand MapFillFromXml(XmlNode node)
        {
            if (node != null)
            {
                MapFillCommand mf = new MapFillCommand();
                mf.SolidFill = (node.Attributes["Solid"] == null) ? false : bool.Parse(node.Attributes["Solid"].InnerText);
                XmlNode fromXml = node.SelectSingleNode("From");
                if (fromXml == null)
                {
                    throw new Exception("Failed to find From element in MapFill Element");
                }
                if (fromXml.Attributes["X"] == null ||
                    fromXml.Attributes["Y"] == null ||
                    fromXml.Attributes["Z"] == null)
                {
                    throw new Exception("Failed to find X,Y, or Z attribute in From Element");
                }
                Position3 from = new Position3(int.Parse(fromXml.Attributes["X"].InnerText),
                                               int.Parse(fromXml.Attributes["Y"].InnerText),
                                               int.Parse(fromXml.Attributes["Z"].InnerText));
                mf.FromPos = from;

                XmlNode toXml = node.SelectSingleNode("To");
                if (toXml == null)
                {
                    throw new Exception("Failed to find To element in MapFill Element");
                }
                if (toXml.Attributes["X"] == null ||
                    toXml.Attributes["Y"] == null ||
                    toXml.Attributes["Z"] == null)
                {
                    throw new Exception("Failed to find X,Y, or Z attribute in To Element");
                }
                Position3 to = new Position3(int.Parse(toXml.Attributes["X"].InnerText),
                                             int.Parse(toXml.Attributes["Y"].InnerText),
                                             int.Parse(toXml.Attributes["Z"].InnerText));
                mf.ToPos = to;

                XmlNode blockType = node.SelectSingleNode("BlockType");
                if (blockType == null)
                {
                    throw new Exception("Failed to find BlockType element in MapFill Element");
                }
                // Find the block type matching the name in the Xml
                mf.BlockType = blockTypes.FirstOrDefault(x => x.Name.Equals(blockType.InnerText));
                if (mf.BlockType == null)
                {
                    throw new Exception("Unknown block type " + blockType.InnerText + " in StartMap Xml");
                }
                return mf;
            }
            return null;
        }

    }
}
