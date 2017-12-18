using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{

    //[HarmonyPatch(typeof(LetterStack))]
    //[HarmonyPatch("ReceiveLetter")]
    //[HarmonyPatch(new Type[] { typeof(Letter), typeof(string) })]
    static class PatchLetterStack
    {
        
        static public void ApplyPatches(HarmonyInstance harmony)
        {
            Log.Message("PatchLetterStack.ApplyPatches() Starting");

            //Get the Method
            MethodInfo LetterStack_ReceiveLetter = typeof(LetterStack).GetMethod("ReceiveLetter", new Type[] { typeof(Letter), typeof(string) });
            Patch.LogNULL(LetterStack_ReceiveLetter, "LetterStack_ReceiveLetter", true);

            //Get the Prefix
            var _LetterStack_ReceiveLetterPrefix = typeof(PatchLetterStack).GetMethod("ReceiveLetterPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_LetterStack_ReceiveLetterPrefix, "_LetterStack_ReceiveLetterPrefix", true);
            
            //Apply the Prefix Patch
            harmony.Patch(LetterStack_ReceiveLetter, new HarmonyMethod(_LetterStack_ReceiveLetterPrefix), null);

            Log.Message("PatchLetterStack.ApplyPatches() Completed");
        }

        public static bool ReceiveLetterPrefix(ref Letter let)
        {
            //Log.Message("Big Threat");
            Log.Message("Letter DefName: '" + let.def.defName + "' Label: '" + let.label + "'");

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

            if (let.def == LetterDefOf.ItemStashFeeDemand & !Mod_EnhancedOptions.Settings.ShowLettersItemStashFeeDemand)
            {
                return false;
            }

            if (Mod_EnhancedOptions.Settings.LetterNamesToSuppressEnabled)
            {
                List<String> _String = Mod_EnhancedOptions.Settings.LetterNamesToSuppress.Split(',').ToList();
                Log.Message(_String.Count().ToString());
                if (_String.Contains(let.label))
                {
                    Log.Message("Matched with LetterNamesToSuppress");
                    return false;
                }
            }

            // Allow any other types of Letters
            return true;
        }
    }
}
