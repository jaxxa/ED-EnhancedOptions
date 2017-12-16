using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.WarningOptions
{
    class Mod_WarningOptions : Verse.Mod
    {

        public static ModSettings_WarningOptions Settings;

        public Mod_WarningOptions(ModContentPack content) : base(content)
        {
            Mod_WarningOptions.Settings = GetSettings<ModSettings_WarningOptions>();
        }

        public override string SettingsCategory()
        {
            return "WarningOptions";
            //return base.SettingsCategory();
        }


        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            //base.DoSettingsWindowContents(inRect);
        }

    }
}
