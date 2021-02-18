using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ZTribble
{
    public static class TribbleUtility
    {
        public static bool CanSpawnNow(IncidentParms parms)
        {
            //IncidentWorker.CanFireNow(parms)

            IncidentDef def = IncidentDef.Named("ZTrib_TribbleArrival");
            if (def.Worker.CanFireNow(parms))
            {
                return true;
            }
            return false;
        }


        public static void ResetInterval(IncidentParms parms)
        {

            Log.Message("Resetting");
            //Dictionary<IncidentDef, int> lastFireTicks = parms.target.StoryState.lastFireTicks;
            IncidentDef def = IncidentDef.Named("ZTrib_TribbleArrival");

            int ticksGame = Find.TickManager.TicksGame;

            //Dictionary<IncidentDef, int> lastFireTicks = parms.target.StoryState.lastFireTicks;

            parms.target.StoryState.lastFireTicks[def] = ticksGame;



            

        }






    }
}
