using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ZTribble
{
	public class CompProperties_TribbleSpawner : CompProperties
	{
		//public List<PawnKindDef> spawnablePawnKinds;

		public FloatRange baseSpawnInterval = new FloatRange(0.85f, 1.15f);

		public CompProperties_TribbleSpawner()
		{
			compClass = typeof(CompTribbleSpawner);
		}
	}
}
