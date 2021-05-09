using Core;
using Core.Display;
using Encoder;
using Modals;
using System;
using System.Collections.Generic;
using System.IO;

namespace SubbyConsole
{
    class Program
    {
        static void Main(string[] args)
        {


            new Select().SetSubtitleFolder();

            SubtitleProcesser processer = new SubtitleProcesser();

            Types.SubtitleFormat ReadFormat = new Select().GetSubtitleReadFormat();
            processer.ReadFormat = ReadFormat;
            Console.WriteLine($"Format: {ReadFormat.ToString()} is selected");

            if (ReadFormat == Types.SubtitleFormat.ASS)
            {
                new Select().ConfigureAssReader();
            }



            List<Subtitle> list = processer.GetSubtitlesByReadFormat();
            var Writer = new StorageAccess.WriteFile();
            Console.WriteLine($"{list.Count} subtitles has been read");

            if (list.Count > 0)
            {
                List<Types.SubtitleFormat> exportFormats = new Select().GetEncodeFormat();
                foreach (Types.SubtitleFormat format in exportFormats)
                {
                    if (format == Types.SubtitleFormat.SRT)
                    {
                        SRTEncoder encoder = new SRTEncoder();
                        foreach (Subtitle subtitle in list)
                        {
                            encoder.Load((List<Dialog>)subtitle.dialogs);
                            Writer.WriteSrt(subtitle.file, encoder.GetSubtitle());
                        }
                    }
                    else if (format == Types.SubtitleFormat.VTT)
                    {
                        VTTEncoder encoder = new VTTEncoder();
                        foreach (Subtitle subtitle in list)
                        {
                            encoder.Load((List<Dialog>)subtitle.dialogs);
                            Writer.WriteVtt(subtitle.file, encoder.GetSubtitle());
                        }
                    }
                    else if (format == Types.SubtitleFormat.SMI)
                    {
                        SMIEncoder encoder = new SMIEncoder();
                        foreach (Subtitle subtitle in list)
                        {
                            string title = Path.GetFileName(subtitle.file);
                            encoder.Load((List<Dialog>)subtitle.dialogs);
                            Writer.WriteSmi(subtitle.file, encoder.GetSubtitle(title));
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("No subtitles found...");
                Console.Read();
            }




            /*foreach (Subtitle subtitle in list)
            {
                Console.WriteLine(subtitle.file);
                foreach (Dialog dialog in subtitle.dialogs)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(new Timing(dialog.startTime).GetSrtTime()).Append(" => ");
                    sb.Append(new Timing(dialog.endTime).GetSrtTime());
                    sb.AppendLine().AppendLine(dialog.text);

                    Console.WriteLine(sb.ToString());
                }

                Console.Read();
            }*/
            Console.WriteLine("Done!");
        }
    }
}
