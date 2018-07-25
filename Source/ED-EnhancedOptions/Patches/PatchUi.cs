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


            //       MethodInfo _Verse_Ui_MapToUIPosition_Vector = typeof(Verse.UI).GetMethod("MapToUIPosition", new Type[] { typeof(Vector2) });
            MethodInfo _PawnUIOverlay_DrawPawnGUIOverlay = typeof(PawnUIOverlay).GetMethod("DrawPawnGUIOverlay", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_PawnUIOverlay_DrawPawnGUIOverlay, "_PawnUIOverlay_DrawPawnGUIOverlay", true);
            
            MethodInfo _Thing_DrawPawnGUIOverlay = typeof(Thing).GetMethod("DrawGUIOverlay", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_Thing_DrawPawnGUIOverlay, "_Thing_DrawPawnGUIOverlay", true);



            //Get the Prefix Patch
            MethodInfo _PawnUIOverlay_DrawPawnGUIOverlay_PRefix = typeof(PatchUi).GetMethod("_PawnUIOverlay_DrawPawnGUIOverlay_PRefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_PawnUIOverlay_DrawPawnGUIOverlay_PRefix, "_PawnUIOverlay_DrawPawnGUIOverlay_PRefix");

            //Get the postfix
            MethodInfo _Verse_Ui_MapToUiPosition_VectorPostfix = typeof(PatchUi).GetMethod("_Verse_Ui_MapToUiPosition_VectorPostfix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_Verse_Ui_MapToUiPosition_VectorPostfix, "_Verse_Ui_MapToUiPosition_VectorPostfix");

            //Apply the Prefix Patch
            harmony.Patch(_PawnUIOverlay_DrawPawnGUIOverlay, new HarmonyMethod(_PawnUIOverlay_DrawPawnGUIOverlay_PRefix), new HarmonyMethod(_Verse_Ui_MapToUiPosition_VectorPostfix), null);
            harmony.Patch(_Thing_DrawPawnGUIOverlay, new HarmonyMethod(_PawnUIOverlay_DrawPawnGUIOverlay_PRefix), new HarmonyMethod(_Verse_Ui_MapToUiPosition_VectorPostfix), null);


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





        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean _PawnUIOverlay_DrawPawnGUIOverlay_PRefix()
        {

            //Verse.UI.screenWidth = 1024;
            //Verse.UI.screenHeight = 768;


            float uIScale = 1.0f;
            float uIScale2 = 1.0f;
            //UI.screenWidth = Mathf.RoundToInt((float)Screen.width / uIScale);
            //UI.screenHeight = Mathf.RoundToInt((float)Screen.height / uIScale);
            GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3(uIScale, uIScale2, 1f));


            //Retuen False so the origional method is not executed, overriting the false result.
            return true;
        }


        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static void _Verse_Ui_MapToUiPosition_VectorPostfix()
        {

            //Verse.UI.screenWidth = 1024;
            //Verse.UI.screenHeight = 768;


            float uIScale = 0.5f;
            float uIScale2 = 0.5f;
            //UI.screenWidth = Mathf.RoundToInt((float)Screen.width / uIScale);
            //UI.screenHeight = Mathf.RoundToInt((float)Screen.height / uIScale);
            GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3(uIScale, uIScale2, 1f));


            //Retuen False so the origional method is not executed, overriting the false result.
            //return true;
        }


        





        public static Boolean MapToUIPositionPrefix(Vector3 v, ref Vector2 __result)
        {
            //GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3(1.0f, 1.0f, 1f));

            Log.Message("Temp");
           // Vector3 vector = Find.Camera.WorldToScreenPoint(v) / Prefs.UIScale;
            //Vector3 vector = Find.Camera.WorldToScreenPoint(v);
            //__result = new Vector2(vector.x, (float)UI.screenHeight - vector.y);



            //float uIScale = 0.5f;
            //float uIScale2 = 0.5f;
            ////UI.screenWidth = Mathf.RoundToInt((float)Screen.width / uIScale);
            ////UI.screenHeight = Mathf.RoundToInt((float)Screen.height / uIScale);
            //GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3(uIScale, uIScale2, 1f));

            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }

    }
}
