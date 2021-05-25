using Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core
{
    public class OptionLoader
    {
        public List<RunOption> GetOptions(string[] args)
        {
            List<RunOption> options = new List<RunOption>();
            var InputFile = GetInput(args);
            if (InputFile != null) { options.Add(InputFile); }

            var OutputDir = GetOutput(args);
            if (OutputDir != null) { options.Add(OutputDir); }

            var InputFormat = GetInputFormat(args);
            if (InputFormat != null) { options.Add(InputFormat); }

            var OutFormat = GetOutputFormat(args);
            if (OutFormat != null) { options.Add(OutFormat); }

            var EnableSAS = GetEnableSignsAndSongs(args);
            if (EnableSAS != null) { options.Add(EnableSAS); }

            var DEBUG = Debug(args);
            if (DEBUG != null) { options.Add(DEBUG); }

            return options;
        }

        private RunOption GetInput(string[] args)
        {
            int pos = Array.FindIndex(args, x => (x.ToLower() == "-input" || x.ToLower() == "-i"));
            string arg = GetArg(args, pos);
            if (arg != null)
            {
                return new RunOption
                {
                    Switch = Types.OptionTypes.INPUT_FILE,
                    Option = arg
                };
            }
            return null;
        }

        private RunOption GetOutput(string[] args)
        {
            int pos = Array.FindIndex(args, x => (x.ToLower() == "-output" || x.ToLower() == "-o"));
            string arg = GetArg(args, pos);
            if (arg != null)
            {
                return new RunOption
                {
                    Switch = Types.OptionTypes.OUTPUT_DIR,
                    Option = arg
                };
            }
            return null;
        }


        private RunOption GetInputFormat(string[] args)
        {
            int pos = Array.FindIndex(args, x => (x.ToLower() == "-in_fmt" || x.ToLower() == "-if"));
            string arg = GetArg(args, pos);
            if (arg != null)
            {
                return new RunOption
                {
                    Switch = Types.OptionTypes.INPUT_FORMAT,
                    Option = arg
                };
            }
            return null;
        }

        private RunOption GetOutputFormat(string[] args)
        {
            int pos = Array.FindIndex(args, x => (x.ToLower() == "-out_fmt" || x.ToLower() == "-of"));
            string arg = GetArg(args, pos);
            if (arg != null)
            {
                return new RunOption
                {
                    Switch = Types.OptionTypes.OUTPUT_FORMAT,
                    Option = arg.ToLower()
                };
            }
            return null;
        }

        /// <summary>
        /// Experimental feature
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private RunOption GetEnableSignsAndSongs(string[] args)
        {
            int pos = Array.FindIndex(args, x => (x.ToLower() == "-enc_sas" || x.ToLower() == "-enc_sign_song"));
            string arg = GetArg(args, pos);
            if (arg != null)
            {
                return new RunOption
                {
                    Switch = Types.OptionTypes.FORCE_SIGNS_AND_SONGS,
                    Option = GetEnable(arg.ToLower(), false)
                };
            }
            return new RunOption
            {
                Switch = Types.OptionTypes.FORCE_SIGNS_AND_SONGS,
                Option = false
            };
        }

        private bool GetEnable(string arg, bool standard = false)
        {
            switch (arg)
            {
                case "enable":
                case "y":
                case "yes":
                    return true;
                case "disable":
                case "n":
                case "no":
                    return false;

                default:
                    return standard;
            }
        }

        private RunOption Debug(string[] args)
        {
            int pos = Array.FindIndex(args, x => x.ToLower() == "-debug");
            string arg = GetArg(args, pos);
            if (arg != null)
            {
                return new RunOption
                {
                    Switch = Types.OptionTypes.DEBUG,
                    Option = GetEnable(arg.ToLower(), false)
                };
            }
            return new RunOption
            {
                Switch = Types.OptionTypes.DEBUG,
                Option = false
            };
        }

        private string GetArg(string[] args, int switchPosition)
        {
            if (switchPosition != -1 && (args.Length - 1) >= (switchPosition + 1))
            {
                return args.ElementAt(switchPosition + 1);
            }
            return null;
        }
    }
}
