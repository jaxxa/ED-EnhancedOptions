using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{

    //[HarmonyPatch(typeof(Plant))]
    //[HarmonyPatch("Resting_Getter")]
    static class PatchTimeControls
    {

        static public void ApplyPatches(HarmonyInstance harmony)
        {
            Log.Message("PatchTimeControls.ApplyPatches() Starting");

            //Get the Method
            MethodInfo _RimWorld_TimeControls_DoTimeControlsGUI = typeof(RimWorld.TimeControls).GetMethod("DoTimeControlsGUI", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_RimWorld_TimeControls_DoTimeControlsGUI, "_RimWorld_TimeControls_DoTimeControlsGUI");

            //Get the Prefix Patch
            MethodInfo _DoTimeControlsGUIPrefix = typeof(EnhancedDevelopment.EnhancedOptions.Detours.TimeControls).GetMethod("DoTimeControlsGUI", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_DoTimeControlsGUIPrefix, "_DoTimeControlsGUIPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_TimeControls_DoTimeControlsGUI, new HarmonyMethod(_DoTimeControlsGUIPrefix), null);
            
            Log.Message("PatchTimeControls.ApplyPatches() Completed");
        }

    }

    public static class TimeControls
    {
        public static readonly Vector2 TimeButSize = new Vector2(32f, 24f);
        private static readonly string[] SpeedSounds = new string[5]
        {
            "ClockStop",
            "ClockNormal",
            "ClockFast",
            "ClockSuperfast",
            "ClockSuperfast"
        };
        private static readonly TimeSpeed[] CachedTimeSpeedValues = (TimeSpeed[])Enum.GetValues(typeof(TimeSpeed));

        private static void PlaySoundOf(TimeSpeed speed)
        {
            SoundStarter.PlayOneShotOnCamera(SoundDef.Named(TimeControls.SpeedSounds[(int)speed]), (Map)null);
        }

        public static bool DoTimeControlsGUI(Rect timerRect)
        {
            TickManager tickManager = Find.TickManager;
            GUI.BeginGroup(timerRect);
            Rect rect = new Rect(0.0f, 0.0f, TimeControls.TimeButSize.x, TimeControls.TimeButSize.y);
            for (int index = 0; index < TimeControls.CachedTimeSpeedValues.Length; ++index)
            {
                TimeSpeed timeSpeed = TimeControls.CachedTimeSpeedValues[index];
                if (timeSpeed != TimeSpeed.Ultrafast)
                {
                    if (Widgets.ButtonImage(rect, TexButton.SpeedButtonTextures[(int)timeSpeed]))
                    {
                        if (timeSpeed == TimeSpeed.Paused)
                        {
                            tickManager.TogglePaused();
                            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.Pause, KnowledgeAmount.SpecificInteraction);
                        }
                        else
                        {
                            tickManager.CurTimeSpeed = timeSpeed;
                            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls, KnowledgeAmount.SpecificInteraction);
                        }
                        TimeControls.PlaySoundOf(tickManager.CurTimeSpeed);
                    }
                    if (tickManager.CurTimeSpeed == timeSpeed)
                        GUI.DrawTexture(rect, (Texture)TexUI.HighlightTex);
                    rect.x += rect.width;
                }
            }
            if (Find.TickManager.slower.ForcedNormalSpeed)
                Widgets.DrawLineHorizontal(rect.width * 2f, rect.height / 2f, rect.width * 2f);
            GUI.EndGroup();
            GenUI.AbsorbClicksInRect(timerRect);
            UIHighlighter.HighlightOpportunity(timerRect, "TimeControls");
            if (Event.current.type != EventType.KeyDown)
                return false;
            if (KeyBindingDefOf.TogglePause.KeyDownEvent)
            {
                Find.TickManager.TogglePaused();
                TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.Pause, KnowledgeAmount.SpecificInteraction);
                Event.current.Use();
            }
            if (KeyBindingDefOf.TimeSpeedNormal.KeyDownEvent)
            {
                Find.TickManager.CurTimeSpeed = TimeSpeed.Normal;
                TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls, KnowledgeAmount.SpecificInteraction);
                Event.current.Use();
            }
            if (KeyBindingDefOf.TimeSpeedFast.KeyDownEvent)
            {
                Find.TickManager.CurTimeSpeed = TimeSpeed.Fast;
                TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls, KnowledgeAmount.SpecificInteraction);
                Event.current.Use();
            }
            if (KeyBindingDefOf.TimeSpeedSuperfast.KeyDownEvent)
            {
                Find.TickManager.CurTimeSpeed = TimeSpeed.Superfast;
                TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls, KnowledgeAmount.SpecificInteraction);
                Event.current.Use();
            }

            //Removed to Allow Speed 4 Without Dev Mode.
            //if (!Prefs.DevMode)
            //    return false;
            if (KeyBindingDefOf.TimeSpeedUltrafast.KeyDownEvent)
            {
                Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
                TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                Event.current.Use();
            }
            if (!KeyBindingDefOf.TickOnce.KeyDownEvent || tickManager.CurTimeSpeed != TimeSpeed.Paused)
                return false;
            tickManager.DoSingleTick();
            SoundStarter.PlayOneShotOnCamera(SoundDef.Named(TimeControls.SpeedSounds[0]), (Map)null);

            return false;
        }
    }
    
    [StaticConstructorOnStartup]
    internal class TexButton
    {
        public static readonly Texture2D[] SpeedButtonTextures = new Texture2D[5]
        {
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Pause", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Normal", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Fast", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Superfast", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Superfast", true)
        };
    }

}
