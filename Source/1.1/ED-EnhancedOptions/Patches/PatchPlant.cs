using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchPlant : Patch
    {
    
        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional Resting Property
            PropertyInfo _RimWorld_Plant_Resting = typeof(RimWorld.Plant).GetProperty("Resting", BindingFlags.NonPublic | BindingFlags.Instance);
            Patcher.LogNULL(_RimWorld_Plant_Resting, "RimWorld_Plant_Resting");

            //Get the Resting Property Getter Method
            MethodInfo _RimWorld_Plant_Resting_Getter = _RimWorld_Plant_Resting.GetGetMethod(true);
            Patcher.LogNULL(_RimWorld_Plant_Resting_Getter, "RimWorld_Plant_Resting_Getter");

            //Get the Prefix Patch
            MethodInfo _RestingGetterPrefix = typeof(PatchPlant).GetMethod("RestingGetterPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_RestingGetterPrefix, "_RestingGetterPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_Plant_Resting_Getter, new HarmonyMethod(_RestingGetterPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "Plant24HEnabled";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.Plant24HEnabled;
        }

        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean RestingGetterPrefix(ref bool __result)
        {

            //This is the result that will be used, note that it was passed as a ref.
            __result = false;

            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }

    }
}
