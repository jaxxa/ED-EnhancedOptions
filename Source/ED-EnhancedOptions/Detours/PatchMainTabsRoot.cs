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

    //[HarmonyPatch(typeof(Plant))]
    //[HarmonyPatch("Resting_Getter")]
    static class PatchMainTabsRoot
    {

        static public void ApplyPatches(HarmonyInstance harmony)
        {

            Log.Message("PatchBuilding_MarriageSpot.ApplyPatches() Starting");

            //Get the Origional Method
            MethodInfo _Building_MainTabsRoot_ToggleTab = typeof(MainTabsRoot).GetMethod("ToggleTab", BindingFlags.Public | BindingFlags.Instance);
            Patch.LogNULL(_Building_MainTabsRoot_ToggleTab, "_Building_MainTabsRoot_ToggleTab", true);


            //Get the Prefix Patch
            MethodInfo _SetDrawStatusPrefix = typeof(PatchMainTabsRoot).GetMethod("SetDrawStatusPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_SetDrawStatusPrefix, "_SetDrawStatusPrefix", true);

            //Apply the Prefix Patch
            harmony.Patch(_Building_MainTabsRoot_ToggleTab, null, new HarmonyMethod(_SetDrawStatusPrefix));

            Log.Message("PatchBuilding_MarriageSpot.ApplyPatches() Completed");
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
                return true;
            }

            //if (string.Equals(__instance.OpenTab.label))
            //{

            //}

                return false;
        }

        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static void SetDrawStatusPrefix(MainTabsRoot __instance)
        {
            Log.Message("Tab Change");


            //Architect
            //Inspect
            if (PatchMainTabsRoot.ShouldShowSpots(__instance))
            {
                Log.Message("Tab Found");
                Log.Message(__instance.OpenTab.defName + " " + __instance.OpenTab.label + " " + __instance.OpenTab.TabWindow.ToString());

                ThingDefOf.MarriageSpot.drawerType = DrawerType.MapMeshAndRealTime;
                ThingDefOf.CaravanPackingSpot.drawerType = DrawerType.MapMeshAndRealTime;
                ThingDefOf.PartySpot.drawerType = DrawerType.MapMeshAndRealTime;

                Find.VisibleMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.MarriageSpot).ToList().ForEach(x =>
                {

                    Find.VisibleMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
                });

                Find.VisibleMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.CaravanPackingSpot).ToList().ForEach(x =>
                {

                    Find.VisibleMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
                });

                Find.VisibleMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.PartySpot).ToList().ForEach(x =>
                {

                    Find.VisibleMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
                });
            }
            else
            {
                
                Log.Message("Null open Tab");
                ThingDefOf.MarriageSpot.drawerType = DrawerType.None;
                ThingDefOf.CaravanPackingSpot.drawerType = DrawerType.None;
                ThingDefOf.PartySpot.drawerType = DrawerType.None;

                Find.VisibleMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.MarriageSpot).ToList().ForEach(x =>
                {

                    Find.VisibleMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
                });

                Find.VisibleMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.CaravanPackingSpot).ToList().ForEach(x =>
                {

                    Find.VisibleMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
                });

                Find.VisibleMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.PartySpot).ToList().ForEach(x =>
                {

                    Find.VisibleMap.mapDrawer.MapMeshDirty(x.Position, MapMeshFlag.Things);
                });

            }
        }
    }
}
