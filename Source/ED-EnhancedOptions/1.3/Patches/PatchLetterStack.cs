using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{

    class PatchLetterStack : Patch
    {


        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Method
            MethodInfo _Verse_LetterStack_ReceiveLetter = typeof(LetterStack).GetMethod("ReceiveLetter", new Type[] { typeof(Letter), typeof(string) });
            Patcher.LogNULL(_Verse_LetterStack_ReceiveLetter, "_Verse_LetterStack_ReceiveLetter");

            //Get the Prefix
            MethodInfo _ReceiveLetterPrefix = typeof(PatchLetterStack).GetMethod("ReceiveLetterPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_ReceiveLetterPrefix, "_ReceiveLetterPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_Verse_LetterStack_ReceiveLetter, new HarmonyMethod(_ReceiveLetterPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchLetterStack";
        }

        protected override bool ShouldPatchApply()
        {
            //Apply the Letter Patch, setting checking is done inside the method so this is always applied.
            return true;
        }

        public static bool ReceiveLetterPrefix(ref Letter let)
        {
            if (Mod_EnhancedOptions.Settings.DebugLogLetters)
            {
                Log.Message("Letter DefName: '" + let.def.defName + "' Label: '" + let.Label + "'");
            }

            if (let.def == LetterDefOf.ThreatBig & !Mod_EnhancedOptions.Settings.ShowLettersThreatBig)
            {
                return false;
            }

            if (let.def == LetterDefOf.ThreatSmall & !Mod_EnhancedOptions.Settings.ShowLettersThreatSmall)
            {
                return false;
            }

            if (let.def == LetterDefOf.NegativeEvent & !Mod_EnhancedOptions.Settings.ShowLettersNegativeEvent)
            {
                return false;
            }

            if (let.def == LetterDefOf.NeutralEvent & !Mod_EnhancedOptions.Settings.ShowLettersNeutralEvent)
            {
                return false;
            }

            if (let.def == LetterDefOf.PositiveEvent & !Mod_EnhancedOptions.Settings.ShowLettersPositiveEvent)
            {
                return false;
            }

            //if (let.def == LetterDefOf.free & !Mod_EnhancedOptions.Settings.ShowLettersItemStashFeeDemand)
            //{
            //    return false;
            //}

            if (Mod_EnhancedOptions.Settings.LetterNamesToSuppressEnabled)
            {
                List<String> _String = Mod_EnhancedOptions.Settings.LetterNamesToSuppress.Split(',').ToList();
                if (_String.Contains(let.Label))
                {
                    if (Mod_EnhancedOptions.Settings.DebugLogLetters)
                    {
                        Log.Message("Matched with LetterNamesToSuppress");
                    }
                    return false;
                }
            }

            // Allow any other types of Letters
            return true;
        }
    }
}
