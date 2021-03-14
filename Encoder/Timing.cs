using System;
using System.Collections.Generic;
using System.Text;

namespace Encoder
{
    public class Timing
    {
        public TimeSpan time { private get; set; }

        /// <summary>
        /// Creates a timespan based on input value
        /// Throws error if not valid timespan as string
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="">FormatException</exception>
        public Timing(string value)
        {
            time = TimeSpan.Parse(value);
        }

        public Timing()
        {

        }

        public Timing(long time)
        {
            this.time = FromMilliseconds(time);
        }

        public TimeSpan FromMilliseconds(long milli)
        {
            return TimeSpan.FromMilliseconds(milli);
        }

        public Timing(int value)
        {
            time = TimeSpan.FromMilliseconds(value);
        }

        public long getMilliseconds()
        {
            return (long)time.TotalMilliseconds;
        }

        public string getTimeAsString()
        {
            return time.ToString();
        }

        public string GetSrtTime()
        {
            return this.time.ToString(@"hh\:mm\:ss\,fff");
        }

        public string GetVttTime()
        {
            return this.time.ToString(@"hh\:mm\:ss\.fff");
        }

    }
}
