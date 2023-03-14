using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    public class Parcel
    {
        public string ParcelID { get; set; }
        public List<Building> Buildings { get; set; }

        public Dictionary<string, IMath> BCAMaths { get; set; } //keyed on alternative_id

        public EBuildingTypeBrookings PropertyType { get; private set; }

        public Parcel(string id)
        {
            ParcelID = id;
            Buildings = new List<Building>();
            BCAMaths = new Dictionary<string, IMath>();
        }

        /***
         * Add building inside Parcel and find the most popular property type
         ***/
        public void AddBuilding(Building building)
        {
            if (building != null)
            {
                var bld = Buildings.Where(bd => bd.BID == building.BID).FirstOrDefault();
                if (bld == null)
                {
                    Buildings.Add(building);
                    TallyBuildingTypes();
                }
            }
        }

        /***
         * Set up parcel's building list's inundation depth measures
         * there could be more than one building in a parcel, so all depths
         * values are to be included to get the statistics representing the whole parcel
         ***/
        public void Calculate()
        {
            foreach (var im in BCAMaths.Values)
            {
                (im as BCAMATH).Clear();
            }
            BCAMaths.Clear();

            IMath m;
            foreach (var building in Buildings)
            {
                foreach (var altkey in building.BCADepthmaxStatistics.Keys)
                {
                    if (BCAMaths.ContainsKey(altkey))
                    {
                        m = BCAMaths[altkey];
                        m.SetData(building.BCADepthmaxStatistics[altkey].GetData(), true);
                    }
                    else
                    {
                        m = new BCAMATH();
                        BCAMaths[altkey] = m;
                        m.SetData(building.BCADepthmaxStatistics[altkey].GetData());
                    }
                }
            }
        }

        private void TallyBuildingTypes()
        {
            var BuildingTypeBrookings = new Dictionary<string, int>();
            foreach (var building in Buildings)
            {
                if (BuildingTypeBrookings.ContainsKey(building.OccupancyType))
                {
                    BuildingTypeBrookings[building.OccupancyType]++;
                }
                else
                {
                    BuildingTypeBrookings.Add(building.OccupancyType, 1);
                }
            }
            var sortedDict = from entry in BuildingTypeBrookings orderby entry.Value descending select entry;
            string ot = sortedDict.First().Key; // most popular property type
            if (ot.StartsWith("Mobi"))
            {
                PropertyType = EBuildingTypeBrookings.Mobile;
            }
            else if (ot.StartsWith("Res"))
            {
                PropertyType = EBuildingTypeBrookings.Residential;
            }
            else if (ot.StartsWith("Apart"))
            {
                PropertyType = EBuildingTypeBrookings.Apartment;
            }
            else if (ot.StartsWith("Deta"))
            {
                PropertyType = EBuildingTypeBrookings.Detached;
            }
            else if (ot.StartsWith("Comm"))
            {
                PropertyType = EBuildingTypeBrookings.Commercial;
            }
            else if (ot.StartsWith("Muni"))
            {
                PropertyType = EBuildingTypeBrookings.Municipal;
            }
            else if (ot.StartsWith("Runw"))
            {
                PropertyType = EBuildingTypeBrookings.Runway;
            }
            else if (ot.StartsWith("Fire"))
            {
                PropertyType = EBuildingTypeBrookings.Fire;
            }
            else if (ot.StartsWith("Police"))
            {
                PropertyType = EBuildingTypeBrookings.Police;
            }
            else if (ot.StartsWith("Heal"))
            {
                PropertyType = EBuildingTypeBrookings.Health;
            }
            else if (ot.StartsWith("Ind"))
            {
                PropertyType = EBuildingTypeBrookings.Industrial;
            }
            else if (ot.StartsWith("Rec"))
            {
                PropertyType = EBuildingTypeBrookings.Recreation;
            }
            else if (ot.StartsWith("Lib"))
            {
                PropertyType = EBuildingTypeBrookings.Library;
            }
            else if (ot.StartsWith("Schoo"))
            {
                PropertyType = EBuildingTypeBrookings.School;
            }
            else
            {
                PropertyType = EBuildingTypeBrookings.Unknown;
            }
        }
    }
}
