using Cleaner.Interfaces;
using Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cleaner
{
    public class Syncro : ISyncro
    {
        private List<Dialog> referenceList;
        //private List<Dialog> 
        public bool AddSignIfNoDialog { get; set; } = false;

        public void Load(List<Dialog> dialogs)
        {
            // Creating a list with new references
            // This should not interfere with existing items from paramter
            this.referenceList = dialogs.ConvertAll(x => new Dialog
            {
                startTime = x.startTime,
                endTime = x.endTime,
                text = x.text,
                isDialog = x.isDialog
            });
            referenceList = referenceList.OrderBy(o => o.startTime).ToList();
        }


        public List<Dialog> GetConflictingDialogs(Dialog current, List<Dialog> referenceList)
        {
            List<Dialog> dialogs = new List<Dialog>();

            int currentIndex = this.referenceList.IndexOf(current) + 1;
            for (int i = currentIndex; i < this.referenceList.Count; i++)
            {
                Dialog pointer = this.referenceList.ElementAt(i);
                if (pointer == null)
                    continue;

                // Checks if Dialog is present in merged list or if pointer is the same as passed value, in that case skip
                

                if (IsOverlapping(current: current, compare: pointer))
                {
                    if (IsAllowedToMerge(current, pointer))
                    {
                        if (!pointer.isDialog)
                        {
                            string[] currentLines = new Sanitize().GetLines(current.text);
                            if (pointer.isDialog || (pointer.isDialog == false && currentLines.Length <= 2))
                                dialogs.Add(pointer);
                        }
                        else
                        {
                            dialogs.Add(pointer);
                        }
                    }
                }
            }
            return dialogs;
        }


        public List<Dialog> SyncOverlapping(Dialog one, Dialog two)
        {
            List<Dialog> newDialogs = new List<Dialog>();
            StringBuilder bothTexts = new StringBuilder();



            // Two ends after one but starts within
            //
            // One   | [-------------------------]
            // Two   |         [---------]
            //
            // After edit
            //
            // OneP1  | [------]
            // OneP2 |                   [-------]
            // Two   |         [=========] 
            if (IsWithin(one, two))
            {
                bothTexts.AppendLine(two.text);
                bothTexts.AppendLine(one.text);
                

                long nTrackStart = two.endTime;
                long nTrackEnd = one.endTime;


                two.text = bothTexts.ToString();
                one.endTime = two.startTime;

                Dialog OneP2 = new Dialog
                {
                    startTime = nTrackStart,
                    endTime = nTrackEnd,
                    text = one.text,
                    isDialog = true
                };
                newDialogs.Add(OneP2);
            }




            // Two ends after one but starts within
            //
            // One   |         [---------]
            // Two   | [-------------------------]
            //
            // After edit
            //
            // TwoP1 | [-------]
            // TwoP2 |                   [-------]
            // One   |         [=========] 
            else if (IsEncapsualting(one, two))
            {
                bothTexts.AppendLine(two.text);
                bothTexts.AppendLine(one.text);


                long nTrackStart = one.endTime;
                long nTrackEnd = two.endTime;


                one.text = bothTexts.ToString();
                two.endTime = one.startTime;

                Dialog TwoP2 = new Dialog
                {
                    startTime = nTrackStart,
                    endTime = nTrackEnd,
                    text = two.text,
                    isDialog = true
                };
                newDialogs.Add(TwoP2);
            }




            // Two starts before one and ends within
            //
            // One   |      [------------]
            // Two   | [---------]
            //
            // After edit
            //
            // One   |           [-------]
            // Two   | [----]
            // Both  |      [====] 
            else if (IsOverreachingStart(one, two))
            {
                bothTexts.AppendLine(one.text);
                bothTexts.AppendLine(two.text);

                long nTrackStart = one.startTime;
                long nTrackEnd = two.endTime;

                two.endTime = nTrackStart;
                one.startTime = nTrackEnd;

                Dialog both = new Dialog
                {
                    startTime = nTrackStart,
                    endTime = nTrackEnd,
                    text = bothTexts.ToString(),
                    isDialog = true
                };

                newDialogs.Add(both);
            }

            // Two ends after one but starts within
            //
            // One   | [------------]
            // Two   |         [---------]
            //
            // After edit
            //
            // One   | [-------]
            // Two   |              [----]
            // Both  |         [====] 
            else if (IsOverreachingEnd(one, two))
            {
                bothTexts.AppendLine(one.text);
                bothTexts.AppendLine(two.text);

                long nTrackStart = two.startTime;
                long nTrackEnd = one.endTime;

                two.startTime = nTrackEnd;
                one.endTime = nTrackStart;


                Dialog both = new Dialog
                {
                    startTime = nTrackStart,
                    endTime = nTrackEnd,
                    text = bothTexts.ToString(),
                    isDialog = true
                };
                newDialogs.Add(both);
            }
            else
            {
                // Do nothing,
                // Should not end up here? Edgecase?
            }


            return newDialogs;
        }

        public bool IsOverlapping(Dialog current, Dialog compare)
        {
            return IsOverreachingEnd(current, compare) || IsOverreachingStart(current, compare) || IsWithin(current, compare) || IsEncapsualting(current, compare);
        }

        public bool IsOverreachingEnd(Dialog current, Dialog compare)
        {
            return (compare.startTime >= current.startTime && compare.endTime >= current.endTime && compare.startTime < current.endTime);
        }

        public bool IsOverreachingStart(Dialog current, Dialog compare)
        {
            return (compare.startTime <= current.startTime && compare.endTime <= current.endTime && compare.endTime < current.endTime);
        }

        public bool IsWithin(Dialog current, Dialog compare)
        {
            return (compare.startTime >= current.startTime && compare.endTime <= current.endTime);
        }

        public bool IsEncapsualting(Dialog current, Dialog compare)
        {
            return (compare.startTime <= current.startTime && compare.endTime >= current.endTime);
        }


        public List<Dialog> GetSynced()
        {
            List<Dialog> synced = new List<Dialog>();
            for (int i = 0; i < referenceList.Count; i++)
            {
                Dialog dialog = referenceList.ElementAt(i);
                synced.Add(dialog);

                List<Dialog> conflictingDialogs = GetConflictingDialogs(dialog, this.referenceList);
                for (int c = 0; c < conflictingDialogs.Count; c++)
                {
                    Dialog inConflict = conflictingDialogs.ElementAt(c);
                    List<Dialog> newDialogs = SyncOverlapping(dialog, inConflict);
                    synced.AddRange(newDialogs);
                }
            }

            // Sort subtitles by start time
            //dialogs.RemoveAll(item => item == null);
            synced.RemoveAll(item => item.startTime == item.endTime);
            synced = synced.OrderBy(o => o.startTime).ToList();
            return synced;
        }


        public bool isEqualNCase(string one, string two)
        {
            one = one.Replace("\r\n", "");
            two = two.Replace("\r\n", "");

            return one.ToLower().Equals(two.ToLower());
        }

        public bool IsAllowedToMerge(Dialog primaryDialog, Dialog dialog)
        {
            bool isIdentical = isEqualNCase(primaryDialog.text, dialog.text);
            if (isIdentical)
                return false; // is not allowed to merge
            /*else if (processed.Contains(dialog) || processed.Contains(primaryDialog))
                return false;*/
            else if (primaryDialog.startTime == primaryDialog.endTime || dialog.startTime == dialog.endTime)
                return false;
            else
                return true;
        }


    }
}
