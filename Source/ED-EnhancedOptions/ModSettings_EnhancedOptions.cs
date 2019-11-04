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
        string _Buffer_DailyLearningSaturationAmmount;

        //Saved
        public bool ShowLettersThreatBig = true;
        public bool ShowLettersThreatSmall = true;
        public bool ShowLettersNegativeEvent = true;
        public bool ShowLettersNeutralEvent = true;
        public bool ShowLettersPositiveEvent = true;
        public bool ShowLettersItemStashFeeDemand = true;

        public bool LetterNamesToSuppressEnabled = false;
        public string LetterNamesToSuppress = String.Empty;
        public bool DebugLogLetters = false;

        public bool Plant24HEnabled = false;
        public bool PlantLights24HEnabled = false;

        public bool SafeTrapEnabled = false;
        public bool TurretControlEnabled = false;
        public bool HidePowerConnections = false;

        public bool SuppressBreakdown = false;
        //public bool LockDevMode = false;
        public bool Speed4WithoutDev = false;
        public bool SuppressCombatSlowdown = false;
        public bool HideSpots = false;
        public bool SuppressRoofColapse = false;
        public bool SuppressRainFire = false;
        //public bool CheckLogFileSize = false;
        //public int LogFileSizeThresholdMB = 50;
        public bool SupressWritingToLogFile = false;

        /*
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
        */

        //public bool SuppressStrippingCremationCorps = false;

        public bool ApplyLearnChanges = false;
        public int LearnFactorPassionNonePercentage = 35;
        public int LearnFactorPassionMinorPercentage = 100;
        public int LearnFactorPassionMajorPercentage = 150;
        public int DailyLearningSaturationAmmount = 4000;
        public bool PreventSkillDecay = false;



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
            Scribe_Values.Look<bool>(ref DebugLogLetters, "DebugLogLetters", false);
                       

            Scribe_Values.Look<bool>(ref Plant24HEnabled, "Plant24HEnabled", false);
            Scribe_Values.Look<bool>(ref PlantLights24HEnabled, "PlantLights24HEnabled", false);
            Scribe_Values.Look<bool>(ref SafeTrapEnabled, "SafeTrapEnabled", false);
            Scribe_Values.Look<bool>(ref TurretControlEnabled, "TurretControlEnabled", false);
            Scribe_Values.Look<bool>(ref HidePowerConnections, "HidePowerConnections", false);
            Scribe_Values.Look<bool>(ref SuppressBreakdown, "SuppressBreakdown", false);
            //Scribe_Values.Look<bool>(ref LockDevMode, "LockDevMode", false);
            Scribe_Values.Look<bool>(ref Speed4WithoutDev, "Speed4WithoutDev", false);
            Scribe_Values.Look<bool>(ref SuppressCombatSlowdown, "SuppressCombatSlowdown", false);

            //Scribe_Values.Look<float>(ref BlightScale, "BlightScale", 1);
            //Scribe_Values.Look<int>(ref BlightImageIndex, "BlightImageIndex", 0);

            //Scribe_Values.Look<bool>(ref SuppressStrippingCremationCorps, "SuppressStrippingCremationCorps", false);
            Scribe_Values.Look<bool>(ref HideSpots, "HideSpots", false);
            Scribe_Values.Look<bool>(ref SuppressRoofColapse, "SuppressRoofColapse", false);
            Scribe_Values.Look<bool>(ref SuppressRainFire, "SuppressRainFire", false);

            //Scribe_Values.Look<bool>(ref CheckLogFileSize, "CheckLogFileSize", false);
            //Scribe_Values.Look<int>(ref LogFileSizeThresholdMB, "LogFileSizeThresholdMB", 50);
            Scribe_Values.Look<bool>(ref SupressWritingToLogFile, "SupressWritingToLogFile", false);

            Scribe_Values.Look<bool>(ref ApplyLearnChanges, "ApplyLearnFactorChanges", false);           
            Scribe_Values.Look<int>(ref LearnFactorPassionNonePercentage, "LearnFactorPassionNonePercentage", 35);
            Scribe_Values.Look<int>(ref LearnFactorPassionMinorPercentage, "LearnFactorPassionMinorPercentage", 100);
            Scribe_Values.Look<int>(ref LearnFactorPassionMajorPercentage, "LearnFactorPassionMajorPercentage", 150);
            Scribe_Values.Look<int>(ref DailyLearningSaturationAmmount, "DailyLearningSaturationAmmount", 4000);
            Scribe_Values.Look<bool>(ref PreventSkillDecay, "PreventSkillDecay", false);
            



        }
        
        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = 275f;
            _Listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);

            _Listing_Standard.Label("THESE SETTINGS ARE ONLY APPLIED WHEN RIMWORLD IS STARTED. After modifying them please restart Rimworld.");
            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Letter Suppression:");
            _Listing_Standard.Gap(12f);
            _Listing_Standard.CheckboxLabeled("Show ThreatBig", ref ShowLettersThreatBig, "True if you want to See Any ThreatBig Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            _Listing_Standard.CheckboxLabeled("Show ThreatSmall", ref ShowLettersThreatSmall, "True if you want to See Any ThreatSmall Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            _Listing_Standard.CheckboxLabeled("Show NegativeEvent", ref ShowLettersNegativeEvent, "True if you want to See Any NegativeEvent Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            _Listing_Standard.CheckboxLabeled("Show NeutralEvent", ref ShowLettersNeutralEvent, "True if you want to See Any NeutralEvent Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            _Listing_Standard.CheckboxLabeled("Show PositiveEvent", ref ShowLettersPositiveEvent, "True if you want to See Any PositiveEvent Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            _Listing_Standard.CheckboxLabeled("Show ItemStashFeeDemand", ref ShowLettersItemStashFeeDemand, "True if you want to See Any ItemStashFeeDemand Letters, False will Hide them. When a Letter is Shown its Name and Type will be written to the Log.");
            _Listing_Standard.Gap(12f);
            _Listing_Standard.CheckboxLabeled("Letter Names To Suppress Enabled", ref LetterNamesToSuppressEnabled, "True will Hide any Letters thats Name is in the following List, False to Ignore the List. List is Comma Separated. When a Letter is Shown its Name and Type will be written to the Log.");
            LetterNamesToSuppress = _Listing_Standard.TextEntry(LetterNamesToSuppress, 2);

            _Listing_Standard.CheckboxLabeled("Write Debug Log Letters", ref DebugLogLetters, "True if you want to Log the Letters to the Log file, useful for finding the mane so you can suppress it.");
            


            _Listing_Standard.GapLine(12f);

            _Listing_Standard.Label("Plant 24H:");
            _Listing_Standard.CheckboxLabeled("Plant 24H", ref Plant24HEnabled, "Enable to allow Plants to Grow 24H a day.");
            _Listing_Standard.CheckboxLabeled("Plant Lights 24H", ref PlantLights24HEnabled, "Enable to allow SunLamps to Shine 24H a day.");
            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Safe Trap Enabled:");
            _Listing_Standard.CheckboxLabeled("Safe Trap Enabled", ref SafeTrapEnabled, "Prevents Traps from triggering on your Colonists.");
            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Turret Control Enabled:");
            _Listing_Standard.CheckboxLabeled("Turret Control Enabled", ref TurretControlEnabled, "Allows force attack commands to be given to turrets.");
            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Hide Power Connections:");
            _Listing_Standard.CheckboxLabeled("Hide Power Connections", ref HidePowerConnections, "Hides the Small Power Connection Wires, Still show in Power overlay Mode.");
            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Suppress Breakdown:");
            _Listing_Standard.CheckboxLabeled("Suppress Breakdown", ref SuppressBreakdown, "Suppress random Breakdowns, This was hard to test so please let me know if you have any issues.");
            _Listing_Standard.GapLine(12f);
            //_Listing_Standard.Label("Suppress LockDevMode:");
            //_Listing_Standard.CheckboxLabeled("Suppress LockDevMode", ref LockDevMode, "Lock Dev Mode to its Current Selection.");
            //_Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Time Speed:");
            _Listing_Standard.CheckboxLabeled("Allow Speed4 Without Dev Mode", ref Speed4WithoutDev, "Allow Speed4 Without Dev Mode needing to be enabled, can be turned on by pressing '4'.");
            _Listing_Standard.CheckboxLabeled("Suppress Combat Slowdown", ref SuppressCombatSlowdown, "Suppress Limiting Speed in Combat.");
            
            /*
            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Blight:");
            _Listing_Standard.Label("Blight Scale:  " + BlightScale);
            BlightScale = (float)Math.Round((Double)_Listing_Standard.Slider(BlightScale, 1, 10), 1);

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


            Rect _BlightSelection = _Listing_Standard.GetRect(30f);
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
            

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Suppress Stripping Cremation Corps:");
            _Listing_Standard.CheckboxLabeled("SuppressStrippingCremationCorps", ref SuppressStrippingCremationCorps, "Stops Gear and Apparel from being removed from a Corps before Cremation, all gear will be lost.");
            */

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Hide Spots:");
            _Listing_Standard.CheckboxLabeled("Hide Spots", ref HideSpots, "Stops Marriage, Caravan Packing and Party Spots from being show all the time. They will still show when Architect menu is open or one of the spots is the first thing selected. (Only checks when menu is changed)");
            
            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Suppress Roof Colapse");
            _Listing_Standard.CheckboxLabeled("Suppress Roof Colapse", ref SuppressRoofColapse, "Stops the Roof from Collapsing when support Pillars are removed.");
            
            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Suppress Rain Fire");
            _Listing_Standard.CheckboxLabeled("Suppress Rain Fire", ref SuppressRainFire, "Stops Fires from Causing Rain, Warning can burn the whole map and large fires can cause lag when they are burning.");
            
            _Listing_Standard.NewColumn();
            _Listing_Standard.GapLine(12f);
            
            _Listing_Standard.Label("Log File");
            //_Listing_Standard.Label("Current Check Size:" + LogFileSizeThresholdMB + " MB.");
            //_Listing_Standard.CheckboxLabeled("Check Log File Size", ref CheckLogFileSize, "Checks once every Ingame Day the Size of the Log File, raises an Alert if the Size is > " + LogFileSizeThresholdMB.ToString() + " MB.");
            //_Listing_Standard.IntAdjuster(ref LogFileSizeThresholdMB, 10, 10);
            _Listing_Standard.CheckboxLabeled("Supress Writing Log to File-Tooltip", ref SupressWritingToLogFile, "When Checked log messages will no longer be written to disk.If you are using this because your log file is getting massive that indicated errors that you should really fix(or report to mod/ game developers to fix). But if you are not going to do that then you may as well use this so you don’t have to deal with Multi GB Log files cluttering you HDD/ SSD and wearing them out and it might even increase performance ingame for you, but really it would be better if you could go fix the errors.");
            _Listing_Standard.GapLine(12f);
            
            _Listing_Standard.Label("Learning Speed Percentages:");
            
            _Listing_Standard.CheckboxLabeled("Learning Changes", ref ApplyLearnChanges, "Must be enabled to apply the following settings.");
            if (ApplyLearnChanges)
            {

                DrawPassionPercentage(_Listing_Standard, "No Passion%: ", ref LearnFactorPassionNonePercentage, ref _Buffer_LearnFactorPassionNone, 35);
                DrawPassionPercentage(_Listing_Standard, "Minor Pass%: ", ref LearnFactorPassionMinorPercentage, ref _Buffer_LearnFactorPassionMinor, 100);
                DrawPassionPercentage(_Listing_Standard, "Major Pass%: ", ref LearnFactorPassionMajorPercentage, ref _Buffer_LearnFactorPassionMajor, 150);
                DrawPassionPercentage(_Listing_Standard, "Daily Cap: ", ref DailyLearningSaturationAmmount, ref _Buffer_DailyLearningSaturationAmmount, 4000);

                _Listing_Standard.CheckboxLabeled("Stop Decay and GreatMemory Trait", ref PreventSkillDecay, "Stops Skill Decay and GreatMemory Trait.");
            }

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.End();
        }

        private void DrawPassionPercentage(Listing_Standard parentListing, string description, ref int passionPercentage, ref string buffer, int defaultValue)
        {
            Rect _Rect = parentListing.GetRect(30f);
            buffer = null;

            Listing_Standard _Listing_Text = new Listing_Standard();
            _Listing_Text.Begin(_Rect.LeftPartPixels(190));
            _Listing_Text.TextFieldNumericLabeled<int>(description, ref passionPercentage, ref buffer);
            _Listing_Text.End();

            Listing_Standard _Listing_Button = new Listing_Standard();
            _Listing_Button.Begin(_Rect.RightPartPixels(75));
            _Listing_Button.IntSetter(ref passionPercentage, defaultValue, "Default");
            _Listing_Button.End();
        }
        
    }//Class


} //NameSpace
