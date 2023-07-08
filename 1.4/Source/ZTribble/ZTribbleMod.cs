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
    public class ZTribbleSettings : ModSettings
    {
        public static bool flagTradeship = true;
        public static bool flagAncientDanger = true;
        public static bool flagShipChunk = true;

        public static bool flagNotifyPlayer = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref flagTradeship, "flagTradeship", true);
            Scribe_Values.Look(ref flagAncientDanger, "flagAncientDanger", true);
            Scribe_Values.Look(ref flagShipChunk, "flagShipChunk", true);

            Scribe_Values.Look(ref flagNotifyPlayer, "flagNotifyPlayer", false);

        }
    }



    public class ZTribbleMod : Mod
    {
        public ZTribbleSettings settings;
        public static ZTribbleMod mod;
        public ZTribbleMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<ZTribbleSettings>();
            mod = this;
        }

        public override string SettingsCategory()
        {
            return "ZTrib_ModName".Translate();
        }


        public override void DoSettingsWindowContents(Rect inRect)
        {

            inRect.width = 450f;
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.Label("ZTrib_SpawnOptions".Translate());
            listing.CheckboxLabeled("ZTrib_AllowTradeship".Translate(), ref ZTribbleSettings.flagTradeship);
            listing.CheckboxLabeled("ZTrib_AllowAncientDanger".Translate(), ref ZTribbleSettings.flagAncientDanger);
            listing.CheckboxLabeled("ZTrib_ShipChunk".Translate(), ref ZTribbleSettings.flagShipChunk);

            listing.GapLine();
            listing.CheckboxLabeled("ZTrib_NotifyPlayer".Translate(), ref ZTribbleSettings.flagNotifyPlayer);


            listing.Gap();

            if (listing.ButtonText("ZTrib_Destroy".Translate()))
            {
                DestroyAllTribbles();

               

            }


            listing.End();

            base.DoSettingsWindowContents(inRect);
        }

        public void DestroyAllTribbles()
        {
            int tribCount = 0;

            while (PawnsFinder.AllMaps_Spawned.Where(p => p.kindDef == PawnKindDef.Named("ZTrib_Tribble")).Any())
            {
                Pawn p = PawnsFinder.AllMaps_Spawned.Where(t => t.kindDef == PawnKindDef.Named("ZTrib_Tribble")).FirstOrDefault();
                p.Destroy();
                tribCount++;
            }

            Messages.Message(tribCount + " " + "ZTrib_DestroyCount".Translate(), MessageTypeDefOf.TaskCompletion, false);
        }
    }


}
