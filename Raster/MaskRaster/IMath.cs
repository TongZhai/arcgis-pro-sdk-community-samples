using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    public abstract class IMath
    {
        public abstract List<double> GetData();
        public abstract void SetData(List<double> data);
        public abstract double Min();
        public abstract double Max();
        public abstract double Median();
        public abstract double Mean();
        public abstract double StandardDeviation();
        public abstract double Percentile(double x);
    }
}
