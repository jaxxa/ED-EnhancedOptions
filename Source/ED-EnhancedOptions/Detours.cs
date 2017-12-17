using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions
{
    [StaticConstructorOnStartup]
    internal class Main
    {
        static Main()
        {
            Log.Message("Patching EnhancedDevelopment.WarningOptions");
            //HarmonyInstance.Create("EnhancedDevelopment.WarningOptions")PatchAll(Assembly.GetExecutingAssembly());
            HarmonyInstance _Harmony = HarmonyInstance.Create("EnhancedDevelopment.WarningOptions");
            _Harmony.PatchAll(Assembly.GetExecutingAssembly());
            //_Harmony.
            //_Harmony.Patch(Patch_ReceiveLetter);
            
            Log.Message("Patching EnhancedDevelopment.WarningOptions Complete");
        }
    }

    [HarmonyPatch(typeof(LetterStack))]
    [HarmonyPatch("ReceiveLetter")]
    [HarmonyPatch(new Type[] { typeof(Letter), typeof(string) })]
    class Patch_ReceiveLetter
    {
        static bool Prefix(ref Letter let)
        {
            //Log.Message("Big Threat");
            Log.Message("Letter DefName: '" + let.def.defName + "' Label: '" + let.label  + "'");

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
