using Core.Reader;
using Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class SubtitleProcesser
    {
        public Types.SubtitleFormat ReadFormat { get; set; } = Types.SubtitleFormat.ASS; // Reads ASS by default
        private IList<Subtitle> ReadSubtitles { get; set; }

        private StorageAccess storage;
        public SubtitleProcesser()
        {
            storage = new StorageAccess();
        }

        public List<Subtitle> GetSubtitlesByReadFormat()
        {
            SubtitleReader reader = null;
            string[] files = null;

            switch (ReadFormat)
            {
                case Types.SubtitleFormat.ASS:
                    reader = new AssSubtitleReader();
                    files = storage.GetAllAssFiles();
                    break;
                case Types.SubtitleFormat.SRT:
                    reader = new SrtSubtitleReader();
                    files = storage.GetAllSrtFiles();
                    break;
                case Types.SubtitleFormat.VTT:
                    reader = new VttSubtitleReader();
                    files = storage.GetAllVttFiles();
                    break;
            }

            if (reader == null)
                return new List<Subtitle>();
            return GetSubtitles(reader, files);
        }


        private List<Subtitle> GetSubtitles(SubtitleReader reader, string[] files)
        {
            List<Subtitle> subtitles = new List<Subtitle>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                Console.Clear();
                Console.WriteLine($"Reading File [{i+1}/{files.Length}]");
                Console.WriteLine($"Current file: {file}");

                string[] contentLines = new StorageAccess.ReadFile().GetLines(file);
                reader.Load(contentLines);

                IList<Dialog> dialogs = reader.GetDialogs();
                subtitles.Add(new Subtitle(file, dialogs));
            }

            return subtitles;
        }


        public Subtitle GetSubtitleByFile(string file, Types.SubtitleFormat format)
        {
            List<Dialog> dialogs = new List<Dialog>();

            SubtitleReader reader = null;

            switch(format)
            {
                case Types.SubtitleFormat.ASS:
                    reader = new AssSubtitleReader();
                    break;
                case Types.SubtitleFormat.SRT:
                    reader = new SrtSubtitleReader();
                    break;
                case Types.SubtitleFormat.VTT:
                    reader = new VttSubtitleReader();
                    break;
            }

            if (reader != null)
            {
                string[] contentLines = new StorageAccess.ReadFile().GetLines(file);
                reader.Load(contentLines);
                dialogs.AddRange(reader.GetDialogs());
            }


            return new Subtitle(file, dialogs);
        }

    }
}
