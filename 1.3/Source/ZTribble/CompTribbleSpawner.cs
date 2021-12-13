using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using Verse.AI.Group;

namespace ZTribble
{
    public class CompTribbleSpawner : ThingComp
	{

		public int nextPawnSpawnTick = -1;

		private CompProperties_TribbleSpawner Props => (CompProperties_TribbleSpawner)props;

	
		public override void Initialize(CompProperties props)
		{
			base.Initialize(props);

		}

		
		private void CalculateNextPawnSpawnTick()
		{
			float delayTicks = Props.baseSpawnInterval.RandomInRange * 60000f;
			nextPawnSpawnTick = Find.TickManager.TicksGame + (int)delayTicks;
		}

		private bool TrySpawnPawn(out Pawn pawn)
		{
			PawnKindDef chosenKind = PawnKindDef.Named("ZTrib_Tribble");
			
			//int index = chosenKind.lifeStages.Count - 1;
			pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(chosenKind, parent.Faction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: false, newborn: true, allowDead: false, allowDowned: false, canGeneratePawnRelations: false, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0, null, 1f, null, null, null, null, null, null));
			//spawnedPawns.Add(pawn);
			GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(parent.Position, parent.Map, 3), parent.Map);

			return true;
		}


		public override void CompTick()
		{
			if (nextPawnSpawnTick == -1)
			{
                //SpawnInitialPawns();
                CalculateNextPawnSpawnTick();
            }
			if (!parent.Spawned)
			{
				return;
			}
			//FilterOutUnspawnedPawns();
			if (Find.TickManager.TicksGame >= nextPawnSpawnTick)
			{
				Pawn pawn = parent as Pawn;

				if (pawn.needs.food.TicksStarving < 50)
                {
					pawn.caller.DoCall();
				}


                if (TrySpawnPawn(out var newPawn) && newPawn.caller != null)
                {
					newPawn.caller.DoCall();
                }
                CalculateNextPawnSpawnTick();
			}
		}

		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look(ref nextPawnSpawnTick, "nextPawnSpawnTick", 0);
			
        }

	}








}
