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
            MethodInfo _SetDrawStatusPostfix = typeof(PatchMainTabsRoot).GetMethod("SetDrawStatusPostfix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_SetDrawStatusPostfix, "_SetDrawStatusPostfix", true);

            //Apply the Prefix Patch
            harmony.Patch(_Building_MainTabsRoot_ToggleTab, null, new HarmonyMethod(_SetDrawStatusPostfix));

            Log.Message("PatchBuilding_MarriageSpot.ApplyPatches() Completed");
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
                    Log.Message(_FirstThing.def.defName);
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
