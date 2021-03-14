using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Encoder.Interfaces;
using Modals;

namespace Encoder
{
    public class SMIEncoder : Encode, ISMIEncoder
    {
        public override void Load(List<Dialog> dialogs)
        {
            base.dialogs = dialogs;
            AppendEncoderLine();
        }


        public XmlDocument GetSubtitle(string title)
        {
            XmlDocument document = new XmlDocument();
            XmlNode root = document.DocumentElement;

            XmlElement sami = document.CreateElement("sami");
            document.AppendChild(sami);

            XmlElement head = document.CreateElement("head");
            sami.AppendChild(head);

            XmlNode subtitleTitle = document.CreateElement("title");
            subtitleTitle.InnerText = title;
            head.AppendChild(subtitleTitle);

            XmlElement body = document.CreateElement("body");
            sami.AppendChild(body);


            foreach (Dialog dialog in dialogs)
            {
                XmlElement sync = document.CreateElement("sync");
                sync.SetAttribute("Start", dialog.startTime.ToString());
                body.AppendChild(sync);

                XmlNode text = document.CreateElement("p");
                text.InnerText = dialog.text;
                sync.AppendChild(text);

                // Clear text
                sync = document.CreateElement("sync");
                sync.SetAttribute("Start", dialog.endTime.ToString());
                body.AppendChild(sync);

                text = document.CreateElement("p");
                text.InnerText = "&nbsp;";
                sync.AppendChild(text);

            }

            return document;
        }
    }
}
