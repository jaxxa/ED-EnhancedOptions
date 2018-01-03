using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions
{
    class ModSettings_EnhancedOptions : ModSettings
    {
        //Speed 4,5,6?
        //-Stop combat slowdown
        //Raid Scale
        //Supress Dev Mode

        //Blight Colours??? (Or split off to separate mod)
        //--Glowing?
        //--Overlay

        //Power Priorities???
        //-Time to Leave
        //--https://ludeon.com/forums/index.php?topic=37751.msg386804#new
        
        public bool ShowLettersThreatBig = true;
        public bool ShowLettersThreatSmall = true;
        public bool ShowLettersNegativeEvent = true;
        public bool ShowLettersNeutralEvent = true;
        public bool ShowLettersPositiveEvent = true;
        public bool ShowLettersItemStashFeeDemand = true;

        public bool LetterNamesToSuppressEnabled = false;
        public string LetterNamesToSuppress = String.Empty;

        public bool Plant24HEnabled = false;
        public bool PlantLights24HEnabled = false;

        public bool SafeTrapEnabled = false;
        public bool TurretControlEnabled = false;
        public bool HidePowerConnections = false;

        public bool SuppressBreakdown = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref ShowLettersThreatBig, "ShowLettersThreatBig", true, true);
            Scribe_Values.Look<bool>(ref ShowLettersThreatSmall, "ShowLettersThreatSmall", true, true);
            Scribe_Values.Look<bool>(ref ShowLettersNegativeEvent, "ShowLettersNegativeEvent", true, true);
            Scribe_Values.Look<bool>(ref ShowLettersNeutralEvent, "ShowLettersNeutralEvent", true, true);
            Scribe_Values.Look<bool>(ref ShowLettersPositiveEvent, "ShowLettersPositiveEvent", true, true);
            Scribe_Values.Look<bool>(ref ShowLettersItemStashFeeDemand, "ShowLettersItemStashFeeDemand", true, true);
            Scribe_Values.Look<bool>(ref LetterNamesToSuppressEnabled, "LetterNamesToSuppressEnabled", false, true);
            Scribe_Values.Look<string>(ref LetterNamesToSuppress, "LetterNamesToSuppress", String.Empty, true);

            Scribe_Values.Look<bool>(ref Plant24HEnabled, "Plant24HEnabled", false, true);
            Scribe_Values.Look<bool>(ref PlantLights24HEnabled, "PlantLights24HEnabled", Plant24HEnabled, true); //If Not Set Default to Plant24HEnabled for backwards compatibility.
            Scribe_Values.Look<bool>(ref SafeTrapEnabled, "SafeTrapEnabled", false, true);
            Scribe_Values.Look<bool>(ref TurretControlEnabled, "TurretControlEnabled", false, true);
            Scribe_Values.Look<bool>(ref HidePowerConnections, "HidePowerConnections", false, true);
            Scribe_Values.Look<bool>(ref SuppressBreakdown, "SuppressBreakdown", false, true);

        }


        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.ColumnWidth = 250f;
            listing_Standard.Begin(canvas);
            //            listing_Standard.
            //            listing_Standard.set_ColumnWidth(rect.get_width() - 4f);

            listing_Standard.Label("Sections Starting with '*' only apply after Restart.");
            listing_Standard.GapLine(12f);
            listing_Standard.Label("Letter Suppression:");
            listing_Standard.Gap(12f);
            listing_Standard.CheckboxLabeled("Show ThreatBig", ref ShowLettersThreatBig, "True if you want to See Any ThreatBig Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            listing_Standard.CheckboxLabeled("Show ThreatSmall", ref ShowLettersThreatSmall, "True if you want to See Any ThreatSmall Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            listing_Standard.CheckboxLabeled("Show NegativeEvent", ref ShowLettersNegativeEvent, "True if you want to See Any NegativeEvent Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            listing_Standard.CheckboxLabeled("Show NeutralEvent", ref ShowLettersNeutralEvent, "True if you want to See Any NeutralEvent Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            listing_Standard.CheckboxLabeled("Show PositiveEvent", ref ShowLettersPositiveEvent, "True if you want to See Any PositiveEvent Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            listing_Standard.CheckboxLabeled("Show ItemStashFeeDemand", ref ShowLettersItemStashFeeDemand, "True if you want to See Any ItemStashFeeDemand Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            listing_Standard.Gap(12f);
            listing_Standard.CheckboxLabeled("Letter Names To Suppress Enabled", ref LetterNamesToSuppressEnabled, "True will Hide any Letters thats Name is in the following List, False to Ignore the List. List is Comma Separated. When a Letter is Shown its Name and Type will be written to the Log.");
            LetterNamesToSuppress = listing_Standard.TextEntry(LetterNamesToSuppress, 2);

            listing_Standard.GapLine(12f);

            listing_Standard.Label("* Plant 24H:");
            listing_Standard.CheckboxLabeled("Plant 24H", ref Plant24HEnabled, "Enable to allow Plants to Grow 24H a day.");
            listing_Standard.CheckboxLabeled("Plant Lights 24H", ref PlantLights24HEnabled, "Enable to allow SunLamps to Shine 24H a day.");
            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Safe Trap Enabled:");
            listing_Standard.CheckboxLabeled("Safe Trap Enabled", ref SafeTrapEnabled, "Prevents Traps from triggering on your Colonists.");
            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Turret Control Enabled:");
            listing_Standard.CheckboxLabeled("Turret Control Enabled", ref TurretControlEnabled, "Allows force attack commands to be given to turrets.");
            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Hide Power Connections:");
            listing_Standard.CheckboxLabeled("Hide Power Connections", ref HidePowerConnections, "Hides the Small Power Connection Wires, Still show in Power overlay Mode.");
            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Suppress Breakdown:");
            listing_Standard.CheckboxLabeled("Suppress Breakdown", ref SuppressBreakdown, "Suppress random Breakdowns, This was hard to test so please let me know if you have any issues.");

            listing_Standard.End();
        }
    }
}
