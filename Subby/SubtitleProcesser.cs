using Modals;
using Subby.Reader;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subby
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
            if (ReadFormat == Types.SubtitleFormat.ASS)
                return GetAssSubtitles();

            return new List<Subtitle>();
        }


        private List<Subtitle> GetAssSubtitles()
        {
            string[] files = storage.GetAllAssFiles();

            List<Subtitle> subtitles = new List<Subtitle>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                Console.Clear();
                Console.WriteLine($"Reading File [{i+1}/{files.Length}]");
                Console.WriteLine($"Current file: {file}");

                string[] contentLines = new StorageAccess.ReadFile().GetLines(file);
                AssSubtitleReader reader = new AssSubtitleReader();
                reader.Load(contentLines);

                IList<Dialog> dialogs = reader.GetDialogs();
                subtitles.Add(new Subtitle(file, dialogs));
            }

            return subtitles;
        }


    }
}
