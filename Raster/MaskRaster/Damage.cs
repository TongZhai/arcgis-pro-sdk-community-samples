using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    public abstract class Damage
    {
        public double Struct;
        public double Content; //FEMA BCA Category 1
        public double Car; //FEMA BCA Category 2
        public double Other; //FEMA BCA Category 3
        public double PARDU65;
        public double PARNU65;
        public double PARDO65;
        public double PARNO65;

        public double Depth;

        public string CategoryName;
        public string OccupancyType;
        public string StructName;
        public string AreaName;

        public int RecurrenceIntervalYears;

        public abstract double Curve();
    }
}
