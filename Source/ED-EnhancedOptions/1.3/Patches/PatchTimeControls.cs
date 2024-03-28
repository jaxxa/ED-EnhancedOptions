using HarmonyLib;
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

    class PatchTimeControls : Patch
    {

        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Method
            MethodInfo _RimWorld_TimeControls_DoTimeControlsGUI = typeof(RimWorld.TimeControls).GetMethod("DoTimeControlsGUI", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_RimWorld_TimeControls_DoTimeControlsGUI, "_RimWorld_TimeControls_DoTimeControlsGUI");

            //Get the Prefix Patch
            MethodInfo _DoTimeControlsGUIPrefix = typeof(EnhancedDevelopment.EnhancedOptions.Detours.TimeControls).GetMethod("DoTimeControlsGUI", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_DoTimeControlsGUIPrefix, "_DoTimeControlsGUIPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_TimeControls_DoTimeControlsGUI, new HarmonyMethod(_DoTimeControlsGUIPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "Speed4WithoutDev";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.Speed4WithoutDev;
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
            SoundDef soundDef = null;
            switch (speed)
            {
                case TimeSpeed.Paused:
                    soundDef = SoundDefOf.Clock_Stop;
                    break;
                case TimeSpeed.Normal:
                    soundDef = SoundDefOf.Clock_Normal;
                    break;
                case TimeSpeed.Fast:
                    soundDef = SoundDefOf.Clock_Fast;
                    break;
                case TimeSpeed.Superfast:
                    soundDef = SoundDefOf.Clock_Superfast;
                    break;
                case TimeSpeed.Ultrafast:
                    soundDef = SoundDefOf.Clock_Superfast;
                    break;
            }
            if (soundDef != null)
            {
                soundDef.PlayOneShotOnCamera(null);
            }
        }

        public static bool DoTimeControlsGUI(Rect timerRect)
        {
            TickManager tickManager = Find.TickManager;
            GUI.BeginGroup(timerRect);
            Vector2 timeButSize = TimeControls.TimeButSize;
            float x = timeButSize.x;
            Vector2 timeButSize2 = TimeControls.TimeButSize;
            Rect rect = new Rect(0f, 0f, x, timeButSize2.y);
            for (int i = 0; i < TimeControls.CachedTimeSpeedValues.Length; i++)
            {
                TimeSpeed timeSpeed = TimeControls.CachedTimeSpeedValues[i];
                if (timeSpeed != TimeSpeed.Ultrafast)
                {
                    if (Widgets.ButtonImage(rect, TexButton.SpeedButtonTextures[(uint)timeSpeed]))
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
                    {
                        GUI.DrawTexture(rect, TexUI.HighlightTex);
                    }
                    rect.x += rect.width;
                }
            }
            if (Find.TickManager.slower.ForcedNormalSpeed)
            {
                Widgets.DrawLineHorizontal(rect.width * 2f, rect.height / 2f, rect.width * 2f);
            }
            GUI.EndGroup();
            GenUI.AbsorbClicksInRect(timerRect);
            UIHighlighter.HighlightOpportunity(timerRect, "TimeControls");
            if (Event.current.type == EventType.KeyDown)
            {
                if (KeyBindingDefOf.TogglePause.KeyDownEvent)
                {
                    Find.TickManager.TogglePaused();
                    TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.Pause, KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }
                if (KeyBindingDefOf.TimeSpeed_Normal.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Normal;
                    TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls, KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }
                if (KeyBindingDefOf.TimeSpeed_Fast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Fast;
                    TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls, KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }
                if (KeyBindingDefOf.TimeSpeed_Superfast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Superfast;
                    TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls, KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }
                //if (Prefs.DevMode)
                //{
                if (KeyBindingDefOf.TimeSpeed_Ultrafast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
                    TimeControls.PlaySoundOf(Find.TickManager.CurTimeSpeed);
                    Event.current.Use();
                }
                if (KeyBindingDefOf.Dev_TickOnce.KeyDownEvent && tickManager.CurTimeSpeed == TimeSpeed.Paused)
                {
                    tickManager.DoSingleTick();
                    SoundDefOf.Clock_Stop.PlayOneShotOnCamera(null);
                }

                //}
            }
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
