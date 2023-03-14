using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    public enum EStructureType
    {
        Residential_Building,
        Non_Residential_Building,  //Non-Residential Building
        Critical_Facility_Building
    }

    public enum ECriticalFacilityType
    {
        Fire_Station,
        Hospital,
        Police_Station
    }

    public enum EFireStationServiceAreaType
    {
        Urban,
        Suburban,
        Rural,
        Wilderness
    }

    public enum EPoliceStationServiceAreaType
    {
        Urban,
        City,
        Rural
    }

    public enum EMitigationActionType
    {
        Acquisition,
        Elevation,
        Floodproofing_Measures,
        Drainage_Improvement,
        Floodplain_and_Stream_Restoration,
        Floodwater_Diversion_and_Storage
    }

    public enum EUseDefaultYearsMaintenance
    {
        NA,
        Yes,
        No
    }

    public enum EBuildingOutside100YearFloodAreaNonResidentialCriticalFacility
    {
        NA,
        Yes,
        No
    }

    public enum EBuildingHasBasementResidential
    {
        NA,
        Yes,
        No
    }

    public enum EBuildingIsEngineeredNonResidentialCriticalFacility
    {
        NA,
        Yes,
        No,
    }

    public enum EBuildingHasActiveNFIPPolicy
    {
        NA, Yes, No,
    }

    public enum EUseDefaultBuildingReplacementValue
    {
        NA, Yes, No,
    }

    public enum EUseDefaultDemolitionThreshold
    {
        NA, Yes, No,
    }

    public enum EUseDefaultBuildingContentsValue
    {
        NA, Yes, No,
    }

    public enum EUtilitiesAreElevatedResidential
    {
        NA, Yes, No,
    }

    public enum EUseDefaultMonthlyCostOfTemporarySpace
    {
        NA,
        Yes,
        No,
    }

    public enum EUseDefaultOneTimeDisplacementCost
    {
        NA, Yes, No,
    }

    public enum EUseDefaultLodgingPerDiem
    {
        NA,
        Yes,
        No,
    }

    public enum EUseDefaultMealsPerDiem
    {
        NA,
        Yes,
        No,
    }

    public enum EUseDefaultPerPersonCostofLodging
    {
        NA, Yes, No,
    }

    public enum EMeasureAcquiredAreaInAcresPerEcosystemServices
    {
        NA, Yes, No,
    }

    public enum EUseDefaultRecurrenceIntervals
    {
        NA, Yes, No,
    }

    public enum EFireStationProvidesEMS
    {
        NA, Yes, No,
    }

    public enum EBuildingTypeResidential
    {
        One_Story,
        Two_or_More_Stories,
        Split_Level,
        Manufactured_Home
    }

    public enum EBuildingTypeNonResidential
    {
        Apartment,
        Clothing,
        Convenience_Store,
        Correctional_Facility,
        Electronics,
        Fast_Food,
        Furniture,
        Grocery,
        Hotel,
        Industrial_Light,
        Medical_Office,
        Non_Fast_Food, //Non-Fast Food
        Office_One_Story, //Office One-Story
        Recreation,
        Religious_Facilities,
        Schools,
        Service_Station,
        Warehouse_Non_Refrig, //Warehouse-Non-Refrig
        Warehouse_Refrig, //Warehous-Refrig
    }

    public enum EBuildingTypeBrookings
    {
        Apartment,
        Commercial,
        Detached,
        Fire,
        Health,
        Industrial,
        Library,
        Mobile,
        Municipal,
        Police,
        Recreation,
        Residential,
        Runway,
        School,
        Unknown,
    }

    public enum EBuildingUseNonResidential
    {
        COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM10,
        IND1, IND2, IND3, IND4, IND5, IND6,
        AGR1,
        REL1,
        GOV1, GOV2,
        EDU1, EDU2
    }

    public class Building
    {
        /* shared attributes of buildings */

        public static List<Alternative> Alternatives;

        public static string City = "Brookings";
        public static string ZipCode = "57006";
        public static string County = "Brookings";
        public static string State = "South Dakota";

        public static Dictionary<EBuildingUseNonResidential, string> DictBuildingUseNonResidential = new Dictionary<EBuildingUseNonResidential, string>() {
            { EBuildingUseNonResidential.COM1, "COM1: Commercial - Retail Trade" },
            { EBuildingUseNonResidential.COM2, "COM2: Commercial - Wholesale Trade" },
            { EBuildingUseNonResidential.COM3, "COM3: Commercial - Personal and Repair Services" },
            { EBuildingUseNonResidential.COM4, "COM4: Commercial - Professional/Technical/Business Services" },
            { EBuildingUseNonResidential.COM5, "COM5: Commercial - Banks" },
            { EBuildingUseNonResidential.COM6, "COM6: Commercial - Hospital" },
            { EBuildingUseNonResidential.COM7, "COM7: Commercial - Medical Office/Clinic" },
            { EBuildingUseNonResidential.COM8, "COM8: Commercial - Entertainment and Recreation" },
            { EBuildingUseNonResidential.COM9, "COM9: Commercial - Theaters" },
            { EBuildingUseNonResidential.COM10, "COM10: Parking" },
            { EBuildingUseNonResidential.IND1, "IND1: Industrial - Heavy" },
            { EBuildingUseNonResidential.IND2, "IND2: Industrial - Light" },
            { EBuildingUseNonResidential.IND3, "IND3: Industrial - Food/Drugs/Chemicals" },
            { EBuildingUseNonResidential.IND4, "IND4: Industrial - Metals/Minerals Processing" },
            { EBuildingUseNonResidential.IND5, "IND5: Industrial - High Technology" },
            { EBuildingUseNonResidential.IND6, "IND6: Industrial - Construction" },
            { EBuildingUseNonResidential.AGR1, "AGR1: Agricultural - Buildings" },
            { EBuildingUseNonResidential.REL1, "REL1: Religious/Non-Profit - Church or Membership Organization" },
            { EBuildingUseNonResidential.GOV1, "GOV1: Government - General Services" },
            { EBuildingUseNonResidential.GOV2, "GOV2: Government - Emergency Response" },
            { EBuildingUseNonResidential.EDU1, "EDU1: Education - Schools/Libraries" },
            { EBuildingUseNonResidential.EDU2, "EDU2: Education - Colleges/Universities" },
        };

        public static Dictionary<int, string> DictDamageCurveBuildingTypes = new Dictionary<int, string>()
        {
            {1, "Apartment"},
            {2, "Convenience Store"},
            {3, "Correctional Facility"},
            {4, "Fast Food"},
            {5, "FEMA FIA"},
            {6, "FEMA FIA, 1-Story, No Basement"},
            {7, "FEMA FIA, 1-Story, With Basement"},
            {8, "FEMA FIA, 2-Story, No Basement"},
            {9, "FEMA FIA, 2-Story, With Basement"},
            {10, "FEMA FIA, Split Level, No Basement"},
            {11, "FEMA FIA, Split Level, With Basement"},
            {12, "Fire Station"},
            {13, "Grocery"},
            {14, "Hospital"},
            {15, "Hotel"},
            {16, "Industrial Light"},
            {17, "Medical Office"},
            {18, "Non-Fast Food"},
            {19, "Office One-Story"},
            {20, "Police Station"},
            {21, "Protective Services"},
            {22, "Recreation"},
            {23, "Religious Facilities"},
            {24, "Retail-Clothing"},
            {25, "Retail-Electronics"},
            {26, "Retail-Furniture"},
            {27, "Schools"},
            {28, "Service Station"},
            {29, "USACE - Chicago: Apartment Unit Grade"},
            {30, "USACE - Chicago: Apartment Unit Sub-Grade"},
            {31, "USACE - Chicago: Mobile Home"},
            {32, "USACE - Chicago: one story, no basement"},
            {33, "USACE - Chicago: one story, w/ basement"},
            {34, "USACE - Chicago: split level, no basement"},
            {35, "USACE - Chicago: split level, w/ basement"},
            {36, "USACE - Chicago: two story, no basement"},
            {37, "USACE - Chicago: two story, w/ basement"},
            {38, "USACE - Galveston: Airport"},
            {39, "USACE - Galveston: Apartment, living area on one floor"},
            {40, "USACE - Galveston: City Hall"},
            {41, "USACE - Galveston: Condominium, living area on multiple floors"},
            {42, "USACE - Galveston: Doctor's Office"},
            {43, "USACE - Galveston: Fire Station"},
            {44, "USACE - Galveston: Food Warehouse"},
            {45, "USACE - Galveston: Hospital"},
            {46, "USACE - Galveston: Hotel"},
            {47, "USACE - Galveston: Library"},
            {48, "USACE - Galveston: Mobile Home"},
            {49, "USACE - Galveston: Motel Unit"},
            {50, "USACE - Galveston: Nursing Home"},
            {51, "USACE - Galveston: one & 1/2 story}, no basement"},
            {52, "USACE - Galveston: one story, no basement"},
            {53, "USACE - Galveston: Police Station"},
            {54, "USACE - Galveston: Post Office"},
            {55, "USACE - Galveston: School"},
            {56, "USACE - Galveston: two story, no basement"},
            {57, "USACE - Galveston: Warehouse"},
            {58, "USACE - New Orleans: College, structure, fresh water, short duration"},
            {59, "USACE - New Orleans: College, structure, salt water, long duration"},
            {60, "USACE - New Orleans: Department Store, structure, fresh water, short duration"},
            {61, "USACE - New Orleans: Department Store, structure, salt water, long duration"},
            {62, "USACE - New Orleans: Elementary school, structure, fresh water, short duration"},
            {63, "USACE - New Orleans: Elementary school, structure, salt water, long duration"},
            {64, "USACE - New Orleans: Government facility, structure, fresh water, short duration"},
            {65, "USACE - New Orleans: Government facility, structure, salt water, long duration"},
            {66, "USACE - New Orleans: Large Grocery,  structure, fresh water, short duration"},
            {67, "USACE - New Orleans: Large Grocery, structure, salt water, long duration"},
            {68, "USACE - New Orleans: Medical Office, structure, fresh water, short duration"},
            {69, "USACE - New Orleans: Medical Office, structure, salt water, long duration"},
            {70, "USACE - New Orleans: one story, Pier foundation, structure, fresh water, short duration"},
            {71, "USACE - New Orleans: one story, Pier foundation, structure, salt water, long duration"},
            {72, "USACE - New Orleans: one story, Slab foundation, structure, fresh water, short duration"},
            {73, "USACE - New Orleans: one story, Slab foundation, structure, salt water, long duration"},
            {74, "USACE - New Orleans: two story, Pier foundation, structure, fresh water, short duration"},
            {75, "USACE - New Orleans: two story, Pier foundation, structure, salt water, long duration"},
            {76, "USACE - New Orleans: two story, Slab foundation, structure, fresh water, short duration"},
            {77, "USACE - New Orleans: two story, Slab foundation, structure, salt water, long duration"},
            {78, "USACE - New Orleans: Utility Company, structure, fresh water, short duration"},
            {79, "USACE - New Orleans: Utility Company, structure, salt water, long duration"},
            {80, "USACE - New Orleans: Warehouse, structure, fresh water, short duration"},
            {81, "USACE - New Orleans: Warehouse, structure, salt water, long duration"},
            {82, "USACE - St. Paul: one story"},
            {83, "USACE - St. Paul: two story"},
            {84, "USACE - Wilmington: Mobile Home"},
            {85, "USACE - Wilmington: one & 1/2 story"},
            {86, "USACE - Wilmington: one & 1/2 story w/ 1/2 living area below"},
            {87, "USACE - Wilmington: one & 1/2 story}, Pile foundation"},
            {88, "USACE - Wilmington: one story"},
            {89, "USACE - Wilmington: one story w/ 1/2 living area below"},
            {90, "USACE - Wilmington: one story w/ basement"},
            {91, "USACE - Wilmington: one story}, Pile foundation"},
            {92, "USACE - Wilmington: split level"},
            {93, "USACE - Wilmington: two story"},
            {94, "USACE - Wilmington: two story w/ 1/2 living area below"},
            {95, "USACE - Wilmington: two story, Pile foundation"},
            {96, "USACE Generic"},
            {97, "Warehouse, Non-Refrig"},
            {98, "Warehouse, Refrig"},
        };

        public static Dictionary<EBuildingTypeBrookings, double> DictBuildingAnnualOperatingBudgetBrookings = new Dictionary<EBuildingTypeBrookings, double>() {
            { EBuildingTypeBrookings.Apartment, 10000 },
            { EBuildingTypeBrookings.Commercial, 10000 },
            { EBuildingTypeBrookings.Detached, 10000 },
            { EBuildingTypeBrookings.Fire, 10000 },
            { EBuildingTypeBrookings.Health, 10000 },
            { EBuildingTypeBrookings.Industrial, 10000 },
            { EBuildingTypeBrookings.Library, 10000 },
            { EBuildingTypeBrookings.Mobile, 10000 },
            { EBuildingTypeBrookings.Municipal, 10000 },
            { EBuildingTypeBrookings.Police, 10000 },
            { EBuildingTypeBrookings.Recreation, 10000 },
            { EBuildingTypeBrookings.Residential, 10000 },
            { EBuildingTypeBrookings.Runway, 10000 },
            { EBuildingTypeBrookings.School, 10000 },
        };

        public int BID;

        public Dictionary<string, double> WSEmax; //alternative_ID -> WSEmax
        public Dictionary<string, double> Depthmax; //alternative_ID -> Depthmax
        public Dictionary<string, double> Terrain; //alternative_ID -> Terrain

        public Dictionary<string, DamageUSACE> Damages; //alternative_ID -> DamageUSACE ($)
        public Dictionary<string, IMath> BCADepthmaxStatistics; //alternative_ID-> IMath
        public Dictionary<string, IMath> BCAWSEmaxStatistics; //alternative_ID-> IMath

        //***** read once ****
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public double? FirstFloorAreaSqFt; // non-residential and critical structure; single story = footprint of building
        public string OccupancyType { get; set; } //this is the raw building type or occupancy type per local government designation
        public string Address { get; set; }
        public string ParcelID { get; set; }
        //***** read once ****

        public EBuildingOutside100YearFloodAreaNonResidentialCriticalFacility BuildingOutside100YearFloodAreaNonResidentialOnly; // Yes/No, for non-residential building only, determine displacement duration
        public EStructureType StructureType;
        public EUseDefaultYearsMaintenance UseDefaultYearsMaintenance;
        public EBuildingTypeNonResidential BuildingTypeNonResidential;
        public EBuildingTypeResidential BuildingTypeResidential;
        public EBuildingUseNonResidential BuildingUseNonResidential;
        public EMitigationActionType MitigationActionType;
        public EBuildingHasBasementResidential BuildingHasBasementResidential;
        public EBuildingIsEngineeredNonResidentialCriticalFacility BuildingIsEngineeredNonResidentialCriticalFacility; //for non-residential, determine default percent content damage
        public EBuildingHasActiveNFIPPolicy BuildingHasActiveNFIPPolicy; //whether has a flood insurance policy from the National Flood Insurance Program
        public string DamageCurve;
        public double SizeofBuildingSqFt; //total living square footage
        public EUseDefaultBuildingReplacementValue UseDefaultBuildingReplacementValue; // For all building types, default = $100 per sqft
        public double BuildingReplacementValuePerSqFt; // if UseDefaultbuildingReplacementValue is Yes, then leave blank
        public EUseDefaultDemolitionThreshold UseDefaultDemolitionThreshold; // for all building types, default is 50%
        public double DemolitionthresholdPCT; // for building types, if UseDefaultDemolitionThreshold = Yes, leave blank
        public EUseDefaultBuildingContentsValue UseDefaultBuildingContentsValue; //for Flood module, Yes=Defult=Total_Building_Rep_Value, i.e. BRV/sf * Building Size
        public double BuildingContentsValue; // leave blank if above is set to 'Yes'
        public EUtilitiesAreElevatedResidential UtilitiesAreElevatedResidential; // for residential buildings only
        public double AnnualStreetMaintenanceBudget; // only applies to large acquisition project; can leave blank
        public double StreetMilesMaintained; // only applies to large acquisition project; can leave blank
        public double StreetMilesNotMaintained; // only applies to large acquisition project; can leave blank
        public double AnnualOperatingBudgetNonResidential; //for non-residential building only; otherwise leave blank
        public EUseDefaultMonthlyCostOfTemporarySpace UseDefaultMonthlyCostOfTemporarySpace; // Yes: use default; No: enter higher monthly rental cost
        public double MonthlyCostOfTemporarySpacePerSqFt; // leave blank if above is Yes
        public EUseDefaultOneTimeDisplacementCost UseDefaultOneTimeDisplacementCost; // Yes, default; No: enter a higher monthly rental cost
        public double OneTimeDisplacementCostPerSqFt; //leave blank if above is Yes
        public EUseDefaultLodgingPerDiem UseDefaultLodgingPerDiem; // for residential buildings
        public double CurrentFederalLodgingPerDiemPerNight; // leave blank if above is Yes

        //Column AU is blank

        public EUseDefaultMealsPerDiem UseDefaultMealsPerDiem; // for residential buildings, based on location
        public double CurrentFederalMealsPerDiemPerDay; // for residential buildings; leave blank if above is Yes;
        public int NumberOfBuildingResidentsResidential; // census data -> residential building occupants
        public int NumberOfVolunteersRequired; // for all structure types; BCA Tool guidance
        public int NumberOfDaysLodgingForVolunteers; // must enter value if above is > 0
        public EUseDefaultPerPersonCostofLodging UseDefaultPerPersonCostofLodging; //Yes: per person cost of lodging is accurate
        public double PerPersonCostofLodgingPerVolunteer; //Must have a value if above is No; leave blank if above is Yes
        public int NumberofFullTimeWorkersInResidentialBuilding;
        public EMeasureAcquiredAreaInAcresPerEcosystemServices MeasureAcquiredAreaInAcresPerEcosystemServices; //if ecosystem services applies-> measure in Acres (Yes); measure in SqFt (No)
        public double TotalAcquiredProjectAreaSize; // acres or sqft based on choice above
        public double UrbanGreenOpenSpacePercent; // % area maintained as urban green open space
        public double RuralGreenOpenSpacePercent; // % area maintained as rural green open space
        public double RiparianAreaPercent; // % area allowed to return to a natural riparian area
        public double CoastalWetlandsPercent; // % area that will change to a coastal wetland
        public double InlandWetlandsPercent; // % area change to an inland (non-coastal) wetland
        public double ForestPercent; // % area allowed to change to a natural forest
        public double CoralReefsPercent; // % area will change to a coral reef
        public double ShellfishReefsPercent; // % area will change to a shellfish reef
        public double BeachesandDunesPercent; // % area will change to a beach or dune

        //Flood Before Mitigation
        public EUseDefaultRecurrenceIntervals UseDefaultRecurrenceIntervals; //Yes: 10-, 50-, 100-, 500-year events; No: other RIs
        public int RecurrenceIntervalYears1;  // blank if above is Yes
        public int RecurrenceIntervalYears2;  // blank if above is Yes
        public int RecurrenceIntervalYears3;  // blank if above is Yes
        public int RecurrenceIntervalYears4;  // blank if above is Yes

        public double WSE_RI_1_Ft_Before; // water surface elevation for the first recurrence interval (in feet).
        public double WSE_RI_2_Ft_Before; // water surface elevation for the second recurrence interval (in feet).
        public double WSE_RI_3_Ft_Before; // water surface elevation for the third recurrence interval (in feet).
        public double WSE_RI_4_Ft_Before; // water surface elevation for the fourth recurrence interval (in feet).

        public double Discharge_RI_1_cfs_Before; // discharge for the first recurrence interval (cfs).
        public double Discharge_RI_2_cfs_Before; // discharge for the second recurrence interval (cfs).
        public double Discharge_RI_3_cfs_Before; // discharge for the third recurrence interval (cfs).
        public double Discharge_RI_4_cfs_Before; // discharge for the fourth recurrence interval (cfs).

        public double WSE_RI_1_Ft_After; // water surface elevation for the first recurrence interval (in feet).
        public double WSE_RI_2_Ft_After; // water surface elevation for the second recurrence interval (in feet).
        public double WSE_RI_3_Ft_After; // water surface elevation for the third recurrence interval (in feet).
        public double WSE_RI_4_Ft_After; // water surface elevation for the fourth recurrence interval (in feet).

        public double Discharge_RI_1_cfs_After; // discharge for the first recurrence interval (cfs).
        public double Discharge_RI_2_cfs_After; // discharge for the second recurrence interval (cfs).
        public double Discharge_RI_3_cfs_After; // discharge for the third recurrence interval (cfs).
        public double Discharge_RI_4_cfs_After; // discharge for the fourth recurrence interval (cfs).

        //Critical Facility Info
        public ECriticalFacilityType CriticalFacilityType;
        public int NumberPeopleServedFireStation;
        public EFireStationServiceAreaType FireStationServiceAreaType;
        public double FireStationAlternateStationDistanceInMiles;
        public EFireStationProvidesEMS fireStationProvidesEMS; //Yes: if fire station being mitigated also provide EMS
        public double FireStationEMSAlternateStationDistanceInMiles;

        public int NumberPeopleServedHospital;
        public double HospitalAlternateDistanceInMiles;
        public int NumberPeopleServedHospitalAlternate;

        public EPoliceStationServiceAreaType PoliceStationServiceAreaType;
        public int NumberPeopleServicedPoliceStation;
        public int NumberPoliceOfficerWorkingatStation;
        public int NumberPoliceOfficerWorkingatStationWhenShutdownByDisaster;


        public Building()
        {
            WSEmax = new Dictionary<string, double>();
            Depthmax = new Dictionary<string, double>();
            Terrain = new Dictionary<string, double>();
            Damages = new Dictionary<string, DamageUSACE>();
            BCADepthmaxStatistics = new Dictionary<string, IMath>();
        }

        
    }
}
