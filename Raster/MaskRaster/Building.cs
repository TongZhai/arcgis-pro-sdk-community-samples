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
        Yes,
        No
    }

    public enum EOutside100YearFloodAreaNonResidentialCriticalFacility
    {
        Yes,
        No
    }

    public enum EBuildingHasBasementResidential
    {
        Yes,
        No
    }

    public enum EBuildingIsEngineeredNonResidentialCriticalFacility
    {
        Yes,
        No,
    }

    public enum EBuildingHasActiveNFIPPolicy
    {
        Yes,
        No,
    }

    public enum EUseDefaultBuildingReplacementValue
    {
        Yes, No,
    }

    public enum EUseDefaultDemolitionThreshold
    {
        Yes, No,
    }

    public enum EUseDefaultBuildingContentsValue
    {
        Yes, No,
    }

    public enum EUtilitiesAreElevatedResidential
    {
        Yes,
        No,
    }

    public enum EUseDefaultMonthlyCostOfTemporarySpace
    {
        Yes,
        No,
    }

    public enum EUseDefaultOneTimeDisplacementCost
    {
        Yes, No,
    }

    public enum EUseDefaultLodgingPerDiem
    {
        Yes,
        No,
    }

    public enum EUseDefaultMealsPerDiem
    {
        Yes,
        No,
    }

    public enum EUseDefaultPerPersonCostofLodging
    {
        Yes,
        No,
    }

    public enum EMeasureAcquiredAreaInAcresPerEcosystemServices
    {
        Yes,
        No,
    }

    public enum EUseDefaultRecurrenceIntervals
    {
        Yes, No,
    }

    public enum EFireStationProvidesEMS
    {
        Yes,
        No,
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

    public enum EBuildingUseNonResidential
    {
        COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM10,
        IND1, IND2, IND3, IND4, IND5, IND6,
        AGR1,
        REL1,
        GOV1, GOV2,
        EDU1, EDU2
    }


    internal class Building
    {
        /* shared attributes of buildings */
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

        public static List<string> DamageCurveBuildingTypes = new List<string>()
        {
            "Apartment",
            "Convenience Store",
            "Correctional Facility",
            "Fast Food",
            "FEMA FIA",
            "FEMA FIA, 1-Story, No Basement",
            "FEMA FIA, 1-Story, With Basement",
            "FEMA FIA, 2-Story, No Basement",
            "FEMA FIA, 2-Story, With Basement",
            "FEMA FIA, Split Level, No Basement",
            "FEMA FIA, Split Level, With Basement",
            "Fire Station",
            "Grocery",
            "Hospital",
            "Hotel",
            "Industrial Light",
            "Medical Office",
            "Non-Fast Food",
            "Office One-Story",
            "Police Station",
            "Protective Services",
            "Recreation",
            "Religious Facilities",
            "Retail-Clothing",
            "Retail-Electronics",
            "Retail-Furniture",
            "Schools",
            "Service Station",
            "USACE - Chicago: Apartment Unit Grade",
            "USACE - Chicago: Apartment Unit Sub-Grade",
            "USACE - Chicago: Mobile Home",
            "USACE - Chicago: one story, no basement",
            "USACE - Chicago: one story, w/ basement",
            "USACE - Chicago: split level, no basement",
            "USACE - Chicago: split level, w/ basement",
            "USACE - Chicago: two story, no basement",
            "USACE - Chicago: two story, w/ basement",
            "USACE - Galveston: Airport",
            "USACE - Galveston: Apartment, living area on one floor",
            "USACE - Galveston: City Hall",
            "USACE - Galveston: Condominium, living area on multiple floors",
            "USACE - Galveston: Doctor's Office",
            "USACE - Galveston: Fire Station",
            "USACE - Galveston: Food Warehouse",
            "USACE - Galveston: Hospital",
            "USACE - Galveston: Hotel",
            "USACE - Galveston: Library",
            "USACE - Galveston: Mobile Home",
            "USACE - Galveston: Motel Unit",
            "USACE - Galveston: Nursing Home",
            "USACE - Galveston: one & 1/2 story, no basement",
            "USACE - Galveston: one story, no basement",
            "USACE - Galveston: Police Station",
            "USACE - Galveston: Post Office",
            "USACE - Galveston: School",
            "USACE - Galveston: two story, no basement",
            "USACE - Galveston: Warehouse",
            "USACE - New Orleans: College, structure, fresh water, short duration",
            "USACE - New Orleans: College, structure, salt water, long duration",
            "USACE - New Orleans: Department Store, structure, fresh water, short duration",
            "USACE - New Orleans: Department Store, structure, salt water, long duration",
            "USACE - New Orleans: Elementary school, structure, fresh water, short duration",
            "USACE - New Orleans: Elementary school, structure, salt water, long duration",
            "USACE - New Orleans: Government facility, structure, fresh water, short duration",
            "USACE - New Orleans: Government facility, structure, salt water, long duration",
            "USACE - New Orleans: Large Grocery,  structure, fresh water, short duration",
            "USACE - New Orleans: Large Grocery, structure, salt water, long duration",
            "USACE - New Orleans: Medical Office, structure, fresh water, short duration",
            "USACE - New Orleans: Medical Office, structure, salt water, long duration",
            "USACE - New Orleans: one story, Pier foundation, structure, fresh water, short duration",
            "USACE - New Orleans: one story, Pier foundation, structure, salt water, long duration",
            "USACE - New Orleans: one story, Slab foundation, structure, fresh water, short duration",
            "USACE - New Orleans: one story, Slab foundation, structure, salt water, long duration",
            "USACE - New Orleans: two story, Pier foundation, structure, fresh water, short duration",
            "USACE - New Orleans: two story, Pier foundation, structure, salt water, long duration",
            "USACE - New Orleans: two story, Slab foundation, structure, fresh water, short duration",
            "USACE - New Orleans: two story, Slab foundation, structure, salt water, long duration",
            "USACE - New Orleans: Utility Company, structure, fresh water, short duration",
            "USACE - New Orleans: Utility Company, structure, salt water, long duration",
            "USACE - New Orleans: Warehouse, structure, fresh water, short duration",
            "USACE - New Orleans: Warehouse, structure, salt water, long duration",
            "USACE - St. Paul: one story",
            "USACE - St. Paul: two story",
            "USACE - Wilmington: Mobile Home",
            "USACE - Wilmington: one & 1/2 story",
            "USACE - Wilmington: one & 1/2 story w/ 1/2 living area below",
            "USACE - Wilmington: one & 1/2 story, Pile foundation",
            "USACE - Wilmington: one story",
            "USACE - Wilmington: one story w/ 1/2 living area below",
            "USACE - Wilmington: one story w/ basement",
            "USACE - Wilmington: one story, Pile foundation",
            "USACE - Wilmington: split level",
            "USACE - Wilmington: two story",
            "USACE - Wilmington: two story w/ 1/2 living area below",
            "USACE - Wilmington: two story, Pile foundation",
            "USACE Generic",
            "Warehouse, Non-Refrig",
            "Warehouse, Refrig",
        };

        public int BID;
        public double WSEmax;
        public double Depthmax;

        public double latitude;
        public double longitude;

        public EOutside100YearFloodAreaNonResidentialCriticalFacility Outside100YearFloodAreaNonResidentialOnly; // Yes/No, for non-residential building only, determine displacement duration
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
        public double FirstFloorAreaSqFt; // non-residential and critical structure; single story = footprint of building
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



        public void func() {
            string p = DictBuildingUseNonResidential[EBuildingUseNonResidential.EDU1];
        }
    }
}
