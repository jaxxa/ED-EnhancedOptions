using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Patches
{
    class PatchUi : Patch
    {
        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {            //Get the Origional Resting Property
            MethodInfo _Verse_Ui_ApplyUIScale = typeof(Verse.UI).GetMethod("ApplyUIScale", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_Verse_Ui_ApplyUIScale, "_Verse_Ui_ApplyUIScale");
            
            //Get the Prefix Patch
            MethodInfo _UiScalePrefix = typeof(PatchUi).GetMethod("UiScalePrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_UiScalePrefix, "_UiScalePrefix");

            //Apply the Prefix Patch
            harmony.Patch(_Verse_Ui_ApplyUIScale, new HarmonyMethod(_UiScalePrefix), null);
        }

        protected override string PatchDescription()
        {
            return "Patch UI";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
        }



        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean UiScalePrefix()
        {

            //Verse.UI.screenWidth = 1024;
            //Verse.UI.screenHeight = 768;


            float uIScale = 0.5f;
            float uIScale2 = 0.5f;
            //UI.screenWidth = Mathf.RoundToInt((float)Screen.width / uIScale);
            //UI.screenHeight = Mathf.RoundToInt((float)Screen.height / uIScale);
            GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3(uIScale, uIScale2, 1f));


            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }
    }
}
