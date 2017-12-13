using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Example.Detours
{
    [StaticConstructorOnStartup]
    internal class Main
    {
        static Main()
        {
            Log.Message("PAtching");
            HarmonyInstance.Create("EnhancedDevelopment.WarningOptions").PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("PAtching");
        }
    }

    [HarmonyPatch(typeof(LetterStack))]
    [HarmonyPatch("ReceiveLetter")]
    [HarmonyPatch(new Type[] { typeof(Letter), typeof(string) })]
    class Patch
    {
        static bool Prefix(ref Letter let)
        {
            ////AllowGood version only
            //if (let. == LetterType.Good)
            //{
            //    return true;
            //}
            return false;
        }
    }

}
