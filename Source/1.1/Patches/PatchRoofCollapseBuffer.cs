using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchRoofCollapseBuffer : Patch
    {

        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional CheckSpring Method
            MethodInfo _Verse_RoofCollapseBuffer_MarkToCollapse = typeof(RoofCollapseBuffer).GetMethod("MarkToCollapse", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_Verse_RoofCollapseBuffer_MarkToCollapse, "_Verse_RoofCollapseBuffer_MarkToCollapse");

            //Get the Prefix Patch
            MethodInfo _MarkToCollapsePrefix = typeof(PatchRoofCollapseBuffer).GetMethod("Supress_MarkToCollapse", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_MarkToCollapsePrefix, "_MarkToCollapsePrefix");

            //Apply the Prefix Patch
            harmony.Patch(_Verse_RoofCollapseBuffer_MarkToCollapse, new HarmonyMethod(_MarkToCollapsePrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchRoofCollapseBuffer";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.SuppressRoofColapse;
        }


        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean Supress_MarkToCollapse()
        {
            //Retuen False so the origional method is not executed.
            return false;
        }
    }
}
