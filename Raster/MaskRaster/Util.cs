﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MapWinUtility;
using atcData;
using Microsoft.Office.Interop.Excel;

namespace MaskRaster
{
    public enum GridDataType
    {
        TERRAIN,
        WSEMAX,
        DEPTHMAX
    }

    public enum EREADRASTERMETHOD
    {
        POINTDIRECT,
        BLOCKAVERAGE
    }
    public enum EINUNDATIONEVALUATIONLOCATION
    {
        STRUCTURECENTER,
        STRUCTURESURROUND,
        STRUCTUREALONG,
    }

    internal class Util
    {
        static string _configfile = @"C:\dev\arcgis-pro-sdk-community-samples\Raster\MaskRaster\ConfigBCA.daml";
        static string _configfileFloodway = @"C:\dev\arcgis-pro-sdk-community-samples\Raster\MaskRaster\ConfigUtility.daml";


        /// <summary>
        /// Get the current add-in module's daml / AddInInfo Id tag (which is the same as the Assembly GUID)
        /// </summary>
        /// <returns></returns>
        public static string GetAddInId()
        {
            // Module.Id is internal, but we can still get the ID from the assembly
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            //var fileName = Path.Combine($@"{{{attribute.Value.ToString()}}}", $@"{assembly.FullName.Split(',')[0]}.esriAddInX");
            //return fileName;
            return "";
        }

        /// <summary>
		/// returns a tuple with version and desktopVersion using the given addin file path
		/// </summary>
		/// <returns>tuple: version, desktopVersion</returns>
		public static List<Alternative> GetConfigDaml()
        {
            // just test to see if loading problem
            //MapWinUtility.Log l = new MapWinUtility.Log();

            XmlDocument xDoc = new XmlDocument();
            try
            {
                string daml = string.Empty;
                using (StreamReader sr = new StreamReader(_configfile))
                {
                    daml = sr.ReadToEnd();
                    xDoc.LoadXml(daml); // @"<?xml version=""1.0"" encoding=""utf - 8""?>" + 
                }

                XmlNodeList alt_list_block = xDoc.GetElementsByTagName("SMCAlternatives");
                Alternative.basefolder = alt_list_block[0].Attributes["basefolder"].Value;
                Alternative.basefolderfia = alt_list_block[0].Attributes["basefolderfia"].Value;
                var lmethod = alt_list_block[0].Attributes["readmethod"].Value;
                switch (lmethod)
                {
                    case nameof(EREADRASTERMETHOD.POINTDIRECT):
                        Alternative.readmethod = EREADRASTERMETHOD.POINTDIRECT;
                        break;
                    case nameof(EREADRASTERMETHOD.BLOCKAVERAGE):
                        Alternative.readmethod = EREADRASTERMETHOD.BLOCKAVERAGE;
                        break;
                }
                lmethod = alt_list_block[0].Attributes["floodevalmethod"].Value;
                switch (lmethod)
                {
                    case nameof(EINUNDATIONEVALUATIONLOCATION.STRUCTURECENTER):
                        Alternative.evalmethod = EINUNDATIONEVALUATIONLOCATION.STRUCTURECENTER;
                        break;
                    case nameof(EINUNDATIONEVALUATIONLOCATION.STRUCTURESURROUND):
                        Alternative.evalmethod = EINUNDATIONEVALUATIONLOCATION.STRUCTURESURROUND;
                        break;
                }
                var offset = alt_list_block[0].Attributes["floodevalstructureoffsetinfeet"].Value;
                double.TryParse(offset, out BCA.FloodEvalStructureOffsetInFeet);
                List<Alternative> alts = new List<Alternative>();
                foreach (XmlNode xalt in alt_list_block[0])
                {
                    string alt_id = xalt.Attributes["id"].Value;
                    string alt_data_type = xalt.Attributes["type"].Value;
                    string alt_fia = xalt.Attributes.GetNamedItem("fia") != null ? xalt.Attributes["fia"].Value : "";
                    string alt_path = xalt.ChildNodes[0].InnerText;
                    Alternative alt = alts.Where(a => a.Name == alt_id).FirstOrDefault();
                    if (alt == null)
                    {
                        alt = new Alternative(alt_id);
                        alt.FIA_Alternative = alt_fia;
                        alts.Add(alt);
                    }
                    switch (alt_data_type)
                    {
                        case nameof(GridDataType.WSEMAX):
                            alt.PathWSEMAX = alt_path;
                            break;
                        case nameof(GridDataType.DEPTHMAX):
                            alt.PathDEPTHMAX = alt_path;
                            break;
                        case nameof(GridDataType.TERRAIN):
                            alt.PathTERRAIN = alt_path;
                            break;
                    }
                }

                XmlNodeList parcel_block = xDoc.GetElementsByTagName("Parcels");
                foreach (XmlNode xp in parcel_block[0])
                {
                    if (xp.Name == "DataFile")
                    {
                        BCA.FilepathParcelTRCN = xp.ChildNodes[0].InnerText;
                    }
                    else if (xp.Name.StartsWith("Included"))
                    {
                        BCA.FilepathParcelIncluded = xp.ChildNodes[0].InnerText;
                    }
                }
                return alts;
            }
            catch (Exception ex)
            {
                throw new Exception($@"Unable to parse config.daml {_configfile}: {ex.Message}");
            }
        }

