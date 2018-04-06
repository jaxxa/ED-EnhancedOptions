using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchProjectile : Patch
    {
    
        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {
            //Get the Origional Resting Property
            //ConstructorInfo[] _RimWorld_Plant_Resting = typeof(Verse.Projectile).GetConstructors();
            //Log.Message("Count:" + _RimWorld_Plant_Resting.Count().ToString());
            // ConstructorInfo _Temp = _RimWorld_Plant_Resting.FirstOrDefault();
            // .GetProperty("Resting", BindingFlags.NonPublic | BindingFlags.Instance);
            // Patcher.LogNULL(_RimWorld_Plant_Resting, "RimWorld_Plant_Resting");




            //Get the Resting Property Getter Method
            //MethodInfo _RimWorld_Plant_Resting_Getter = _RimWorld_Plant_Resting.GetGetMethod(true);
            //Patcher.LogNULL(_RimWorld_Plant_Resting_Getter, "RimWorld_Plant_Resting_Getter");


            Type[] _TypeArray = new Type[] { typeof (Verse.Thing), typeof(Vector3), typeof(LocalTargetInfo), typeof(Thing), typeof(Thing) };
             //Get the Method
            //MethodInfo _ProjectileLaunch = typeof(Verse.Projectile).GetMethod("Launch", BindingFlags.Public | BindingFlags.Instance);
            MethodInfo _ProjectileLaunch = typeof(Verse.Projectile).GetMethod("Launch", _TypeArray);
            Patcher.LogNULL(_ProjectileLaunch, "_ProjectileLaunch");


            //Get the Prefix Patch
            MethodInfo _RestingGetterPrefix = typeof(PatchProjectile).GetMethod("RestingGetterPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_RestingGetterPrefix, "_PatchProjectile.Method");

            //Apply the Prefix Patch
            harmony.Patch(_ProjectileLaunch, new HarmonyMethod(_RestingGetterPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchProjectile";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
            return Mod_EnhancedOptions.Settings.Plant24HEnabled;
        }

        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean RestingGetterPrefix()
        {
            Log.Message("Created Projectile");
            //This is the result that will be used, note that it was passed as a ref.
          //  __result = false;

            //Retuen False so the origional method is not executed, overriting the false result.
            return true;
        }

    }
}
