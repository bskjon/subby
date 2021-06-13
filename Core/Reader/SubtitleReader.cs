﻿using Cleaner;
using Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Reader
{
    public abstract class SubtitleReader
    {

        //readonly Regex signRegex = new Regex("(?<=,)(?i)(sign|Screen)");
        readonly Regex textTags = new Regex(@"([<](?<=<).*?(?=>)[>])|({\\[^ib])");
        //readonly Regex textTags = new Regex(@"([<](?<=<).*?(?=>)[>])|([{](?<={).*?(?=})[}])");
        readonly Regex songText = new Regex(@"[^A-Za-z\s]+((?i)OP(?-i)|(?i)ED(?-i))");
        readonly Regex signRegex = new Regex("((?i)Signs(?-i)|(?i)Screen(?-i))");
        readonly Regex possiblyScreenOrSign = new Regex(@"\\[a-zA-Z0-9]+&[A-Za-z0-9]+&\\");

        readonly Regex dialog = new Regex("(?i)Dialogue:(?i)");


        private List<Dialog> Dialogs { get; set; }

        private void SanitizeDialogs(List<Dialog> dialogs)
        {
            var sanitize = new Sanitize();
            foreach (Dialog dialog in dialogs)
            {
                dialog.text = sanitize.GetSanitized(dialog.text);
            }
        }

        /*protected bool IsDialog(string text)
        {
            bool isDialogEntry = dialog.IsMatch(text);
            bool isPossiblyScreenOrSign = possiblyScreenOrSign.IsMatch(text);

            bool isDialog = !isPossiblyScreenOrSign && isDialogEntry; 
            return isDialog;
        }*/

        protected bool IsDialog(string key, string text, bool hasDifferentTags)
        {
            bool isSignOrSong = signRegex.IsMatch(key) || songText.IsMatch(key);
            bool isPossiblyScreenOrSign = possiblyScreenOrSign.IsMatch(text);

            bool isDialogEntry = dialog.IsMatch(text);

            bool hasTextTags =  textTags.IsMatch(text);

            bool isDialog = (!hasDifferentTags) ? (!isSignOrSong && !isPossiblyScreenOrSign && isDialogEntry) : !hasTextTags;
            return isDialog;
        }

        protected bool IsSongOrSign(string key, string text, bool hasDifferentTags)
        {
            bool isSignOrSong = signRegex.IsMatch(key) || songText.IsMatch(key);
            bool isPossiblyScreenOrSign = possiblyScreenOrSign.IsMatch(text);

            bool isDialogEntry = dialog.IsMatch(text);

            bool hasTextTags = textTags.IsMatch(text);

            bool isDialog = (!hasDifferentTags) ? (isSignOrSong || isPossiblyScreenOrSign) && isDialogEntry : hasTextTags;
            return isDialog;
        }

        public abstract void Load(string[] data);

        public void SetDialogs(List<Dialog> dialogs)
        {
            SanitizeDialogs(dialogs);
            SquashDuplicates(dialogs);
            this.Dialogs = dialogs;

        }

        public IList<Dialog> GetDialogs()
        {
            Syncro syncro = new Syncro();
            syncro.Load(Dialogs);
            this.Dialogs = syncro.GetSynced();

            return this.Dialogs;
        }


        public bool isEqualNCase(string one, string two)
        {
            one = one.Replace("\r\n", "");
            two = two.Replace("\r\n", "");

            return one.ToLower().Equals(two.ToLower());
        }

        protected void SquashDuplicates(List<Dialog> dialogs)
        {
            for (int i = 0; i < dialogs.Count; i++)
            {
                Dialog pointer = dialogs.ElementAt(i);
                if (pointer == null)
                    continue;
                var DupesList = GetDuplicates(dialogs, pointer);
                if (DupesList.Count > 0)
                {
                    for (int x = 0; x < DupesList.Count; x++)
                    {
                        Dialog dupe = DupesList.ElementAt(x);
                        if (dupe == pointer)
                            continue;
                        else
                        {

                            dialogs[dialogs.IndexOf(dupe)] = null;
                        }
                    }
                }
            }
            dialogs.RemoveAll(item => item == null);
        }

        private List<Dialog> GetDuplicates(List<Dialog> dialogs, Dialog current)
        {
            List<Dialog> duplicates = new List<Dialog>();
            foreach (Dialog dialog in dialogs)
            {
                if (dialog != null && isEqualNCase(dialog.text, current.text) && dialog != current)
                {
                    if (dialog.startTime == current.startTime && dialog.endTime == current.endTime)
                    {
                        duplicates.Add(dialog);
                    }
                    /*else if (dialog.startTime >= current.startTime && dialog.endTime >= current.endTime)
                    {
                        current.startTime = dialog.startTime;
                        current.endTime = dialog.endTime;
                        duplicates.Add(dialog);
                    }*/
                }
            }
            return duplicates;
        }
    }
}
