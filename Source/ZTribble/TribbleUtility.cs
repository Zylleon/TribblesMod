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


        public static void ResetInterval(Map map)
        {
            FiringIncident fi = new FiringIncident();
            fi = new FiringIncident(IncidentDef.Named("ZTrib_TribbleArrival"), null, null);
            map.StoryState.Notify_IncidentFired(fi);
        }






    }
}
