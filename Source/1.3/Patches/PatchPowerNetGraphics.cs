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
    
    class PatchPowerNetGraphics : Patch
    {
        
        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Method
            MethodInfo _PowerNetGraphics_PrintWirePieceConnecting = typeof(PowerNetGraphics).GetMethod("PrintWirePieceConnecting", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_PowerNetGraphics_PrintWirePieceConnecting, "_PowerNetGraphics_PrintWirePieceConnecting");

            //Get the Prefix
            MethodInfo _PrintWirePieceConnectingPrefixPrefix = typeof(PatchPowerNetGraphics).GetMethod("PrintWirePieceConnectingPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_PrintWirePieceConnectingPrefixPrefix, "_PrintWirePieceConnectingPrefixPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_PowerNetGraphics_PrintWirePieceConnecting, new HarmonyMethod(_PrintWirePieceConnectingPrefixPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchPowerNetGraphics";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.HidePowerConnections;
        }
        
        public static bool PrintWirePieceConnectingPrefix(bool forPowerOverlay)
        {
            //When showing forPowerOverlay return true to allow the base method to run, drawing the cables.
            //Else stop them from being drawn.
            return forPowerOverlay;
        }
    }
}
