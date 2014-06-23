using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace HiringAutomationTool
{
    class xmlHandler
    {
        public void ReadList()
        {
            Themes theme = new Themes();
            using (XmlReader reader = XmlReader.Create("Themes.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {                        
                        switch (reader.Name.ToString())
                        {
                            case "Name":
                                theme = new Themes();
                                theme.ThemeName = reader.ReadString();
                                break;
                            case "BackColor":
                                theme.BackColor = reader.ReadString();
                                break;
                            case "TextColor":
                                theme.TextColor = reader.ReadString();
                                break;
                            case "TopColor":
                                theme.TopColor = reader.ReadString();
                                break;
                            case "IsActive":
                                bool temp;
                                bool.TryParse(reader.ReadString(), out temp);
                                theme.IsActive = temp;
                                if (theme.ThemeName.Equals("Default") == false)
                                    if (ThemesCollection.Contains(theme.ThemeName)==null)
                                    {
                                        ThemesCollection.themesList.Add(theme);
                                    }
                                break;
                        }
                       


                    }
                }
            }
        }
        public void WriteList()
        {
            using (XmlWriter writer = XmlWriter.Create("Themes.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Themes");
                foreach (Themes x in ThemesCollection.themesList)
                {
                    writer.WriteStartElement("Theme");
                    writer.WriteElementString("Name",x.ThemeName);
                    writer.WriteElementString("BackColor", x.BackColor);
                    writer.WriteElementString("TextColor", x.TextColor);
                    writer.WriteElementString("TopColor", x.TopColor);
                    writer.WriteElementString("IsActive", x.IsActive.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
