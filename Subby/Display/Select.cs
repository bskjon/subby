using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Subby.Display
{
    public class Select
    {
        public void SetSubtitleFolder()
        {
            PrintSubtitleFolderScreen();

            bool validPath = false;
            while (validPath == false)
            {
                Console.Clear();
                PrintSubtitleFolderScreen();
                try
                {
                    Console.Write("Path: ");
                    var input = Console.ReadLine().ToString();
                    if (input.Equals("c") || input.Equals("C"))
                    {
                        Console.Clear();
                        return;
                    }


                    FileAttributes attr = File.GetAttributes(input);
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        validPath = true;
                        Program.SubtitleFolder = input;
                    }
                    else
                    {
                        Console.WriteLine("Path is not valid directory..");
                    }
                        
                }
                catch
                {

                }
                
            }
            Console.Clear();
        }

        public void PrintSubtitleFolderScreen()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string selectionTopText = @"

Subtitle folder:
";
            string selectionOptions = $@"

Current Path {Program.SubtitleFolder}

[c] Cancel
";
            Console.WriteLine(selectionTopText);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(selectionOptions);
            Console.ForegroundColor = ConsoleColor.White;
        }








        public Types.SubtitleFormat GetSubtitleReadFormat()
        {
            Types.SubtitleFormat format = Types.SubtitleFormat.NOT_SET;
            while (format == Types.SubtitleFormat.NOT_SET)
            {
                PrintFormatSelection();
                try
                {
                    Console.Write("Select: ");
                    var input = Console.ReadLine().ToString();
                    if (input.Equals("c") || input.Equals("C"))
                        return format;

                    var selection = Convert.ToInt32(input);
                    if (selection == 0)
                        format = Types.SubtitleFormat.ASS;
                    else if (selection == 1)
                        format = Types.SubtitleFormat.SRT;
                    else if (selection == 2)
                        format = Types.SubtitleFormat.VTT;
                }
                catch
                {

                }
                Console.Clear();
            }

            return format;
        }

        public void PrintFormatSelection()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string selectionTopText= @"

Please select subtitle format to read
Available formats:
";
            string selectionOptions = @"

[0] ASS     (SSA)
[1] SRT     (Subrip)
[2] VTT     (WebVtt)

[c] Cancel
";
            Console.WriteLine(selectionTopText);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(selectionOptions);
            Console.ForegroundColor = ConsoleColor.White;
        }



        public List<Types.SubtitleFormat> GetEncodeFormat()
        {
            List<Types.SubtitleFormat> formats = new List<Types.SubtitleFormat>();

            while (formats.Count == 0)
            {
                PrintEncodeFormatSelectionList();
                try
                {
                    Console.Write("Select: ");
                    var input = Console.ReadLine().ToString();
                    if (input.Equals("c") || input.Equals("C"))
                        return formats;
                    else if (input.Equals("a") || input.Equals("A"))
                    {
                        formats.Add(Types.SubtitleFormat.SRT);
                        formats.Add(Types.SubtitleFormat.VTT);
                        formats.Add(Types.SubtitleFormat.SMI);
                    }

                    var selection = Convert.ToInt32(input);
                    if (selection == 0)
                        formats.Add(Types.SubtitleFormat.SRT);
                    else if (selection == 1)
                        formats.Add(Types.SubtitleFormat.VTT);
                    else if (selection == 2)
                        formats.Add(Types.SubtitleFormat.SMI);
                }
                catch
                {

                }
                Console.Clear();
            }





            return formats;
        }

        public void PrintEncodeFormatSelectionList()
        {

            Console.ForegroundColor = ConsoleColor.White;
            string selectionTopText = @"

Please select subtitle format to export
Available formats:
";
            string selectionOptions = @"

[0] SRT     (Subrip)
[2] VTT     (WebVtt)
[3] SMI     (Sami)

[a] All     (Export to all formats listed)
[c] Cancel
";
            Console.WriteLine(selectionTopText);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(selectionOptions);
            Console.ForegroundColor = ConsoleColor.White;

        }


        public void ConfigureAssReader()
        {
            var input = "";
            while (!input.Equals("c") && !input.Equals("C"))
            {
                PrintAssSelection();
                try
                {
                    Console.Write("Select: ");
                    input = Console.ReadLine().ToString();
                    /*if (input.Equals("c") || input.Equals("C"))
                        return;*/

                    var selection = Convert.ToInt32(input);
                    if (selection == 0)
                        Program.AddSigns = !Program.AddSigns;
                    
                }
                catch
                {

                }
                Console.Clear();
            }
        }

        public void PrintAssSelection()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string selectionTopText = @"

ASS Configurations:
";
            string selectionOptions = $@"

[0] Ignore Signs and Screens {!Program.AddSigns}

[c] Cancel
";
            Console.WriteLine(selectionTopText);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(selectionOptions);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
