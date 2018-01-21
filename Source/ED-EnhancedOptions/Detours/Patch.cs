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
            }
            else
            {
                Log.Message("Skipping Applying PatchPlant as it is Disabled in settings.");
            }

            //If PlantLights24HEnabled is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.PlantLights24HEnabled)
            {
                PatchCompSchedule.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying PatchCompSchedule (SunLamps) as it is Disabled in settings.");
            }

            //If SafeTrap is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.SafeTrapEnabled)
            {
                PatchBuildingTrap.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying PatchBuildingTrap as it is Disabled in settings.");
            }

            //If TurretControl is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.TurretControlEnabled)
            {
                PatchBuildingTurretGun.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying PatchBuildingTurretGun as it is Disabled in settings.");
            }

            //If HidePowerConnections is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.HidePowerConnections)
            {
                PatchPowerNetGraphics.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying HidePowerConnections as it is Disabled in settings.");
            }

            //If PatchCompBreakdownable is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.HidePowerConnections)
            {
                PatchCompBreakdownable.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying PatchCompBreakdownable as it is Disabled in settings.");
            }

            //If LockDevMode is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.LockDevMode)
            {
                PatchPerfs.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying PatchPerfs as LockDevMode is Disabled in settings.");
            }

            //If Speed4WithoutDev is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.Speed4WithoutDev)
            {
                PatchTimeControls.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying Speed4WithoutDev as it is Disabled in settings.");
            }

            if (Mod_EnhancedOptions.Settings.SuppressCombatSlowdown)
            {
                PatchTimeSlower.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying SuppressCombatSlowdown as it is Disabled in settings.");
            }

            if (Mod_EnhancedOptions.Settings.SuppressStrippingCremationCorps)
            {
                PatchToils_Recipe.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying SuppressStrippingCremationCorps as it is Disabled in settings.");
            }
                        
            PatchMainTabsRoot.ApplyPatches(_Harmony);

            BlightGraphics.UpdateBlightGraphics();

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
