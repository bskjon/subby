using Core;
using Encoder;
using Modals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SubbyNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            SubtitleProcesser processer = new SubtitleProcesser();

            List<RunOption> optionList = new OptionLoader().GetOptions(args);


            string input_subtitle = null;
            Types.SubtitleFormat ReadFormat = Types.SubtitleFormat.NOT_SET;

            if (optionList.Any(x => x.Switch == Types.OptionTypes.INPUT_FILE))
            {
                input_subtitle = (string)optionList.FirstOrDefault(x => x.Switch == Types.OptionTypes.INPUT_FILE).Option;

                if (optionList.Any(x => x.Switch == Types.OptionTypes.INPUT_FORMAT))
                    ReadFormat = Helper.GetSubtitleFormat((string)optionList.FirstOrDefault(x => x.Switch == Types.OptionTypes.INPUT_FORMAT).Option);
                else if (optionList.Any(x => x.Switch == Types.OptionTypes.INPUT_FILE))
                    ReadFormat = Helper.GetSubtitleFormat((string)optionList.FirstOrDefault(x => x.Switch == Types.OptionTypes.INPUT_FILE).Option);
                else
                {
                    Console.Write("Please provide a input file or a file format");
                    Environment.Exit(1);
                }
            }
            else
            {
                Console.Write("Please provide a input file");
                Environment.Exit(1);
            }


            // FOR ASS
            if (optionList.Any(x => x.Switch == Types.OptionTypes.FORCE_SIGNS_AND_SONGS))
            {
                Config.AddSigns = (bool)optionList.FirstOrDefault(x => x.Switch == Types.OptionTypes.FORCE_SIGNS_AND_SONGS).Option;
            }


            string output_dir = null;
            Types.SubtitleFormat OutFormat = Types.SubtitleFormat.NOT_SET;

            if (optionList.Any(x => x.Switch == Types.OptionTypes.OUTPUT_FORMAT))
            {
                OutFormat = Helper.GetSubtitleFormat((string)optionList.FirstOrDefault(x => x.Switch == Types.OptionTypes.OUTPUT_FORMAT).Option);
            }

            if (optionList.Any(x => x.Switch == Types.OptionTypes.OUTPUT_DIR))
            {
                output_dir = (string)optionList.FirstOrDefault(x => x.Switch == Types.OptionTypes.OUTPUT_DIR).Option;
            }

            

            List<Subtitle> list = new List<Subtitle>();
            if (input_subtitle != null && ReadFormat != Types.SubtitleFormat.NOT_SET)
            {
                list.Add(processer.GetSubtitleByFile(input_subtitle, ReadFormat));
            }
            else
            {
                list.AddRange(processer.GetSubtitlesByReadFormat());
            }



            if (list.Count > 0)
            {
                var Writer = new StorageAccess.WriteFile();

                List<Types.SubtitleFormat> exportFormats = Helper.GetExportFormats(OutFormat);

                foreach (Subtitle subtitle in list)
                {
                    string outFile = (output_dir != null) ? Helper.GetOutputFile(output_dir, subtitle.file) : subtitle.file;

                    if (Types.SubtitleFormat.SRT != ReadFormat && exportFormats.Any(x => x == Types.SubtitleFormat.SRT))
                    {
                        SRTEncoder encoder = new SRTEncoder();
                        encoder.Load((List<Dialog>)subtitle.dialogs);
                        Writer.WriteSrt(outFile, encoder.GetSubtitle());
                    }
                    if (Types.SubtitleFormat.VTT != ReadFormat && exportFormats.Any(x => x == Types.SubtitleFormat.VTT))
                    {
                        VTTEncoder encoder = new VTTEncoder();
                        encoder.Load((List<Dialog>)subtitle.dialogs);
                        Writer.WriteVtt(outFile, encoder.GetSubtitle());
                    }
                    if (Types.SubtitleFormat.SMI != ReadFormat && exportFormats.Any(x => x == Types.SubtitleFormat.SMI))
                    {
                        SMIEncoder encoder = new SMIEncoder();
                        string title = Path.GetFileNameWithoutExtension(subtitle.file);
                        encoder.Load((List<Dialog>)subtitle.dialogs);
                        Writer.WriteSmi(outFile, encoder.GetSubtitle(title));
                    }
                }
            }
            else
            {
                Console.WriteLine("No subtitles found...");
                Environment.Exit(1);
            }
        }
    }
}
