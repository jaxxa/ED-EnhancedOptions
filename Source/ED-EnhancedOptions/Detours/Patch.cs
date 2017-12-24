using EnhancedDevelopment.EnhancedOptions.Detours;
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
    internal class Patch
    {
        static Patch()
        {
            Log.Message("Patching EnhancedDevelopment.WarningOptions");
            //HarmonyInstance.Create("EnhancedDevelopment.WarningOptions")PatchAll(Assembly.GetExecutingAssembly());
            HarmonyInstance _Harmony = HarmonyInstance.Create("EnhancedDevelopment.WarningOptions");

            //Apply the Letter Patch, setting checking is done inside the method so this is always applied.
            PatchLetterStack.ApplyPatches(_Harmony);

            //If plant24H is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.Plant24HEnabled)
            {
                PatchPlant.ApplyPatches(_Harmony);
                PatchCompSchedule.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying PatchPlant as it is desabled in settings.");
            }

            //If SafeTrap is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.SafeTrapEnabled)
            {
                PatchBuildingTrap.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying PatchBuildingTrap as it is desabled in settings.");
            }

            //If TurretControl is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.TurretControlEnabled)
            {
                PatchBuildingTurretGun.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying PatchBuildingTurretGun as it is desabled in settings.");
            }


            PatchPowerNetGraphics.ApplyPatches(_Harmony);

            Log.Message("Patching EnhancedDevelopment.WarningOptions Complete");
        }

        /// <summary>
        /// Debug Logging Helper
        /// </summary>
        /// <param name="objectToTest"></param>
        /// <param name="name"></param>
        /// <param name="logSucess"></param>
        public static void LogNULL(object objectToTest, String name, bool logSucess = false)
        {
            if (objectToTest == null)
            {
                Log.Error(name + " Is NULL.");
            }
            else
            {
                if (logSucess)
                {
                    Log.Message(name + " Is Not NULL.");
                }
            }
        }

    }

}