        /*
         * Every time this is called, the list of DDFs will be recreated from scratch
         * essentially update the DDFs such that a new analysis can be done with another set of curves
         */
        public static void GetConfigDamlDDFs()
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                string daml = string.Empty;
                using (StreamReader sr = new StreamReader(_configfile))
                {
                    daml = sr.ReadToEnd();
                    xDoc.LoadXml(daml); // @"<?xml version=""1.0"" encoding=""utf - 8""?>" + 
                }
                //now setup list of DDFs
                XmlNodeList DDF_list_block = xDoc.GetElementsByTagName("DDFs");
                var DDFfilepath = DDF_list_block[0].Attributes["DDF"].Value;
                BCA.DDFs = new Dictionary<string, DepthDamageFunction>();
                double v;
                foreach (XmlNode xalt in DDF_list_block[0])
                {
                    string occupancytypealias = xalt.Attributes["alias"].Value;
                    double.TryParse(xalt.Attributes["freeboardfeet"].Value, out v);
                    string occupancytype = xalt.ChildNodes[0].InnerText;
                    DepthDamageFunction ddf = BCA.DDFs.Values.Where(d => d.OccupancyType == occupancytype).FirstOrDefault();
                    if (ddf == null)
                    {
                        ddf = new DepthDamageFunction(occupancytype, occupancytypealias, DDFfilepath);
                        ddf.FreeboardFeet = v;
                        BCA.DDFs.Add(occupancytype, ddf);
                    }
                }
                BCA.SetupDDFs(DDFfilepath);
            }
            catch (Exception ex)
            {
                throw new Exception($@"Unable to parse config.daml {_configfile}: {ex.Message}");
            }
        }

        /*
         * Every time this is called, the list of Floodway alternatives will be read anew
         */
		public static List<Alternative> GetConfigDamlFloodway()
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                string daml = string.Empty;
                using (StreamReader sr = new StreamReader(_configfileFloodway))
                {
                    daml = sr.ReadToEnd();
                    xDoc.LoadXml(daml);
                }

                XmlNodeList alt_list_block = xDoc.GetElementsByTagName("SMCFWAlternatives");
                Alternative.basefolderfw = alt_list_block[0].Attributes["basefolder"].Value;
                var fw_readlmethodstr = alt_list_block[0].Attributes["readmethod"].Value;
                var fw_readlmethod = EREADRASTERMETHOD.BLOCKAVERAGE;
                switch (fw_readlmethodstr)
                {
                    case nameof(EREADRASTERMETHOD.POINTDIRECT):
                        fw_readlmethod = EREADRASTERMETHOD.POINTDIRECT;
                        break;
                    case nameof(EREADRASTERMETHOD.BLOCKAVERAGE):
                        fw_readlmethod = EREADRASTERMETHOD.BLOCKAVERAGE;
                        break;
                }
                var fw_evalmethodstr = alt_list_block[0].Attributes["floodevalmethod"].Value;
                var fw_evalmethod = EINUNDATIONEVALUATIONLOCATION.STRUCTURECENTER;
                switch (fw_evalmethodstr)
                {
                    case nameof(EINUNDATIONEVALUATIONLOCATION.STRUCTURECENTER):
                        fw_evalmethod = EINUNDATIONEVALUATIONLOCATION.STRUCTURECENTER;
                        break;
                    case nameof(EINUNDATIONEVALUATIONLOCATION.STRUCTURESURROUND):
                        fw_evalmethod = EINUNDATIONEVALUATIONLOCATION.STRUCTURESURROUND;
                        break;
                    case nameof(EINUNDATIONEVALUATIONLOCATION.STRUCTUREALONG):
                        fw_evalmethod = EINUNDATIONEVALUATIONLOCATION.STRUCTUREALONG;
                        break;
                }

                var offset = alt_list_block[0].Attributes["floodevalstructureoffsetinfeet"].Value;
                double readingoffsetinfeet;
                double.TryParse(offset, out readingoffsetinfeet);
                List<Alternative> fw_alts = new List<Alternative>();
                foreach (XmlNode xalt in alt_list_block[0])
                {
                    string alt_id = xalt.Attributes["id"].Value;
                    string alt_data_type = xalt.Attributes["type"].Value;
                    string alt_path = xalt.ChildNodes[0].InnerText;
                    Alternative alt = fw_alts.Where(a => a.Name == alt_id).FirstOrDefault();
                    if (alt == null)
                    {
                        alt = new Alternative(alt_id);
                        fw_alts.Add(alt);
                    }
                    switch (alt_data_type)
                    {
                        case nameof(GridDataType.WSEMAX):
                            alt.PathWSEMAX = alt_path;
                            break;
                        case nameof(GridDataType.DEPTHMAX):
                            alt.PathDEPTHMAX = alt_path;
                            break;
                        case nameof(GridDataType.TERRAIN):
                            alt.PathTERRAIN = alt_path;
                            break;
                    }
                }
                
                return fw_alts;
            }
            catch (Exception ex)
            {
                throw new Exception($@"Unable to parse config.daml {_configfile}: {ex.Message}");
            }
        }
        

        public static void SetupBCATemplate()
        {


        }

        public static IEnumerable<string> ReadLines(string filepath, Encoding encoding)
        {
            using (var reader = new StreamReader(filepath, encoding))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static bool LoadDataSourcePlugins()
        {
            try
            {
                if (atcDataManager.DataSources is null)
                    atcDataManager.Clear();
                if (atcDataManager.DataPlugins.Count > 0)
                    return true;
                var att = new atcDataAttributes();
                atcTimeseriesStatistics.atcTimeseriesStatistics.InitializeShared();
                var stat = new atcTimeseriesStatistics.atcTimeseriesStatistics();
                var TSMath = new atcTimeseriesMath.atcTimeseriesMath();
                foreach (var attr in TSMath.AvailableOperations)
                {
                    string key = attr.Definition.Name;
                }
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.InnerException.Message);
                return false;
            }
        }
    }
}
