using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace ZTribble
{
    static class HarmonyPatches
    {
        [StaticConstructorOnStartup]
        public static class ZTribble
        {
            static ZTribble()
            {
                Harmony harmony = new Harmony("zylle.Tribbles");
                Log.Message("Initializing Tribbles.... ");
                harmony.PatchAll();
            }
        }
    }



    [HarmonyPatch(typeof(RimWorld.ForbidUtility), "CaresAboutForbidden")]
    internal static class CaresAboutForbidden
    {
        static bool Prefix(Pawn pawn, ref bool __result)
        {
            if (pawn.kindDef.defName == "ZTrib_Tribble")
            {
                __result = false;
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(RimWorld.ForbidUtility), "IsForbiddenToPass")]
    internal static class IsForbiddenToPass
    {
        static bool Prefix(Pawn pawn, ref bool __result)
        {
            if (pawn.kindDef.defName == "ZTrib_Tribble")
            {
                __result = false;
                return false;
            }

            return true;
        }
    }



    [HarmonyPatch(typeof(RimWorld.Building_Door), "PawnCanOpen")]
    internal static class PawnCanOpen
    {
        static bool Prefix(Pawn p, ref bool __result)
        {
            if (p.kindDef.defName == "ZTrib_Tribble")
            {
                __result = true;
                return false;
            }
            return true;
        }
    }



}
