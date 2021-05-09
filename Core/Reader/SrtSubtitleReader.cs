using Cleaner;
using Encoder;
using Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Reader
{
    public class SrtSubtitleReader : SubtitleReader
    {
        public override void Load(string[] data)
        {
            List<Dialog> dialogs = GetDialogFromLines(data);
            base.SetDialogs(dialogs);
        }


        protected List<Dialog> GetDialogFromLines(IList<string> lines)
        {
            List<Dialog> dialogs = new List<Dialog>();

            List<string> textLines = new List<string>();
            string start = string.Empty;
            string stop = string.Empty;

            foreach (string _line in lines)
            {
                string line = _line.Trim(); // in case encoding messes up and some line numbers are indented..

                if (Sanitize.onlyNumber.IsMatch(line))
                {
                    if (Config.Debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("[Idify] Id: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(string.Format("{0}", line));
                    }
                    
                }
                else if(Sanitize.timeArrow.IsMatch(line))
                {
                    string[] times = Sanitize.timeArrow.Split(line);
                    start = times[0].Trim();
                    stop = times[1].Trim();

                    if (Config.Debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("[Idify] Time: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(string.Format("{0}", line));
                    }
                }
                else if (!Sanitize.onlyNumber.IsMatch(line) && line.Length > 0)
                {
                    textLines.Add(line);

                    if (Config.Debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[Idify] Text: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(string.Format("{0}", line));
                    }
                }
                else if (line.Trim().Length == 0 && (textLines.Count > 0 && start.Length > 0 && stop.Length > 0))
                {
                    // Spacing between lines
                    // Using this to indicate between different dialogs
                    try
                    {
                        dialogs.Add(
                        new Dialog
                        {
                            startTime = new Timing(start).getMilliseconds(),
                            endTime = new Timing(stop).getMilliseconds(),
                            text = string.Join("\n", textLines)
                        });
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                    start = string.Empty;
                    stop = string.Empty;
                    textLines = new List<string>();

                    if (Config.Debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[Confirm] ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Adding dialog\n");
                    }
                }
            }

            if (textLines.Count > 0 && start.Length > 0 && stop.Length > 0)
            {
                dialogs.Add(
                        new Dialog
                        {
                            startTime = new Timing(start).getMilliseconds(),
                            endTime = new Timing(stop).getMilliseconds(),
                            text = string.Join("\n", textLines)
                        });
            }


            return dialogs;
        }
    }
}
