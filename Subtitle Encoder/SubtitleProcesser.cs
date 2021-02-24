using Encoder;
using Encoder.Modal;
using Storage;
using System;
using System.Collections.Generic;
using Encoder.Reader;
using System.Text;

namespace Subtitle_Encoder
{
    public class SubtitleProcesser
    {
        private FileRead read;
        public IList<Subtitle> subtitles { get; private set; }
        public string readFormat { get; set; }

        public SubtitleProcesser()
        {
            read = new FileRead();
        }

        public void readASS()
        {
            string[] files = read.getAllAssFiles();
            this.subtitles = GetSubtitles(files);
        }

        public void writeSrt(string file, string[] dialogs)
        {
            FileWrite fw = new FileWrite();
            fw.WriteSrt(file, dialogs);
        }


        private IList<Subtitle> GetSubtitles(string[] files)
        {
            IList<Subtitle> subtitles = new List<Subtitle>();

            foreach (string file in files)
            {
                string[] contentLines = read.GetContent(file);

                IList<Dialog> dialogs = new AssSubtitleReader(contentLines).GetDialogs();
                subtitles.Add(new Subtitle(file, dialogs));
            }
            return subtitles;
        }




    }
}
