using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchPreventGreatMemoryTrait : Patch
    {
        protected override void ApplyPatch(Harmony harmony = null)
        {
            FieldInfo _commonalityFieldInfo = typeof(TraitDef).GetField("commonality", BindingFlags.NonPublic | BindingFlags.Instance);
            Patcher.LogNULL(_commonalityFieldInfo, "_commonalityFieldInfo");
            _commonalityFieldInfo.SetValue(TraitDefOf.GreatMemory, 0.0f);
        }

        protected override string PatchDescription()
        {
            return "PatchPreventGreatMemoryTrait";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.PreventSkillDecay;
        }
    }
}
