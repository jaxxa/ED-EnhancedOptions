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

    class PatchMainTabsRoot : Patch
    {

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.HideSpots;
        }

        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional Method
            MethodInfo _Building_MainTabsRoot_ToggleTab = typeof(MainTabsRoot).GetMethod("ToggleTab", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_Building_MainTabsRoot_ToggleTab, "_Building_MainTabsRoot_ToggleTab");
            
            //Get the Prefix Patch
            MethodInfo _SetDrawStatusPostfix = typeof(PatchMainTabsRoot).GetMethod("SetDrawStatusPostfix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_SetDrawStatusPostfix, "_SetDrawStatusPostfix");

            //Apply the Prefix Patch
            harmony.Patch(_Building_MainTabsRoot_ToggleTab, null, new HarmonyMethod(_SetDrawStatusPostfix));
        }

        protected override string PatchDescription()
        {
            return "PatchMainTabsRoot - HideSpots";
        }

        // postfix
        public static void SetDrawStatusPostfix(MainTabsRoot __instance)
        {
            if (PatchMainTabsRoot.ShouldShowSpots(__instance))
            {
                ThingDefOf.MarriageSpot.drawerType = DrawerType.MapMeshAndRealTime;
                ThingDefOf.CaravanPackingSpot.drawerType = DrawerType.MapMeshAndRealTime;
                ThingDefOf.PartySpot.drawerType = DrawerType.MapMeshAndRealTime;
                PatchMainTabsRoot.MarkMapMeshAsDirty();
            }
            else
            {
                ThingDefOf.MarriageSpot.drawerType = DrawerType.None;
                ThingDefOf.CaravanPackingSpot.drawerType = DrawerType.None;
                ThingDefOf.PartySpot.drawerType = DrawerType.None;
                PatchMainTabsRoot.MarkMapMeshAsDirty();
            }
        }

        public static bool ShouldShowSpots(MainTabsRoot __instance)
        {
            if (__instance.OpenTab == null)
            {
                return false;
            }

            if (string.Equals(__instance.OpenTab.defName, "Architect"))
            {
                return true;
            }

            if (string.Equals(__instance.OpenTab.defName, "Inspect"))
            {
                Thing _FirstThing = Find.Selector.FirstSelectedObject as Thing;

                if (_FirstThing != null)
                {
                    if (String.Equals(_FirstThing.def.defName, "MarriageSpot") ||
                        String.Equals(_FirstThing.def.defName, "PartySpot") ||
                        String.Equals(_FirstThing.def.defName, "CaravanPackingSpot"))
                    {
                        return true;
                    }

                }
            }

            return false;
        }

        public static void MarkMapMeshAsDirty()
        {

            

            //Update the map mesh for the things that have changed
            Find.CurrentMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.MarriageSpot).ToList().ForEach(x =>
            {
                Find.CurrentMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
            });

            Find.CurrentMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.CaravanPackingSpot).ToList().ForEach(x =>
            {
                Find.CurrentMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
            });

            Find.CurrentMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.PartySpot).ToList().ForEach(x =>
            {
                Find.CurrentMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
            });
        }
    }
}
