using EnhancedDevelopment.EnhancedOptions.Detours;
using HarmonyLib;
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

            Log.Message(_LogLocation + "Starting Patching.");           

            //Create List of Patches
            List<Patch> _Patches = new List<Patch>();
            _Patches.Add(new PatchDebug());
            //_Patches.Add(new PatchBlightGraphics());
            _Patches.Add(new PatchBuildingTrap());
            _Patches.Add(new PatchBuildingTurretGun());
            _Patches.Add(new PatchCompBreakdownable());
            _Patches.Add(new PatchCompSchedule());
            _Patches.Add(new PatchLetterStack());
            _Patches.Add(new PatchMainTabsRoot());
            //_Patches.Add(new PatchPerfs());
            _Patches.Add(new PatchPlant());
            _Patches.Add(new PatchPowerNetGraphics());
            _Patches.Add(new PatchTimeControls());
            _Patches.Add(new PatchTimeSlower());
            //_Patches.Add(new PatchToils_Recipe());
            _Patches.Add(new PatchRoofCollapseBuffer());
            _Patches.Add(new PatchFireWatcher());
            _Patches.Add(new PatchSkillRecord());
            _Patches.Add(new PatchSkillUI());
            //_Patches.Add(new PatchBuilding_TurretGunCE());
            _Patches.Add(new PatchPreventGreatMemoryTrait());

            //Create Harmony Instance

            Harmony _Harmony = new HarmonyLib.Harmony("EnhancedDevelopment.WarningOptions");

            //Iterate Patches
            _Patches.ForEach(p => p.ApplyPatchIfRequired(_Harmony));

            Log.Message(_LogLocation + "Completed Patching.");
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

        /// <summary>
        /// Logs Messages only if in Verbose Mode.
        /// </summary>
        /// <param name="messageToLog">The Message to Log if in Verbose Mode.</param>
        public static void LogMessageIfVerbose(string messageToLog)
        {
            if (Prefs.LogVerbose)
            {
                Log.Message(messageToLog);
            }
        }

    }

}
