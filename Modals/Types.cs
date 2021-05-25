using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Types
    {
        public enum SubtitleFormat
        {
            VTT,
            SRT,
            ASS,
            SMI,
            NOT_SET
        }

        public enum OptionTypes
        {
            OUTPUT_DIR,
            INPUT_FILE,
            INPUT_FORMAT, // Should not be overriden.. Can be used in cases where the encoding is correct but is stored wrongly
            OUTPUT_FORMAT, // If not defined, will output to all formats
            FORCE_SIGNS_AND_SONGS, // Enabling this can cause issues with the output...
            NOT_SET,
            DEBUG
        }
    }
}
