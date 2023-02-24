using ArcGIS.Desktop.Internal.Mapping.Locate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Controls;
using System.Runtime.CompilerServices;

namespace MaskRaster
{
    internal class BCA
    {
        public static string BCA_Worksheet1 = "Riverine Flood";
        public static string BCA_Worksheet2 = "Flood Before Mitigation";
        public static string BCA_Worksheet3 = "Flood After Mitigation";
        public static string BCA_Worksheet4 = "Critical Facility Info";

        public static Application App;
        public static Workbook BCAWorkbook = null;

        public static Dictionary<int, Building> Buildings = new Dictionary<int, Building>();

        public System.Data.DataTable Tab_RiverineFlood;
        public System.Data.DataTable Tab_FloodBeforeMitigation;
        public System.Data.DataTable Tab_FloodAfterMitigation;
        public System.Data.DataTable Tab_CriticalFacilityInfo;

        public static Dictionary<string, string> Dict_RiverineFlood;
        public static Dictionary<string, string> Dict_FloodBeforeMitigation;
        public static Dictionary<string, string> Dict_FloodAfterMitigation;
        public static Dictionary<string, string> Dict_CriticalFacilityInfo;

        public void Setup()
        {
            Tab_RiverineFlood = new System.Data.DataTable("Riverine Flood");
            Tab_RiverineFlood.Columns.Add("1_A", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("2_B", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("3_C", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("4_D", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("5_E", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("6_F", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("7_G", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("8_H", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("9_I", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("10_J", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("11_K", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("12_L", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("13_M", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("14_N", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("15_O", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("16_P", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("17_Q", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("18_R", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("19_S", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("20_T", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("21_U", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("22_V", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("23_W", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("24_X", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("25_Y", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("26_Z", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("27_AA", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("28_AB", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("29_AC", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("30_AD", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("31_AE", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("32_AF", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("33_AG", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("34_AH", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("35_AI", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("36_AJ", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("37_AK", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("38_AL", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("39_AM", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("40_AN", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("41_AO", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("42_AP", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("43_AQ", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("44_AR", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("45_AS", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("46_AT", System.Type.GetType("System.String"));
            Tab_RiverineFlood.Columns.Add("47_AU", System.Type.GetType("System.String")); //skipped in Excel template

            /*
            var row = Tab_RiverineFlood.NewRow();
            row["1_A"] = 1;
            row[1] = 2;
            Tab_RiverineFlood.Rows.Add(row);
            Tab_RiverineFlood.Select("[1_A]=1").Where(dr => (int)dr[2] == 1).ToList();
            Tab_RiverineFlood.Select("[1_A]=1").FirstOrDefault();
            */

            Dict_RiverineFlood = new Dictionary<string, string>();
            Dict_RiverineFlood.Add("1_A", "Identifier*");
            Dict_RiverineFlood.Add("2_B", "Street Address*");
            Dict_RiverineFlood.Add("3_C", "City*");
            Dict_RiverineFlood.Add("4_D", "State*");
            Dict_RiverineFlood.Add("5_E", "Zip Code*");
            Dict_RiverineFlood.Add("6_F", "County*");
            Dict_RiverineFlood.Add("7_G", "Latitude");
            Dict_RiverineFlood.Add("8_H", "Longitude");
            Dict_RiverineFlood.Add("9_I", "Structure Type*");
            Dict_RiverineFlood.Add("10_J", "Mitigation Action Type*");
            Dict_RiverineFlood.Add("11_K", "Project Useful Life*");
            Dict_RiverineFlood.Add("12_L", "Mitigation Project Cost ($)*");
            Dict_RiverineFlood.Add("13_M", "Use Default Number of Years of Maintenance?");
            Dict_RiverineFlood.Add("14_N", "Number of Years of Maintenance");
            Dict_RiverineFlood.Add("15_O", "Annual Maintenance Cost ($)");
            Dict_RiverineFlood.Add("16_P", "Lowest Floor Elevation of the Property (ft)");
            Dict_RiverineFlood.Add("17_Q", "Streambed Elevation at Property Location (ft)");
            Dict_RiverineFlood.Add("18_R", "Feet Lowest Floor Is Being Raised");
            Dict_RiverineFlood.Add("19_S", "Elevation for the Top of Barrier or Floodproofing (ft)");
            Dict_RiverineFlood.Add("20_T", "Building Type (Residential)");
            Dict_RiverineFlood.Add("21_U", "Building Use (Non-Residential)");
            Dict_RiverineFlood.Add("22_V", "Building Type (Non-Residential)");
            Dict_RiverineFlood.Add("23_W", "Building is located outside of hundred-year flood area (Non-Residential/Critical Facility)");
            Dict_RiverineFlood.Add("24_X", "Building has Basement (Residential)");
            Dict_RiverineFlood.Add("25_Y", "Building is Engineered (Non-Residential/Critical Facility)");
            Dict_RiverineFlood.Add("26_Z", "Building has Active NFIP Policy");
            Dict_RiverineFlood.Add("27_AA", "Damage Curve");
            Dict_RiverineFlood.Add("28_AB", "First Floor Area (Non-Residential/Critical Facility - sq.ft)");
            Dict_RiverineFlood.Add("29_AC", "Size of Building (sq.ft)");
            Dict_RiverineFlood.Add("30_AD", "Use Default Building Replacement Value?");
            Dict_RiverineFlood.Add("31_AE", "Building Replacement Value ($/sq.ft)");
            Dict_RiverineFlood.Add("32_AF", "Use Default Demolition Threshold?");
            Dict_RiverineFlood.Add("33_AG", "Demolition Threshold (%)");
            Dict_RiverineFlood.Add("34_AH", "Use Default Building Contents Value?");
            Dict_RiverineFlood.Add("35_AI", "Contents Value ($)");
            Dict_RiverineFlood.Add("36_AJ", "Utilities are Elevated (Residential)");
            Dict_RiverineFlood.Add("37_AK", "Annual Street Maintenance Budget ($)");
            Dict_RiverineFlood.Add("38_AL", "Number of Street Miles Maintained");
            Dict_RiverineFlood.Add("39_AM", "Street Miles that will not require future maintenance");
            Dict_RiverineFlood.Add("40_AN", "Annual Operating Budget ($)");
            Dict_RiverineFlood.Add("41_AO", "Use Default Monthly Cost of Temporary Space?");
            Dict_RiverineFlood.Add("42_AP", "Monthly Cost of Temporary Space ($/sq.ft/month)");
            Dict_RiverineFlood.Add("43_AQ", "Use Default One Time Displacement Cost?");
            Dict_RiverineFlood.Add("44_AR", "One Time Displacement Cost ($/sq.ft)");
            Dict_RiverineFlood.Add("45_AS", "Use Default Lodging Per Diem?");
            Dict_RiverineFlood.Add("46_AT", "Current Federal Lodging Per Diem ($/night)");
            Dict_RiverineFlood.Add("47_AU", "");
            Dict_RiverineFlood.Add("48_AV", "Use Default Meals Per Diem?");
            Dict_RiverineFlood.Add("49_AW", "Current Federal Meals Per Diem ($/day)");
            Dict_RiverineFlood.Add("50_AX", "Number of Building Residents");
            Dict_RiverineFlood.Add("51_AY", "Number of Volunteers Required");
            Dict_RiverineFlood.Add("52_AZ", "Enter the Number of Days Lodging for Volunterr ($)");
            Dict_RiverineFlood.Add("53_BA", "Use Default Per-Person cost of Lodging?");
            Dict_RiverineFlood.Add("54_BB", "enter the Per-Person Cost of Lodging for a Volunteer ($)");
            Dict_RiverineFlood.Add("55_BC", "Number of Workers");
            Dict_RiverineFlood.Add("56_BD", "Use Acres?");
            Dict_RiverineFlood.Add("57_BE", "Total Project Area (acres or sq.ft)");
            Dict_RiverineFlood.Add("58_BF", "Urban Green Open Space (%)");
            Dict_RiverineFlood.Add("59_BG", "Rural Green Open Space (%)");
            Dict_RiverineFlood.Add("60_BH", "Riparian (%)");
            Dict_RiverineFlood.Add("61_BI", "Coastal Wetlands (%)");
            Dict_RiverineFlood.Add("62_BJ", "Inland Wetlands (%)");
            Dict_RiverineFlood.Add("63_BK", "Forest (%)");
            Dict_RiverineFlood.Add("64_BL", "Coral Reefs (%)");
            Dict_RiverineFlood.Add("65_BM", "Shellfish Reefs (%)");
            Dict_RiverineFlood.Add("66_BN", "Beaches & Dunes (%)");

            Tab_FloodBeforeMitigation = new System.Data.DataTable("Flood Before Mitigation");
            Tab_FloodBeforeMitigation.Columns.Add("1_A", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("2_B", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("3_C", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("4_D", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("5_E", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("6_F", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("7_G", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("8_H", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("9_I", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("10_J", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("11_K", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("12_L", typeof(string));
            Tab_FloodBeforeMitigation.Columns.Add("13_M", typeof(string));

            Dict_FloodBeforeMitigation.Add("1_A", "Identifier");
            Dict_FloodBeforeMitigation.Add("2_B", "Use Default Recurrence Intervals?");
            Dict_FloodBeforeMitigation.Add("3_C", "Recurrence Interval (years) 1");
            Dict_FloodBeforeMitigation.Add("4_D", "Water Surface Elevation (ft) 1");
            Dict_FloodBeforeMitigation.Add("5_E", "Discharge (cfs) 1");
            Dict_FloodBeforeMitigation.Add("6_F", "Recurrence Interval (years) 2");
            Dict_FloodBeforeMitigation.Add("7_G", "Water Surface Elevation (ft) 2");
            Dict_FloodBeforeMitigation.Add("8_H", "Discharge (cfs) 2");
            Dict_FloodBeforeMitigation.Add("9_I", "Recurrence Interval (years) 3");
            Dict_FloodBeforeMitigation.Add("10_J", "Water Surface Elevation (ft) 3");
            Dict_FloodBeforeMitigation.Add("11_K", "Discharge (cfs) 3");
            Dict_FloodBeforeMitigation.Add("12_L", "Recurrence Interval (years) 4");
            Dict_FloodBeforeMitigation.Add("13_M", "Water Surface Elevation (ft) 4");

            Tab_FloodAfterMitigation = new System.Data.DataTable("Flood After Mitigation");
            Tab_FloodAfterMitigation.Columns.Add("1_A", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("2_B", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("3_C", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("4_D", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("5_E", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("6_F", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("7_G", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("8_H", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("9_I", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("10_J", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("11_K", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("12_L", typeof(string));
            Tab_FloodAfterMitigation.Columns.Add("13_M", typeof(string));

            Dict_FloodAfterMitigation.Add("1_A", "Identifier");
            Dict_FloodAfterMitigation.Add("2_B", "Use Default Recurrence Intervals?");
            Dict_FloodAfterMitigation.Add("3_C", "Recurrence Interval (years) 1");
            Dict_FloodAfterMitigation.Add("4_D", "Water Surface Elevation (ft) 1");
            Dict_FloodAfterMitigation.Add("5_E", "Discharge (cfs) 1");
            Dict_FloodAfterMitigation.Add("6_F", "Recurrence Interval (years) 2");
            Dict_FloodAfterMitigation.Add("7_G", "Water Surface Elevation (ft) 2");
            Dict_FloodAfterMitigation.Add("8_H", "Discharge (cfs) 2");
            Dict_FloodAfterMitigation.Add("9_I", "Recurrence Interval (years) 3");
            Dict_FloodAfterMitigation.Add("10_J", "Water Surface Elevation (ft) 3");
            Dict_FloodAfterMitigation.Add("11_K", "Discharge (cfs) 3");
            Dict_FloodAfterMitigation.Add("12_L", "Recurrence Interval (years) 4");
            Dict_FloodAfterMitigation.Add("13_M", "Water Surface Elevation (ft) 4");

            Tab_CriticalFacilityInfo = new System.Data.DataTable("Critical Facility Info");
            Tab_CriticalFacilityInfo.Columns.Add("1_A", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("2_B", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("3_C", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("4_D", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("5_E", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("6_F", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("7_G", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("8_H", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("9_I", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("10_J", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("11_K", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("12_L", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("13_M", typeof(string));
            Tab_CriticalFacilityInfo.Columns.Add("14_N", typeof(string));

            Dict_CriticalFacilityInfo.Add("1_A", "Identifier");
            Dict_CriticalFacilityInfo.Add("2_B", "Critical Facility Type");
            Dict_CriticalFacilityInfo.Add("3_C", "Number of people served (Fire Station)");
            Dict_CriticalFacilityInfo.Add("4_D", "Type of area served (Fire Station)");
            Dict_CriticalFacilityInfo.Add("5_E", "Distance between alternate station (Fire Station)");
            Dict_CriticalFacilityInfo.Add("6_F", "Does fire station provides EMS? (Fire Station)");
            Dict_CriticalFacilityInfo.Add("7_G", "Distance between EMS Station (Fire Station)");
            Dict_CriticalFacilityInfo.Add("8_H", "Number of people served (Hospital)");
            Dict_CriticalFacilityInfo.Add("9_I", "Distance betweeen alternate hospital (Hospital)");
            Dict_CriticalFacilityInfo.Add("10_J", "Number of people served by alternate hospital (Hospital)");
            Dict_CriticalFacilityInfo.Add("11_K", "Type of area served (Police Station)");
            Dict_CriticalFacilityInfo.Add("12_L", "Number of people served (Police Station)");
            Dict_CriticalFacilityInfo.Add("13_M", "Number of police officers working at station (Police Station)");
            Dict_CriticalFacilityInfo.Add("14_N", "Number of police officers working at station if station was shutdown by disaster (Police Station)");

        }

        public static void OpenBCATemplateFile(string path)
        {
            try
            {
                App = new Application();
                App.Visible = true;
                BCAWorkbook = App.Workbooks.Open(path);
            }
            catch
            {
                throw new ApplicationException($"Open BCA Excel template file failed.\n{path}");
            }
        }

        public static Dictionary<int, Building> GetBuildingsIn500YearFloodplain()
        {
            Dictionary<int, Building> l = new Dictionary<int, Building>();
            double depth = 0;
            double? WSEmax500Yr = null;
            double? WSEmaxCurrent = null;
            foreach(int b_key in Buildings.Keys)
            {
                depth = 0;
                WSEmax500Yr = Buildings[b_key].WSEmax["500Yr_Current"];
                WSEmaxCurrent =Buildings[b_key].TerrainElevationFt;

                if (WSEmaxCurrent != null && WSEmax500Yr != null) 
                { 
                    depth = WSEmax500Yr.Value - WSEmaxCurrent.Value;
                }
                if (depth > 0)
                {
                    l.Add(b_key, Buildings[b_key]);
                }
            }
            return l;
        }
        public static Dictionary<int, Building> GetBuildingsIn500YearFloodplainByDepthmax()
        {
            Dictionary<int, Building> l = new Dictionary<int, Building>();
            double? depthMax = 0;
            foreach(int b_key in Buildings.Keys)
            {
                depthMax = Buildings[b_key].Depthmax["500Yr_Current"];
                if (depthMax != null && depthMax > 0)
                {
                    l.Add(b_key, Buildings[b_key]);
                }
            }
            return l;
        }

        public static void SetupBCAInputs(ProgressBar pb, List<Alternative> alts, Alternative selectedAlternative)
        {
            pb.Minimum = 0;
            pb.Maximum = 4;
            pb.Value = 1;

            int column;
            int row;
            Dictionary<int, Building> b_in_500YrFP = GetBuildingsIn500YearFloodplain();
            //int[] building_keys = Buildings.Keys.ToArray();
            int[] building_keys = b_in_500YrFP.Keys.ToArray();

            //Riverine Flood worksheet setup
            var worksheet = BCAWorkbook.Worksheets[BCA_Worksheet1] as Worksheet;
            foreach (var key in Dict_RiverineFlood.Keys)
            {
                int.TryParse(key.Substring(0, key.LastIndexOf("_")), out column);
                string[,] myValues = new string[building_keys.Length, 1];

                switch (column)
                {
                    case 1: //id
                        for (int bid = 0; bid < building_keys.Length; bid++)
                        {
                            myValues[bid, 0] = building_keys[bid].ToString();
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        /*
                        var startCell = worksheet.Cells[2, column];
                        var endCell = worksheet.Cells[2 + myNum.Length - 1, 1];
                        worksheet.Range[startCell, endCell].Value2 = myNum;
                        */
                        break;
                    case 2: //street address
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = Buildings[bid].Address;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        /*
                        var startCell = worksheet.Cells[2, column];
                        var endCell = worksheet.Cells[2 + keys.Length - 1, column];
                        worksheet.Range[startCell, endCell] = locations;
                        */
                        break;
                    case 3: //city
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = Building.City;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 4: //State
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = Building.State;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 5: //ZipCode
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = Building.ZipCode;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 6: //County
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = Building.County;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 7: //Latitude 
                        double? lat;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            lat = Buildings[bid].latitude;
                            myValues[row, 0] = lat == null ? "" : lat.ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 8: //Longitude
                        double? lon;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            lon = Buildings[bid].longitude;
                            myValues[row, 0] = lon == null ? "" : lon.ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 9: //Structure Type
                        string ot = "";
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Res") || ot.StartsWith("Mobi") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = (nameof(EStructureType.Residential_Building)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Fire") || ot.StartsWith("Health") || ot.StartsWith("Polic"))
                            {
                                myValues[row, 0] = (nameof(EStructureType.Critical_Facility_Building)).Replace("_", " ");
                            }
                            else
                            {
                                myValues[row, 0] = (nameof(EStructureType.Non_Residential_Building)).Replace('_', ' ').Replace("Non R", "Non-R");
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 10: //Mitigation Action Type
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = (nameof(EMitigationActionType.Drainage_Improvement)).Replace("_", " ");
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 11: //Project Useful Life
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = "50";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 12: //Mitigation Project Cost $
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = "1.0"; //ToDo: this could be the total cost of project/number of buildings
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 13: //Use default # of years of maintenance
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = nameof(EUseDefaultYearsMaintenance.Yes);
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 14: //# of years of maintenance
                        /*
                        row=0;
                        foreach (int bid in keys)
                        {
                            myValues[row, 0] = "";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        */
                        break;
                    case 15: //Annual maintenance cost
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = "0";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 16: //Lowest floor elevation of property
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            myValues[row, 0] = (Buildings[bid].TerrainElevationFt).ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 17: //Streambed Elevation at Property location
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            //assume streambed is just the terrain elevation as this is in the floodplain???
                            myValues[row, 0] = (Buildings[bid].TerrainElevationFt).ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 18: //Feet lowest floor is being raised
                        row = 0;
                        foreach (int bid in building_keys)
                        {   //Only required if mitigation action is: Elevation
                            myValues[row, 0] = "";
                            row++;
                        }
                        //FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 19: //Elevation for the top of barrier or floodproofing in ft
                        row = 0;
                        foreach (int bid in building_keys)
                        {   //Only required if mitigation action is: floodproofing; others blank
                            myValues[row, 0] = "";
                            row++;
                        }
                        //FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 20: //Building Type (residential)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   //Only required if residential structure; others blank
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeResidential.Manufactured_Home)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Res"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeResidential.One_Story)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeResidential.Two_or_More_Stories)).Replace("_", " ");
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 21: //Building Use (non-residential)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   //Only required if non-residential structure; others blank
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (ot.StartsWith("Apart"))
                            {
                                myValues[row, 0] = Building.DictBuildingUseNonResidential[EBuildingUseNonResidential.COM1];
                            }
                            else if (ot.StartsWith("Comm"))
                            {
                                myValues[row, 0] = Building.DictBuildingUseNonResidential[EBuildingUseNonResidential.COM1];
                            }
                            else if (ot.StartsWith("Muni") || ot.StartsWith("Runw"))
                            {
                                myValues[row, 0] = Building.DictBuildingUseNonResidential[EBuildingUseNonResidential.GOV1];
                            }
                            else if (ot.StartsWith("Fire") || ot.StartsWith("Police"))
                            {
                                myValues[row, 0] = Building.DictBuildingUseNonResidential[EBuildingUseNonResidential.GOV2];
                            }
                            else if (ot.StartsWith("Heal"))
                            {
                                myValues[row, 0] = Building.DictBuildingUseNonResidential[EBuildingUseNonResidential.COM6];
                            }
                            else if (ot.StartsWith("Ind"))
                            {
                                myValues[row, 0] = Building.DictBuildingUseNonResidential[EBuildingUseNonResidential.IND2];
                            }
                            else if (ot.StartsWith("Rec"))
                            {
                                myValues[row, 0] = Building.DictBuildingUseNonResidential[EBuildingUseNonResidential.COM8];
                            }
                            else if (ot.StartsWith("Lib") || ot.StartsWith("Schoo"))
                            {
                                myValues[row, 0] = Building.DictBuildingUseNonResidential[EBuildingUseNonResidential.EDU1];
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 22: //Building type (Non-Residential)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   //Only required if non-residential /critical facility structure; others blank
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (ot.StartsWith("Apart"))
                            {
                                //Apartment is considered as Non-Residential according to FEMA template
                                myValues[row, 0] = nameof(EBuildingTypeNonResidential.Apartment);
                            }
                            else if (ot.StartsWith("Comm"))
                            {
                                myValues[row, 0] = nameof(EBuildingTypeNonResidential.Clothing);
                            }
                            else if (ot.StartsWith("Muni"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Office_One_Story)).Replace("_", " ").Replace("e S", "e-S");
                            }
                            else if (ot.StartsWith("Runw"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Service_Station)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Fire"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Service_Station)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Police"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Service_Station)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Heal"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Medical_Office)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Ind"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Industrial_Light)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Rec"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Recreation)).Replace("_", " ");
                            }
                            else if (ot.StartsWith("Lib") || ot.StartsWith("Schoo"))
                            {
                                myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Schools)).Replace("_", " ");
                            }
                            else
                            {
                                //myValues[row, 0] = (nameof(EBuildingTypeNonResidential.Service_Station)).Replace("_", " ");
                                throw new ApplicationException($"Unknown Non-Residential Structure Type {ot}");
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 23: //Building is outside 100Yr flood area (non-residential/critical facility)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   //Only required if non-residential structure; others blank
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                            }
                            else
                            {
                                if (Buildings[bid].WSEmax.ContainsKey("100Yr_Current") && Buildings[bid].WSEmax["100Yr_Current"] > Buildings[bid].TerrainElevationFt)
                                {
                                    myValues[row, 0] = nameof(EBuildingOutside100YearFloodAreaNonResidentialCriticalFacility.No);
                                }
                                else if (Buildings[bid].Depthmax.ContainsKey("100Yr_Current") && Buildings[bid].Depthmax["100Yr_Current"] > 0)
                                {
                                    myValues[row, 0] = nameof(EBuildingOutside100YearFloodAreaNonResidentialCriticalFacility.No);
                                }
                                else
                                {
                                    myValues[row, 0] = nameof(EBuildingOutside100YearFloodAreaNonResidentialCriticalFacility.Yes);
                                }
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 24: //Building has basement (Residential)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // For residential building: yes/no; other blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi"))
                            {
                                myValues[row, 0] = nameof(EBuildingHasBasementResidential.No);
                            }
                            else if (ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = nameof(EBuildingHasBasementResidential.No); //ToDo: need to determine
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 25: //Building is engineered (Non-Residential/Critical Facility)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // For non-residential building: yes/no; other blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                            }
                            else
                            {
                                myValues[row, 0] = nameof(EBuildingIsEngineeredNonResidentialCriticalFacility.No); //ToDo: need to determine
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 26: //Building has active NFIP policy
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // For all buildings: yes/no;
                            // ToDo: need to determine
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = nameof(EBuildingHasActiveNFIPPolicy.No);
                            }
                            else
                            {
                                myValues[row, 0] = nameof(EBuildingHasActiveNFIPPolicy.No);
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 27: //Damage Curve
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            // ToDo: need to determine
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[6];
                            }
                            else if (ot.StartsWith("Res"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[8];
                            }
                            else if (ot.StartsWith("Apart"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[1];
                            }
                            else if (ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[8];
                            }
                            else if (ot.StartsWith("Comm"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[96];
                            }
                            else if (ot.StartsWith("Muni"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[5];
                            }
                            else if (ot.StartsWith("Runw"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[38];
                            }
                            else if (ot.StartsWith("Fire"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[12];
                            }
                            else if (ot.StartsWith("Police"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[20];
                            }
                            else if (ot.StartsWith("Heal"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[14];
                            }
                            else if (ot.StartsWith("Ind"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[16];
                            }
                            else if (ot.StartsWith("Rec"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[22];
                            }
                            else if (ot.StartsWith("Lib") || ot.StartsWith("Schoo"))
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[27];
                            }
                            else
                            {
                                myValues[row, 0] = Building.DictDamageCurveBuildingTypes[5];
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 28: //First Floor Area (Non-Residential/Critical Facility only) in sqft
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for non-residential and critical facility only, other blank
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                            }
                            else
                            {
                                myValues[row, 0] = (Buildings[bid].FirstFloorAreaSqFt).ToString();
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 29: //Size of building in sq.ft
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures, enter total square footage for the building;
                            // for residential building, only livable area
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                // for residential, the total square footage might be more than just living portion
                                myValues[row, 0] = (Buildings[bid].FirstFloorAreaSqFt).ToString();
                            }
                            else
                            {
                                myValues[row, 0] = (Buildings[bid].FirstFloorAreaSqFt).ToString();
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 30: //use default building replacement value i.e. $100/sqft
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            //ot = Alternative.BuildingOccupancyType[bid];
                            myValues[row, 0] = nameof(EUseDefaultBuildingReplacementValue.Yes);
                            Buildings[bid].UseDefaultBuildingReplacementValue = EUseDefaultBuildingReplacementValue.Yes;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 31: //Building replacement value, leave blank if above is Yes
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            if (Buildings[bid].UseDefaultBuildingReplacementValue == EUseDefaultBuildingReplacementValue.No)
                            {
                                myValues[row, 0] = "150.0";
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 32: //use default demolition threshold, i.e. 50%
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            //ot = Alternative.BuildingOccupancyType[bid];
                            myValues[row, 0] = nameof(EUseDefaultDemolitionThreshold.Yes);
                            Buildings[bid].UseDefaultDemolitionThreshold = EUseDefaultDemolitionThreshold.Yes;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 33: //Demolition threshold value, leave blank if above is Yes
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            if (Buildings[bid].UseDefaultDemolitionThreshold == EUseDefaultDemolitionThreshold.No)
                            {
                                myValues[row, 0] = "60"; // 10% more than default 50%
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 34: //use default building content value
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            //ot = Buildings[bid].OccupancyType;
                            myValues[row, 0] = nameof(EUseDefaultBuildingContentsValue.Yes);
                            Buildings[bid].UseDefaultBuildingContentsValue = EUseDefaultBuildingContentsValue.Yes;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 35: //Building content value ($), leave blank if above is Yes
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            if (Buildings[bid].UseDefaultBuildingContentsValue == EUseDefaultBuildingContentsValue.No)
                            {
                                myValues[row, 0] = "1500.0";
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 36: //Residential building Utilities are elevated
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for residential only,
                            // others blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = nameof(EUtilitiesAreElevatedResidential.Yes);
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 37: //Annual street maintanence budget ($)// for acquisition projects only  (column 10 above)// others blank
                        /*
                        row = 0;
                        foreach (int bid in keys)
                        {   
                            myValues[row, 0] = "";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        */
                        break;
                    case 38: //# street miles maintained// for acquisition projects only  (column 10 above)// others blank
                        /*
                        row = 0;
                        foreach (int bid in keys)
                        {   
                            myValues[row, 0] = "";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        */
                        break;
                    case 39: //# street miles not need maintanence// for acquisition projects only  (column 10 above)// others blank
                        /*
                        row = 0;
                        foreach (int bid in keys)
                        {   
                            myValues[row, 0] = "";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        */
                        break;
                    case 40: //Annual operating budget ($), for non-residential building only; others blank
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for non-residential building only other blank
                            // ToDo: need to determine
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (ot.StartsWith("Apart"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Apartment]).ToString();
                            }
                            else if (ot.StartsWith("Comm"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Commercial]).ToString();
                            }
                            else if (ot.StartsWith("Muni"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Municipal]).ToString();
                            }
                            else if (ot.StartsWith("Runw"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Runway]).ToString();
                            }
                            else if (ot.StartsWith("Fire"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Fire]).ToString();
                            }
                            else if (ot.StartsWith("Police"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Police]).ToString();
                            }
                            else if (ot.StartsWith("Heal"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Health]).ToString();
                            }
                            else if (ot.StartsWith("Ind"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Industrial]).ToString();
                            }
                            else if (ot.StartsWith("Rec"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Recreation]).ToString();
                            }
                            else if (ot.StartsWith("Lib"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.Library]).ToString();
                            }
                            else if (ot.StartsWith("Schoo"))
                            {
                                myValues[row, 0] = (Building.DictBuildingAnnualOperatingBudgetBrookings[EBuildingTypeBrookings.School]).ToString();
                            }
                            row++;
                        }

                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 41: //Use default monthly cost of temporary space
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for non-residential critical facility only,
                            // others blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].UseDefaultMonthlyCostOfTemporarySpace = EUseDefaultMonthlyCostOfTemporarySpace.NA;
                            }
                            else
                            {
                                myValues[row, 0] = nameof(EUseDefaultMonthlyCostOfTemporarySpace.Yes);
                                Buildings[bid].UseDefaultMonthlyCostOfTemporarySpace = EUseDefaultMonthlyCostOfTemporarySpace.Yes;
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 42: //Monthly cost of temp space ($/sqft/month)// for non-residential critical facility only, other blank
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                            }
                            else
                            {
                                myValues[row, 0] = ""; //ToDo: if above is no, then need to enter value building by building
                                if (Buildings[bid].UseDefaultMonthlyCostOfTemporarySpace == EUseDefaultMonthlyCostOfTemporarySpace.No)
                                {
                                    myValues[row, 0] = "500.0";
                                }
                                else
                                {
                                    myValues[row, 0] = "";
                                }
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 43: //Use default one time displacement cost
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for non-residential critical facility only,
                            // others blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].UseDefaultOneTimeDisplacementCost = EUseDefaultOneTimeDisplacementCost.NA;
                            }
                            else
                            {
                                myValues[row, 0] = nameof(EUseDefaultOneTimeDisplacementCost.Yes);
                                Buildings[bid].UseDefaultOneTimeDisplacementCost = EUseDefaultOneTimeDisplacementCost.Yes;
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 44: //One time displacement cost ($/sq.ft)// for non-residential critical facility only, others blank
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                            }
                            else
                            {
                                myValues[row, 0] = ""; //ToDo: if above is no, then need to enter value building by building
                                if (Buildings[bid].UseDefaultOneTimeDisplacementCost == EUseDefaultOneTimeDisplacementCost.No)
                                {
                                    myValues[row, 0] = "500.0";
                                }
                                else
                                {
                                    myValues[row, 0] = "";
                                }
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 45: //Use default lodging per diem, for residential building
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for residential building,
                            // others blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = nameof(EUseDefaultLodgingPerDiem.Yes);
                                Buildings[bid].UseDefaultLodgingPerDiem = EUseDefaultLodgingPerDiem.Yes;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].UseDefaultLodgingPerDiem = EUseDefaultLodgingPerDiem.NA;
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 46: //Current federal lodging per diem ($/night), // leave blank if above is Yes 
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "";
                                if (Buildings[bid].UseDefaultLodgingPerDiem == EUseDefaultLodgingPerDiem.No)
                                {
                                    myValues[row, 0] = "70.0";
                                }
                                else
                                {
                                    myValues[row, 0] = "";
                                }
                            }
                            else
                            {
                                myValues[row, 0] = ""; //ToDo: if above is no, then need to enter value building by building
                            }
                            row++;
                        }
                        //FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 47: //this column is left empty in the FEMA template
                        break;
                    case 48: //Use default Meals per diem (yes/no)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for residential building,
                            // others blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = nameof(EUseDefaultMealsPerDiem.Yes);
                                Buildings[bid].UseDefaultMealsPerDiem = EUseDefaultMealsPerDiem.Yes;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].UseDefaultMealsPerDiem = EUseDefaultMealsPerDiem.NA;
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 49: //Current federal Meals per diem ($/day)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for residential building,
                            // others blank?
                            if (Buildings[bid].UseDefaultMealsPerDiem == EUseDefaultMealsPerDiem.Yes)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].UseDefaultMealsPerDiem == EUseDefaultMealsPerDiem.No)
                            {
                                myValues[row, 0] = "25.0";
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 50: //# of Building residents
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for residential building,
                            // others blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "4"; //typical family of 4, do dogs and cats count
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 51: //# of volunteers required, for all structure types
                        //assume 1 volunteer per building, BCA Tool guide might have more info
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            //ot = Buildings[bid].OccupancyType;
                            myValues[row, 0] = "";
                            Buildings[bid].NumberOfVolunteersRequired = 0;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 52: //# of days lodging for volunteers, if above is greater than 0
                        //assume 3 days lodging per volunteer per building, BCA Tool guide might have more info
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            //ot = Buildings[bid].OccupancyType;
                            if (Buildings[bid].NumberOfVolunteersRequired > 0)
                            {
                                myValues[row, 0] = "3";
                                Buildings[bid].NumberOfDaysLodgingForVolunteers = 3;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].NumberOfDaysLodgingForVolunteers = 0;
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 53: //Use default per-person cost of lodging, (yes/no)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for residential building
                            // others blank?
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = nameof(EUseDefaultPerPersonCostofLodging.Yes);
                                Buildings[bid].UseDefaultPerPersonCostofLodging = EUseDefaultPerPersonCostofLodging.Yes;
                            }
                            else
                            {
                                myValues[row, 0] = nameof(EUseDefaultPerPersonCostofLodging.Yes);
                                Buildings[bid].UseDefaultPerPersonCostofLodging = EUseDefaultPerPersonCostofLodging.Yes;
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 54: // per-person cost of lodging for a volunteer, ($)
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            // blank if above is Yes
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = nameof(EUseDefaultPerPersonCostofLodging.Yes);
                                if (Buildings[bid].UseDefaultPerPersonCostofLodging == EUseDefaultPerPersonCostofLodging.No)
                                {
                                    myValues[row, 0] = "1000.0";
                                }
                                else
                                {
                                    myValues[row, 0] = "";
                                }
                            }
                            else
                            {
                                myValues[row, 0] = nameof(EUseDefaultPerPersonCostofLodging.Yes);
                                if (Buildings[bid].UseDefaultPerPersonCostofLodging == EUseDefaultPerPersonCostofLodging.No)
                                {
                                    myValues[row, 0] = "1000.0";
                                }
                                else
                                {
                                    myValues[row, 0] = "";
                                }
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 55: // # of full time workers in the home, for residential buildings only
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for residential buildings // others blank
                            ot = Buildings[bid].OccupancyType;
                            if (ot.StartsWith("Mobi") || ot.StartsWith("Res") || ot.StartsWith("Deta"))
                            {
                                myValues[row, 0] = "2"; // two working parents
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 56: // if ecosystem services benefit applicable for the project, determine area to be acquired is in Acre or sqft
                        bool needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            ot = Buildings[bid].OccupancyType;
                            //myValues[row, 0] = nameof(EMeasureAcquiredAreaInAcresPerEcosystemServices.No);
                            myValues[row, 0] = "";
                            Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices = EMeasureAcquiredAreaInAcresPerEcosystemServices.NA;
                            if (!string.IsNullOrWhiteSpace(myValues[row, 0]))
                            {
                                needEnterData = true;
                                Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices = EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes;
                            }
                        }
                        if (needEnterData)
                        {
                            FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 57: // if ecosystem services benefit applicable for the project, determine area to be acquired sq.ft or acre 
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            ot = Buildings[bid].OccupancyType;
                            //myValues[row, 0] = nameof(EMeasureAcquiredAreaInAcresPerEcosystemServices.No);
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes)
                            {
                                myValues[row, 0] = ""; //enter acres
                                needEnterData = true;
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = ""; //enter sq.ft
                                needEnterData = true;
                            }
                            row++;
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 58: // if ecosystem services benefit applicable for the project, determine % Green open space
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = "50"; //50% green open space
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 59: // if ecosystem services benefit applicable for the project, determine % Rural Green open space
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = "30"; //30% rural green open space
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 60: // if ecosystem services benefit applicable for the project, determine % riparian
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = "10"; //10% riparian
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 61: // if ecosystem services benefit applicable for the project, determine % Coastal Wetlands
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = ""; // no coastal wetlands
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 62: // if ecosystem services benefit applicable for the project, determine % inland Wetlands
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = ""; // no inland wetlands
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 63: // if ecosystem services benefit applicable for the project, determine % Forest
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = ""; // no forest
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 64: // if ecosystem services benefit applicable for the project, determine % Coral Reefs
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = ""; // no Coral Reefs
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 65: // if ecosystem services benefit applicable for the project, determine % Shellfish Reefs
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = ""; // no Shellfish Reefs
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 66: // if ecosystem services benefit applicable for the project, determine %  Beaches and Dunes
                        needEnterData = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {
                            // for all struture types, could leave blank if no ecosystem benefit
                            if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.NA)
                            {
                                myValues[row, 0] = "";
                            }
                            else if (Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.Yes ||
                                     Buildings[bid].MeasureAcquiredAreaInAcresPerEcosystemServices == EMeasureAcquiredAreaInAcresPerEcosystemServices.No)
                            {
                                myValues[row, 0] = ""; // no Beaches and Dunes
                                needEnterData = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        if (needEnterData)
                        {
                            //FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                }
            }
            pb.Value++;

            //Flood Before Mitigation Setup
            worksheet = BCAWorkbook.Worksheets[BCA_Worksheet2] as Worksheet;
            SetupRiverineFloodTemplateFloodBeforeAfterMitigation(worksheet, building_keys, "XYr_Current");
            pb.Value++;

            //Flood After Mitigation Setup
            worksheet = BCAWorkbook.Worksheets[BCA_Worksheet3] as Worksheet;
            SetupRiverineFloodTemplateFloodBeforeAfterMitigation(worksheet, building_keys, selectedAlternative.Name);
            pb.Value++;

            //Critical Facility Info Setup
            worksheet = BCAWorkbook.Worksheets[BCA_Worksheet4] as Worksheet;
            SetupRiverineFloodTemplateCriticalFacilityInfo(worksheet, building_keys);
            pb.Value++;

            //BCAWorkbook.Close();
            //App.Quit();
        }

        public static void SetupRiverineFloodTemplateFloodBeforeAfterMitigation(Worksheet worksheet, int[] building_keys, string alt_name)
        {
            string alt_scenario_name = alt_name.Substring(alt_name.IndexOf("_") + 1);

            //Flood Before and After Mitigation keys are the same, so could use either one for iteration below.
            int column = 0;
            int row;
            foreach (var key in Dict_FloodBeforeMitigation.Keys)
            {
                int.TryParse(key.Substring(0, key.LastIndexOf("_")), out column);
                string[,] myValues = new string[building_keys.Length, 0];

                switch (column)
                {
                    case 1: //id
                        row = 0;
                        for (int bid = 0; bid < building_keys.Length; bid++)
                        {
                            myValues[row, 0] = building_keys[bid].ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 2: //Use default recurrent intervals? (Yes/No) 10- 50- 100- 500-year events
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            //var ot = Buildings[bid].OccupancyType;
                            myValues[row, 0] = nameof(EUseDefaultRecurrenceIntervals.Yes);
                            Buildings[bid].UseDefaultRecurrenceIntervals = EUseDefaultRecurrenceIntervals.Yes;
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 3: //Recurrence Interval (years) 1
                        bool needDataEntry = false;
                        int years1 = 10; // can change to some other year
                        int years1Default = 10;
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            if (Buildings[bid].UseDefaultRecurrenceIntervals == EUseDefaultRecurrenceIntervals.No)
                            {
                                myValues[row, 0] = years1.ToString();
                                Buildings[bid].RecurrenceIntervalYears1 = years1;
                                needDataEntry = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].RecurrenceIntervalYears1 = years1Default;
                            }
                            row++;
                        }
                        if (needDataEntry)
                        {
                            FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 4: //Water Surface Elevation 1 in feet
                        string alt_key = "";
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            alt_key = Buildings[bid].RecurrenceIntervalYears1 + "Yr_" + alt_scenario_name;
                            myValues[row, 0] = Buildings[bid].WSEmax[alt_key].ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 5: //Discharge (cfs) 1
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            myValues[row, 0] = "0.0";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 6: //Recurrence Interval (years) 2
                        int years2 = 50; //change to some other years
                        int years2Default = 50;
                        needDataEntry = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            if (Buildings[bid].UseDefaultRecurrenceIntervals == EUseDefaultRecurrenceIntervals.No)
                            {
                                myValues[row, 0] = years2.ToString();
                                Buildings[bid].RecurrenceIntervalYears2 = years2;
                                needDataEntry = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].RecurrenceIntervalYears2 = years2Default;
                            }
                            row++;
                        }
                        if (needDataEntry)
                        {
                            FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 7: //Water Surface Elevation 2 in feet
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            alt_key = Buildings[bid].RecurrenceIntervalYears2 + "Yr_" + alt_scenario_name;
                            myValues[row, 0] = Buildings[bid].WSEmax[alt_key].ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 8: //Discharge (cfs) 2
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            myValues[row, 0] = "0.0";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 9: //Recurrence Interval (years) 3
                        int years3 = 100; //change to some other years
                        int years3Default = 100;
                        needDataEntry = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            if (Buildings[bid].UseDefaultRecurrenceIntervals == EUseDefaultRecurrenceIntervals.No)
                            {
                                myValues[row, 0] = years3.ToString();
                                Buildings[bid].RecurrenceIntervalYears3 = years3;
                                needDataEntry = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].RecurrenceIntervalYears3 = years3Default;
                            }
                            row++;
                        }
                        if (needDataEntry)
                        {
                            FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 10: //Water Surface Elevation 3 in feet
                        alt_key = "";
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            alt_key = Buildings[bid].RecurrenceIntervalYears3 + "Yr_" + alt_scenario_name;
                            myValues[row, 0] = Buildings[bid].WSEmax[alt_key].ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 11: //Discharge (cfs) 3
                        alt_key = "";
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            myValues[row, 0] = "0.0";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 12: //Recurrence Interval (years) 4
                        int years4 = 500; //change to some other years
                        int years4Default = 500;
                        needDataEntry = false;
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            if (Buildings[bid].UseDefaultRecurrenceIntervals == EUseDefaultRecurrenceIntervals.No)
                            {
                                myValues[row, 0] = years4.ToString();
                                Buildings[bid].RecurrenceIntervalYears4 = years4;
                                needDataEntry = true;
                            }
                            else
                            {
                                myValues[row, 0] = "";
                                Buildings[bid].RecurrenceIntervalYears4 = years4Default;
                            }
                            row++;
                        }
                        if (needDataEntry)
                        {
                            FillColumnRiverineFlood(worksheet, column, myValues);
                        }
                        break;
                    case 13: //Water Surface Elevation 4 in feet
                        alt_key = "";
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            alt_key = Buildings[bid].RecurrenceIntervalYears4 + "Yr_" + alt_scenario_name;
                            myValues[row, 0] = Buildings[bid].WSEmax[alt_key].ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 14: //Discharge (cfs) 4
                        alt_key = "";
                        row = 0;
                        foreach (int bid in building_keys)
                        {   // for all structures
                            myValues[row, 0] = "0.0";
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                }
            }
        }

        public static void SetupRiverineFloodTemplateCriticalFacilityInfo(Worksheet worksheet, int[] building_keys)
        {
            int column = 0;
            //get a list of only the critical buildings
            List<Building> critical_buildings = new List<Building>();
            foreach (int bid in building_keys)
            {   //Only required if critical facility structure; others skip
                var ot = Buildings[bid].OccupancyType;
                if (ot.StartsWith("Fire"))
                {
                    critical_buildings.Add(Buildings[bid]);
                }
                else if (ot.StartsWith("Police"))
                {
                    critical_buildings.Add(Buildings[bid]);
                }
                else if (ot.StartsWith("Heal"))
                {
                    critical_buildings.Add(Buildings[bid]);
                }
                else
                {
                    continue;
                }
            }

            foreach (var key in Dict_CriticalFacilityInfo.Keys)
            {
                int.TryParse(key.Substring(0, key.LastIndexOf("_")), out column);
                string[,] myValues = new string[critical_buildings.Count, 0];

                switch (column)
                {
                    case 1: //id
                        int row = 0;
                        foreach (Building b in critical_buildings)
                        {
                            myValues[row, 0] = b.BID.ToString();
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 2: //Critical Facility Type, enum
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = (nameof(ECriticalFacilityType.Fire_Station)).Replace("_", " ");
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = (nameof(ECriticalFacilityType.Police_Station)).Replace("_", " ");
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = nameof(ECriticalFacilityType.Hospital);
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 3: //# of people served (Fire Station)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = "2000"; //ToDo: need to determine
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 4: //Type of area served (Fire Station)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = nameof(EFireStationServiceAreaType.Urban); //ToDo: need to determine
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 5: //Distance between alternate station in miles (Fire Station)
                        row = 0;
                        int distance = 15; //miles, ToDo: need to determine
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = distance.ToString();
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 6: //Does fire station provides EMS, Yes/No (Fire Station)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = nameof(EFireStationProvidesEMS.No); //todo: need to determine
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row ++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 7: //Distance between EMS station, in miles (Fire Station)
                        row = 0;
                        distance = 15; //ToDo: need to determine
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = distance.ToString();
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 8: //# of people served (Hospital)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "2500";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 9: //Distance between alternate hospital, in miles (Hospital)
                        row = 0;
                        distance = 25; //ToDo: need to be determined
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = distance.ToString();
                            }
                            row ++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 10: //# of people served by alternate hospital (Hospital)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "2500"; //ToDo: need to be determined
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 11: //Type of area served (Police Station)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = nameof(EPoliceStationServiceAreaType.Urban); //ToDo: need to be determined
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 12: //# of people served (Police Station)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "2500"; //ToDo: need to be determined
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 13: //# of police officers working at station (Police Station)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "15"; //ToDo: need to be determined
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                    case 14: //# of police officers working at station if shutdown by disaster (Police Station)
                        row = 0;
                        foreach (Building b in critical_buildings)
                        {   //Only required if critical facility structure; others skip
                            if (b.OccupancyType.StartsWith("Fire"))
                            {
                                myValues[row, 0] = "";
                            }
                            else if (b.OccupancyType.StartsWith("Police"))
                            {
                                myValues[row, 0] = "5"; //ToDo: need to be determined
                            }
                            else if (b.OccupancyType.StartsWith("Heal"))
                            {
                                myValues[row, 0] = "";
                            }
                            row++;
                        }
                        FillColumnRiverineFlood(worksheet, column, myValues);
                        break;
                }
            }
        }


        public static void FillColumnRiverineFlood(Worksheet worksheet, int column, string[,] data)
        {
            var keys = Buildings.Keys.ToArray();
            var startCell = worksheet.Cells[2, column];
            var endCell = worksheet.Cells[2 + keys.Length - 1, column];
            worksheet.Range[startCell, endCell] = data;
        }
    }
}
