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
        
            //Plant Toggle?

            //Raid Scale

        public bool ShowLettersThreatBig = true;
        public bool ShowLettersThreatSmall = true;
        public bool ShowLettersNegativeEvent = true;
        public bool ShowLettersNeutralEvent = true;
        public bool ShowLettersPositiveEvent = true;
        public bool ShowLettersItemStashFeeDemand = true;

        public bool LetterNamesToSuppressEnabled = false;
        public string LetterNamesToSuppress = String.Empty;

        public bool Plant24HEnabled = false;
        public bool SafeTrapEnabled = false;
        public bool TurretControlEnabled = false;


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
            Scribe_Values.Look<bool>(ref SafeTrapEnabled, "SafeTrapEnabled", false, true);
            Scribe_Values.Look<bool>(ref TurretControlEnabled, "TurretControlEnabled", false, true);

        }


        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.ColumnWidth = 250f;
            listing_Standard.Begin(canvas);
            //            listing_Standard.
            //            listing_Standard.set_ColumnWidth(rect.get_width() - 4f);
            listing_Standard.Label("Letter Suppression:");
            listing_Standard.Gap(12f);
            listing_Standard.CheckboxLabeled("Show ThreatBig", ref ShowLettersThreatBig);
            listing_Standard.CheckboxLabeled("Show ThreatSmall", ref ShowLettersThreatSmall);
            listing_Standard.CheckboxLabeled("Show NegativeEvent", ref ShowLettersNegativeEvent);
            listing_Standard.CheckboxLabeled("Show NeutralEvent", ref ShowLettersNeutralEvent);
            listing_Standard.CheckboxLabeled("Show PositiveEvent", ref ShowLettersPositiveEvent);
            listing_Standard.CheckboxLabeled("Show ItemStashFeeDemand", ref ShowLettersItemStashFeeDemand);
            listing_Standard.Gap(12f);
            listing_Standard.CheckboxLabeled("Letter Names To Suppress Enabled", ref LetterNamesToSuppressEnabled);
            LetterNamesToSuppress = listing_Standard.TextEntry(LetterNamesToSuppress, 2);

            listing_Standard.Gap(12f);
            listing_Standard.Label("Plant 24H:");
            listing_Standard.CheckboxLabeled("Plant24H", ref Plant24HEnabled);
            listing_Standard.Gap(12f);
            listing_Standard.Label("SafeTrapEnabled:");
            listing_Standard.CheckboxLabeled("SafeTrapEnabled", ref SafeTrapEnabled);
            listing_Standard.Gap(12f);
            listing_Standard.Label("TurretControlEnabled:");
            listing_Standard.CheckboxLabeled("TurretControlEnabled", ref TurretControlEnabled);

            listing_Standard.End();
        }
    }
}
