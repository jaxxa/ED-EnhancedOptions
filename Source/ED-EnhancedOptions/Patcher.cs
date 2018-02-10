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
    internal class Patcher
    {
        static Patcher()
        {
            string _LogLocation = "EnhancedOptions.Patcher.Patcher(): ";

            Log.Message(_LogLocation + "Starting.");

            //Create List of Patches
            List<Patch> _Patches = new List<Patch>();
            _Patches.Add(new PatchBlightGraphics());
            _Patches.Add(new PatchBuildingTrap());
            _Patches.Add(new PatchBuildingTurretGun());
            _Patches.Add(new PatchCompBreakdownable());
            _Patches.Add(new PatchCompSchedule());
            _Patches.Add(new PatchLetterStack());
            _Patches.Add(new PatchMainTabsRoot());
            _Patches.Add(new PatchPerfs());
            _Patches.Add(new PatchPlant());

            //Create Harmony Instance
            HarmonyInstance _Harmony = HarmonyInstance.Create("EnhancedDevelopment.WarningOptions");

            //Iterate Patches
            _Patches.ForEach(p => p.ApplyPatchIfRequired(_Harmony));


            //If HidePowerConnections is enabled then apply the Patch.
            if (Mod_EnhancedOptions.Settings.HidePowerConnections)
            {
                PatchPowerNetGraphics.ApplyPatches(_Harmony);
            }
            else
            {
                Log.Message("Skipping Applying HidePowerConnections as it is Disabled in settings.");
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

            Log.Message(_LogLocation + "Complete.");
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
            else if (logSucess)
            {
                Log.Message(name + " Is Not NULL.");
            }
        }

    }

}
