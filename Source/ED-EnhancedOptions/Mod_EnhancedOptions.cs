using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions
{
    class Mod_EnhancedOptions : Verse.Mod
    {

        public static ModSettings_EnhancedOptions Settings;

        public Mod_EnhancedOptions(ModContentPack content) : base(content)
        {
            Mod_EnhancedOptions.Settings = GetSettings<ModSettings_EnhancedOptions>();
        }

        public override string SettingsCategory()
        {
            return "ED-Enhanced Options";
            //return base.SettingsCategory();
        }


        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            //base.DoSettingsWindowContents(inRect);
        }

    }
}
