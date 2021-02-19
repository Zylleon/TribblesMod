using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ZTribble
{
	internal class IncidentWorker_Tribbles : IncidentWorker
	{

		protected override bool CanFireNowSub(IncidentParms parms)
		{
			if (!base.CanFireNowSub(parms))
			{
				return false;
			}
			Map map = (Map)parms.target;
			IntVec3 result;

			// Don't spawn if there's low food
			int freeColonistsSpawnedCount = map.mapPawns.FreeColonistsSpawnedCount;
			if (map.resourceCounter.TotalHumanEdibleNutrition < 10f * (float)freeColonistsSpawnedCount)
			{
				return false;
			}

			return RCellFinder.TryFindRandomPawnEntryCell(out result, map, CellFinder.EdgeRoadChance_Animal);
		}


		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			PawnKindDef tribble = PawnKindDef.Named("ZTrib_Tribble");
			if (!RCellFinder.TryFindRandomPawnEntryCell(out var result, map, CellFinder.EdgeRoadChance_Animal))
			{
				return false;
			}


			IntVec3 loc = CellFinder.RandomClosewalkCellNear(result, map, 10);
			((Pawn)GenSpawn.Spawn(PawnGenerator.GeneratePawn(tribble), loc, map)).needs.food.CurLevelPercentage = 1f;


			//if(ZTribbleSettings.flagNotifyPlayer)
   //         {
			//	SendStandardLetter("ZTrib_LetterLabelTribblesArrived".Translate(), "ZTrib_TribblesArrived".Translate(), LetterDefOf.ThreatBig, parms, new TargetInfo(result, map));
			//}

			return true;
		}
	}
}
