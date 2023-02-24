using ArcGIS.Desktop.Internal.Mapping.Symbology;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    public class Alternative
    {
        public static string basefolder;
        public static READRASTERMETHOD method;

        /*
        public static SortedDictionary<int, (double, double)> BuildingLatLong {get; set;}
        public static SortedDictionary<int, double> BuildingFirstFloorSqFt { get; set; }
        public static SortedDictionary<int, string> BuildingOccupancyType { get; set; }
        public static SortedDictionary<int, string> BuildingAddress { get; set; }
        public static SortedDictionary<int, double> BuildingTerrainElevationFt { get; set; }

        public SortedDictionary<int, double> BuildingWSEmax { get; set; }
        public SortedDictionary<int, bool> BuildingFlooded {get; set;}
        public SortedDictionary<int, double> BuildingFloodDepth {get; set;}

        public static SortedDictionary<int, Building> Buildings { get; set; }
        */

        public string Name { get; set; }
        public string PathWSEMAX { get; set; }
        public string PathDEPTHMAX { get; set; }
        public string PathTERRAIN { get; set; }
        public RasterLayer WSEmax { get; set; }
        public RasterLayer Depthmax { get; set; }
        public RasterLayer DataLayer { get; set; }


        public Alternative(string name)
        {
            Name = name;
            /*
            BuildingWSEmax = new SortedDictionary<int, double>();
            BuildingLatLong = new SortedDictionary<int, (double, double)>();
            BuildingFloodDepth = new SortedDictionary<int, double>();
            BuildingFlooded= new SortedDictionary<int, bool>();
            */
        }

        public string fullpath(GridDataType datatype)
        {
            switch(datatype)
            {
                case GridDataType.WSEMAX:
                    return System.IO.Path.Combine(basefolder, PathWSEMAX);
                case GridDataType.DEPTHMAX:
                    return System.IO.Path.Combine(basefolder, PathDEPTHMAX);
                case GridDataType.TERRAIN:
                    return System.IO.Path.Combine(basefolder, PathTERRAIN);
            }
            return "";
        }

        public string layerName(GridDataType datatype) 
        {
            //ArcGIS Pro will display raster layer with its full filename with extension
            return System.IO.Path.GetFileName(fullpath(datatype));
        }
    }
}
