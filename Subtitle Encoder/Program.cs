using Encoder;
using Encoder.Reader;
using Storage;
using System;
using System.Collections.Generic;

namespace Subtitle_Encoder
{
    class Program
    {
        static void Main(string[] args)
        {
            FileRead read = new FileRead();
            FileWrite write = new FileWrite();
            string[] assFiles = read.getAllAssFiles();
            
            foreach (string file in assFiles)
            {
                string[] lines = read.GetContent(file);
                List<Dialog> dialogs = (List<Dialog>) new AssSubtitleReader(lines).GetDialogs();
                Encode enc = new Encode();
                write.WriteSrt(file, enc.GetSrtEncoded(dialogs));

            }

        }



    }
}
