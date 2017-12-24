using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    
    static class PatchPowerNetGraphics
    {
        
        static public void ApplyPatches(HarmonyInstance harmony)
        {
            Log.Message("PatchLetterStack.ApplyPatches() Starting");

            //Get the Method
            MethodInfo _PowerNetGraphics_PrintWirePieceConnecting = typeof(PowerNetGraphics).GetMethod("PrintWirePieceConnecting", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_PowerNetGraphics_PrintWirePieceConnecting, "_PowerNetGraphics_PrintWirePieceConnecting", true);

            //Get the Prefix
            MethodInfo _PrintWirePieceConnectingPrefixPrefix = typeof(PatchPowerNetGraphics).GetMethod("PrintWirePieceConnectingPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_PrintWirePieceConnectingPrefixPrefix, "_PrintWirePieceConnectingPrefixPrefix", true);
            
            //Apply the Prefix Patch
            harmony.Patch(_PowerNetGraphics_PrintWirePieceConnecting, new HarmonyMethod(_PrintWirePieceConnectingPrefixPrefix), null);

            Log.Message("PatchLetterStack.ApplyPatches() Completed");
        }

        public static bool PrintWirePieceConnectingPrefix(bool forPowerOverlay)
        {
            //When showing forPowerOverlay return true to allow the base method to run, drawing the cables.
            //Else stop them from being drawn.
            return forPowerOverlay;
        }
    }
}
