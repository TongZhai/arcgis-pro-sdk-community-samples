using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    internal class BCAMATH : IMath
    {
        internal atcData.atcTimeseries depths { get; set; }

        internal List<double> dataset;

        public override List<double> GetData()
        {
                if (depths != null)
                {
                    return depths.Values.ToList();
                }
                return dataset;
        }

        public override void SetData(List<double> data)
        {
                depths = new atcData.atcTimeseries();
            if (data.Count > 1)
            {
                depths.numValues = data.Count - 1;
                for(int i = 0; i <= depths.numValues ; i++)
                {
                    depths.Values[i] = data[i];
                }
            }
            else if (data.Count == 1)
            {
                depths.numValues = 1;
                depths.Values[0] = data[0];
                depths.Values[1] = double.NaN;
            }
                
        }

        public override double Min()
        {
            return GetData().Min();
        }

        public override double Max()
        {
            return GetData().Max();
        }

        public override double Mean()
        {
            return GetData().Average();
        }

        public override double Median()
        {
            return GetData().OrderBy(x => x).Skip(GetData().Count/2).First();
        }

        public override double Percentile(double x)
        {
            double depth_pct = double.NaN;
            try
            {
                depth_pct = (double)depths.Attributes.GetValue($"%{(int)x}");
                return depth_pct;
            }
            catch
            {
                return double.NaN;
            }
        }

        public override double StandardDeviation()
        {
            double depth_std = double.NaN;
            try
            {
                depth_std = (double)depths.Attributes.GetValue("standard deviation");
                return depth_std;
            }
            catch
            {
                return double.NaN;
            }
        }
    }
}
