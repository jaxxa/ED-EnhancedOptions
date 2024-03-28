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
    class PatchSkillUI : Patch
    {



        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional CheckSpring Method
            MethodInfo _RimWorld_SkillUI_GetSkillDescription = typeof(RimWorld.SkillUI).GetMethod("GetSkillDescription", BindingFlags.NonPublic | BindingFlags.Static);
            Patcher.LogNULL(_RimWorld_SkillUI_GetSkillDescription, "_RimWorld_SkillUI_GetSkillDescription");
            
            //Get the Prefix Patch
            MethodInfo _GetSkillDescriptionPrefix = typeof(PatchSkillUI).GetMethod("GetSkillDescriptionPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_GetSkillDescriptionPrefix, "_GetSkillDescriptionPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_SkillUI_GetSkillDescription, new HarmonyMethod(_GetSkillDescriptionPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchSkillUI";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.ApplyLearnChanges;
        }
        
        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean GetSkillDescriptionPrefix(ref string __result, SkillRecord sk)
        {

            StringBuilder stringBuilder = new StringBuilder();
            if (sk.TotallyDisabled)
            {
                stringBuilder.Append("DisabledLower".Translate().CapitalizeFirst());
            }
            else
            {
                stringBuilder.AppendLine("Level".Translate() + " " + sk.Level + ": " + sk.LevelDescriptor);
                if (Current.ProgramState == ProgramState.Playing)
                {
                    string text = (sk.Level != 20) ? "ProgressToNextLevel".Translate() : "Experience".Translate();
                    stringBuilder.AppendLine(text + ": " + sk.xpSinceLastLevel.ToString("F0") + " / " + sk.XpRequiredForLevelUp);
                }
                stringBuilder.Append("Passion".Translate() + ": ");
                switch (sk.passion)
                {
                    case Passion.None:
                        stringBuilder.Append("PassionNone".Translate((Mod_EnhancedOptions.Settings.LearnFactorPassionNonePercentage / 100f).ToStringPercent("F0")));
                        break;
                    case Passion.Minor:
                        stringBuilder.Append("PassionMinor".Translate((Mod_EnhancedOptions.Settings.LearnFactorPassionMinorPercentage / 100f).ToStringPercent("F0")));
                        break;
                    case Passion.Major:
                        stringBuilder.Append("PassionMajor".Translate((Mod_EnhancedOptions.Settings.LearnFactorPassionMajorPercentage / 100f).ToStringPercent("F0")));
                        break;
                }
                if (sk.xpSinceMidnight > Mod_EnhancedOptions.Settings.DailyLearningSaturationAmmount)
                {
                    stringBuilder.AppendLine();
                    stringBuilder.Append("LearnedMaxToday".Translate(sk.xpSinceMidnight, Mod_EnhancedOptions.Settings.DailyLearningSaturationAmmount, 0.2f.ToStringPercent("F0")));
                }
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.Append(sk.def.description);
            __result = stringBuilder.ToString();


            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }

    }
}
