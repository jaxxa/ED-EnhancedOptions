/*using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;
using Verse.AI;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{

    class PatchToils_Recipe : Patch
    {

        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {
            //Get the Origional Method
            MethodInfo _Toils_Recipe_CalculateIngredients = typeof(Toils_Recipe).GetMethod("CalculateIngredients", BindingFlags.NonPublic | BindingFlags.Static);
            Patcher.LogNULL(_Toils_Recipe_CalculateIngredients, "_Toils_Recipe_CalculateIngredients");

            //Get the Prefix Patch
            MethodInfo _CalculateIngredientsPrefix = typeof(PatchToils_Recipe).GetMethod("CalculateIngredientsPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_CalculateIngredientsPrefix, "_CalculateIngredientsPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_Toils_Recipe_CalculateIngredients, new HarmonyMethod(_CalculateIngredientsPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "SuppressStrippingCremationCorps";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.SuppressStrippingCremationCorps;
        }
        
        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean CalculateIngredientsPrefix(Job job, Pawn actor, ref List<Thing> __result)
        {
            UnfinishedThing unfinishedThing = job.GetTarget(TargetIndex.B).Thing as UnfinishedThing;
            if (unfinishedThing != null)
            {
                List<Thing> ingredients = unfinishedThing.ingredients;
                job.RecipeDef.Worker.ConsumeIngredient(unfinishedThing, job.RecipeDef, actor.Map);
                job.placedThings = null;
                //return ingredients;
                __result = ingredients;
                return false;
            }
            List<Thing> list = new List<Thing>();
            if (job.placedThings != null)
            {
                for (int i = 0; i < job.placedThings.Count; i++)
                {
                    if (job.placedThings[i].Count <= 0)
                    {
                        Log.Error("PlacedThing " + job.placedThings[i] + " with count " + job.placedThings[i].Count + " for job " + job);
                    }
                    else
                    {
                        Thing thing = (job.placedThings[i].Count >= job.placedThings[i].thing.stackCount) ? job.placedThings[i].thing : job.placedThings[i].thing.SplitOff(job.placedThings[i].Count);
                        job.placedThings[i].Count = 0;
                        if (list.Contains(thing))
                        {
                            Log.Error("Tried to add ingredient from job placed targets twice: " + thing);
                        }
                        else
                        {
                            list.Add(thing);

                            //Check if the Name of the Recipe is "CremateCorpse", if so do not strip.                  
                            if (!string.Equals(job.RecipeDef.defName, "CremateCorpse"))
                            {
                                IStrippable strippable = thing as IStrippable;
                                if (strippable != null)
                                {
                                    strippable.Strip();
                                }
                            }
                        }
                    }
                }
            }
            job.placedThings = null;

            // return list;
            __result = list;
            return false;
        }

    }
}
*/