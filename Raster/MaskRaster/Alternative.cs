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
        public static string basefolderfia;
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
        public string FIA_Alternative { get; set; }
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

        public bool isPathSet(GridDataType datatype)
        {
            switch(datatype)
            {
                case GridDataType.WSEMAX:
                    if (string.IsNullOrEmpty(PathWSEMAX))
                        return false;
                    break;
                case GridDataType.DEPTHMAX:
                    if (string.IsNullOrEmpty(PathDEPTHMAX))
                        return false;
                    break;
                case GridDataType.TERRAIN:
                    if (string.IsNullOrEmpty(PathTERRAIN))
                        return false;
                    break;
            }
            return true;
        }

        public string fullpath(GridDataType datatype)
        {
            string fp = "";
            switch(datatype)
            {
                case GridDataType.WSEMAX:
                    if (!string.IsNullOrEmpty(PathWSEMAX))
                        fp = System.IO.Path.Combine(basefolder, PathWSEMAX);
                    break;
                case GridDataType.DEPTHMAX:
                    if (!string.IsNullOrEmpty(PathDEPTHMAX))
                        fp = System.IO.Path.Combine(basefolder, PathDEPTHMAX);
                    break;
                case GridDataType.TERRAIN:
                    if (!string.IsNullOrEmpty(PathTERRAIN))
                        fp = System.IO.Path.Combine(basefolder, PathTERRAIN);
                    break;
            }
            if (System.IO.File.Exists(fp))
            {
                return fp;
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
