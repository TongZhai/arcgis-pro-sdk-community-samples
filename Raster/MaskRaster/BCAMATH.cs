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

        /***
         * Set the series of data, with the ability to append (i.e., append)
         ***/
        public override void SetData(List<double> data, bool append = false)
        {
            dataset = new List<double>();
            if (append)
            {
                // if append, then preserve the existing set of values
                if (depths != null && depths.numValues > 0)
                {
                    dataset.AddRange(depths.Values);
                }
            }
            dataset.AddRange(data);

            if (depths != null)
            {
                //free up memory
                depths.Clear();
            }
            depths = new atcData.atcTimeseries();

            if (dataset.Count > 1)
            {
                depths.numValues = dataset.Count - 1;
                for (int i = 0; i <= depths.numValues; i++)
                {
                    depths.Values[i] = dataset[i];
                }
            }
            else if (dataset.Count == 1)
            {
                depths.numValues = 1;
                depths.Values[0] = dataset[0];
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
            return GetData().OrderBy(x => x).Skip(GetData().Count / 2).First();
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

        public void Clear()
        {
            if (depths != null)
            {
                depths.numValues = 0;
                depths.Clear();
            }

            if (dataset != null)
            {
                dataset.Clear();
            }
        }
    }
}
