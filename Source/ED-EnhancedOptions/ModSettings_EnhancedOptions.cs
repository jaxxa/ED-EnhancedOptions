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
        //Un Saved
        string _Buffer_LearnFactorPassionNone;
        string _Buffer_LearnFactorPassionMinor;
        string _Buffer_LearnFactorPassionMajor;

        //Saved
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
        public bool LockDevMode = false;
        public bool Speed4WithoutDev = false;
        public bool SuppressCombatSlowdown = false;
        public bool HideSpots = false;
        public bool SuppressRoofColapse = false;
        public bool SuppressRainFire = false;
        public bool CheckLogFileSize = false;
        public int LogFileSizeThresholdMB = 50;

        /// <summary>
        /// DrawSize of the Blight, Default 1
        /// </summary>
        public float BlightScale = 1;

        /// <summary>
        /// Number of the Image to use for the Blight
        /// 
        /// 0 = Default
        /// 1 = Red
        /// 2 = Blue
        /// 3 = Orange
        /// 4 = Purple
        /// 
        /// </summary>
        public int BlightImageIndex = 0;

        public bool SuppressStrippingCremationCorps = false;

        public bool ApplyLearnFactorChanges = false;
        public int LearnFactorPassionNonePercentage = 35;
        public int LearnFactorPassionMinorPercentage = 100;
        public int LearnFactorPassionMajorPercentage = 150;

       
        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref ShowLettersThreatBig, "ShowLettersThreatBig", true);
            Scribe_Values.Look<bool>(ref ShowLettersThreatSmall, "ShowLettersThreatSmall", true);
            Scribe_Values.Look<bool>(ref ShowLettersNegativeEvent, "ShowLettersNegativeEvent", true);
            Scribe_Values.Look<bool>(ref ShowLettersNeutralEvent, "ShowLettersNeutralEvent", true);
            Scribe_Values.Look<bool>(ref ShowLettersPositiveEvent, "ShowLettersPositiveEvent", true);
            Scribe_Values.Look<bool>(ref ShowLettersItemStashFeeDemand, "ShowLettersItemStashFeeDemand", true);
            Scribe_Values.Look<bool>(ref LetterNamesToSuppressEnabled, "LetterNamesToSuppressEnabled", false);
            Scribe_Values.Look<string>(ref LetterNamesToSuppress, "LetterNamesToSuppress", String.Empty);

            Scribe_Values.Look<bool>(ref Plant24HEnabled, "Plant24HEnabled", false);
            Scribe_Values.Look<bool>(ref PlantLights24HEnabled, "PlantLights24HEnabled", false);
            Scribe_Values.Look<bool>(ref SafeTrapEnabled, "SafeTrapEnabled", false);
            Scribe_Values.Look<bool>(ref TurretControlEnabled, "TurretControlEnabled", false);
            Scribe_Values.Look<bool>(ref HidePowerConnections, "HidePowerConnections", false);
            Scribe_Values.Look<bool>(ref SuppressBreakdown, "SuppressBreakdown", false);
            Scribe_Values.Look<bool>(ref LockDevMode, "LockDevMode", false);
            Scribe_Values.Look<bool>(ref Speed4WithoutDev, "Speed4WithoutDev", false);
            Scribe_Values.Look<bool>(ref SuppressCombatSlowdown, "SuppressCombatSlowdown", false);

            Scribe_Values.Look<float>(ref BlightScale, "BlightScale", 1);
            Scribe_Values.Look<int>(ref BlightImageIndex, "BlightImageIndex", 0);

            Scribe_Values.Look<bool>(ref SuppressStrippingCremationCorps, "SuppressStrippingCremationCorps", false);
            Scribe_Values.Look<bool>(ref HideSpots, "HideSpots", false);
            Scribe_Values.Look<bool>(ref SuppressRoofColapse, "SuppressRoofColapse", false);
            Scribe_Values.Look<bool>(ref SuppressRainFire, "SuppressRainFire", false);
            Scribe_Values.Look<bool>(ref CheckLogFileSize, "CheckLogFileSize", false);

            Scribe_Values.Look<bool>(ref ApplyLearnFactorChanges, "ApplyLearnFactorChanges", false);

            
            Scribe_Values.Look<int>(ref LearnFactorPassionNonePercentage, "LearnFactorPassionNonePercentage", 35);
            Scribe_Values.Look<int>(ref LearnFactorPassionMinorPercentage, "LearnFactorPassionMinorPercentage", 100);
            Scribe_Values.Look<int>(ref LearnFactorPassionMajorPercentage, "LearnFactorPassionMajorPercentage", 150);
            
        }
        
        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.ColumnWidth = 250f;
            listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);

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
            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Suppress LockDevMode:");
            listing_Standard.CheckboxLabeled("Suppress LockDevMode", ref LockDevMode, "Lock Dev Mode to its Current Selection.");
            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Time Speed:");
            listing_Standard.CheckboxLabeled("Allow Speed4 Without Dev Mode", ref Speed4WithoutDev, "Allow Speed4 Without Dev Mode needing to be enabled, can be turned on by pressing '4'.");
            listing_Standard.CheckboxLabeled("Suppress Combat Slowdown", ref SuppressCombatSlowdown, "Suppress Limiting Speed in Combat.");

            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Blight:");
            listing_Standard.Label("Blight Scale:  " + BlightScale);
            BlightScale = (float)Math.Round((Double)listing_Standard.Slider(BlightScale, 1, 10), 1);

            String _CurrentBlightImageDescription = string.Empty;
            switch (BlightImageIndex)
            {
                case 0:
                    _CurrentBlightImageDescription = "Default";
                    break;
                case 1:
                    _CurrentBlightImageDescription = "Red";
                    break;
                case 2:
                    _CurrentBlightImageDescription = "Blue";
                    break;
                case 3:
                    _CurrentBlightImageDescription = "Orange";
                    break;
                case 4:
                    _CurrentBlightImageDescription = "Purple";
                    break;
                default:
                    _CurrentBlightImageDescription = "Default";
                    break;
            }


            Rect _BlightSelection = listing_Standard.GetRect(30f);
            Widgets.Label(_BlightSelection.RightHalf(), _CurrentBlightImageDescription);
            if (Widgets.ButtonText(_BlightSelection.LeftHalf(), "Select Blight:"))
            {
                Find.WindowStack.Add(
                    new FloatMenu(new List<FloatMenuOption> {
                        new FloatMenuOption("Default (Green)", () => BlightImageIndex = 0),
                        new FloatMenuOption("Red", () => BlightImageIndex = 1),
                        new FloatMenuOption("Blue", () => BlightImageIndex = 2),
                        new FloatMenuOption("Orange", () => BlightImageIndex = 3),
                        new FloatMenuOption("Purple", () => BlightImageIndex = 4)
                    }));
            }

            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Suppress Stripping Cremation Corps:");
            listing_Standard.CheckboxLabeled("SuppressStrippingCremationCorps", ref SuppressStrippingCremationCorps, "Stops Gear and Apparel from being removed from a Corps before Cremation, all gear will be lost.");

            listing_Standard.GapLine(12f);
            listing_Standard.Label("* Hide Spots:");
            listing_Standard.CheckboxLabeled("Hide Spots", ref HideSpots, "Stops Marriage, Caravan Packing and Party Spots from being show all the time. They will still show when Architect menu is open or one of the spots is the first thing selected. (Only checks when menu is changed)");

            listing_Standard.GapLine(12f);
            listing_Standard.Label("*Suppress Roof Colapse");
            listing_Standard.CheckboxLabeled("Suppress Roof Colapse", ref SuppressRoofColapse, "Stops the Roof from Collapsing when support Pillars are removed.");

            listing_Standard.GapLine(12f);
            listing_Standard.Label("*Suppress Rain Fire");
            listing_Standard.CheckboxLabeled("Suppress Rain Fire", ref SuppressRainFire, "Stops Fires from Causing Rain, Warning can burn the whole map and large fires can cause lag when they are burning.");

            listing_Standard.GapLine(12f);
            listing_Standard.Label("Check Log File Size - Currently " + LogFileSizeThresholdMB + " MB.");
            listing_Standard.CheckboxLabeled("Check Log File Size", ref CheckLogFileSize, "Checks once every Ingame Day the Size of the Log File, raises an Alert if the Size is > " + LogFileSizeThresholdMB.ToString() + " MB.");
            listing_Standard.IntAdjuster(ref LogFileSizeThresholdMB, 10, 10);

            listing_Standard.GapLine(12f);

            listing_Standard.Label("Learning Speed Percentages:");
            
            listing_Standard.CheckboxLabeled("*Learning Percentages", ref ApplyLearnFactorChanges, "Must be enabled to apply the following settings.");


            DrawPassionPercentage(listing_Standard, "No Passion: ", ref LearnFactorPassionNonePercentage, ref _Buffer_LearnFactorPassionNone, 35);
            DrawPassionPercentage(listing_Standard, "Minor Pass: ", ref LearnFactorPassionMinorPercentage, ref _Buffer_LearnFactorPassionMinor, 100);
            DrawPassionPercentage(listing_Standard, "Major Pass: ", ref LearnFactorPassionMajorPercentage, ref _Buffer_LearnFactorPassionMajor, 150);

            listing_Standard.GapLine(12f);
            listing_Standard.End();
        }

        private void DrawPassionPercentage(Listing_Standard parentListing, string description, ref int passionPercentage, ref string buffer, int defaultValue)
        {
            Rect _Rect = parentListing.GetRect(30f);
            buffer = null;

            Listing_Standard _Listing_Text = new Listing_Standard();
            _Listing_Text.Begin(_Rect.LeftPartPixels(175));
            _Listing_Text.TextFieldNumericLabeled<int>(description, ref passionPercentage, ref buffer);
            _Listing_Text.End();

            Listing_Standard _Listing_Button = new Listing_Standard();
            _Listing_Button.Begin(_Rect.RightPartPixels(75));
            _Listing_Button.IntSetter(ref passionPercentage, defaultValue, "Default");
            _Listing_Button.End();
        }
        
    }//Class


} //NameSpace
