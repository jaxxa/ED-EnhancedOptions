using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.GameComps
{
    class GameComponent_LogFileCleanup : Verse.GameComponent
    {

        public GameComponent_LogFileCleanup(Game game)
        {
            // GameComponent_QuantumShield.GameComp = this;
        }

        public override void GameComponentTick()
        {
            base.GameComponentTick();

            //Only Run every 20 Ticks.
            if (Find.TickManager.TicksGame % 60000 != 0)
            {
                return;
            }
            
            System.IO.FileInfo _Temp = new System.IO.FileInfo("./RimWorld1722Win_Data/output_log.txt");
            //Log.Message(_Temp.Exists.ToString());
            Log.Message("Length in Bytes: " + _Temp.Length.ToString());

        }


    }
}
