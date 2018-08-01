using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions
{
    abstract class Patch
    {
        /// <summary>
        /// Checks if this Patch should be applied now.
        /// </summary>
        /// <returns>Returns true if the Patch should be applied.</returns>
        protected abstract  bool ShouldPatchApply();

        /// <summary>
        /// Apply the patch.
        /// </summary>
        protected abstract void ApplyPatch(HarmonyInstance harmony = null);

        /// <summary>
        /// The Description of this patch.
        /// Mainly used for logging.
        /// </summary>
        /// <returns>The Patch Description.</returns>
        protected abstract string PatchDescription();

        /// <summary>
        /// Checks if this Patch needs to be applied, and applies if needed.
        /// </summary>
        public void ApplyPatchIfRequired(HarmonyInstance harmony = null)
        {
            string _LogLocation = "EnhancedOptions.Patch.ApplyPatchIfRequired: ";

            if (this.ShouldPatchApply())
            {
                if(Prefs.LogVerbose)
                {
                    Log.Message(_LogLocation + "Applying Patch: " + this.PatchDescription());                
                }
                this.ApplyPatch(harmony);
                if(Prefs.LogVerbose)
                {
                    Log.Message(_LogLocation + "Applied Patch: " + this.PatchDescription());
                }
            }
            else
            {
                if(Prefs.LogVerbose)
                {
                    Log.Message(_LogLocation + "Skipping Applying Patch: " + this.PatchDescription());
                }
            }
        }



    }
}
