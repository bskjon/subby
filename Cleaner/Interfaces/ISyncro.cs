using Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaner.Interfaces
{
    interface ISyncro
    {

        void Load(List<Dialog> dialogs);


        /// <summary>
        /// Returns a list of Dialogs that are in conflict with the timespan of <paramref name="current"/>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="referenceList">If Dialog is consumed, Dialog will be replaced with null</param>
        /// <returns></returns>
        List<Dialog> GetConflictingDialogs(Dialog current, List<Dialog> referenceList);


        /// <summary>
        /// Processes referenced data and returns new list
        /// </summary>
        /// <returns></returns>
        List<Dialog> GetSynced();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="overreaching">Dialog that is to be merged with <paramref name="current"/></param>
        /// <returns></returns>
        //List<Dialog> GetOverreachStartMerged(Dialog current, Dialog overreaching);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="overreaching">Dialog that is to be merged with <paramref name="current"/></param>
        /// <returns></returns>
        //List<Dialog> GetOverreachEndMerged(Dialog current, Dialog overreaching);


        /// <summary>
        /// Merges the Dialog that is within the timespan of <paramref name="current"/>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="within"></param>
        /// <returns></returns>
        //List<Dialog> GetWithinMerged(Dialog current, Dialog within);


        /// <summary>
        /// Check wenether the compare dialog overlaps the current dialog
        /// Returns true if timespan overlaps in any way
        /// 
        /// Can be used to determine if "Sign" entry should be added to subtitle.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        bool IsOverlapping(Dialog current, Dialog compare);



        /// <summary>
        /// Returns true if <paramref name="compare"/> starts before or at the same time as current, while still ending before <paramref name="current"/>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        bool IsOverreachingStart(Dialog current, Dialog compare);



        /// <summary>
        /// Returns true if <paramref name="compare"/> starts within the timespan of <paramref name="current"/> and ends at the same time or after.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        bool IsOverreachingEnd(Dialog current, Dialog compare);


        /// <summary>
        /// Returns true if start time or end tiem of <paramref name="compare"/> is within the timespan of <paramref name="current"/>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        bool IsWithin(Dialog current, Dialog compare);
    }
}
