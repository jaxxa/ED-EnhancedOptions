/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.GameComps
{
    class GameComponent_LogFileCleanup : Verse.GameComponent
    {
        private System.IO.FileInfo LogFile;

        public GameComponent_LogFileCleanup(Game game)// : base()
        {
            Log.Message("GameComponent_LogFileCleanup.GameComponent_LogFileCleanup()");
            if (LogFile != null)
            {
                Log.Message("GameComponent_LogFileCleanup Skipping Setup, LogFile initilised.");
                return;
            }

            String _FolderNamre = System.IO.Directory.GetDirectories("./").Where(d => d.Contains("Data")).FirstOrDefault();

            if (_FolderNamre == null)
            {
                Log.Message("GameComponent_LogFileCleanup Failed, Folder not Found.");
                return;
            }

            this.LogFile = new System.IO.FileInfo("./" + _FolderNamre + "/output_log.txt");
            Log.Message("LogFile Found: " + this.LogFile.Exists.ToString());
        }
        
        public override void GameComponentTick()
        {
            base.GameComponentTick();

            //Only Run if Enabled.
            if (!Mod_EnhancedOptions.Settings.CheckLogFileSize)
            {
                return;
            }

            //In - Game Time Ticks   Real Time
            //1.5 Min     60  1s
            //1 Hour  60 Min  2,500   41s
            //1 Day   24 Hours    60,000  16m 40s
            //1 Quadrum   15 Days 900,000 4h 10m 0s
            //1 Year  4 Quadrums  3,600,000   16h 40m 0s

            //Only Run every x Ticks.
            if (Find.TickManager.TicksGame % 60000 != 0)
            {
                return;
            }

            //Only Run if LogFile is Setup
            if (this.LogFile == null)
            {
                return;
            }

            this.LogFile.Refresh();
            long _FileSizeInBytes = this.LogFile.Length;
            long _FileSizeInMB = _FileSizeInBytes / (1024 * 1024);
            int _MaxFileSizeMB = Mod_EnhancedOptions.Settings.LogFileSizeThresholdMB;

            //Log.Message("_FileSizeInMB" + _FileSizeInMB);
            if (_FileSizeInMB > _MaxFileSizeMB)
            {
                String _Message = "Warning: Log File Size Exceeds " + _MaxFileSizeMB + " MB - Current Size " + _FileSizeInMB + " MB. This Probably indicates repeating Errors that should be fixed.";

                //Log.Error(_Message);
                Find.LetterStack.ReceiveLetter("Warning: Log File Size", _Message, RimWorld.LetterDefOf.NegativeEvent, (string)null);
                Messages.Message(_Message, RimWorld.MessageTypeDefOf.NegativeEvent);

            }
        }


    }
}
*/