using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
/*
namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchBlightGraphics : Patch
    {

        protected override string PatchDescription()
        {
            return "Blight Graphics";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
        }

        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {
            if (Mod_EnhancedOptions.Settings.BlightImageIndex != 0)
            {
                String _BlightPath = string.Empty;
                switch (Mod_EnhancedOptions.Settings.BlightImageIndex)
                {
                    case 0:
                        _BlightPath = "Things/Plant/Blight";
                        break;
                    case 1:
                        _BlightPath = "Things/Plant/Blights/Blight_Red";
                        break;
                    case 2:
                        _BlightPath = "Things/Plant/Blights/Blight_Blue";
                        break;
                    case 3:
                        _BlightPath = "Things/Plant/Blights/Blight_Orange";
                        break;
                    case 4:
                        _BlightPath = "Things/Plant/Blights/Blight_Purple";
                        break;
                    default:
                        _BlightPath = "Things/Plant/Blight";
                        break;
                }
                
                GraphicRequest requestActive = new GraphicRequest(Type.GetType("Graphic_Single"),
                                                              _BlightPath,
                                                              ShaderDatabase.DefaultShader,
                                                              new Vector2(1,
                                                                          1),
                                                              Color.white,
                                                              Color.white,
                                                              new GraphicData(), 1);
                ThingDefOf.Blight.graphic.Init(requestActive);
            }


            if (Mod_EnhancedOptions.Settings.BlightScale != 1)
            {
                ThingDefOf.Blight.graphic.drawSize = new Vector2(Mod_EnhancedOptions.Settings.BlightScale, Mod_EnhancedOptions.Settings.BlightScale);
            }
        }
    }
}
*/