using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchDebug : Patch
    {

        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional Log Method
            List<MethodInfo> _Debug_Log = typeof(UnityEngine.Debug).GetMethods().ToList().Where(x => x.Name.Equals("Log")).ToList();
            Patcher.LogNULL(_Debug_Log, "_Debug_Log");
            
            //Get the Prefix Patch
            MethodInfo _LogPrefix = typeof(PatchDebug).GetMethod("LogPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_LogPrefix, "_LogPrefix");

            Log.Warning("########## The Mod ED-EnhancedOptions is Supressing Future calls to UnityEngine.Debug.Log, This can be changed in Mod Settings. ##########");
            //Apply the Prefix Patches
            _Debug_Log.ForEach(x => harmony.Patch(x, new HarmonyMethod(_LogPrefix), null));

        }

        protected override string PatchDescription()
        {
            return "Debug Log";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.SupressWritingToLogFile;
        }


        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean LogPrefix()
        {

            //This is the result that will be used, note that it was passed as a ref.
            //__result = false;

            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }
    }
}
