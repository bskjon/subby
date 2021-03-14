using Cleaner;
using Encoder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanerTest
{
    [TestClass]
    public class SyncroTest
    {

        #region IsOverreachingStart
        [TestMethod]
        public void IsOverreachingStart_OverlappingStart()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 100,
                endTime = 600,
                text = "Line Two",
                isDialog = true
            };


            Syncro syncro = new Syncro();
            Assert.IsTrue(syncro.IsOverreachingStart(current, compare));
        }

        [TestMethod]
        public void IsOverreachingStart_OverlappingStartTime()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 500,
                endTime = 600,
                text = "Line Two",
                isDialog = true
            };


            Syncro syncro = new Syncro();
            Assert.IsTrue(syncro.IsOverreachingStart(current, compare));
        }

        [TestMethod]
        public void IsOverreachingStart_OverlappingWithin()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line Two",
                isDialog = true
            };


            Syncro syncro = new Syncro();
            Assert.IsFalse(syncro.IsOverreachingStart(current, compare));
        }

        [TestMethod]
        public void IsOverreachingStart_OverlappingWithinSmaller()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 600,
                endTime = 7000,
                text = "Line Two",
                isDialog = true
            };


            Syncro syncro = new Syncro();
            Assert.IsFalse(syncro.IsOverreachingStart(current, compare));
        }

        [TestMethod]
        public void IsOverreachingStart_NotOverlappingStart()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 600,
                endTime = 800,
                text = "Line Two",
                isDialog = true
            };


            Syncro syncro = new Syncro();
            Assert.IsFalse(syncro.IsOverreachingStart(current, compare));
        }

        [TestMethod]
        public void IsOverreachingStart_NotOverlappingStartButEnd()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 600,
                endTime = 1200,
                text = "Line Two",
                isDialog = true
            };


            Syncro syncro = new Syncro();
            Assert.IsFalse(syncro.IsOverreachingStart(current, compare));
        }

        #endregion

        #region IsOverreachingEnd

        [TestMethod]
        public void IsOverreachingEnd_OverlappingEnd()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 7000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 600,
                endTime = 7000,
                text = "Line Two",
                isDialog = true
            };

            Syncro syncro = new Syncro();
            Assert.IsTrue(syncro.IsOverreachingEnd(current, compare));
        }

        [TestMethod]
        public void IsOverreachingEnd_OverlappingEndTime()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 7000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 500,
                endTime = 8000,
                text = "Line Two",
                isDialog = true
            };

            Syncro syncro = new Syncro();
            Assert.IsTrue(syncro.IsOverreachingEnd(current, compare));
        }

        [TestMethod]
        public void IsOverreachingEnd_OverreachingEnd()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 7000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 1000,
                endTime = 9000,
                text = "Line Two",
                isDialog = true
            };

            Syncro syncro = new Syncro();
            Assert.IsTrue(syncro.IsOverreachingEnd(current, compare));
        }

        [TestMethod]
        public void IsOverreachingEnd_Outside()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 7000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 7000,
                endTime = 9000,
                text = "Line Two",
                isDialog = true
            };

            Syncro syncro = new Syncro();
            Assert.IsFalse(syncro.IsOverreachingEnd(current, compare));
        }


        #endregion


        [TestMethod]
        public void IsOverlapping()
        {
            Dialog current = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line One",
                isDialog = true
            };

            Dialog compare = new Dialog
            {
                startTime = 500,
                endTime = 1000,
                text = "Line Two",
                isDialog = true
            };


            Syncro syncro = new Syncro();
            Assert.IsTrue(syncro.IsWithin(current, compare));
        }

        [TestMethod]
        public void SyncTest()
        {
            Syncro syncro = new Syncro();
            syncro.Load(SyncTestData());
            List<Dialog> synced = syncro.GetSynced();
            
            foreach (Dialog dialog in synced)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(new Timing(dialog.startTime).GetSrtTime()).Append(" => ");
                sb.Append(new Timing(dialog.endTime).GetSrtTime());
                sb.AppendLine().AppendLine(dialog.text);

                System.Diagnostics.Debug.WriteLine(sb.ToString());
            }

        }


        private List<Dialog> SyncTestData()
        {
            List<Dialog> dialogs = new List<Dialog>
            {
#region Within
                new Dialog
                {
                    startTime = 200,
                    endTime = 500,
                    text = "Hello",
                    isDialog = true
                },
                new Dialog
                {
                    startTime = 400,
                    endTime = 500,
                    text = "- Hello",
                    isDialog = true
                },
#endregion
                // Standalone
                new Dialog
                {
                    startTime = 500,
                    endTime = 2000,
                    text = "This is a test to see how sync merges text",
                    isDialog = true
                },


                new Dialog
                {
                    startTime = 2500,
                    endTime = 5000,
                    text = "Subby should handle merging on start and end",
                    isDialog = true,
                },
                new Dialog
                {
                    startTime = 2250,
                    endTime = 5500,
                    text = "But does it work?",
                    isDialog = true
                },

                new Dialog
                {
                    startTime = 10000,
                    endTime = 15000,
                    text = "It should work",
                    isDialog = true
                },
                new Dialog
                {
                    startTime = 12500,
                    endTime = 20000,
                    text = "But does it?",
                    isDialog = true
                }
            };

            return dialogs;
        }

    }
}
