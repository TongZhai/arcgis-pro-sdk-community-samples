using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    public class DepthDamageFunction
    {
        public string OccupancyType { get; private set; }
        public string OccupancyTypeAlias { get; private set; }
        public string DataSource { get; private set; }

        public bool DamageBasedOnAggregate = false;
        public double ContentToStructureValueRatio { get; set; }
        public SortedDictionary<double, double> DDFStructure { get; private set; }
        public SortedDictionary<double, double> DDFContent { get; private set; }
        public SortedDictionary<double, double> DDFDisplacement { get; private set; }

        public DepthDamageFunction(string occupancyType, string occupancyTypeAlias, string datapath) 
        { 
            OccupancyType = occupancyType;
            OccupancyTypeAlias = occupancyTypeAlias;
            DataSource = datapath;
            DDFStructure = new SortedDictionary<double, double>();
            DDFContent = new SortedDictionary<double, double>();
            DDFDisplacement = new SortedDictionary<double, double>();
        }
    }
}
