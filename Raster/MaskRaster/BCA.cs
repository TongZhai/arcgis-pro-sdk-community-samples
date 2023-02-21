using ArcGIS.Desktop.Internal.Mapping.Locate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    internal class BCA
    {

        public static string BCA_Worksheet1 = "Riverine Flood";
        public static string BCA_Worksheet2 = "Flood Before Mitigation";
        public static string BCA_Worksheet3 = "Flood After Mitigation";
        public static string BCA_Worksheet4 = "Critical Facility Info";

        public System.Data.DataTable Tab_RiverineFlood;
        public System.Data.DataTable Tab_FloodBeforeMitigation;
        public System.Data.DataTable Tab_FloodAfterMitigation;
        public System.Data.DataTable Tab_CriticalFacilityInfo;

        public Dictionary<string, string> Dict_RiverineFlood;
        public Dictionary<string, string> Dict_FloodBeforeMitigation;
        public Dictionary<string, string> Dict_FloodAfterMitigation;
        public Dictionary<string, string> Dict_CriticalFacilityInfo;

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
    }
}
