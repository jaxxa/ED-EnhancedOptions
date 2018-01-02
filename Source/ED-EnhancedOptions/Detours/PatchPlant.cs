using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{

    //[HarmonyPatch(typeof(Plant))]
    //[HarmonyPatch("Resting_Getter")]
    static class PatchPlant
    {

        static public void ApplyPatches(HarmonyInstance harmony)
        {

            Log.Message("PatchPlant.ApplyPatches() Starting");

            //Get the Origional Resting Property
            PropertyInfo _RimWorld_Plant_Resting = typeof(RimWorld.Plant).GetProperty("Resting", BindingFlags.NonPublic | BindingFlags.Instance);
            Patch.LogNULL(_RimWorld_Plant_Resting, "RimWorld_Plant_Resting", true);

            //Get the Resting Property Getter Method
            MethodInfo _RimWorld_Plant_Resting_Getter = _RimWorld_Plant_Resting.GetGetMethod(true);
            Patch.LogNULL(_RimWorld_Plant_Resting_Getter, "RimWorld_Plant_Resting_Getter", true);

            //Get the Prefix Patch
            MethodInfo _RestingGetterPrefix = typeof(PatchPlant).GetMethod("RestingGetterPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_RestingGetterPrefix, "_RestingGetterPrefix", true);

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_Plant_Resting_Getter, new HarmonyMethod(_RestingGetterPrefix), null);

            Log.Message("PatchPlant.ApplyPatches() Completed");
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
