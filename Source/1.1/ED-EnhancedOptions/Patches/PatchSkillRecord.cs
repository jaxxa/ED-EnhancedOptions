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
    class PatchSkillRecord : Patch
    {

        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional LearnRateFactor Method
            MethodInfo _RimWorld_SkillRecord_LearnRateFactor = typeof(RimWorld.SkillRecord).GetMethod("LearnRateFactor", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_RimWorld_SkillRecord_LearnRateFactor, "_RimWorld_SkillRecord_LearnRateFactor");
                        

            //Get the LearnRateFactor Prefix Patch
            MethodInfo _LearnRateFactorPrefix = typeof(PatchSkillRecord).GetMethod("LearnRateFactorPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_LearnRateFactorPrefix, "_LearnRateFactorPrefix");

            //Apply the LearnRateFactor Prefix Patch
            harmony.Patch(_RimWorld_SkillRecord_LearnRateFactor, new HarmonyMethod(_LearnRateFactorPrefix), null);
                       

            if(Mod_EnhancedOptions.Settings.PreventSkillDecay)
            {

                MethodInfo _RimWorld_SkillRecord_Interval = typeof(RimWorld.SkillRecord).GetMethod("Interval", BindingFlags.Public | BindingFlags.Instance);
                Patcher.LogNULL(_RimWorld_SkillRecord_Interval, "_RimWorld_SkillRecord_Interval");

                MethodInfo _RimWorld_SkillRecord_Interval_Prefix = typeof(PatchSkillRecord).GetMethod("IntervalPrefix", BindingFlags.Public | BindingFlags.Static);
                Patcher.LogNULL(_RimWorld_SkillRecord_Interval_Prefix, "_RimWorld_SkillRecord_Interval_Prefix");

                harmony.Patch(_RimWorld_SkillRecord_Interval, new HarmonyMethod(_RimWorld_SkillRecord_Interval_Prefix), null);

            }
            else
            {
                Log.Message("Skipping PreventSkillDecay");
            }
        }

        protected override string PatchDescription()
        {
            return "PatchSkillRecord";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.ApplyLearnChanges;
        }
        
        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean LearnRateFactorPrefix(ref float __result, ref SkillRecord __instance, bool direct = false)
        {

            //This is the result that will be used, note that it was passed as a ref.
            
            if (DebugSettings.fastLearning)
            {
                __result = 200f;
                return false;
            }
            float num;
            switch (__instance.passion)
            {
                case Passion.None:
                    num = (Mod_EnhancedOptions.Settings.LearnFactorPassionNonePercentage / 100f);
                    break;
                case Passion.Minor:
                    num = (Mod_EnhancedOptions.Settings.LearnFactorPassionMinorPercentage / 100f);
                    break;
                case Passion.Major:
                    num = (Mod_EnhancedOptions.Settings.LearnFactorPassionMajorPercentage / 100f);
                    break;
                default:
                    throw new NotImplementedException("Passion level " + __instance.passion);
            }
            if (!direct)
            {
                //Private Field acessed through Reflection

                // num *= __instance.pawn.GetStatValue(StatDefOf.GlobalLearningFactor, true);
                FieldInfo _PawnProperty = typeof(SkillRecord).GetField("pawn", BindingFlags.NonPublic | BindingFlags.Instance);
                //Patcher.LogNULL(_PawnProperty, "_PawnProperty", true);
                Pawn _Pawn =  _PawnProperty.GetValue(__instance) as Pawn;
                //Patcher.LogNULL(_Pawn, "_Pawn", true);
                num *= _Pawn.GetStatValue(StatDefOf.GlobalLearningFactor, true);

                if (__instance.xpSinceMidnight > Mod_EnhancedOptions.Settings.DailyLearningSaturationAmmount)
                {
                    num *= 0.2f;
                }
            }
            
            __result = num;
            
            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }

        public static Boolean IntervalPrefix()
        {
            return false;
        }
    }
}
