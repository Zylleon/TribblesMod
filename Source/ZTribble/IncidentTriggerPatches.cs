using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ZTribble
{

    //TODO: add filtering and randomness! IMPORTANT


    //Orbital traders
    [HarmonyPatch(typeof(RimWorld.TradeShip), "GiveSoldThingToPlayer")]
    internal static class GiveSoldThingToPlayer
    {
        static void Postfix(Thing toGive, int countToGive, Pawn playerNegotiator)
        {
            Map map = playerNegotiator.Map;

            IntVec3 loc = DropCellFinder.TradeDropSpot(map);

            PawnKindDef tribbleKind = PawnKindDef.Named("ZTrib_Tribble");
            Pawn tribble = PawnGenerator.GeneratePawn(tribbleKind);

            // spawn extra drop pod containing tribble
            TradeUtility.SpawnDropPod(loc, map, tribble);
        }
    }



    // ancient caskets, along with scarabs
    [HarmonyPatch(typeof(RimWorld.ThingSetMaker_MapGen_AncientPodContents), "GenerateScarabs")]
    internal static class ThingSetMaker_MapGen_AncientPodContents
    {
        static void Postfix(ref List<Thing> __result)
        {

            //TODO: add filtering and randomness! IMPORTANT

            Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("ZTrib_Tribble"), null);

            __result.Add(pawn);
        }
    }



}
