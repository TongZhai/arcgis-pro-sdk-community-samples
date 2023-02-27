using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskRaster
{
    public class DamageUSACE: Damage
    {
        public override double Curve()
        {
            if (OccupancyType.StartsWith("Res"))
            {
            }
            else if (OccupancyType.StartsWith("Mobi"))
            {
            }
            else if (OccupancyType.StartsWith("Deta"))
            {
            }
            else if (OccupancyType.StartsWith("Comm"))
            {
            }
            else if (OccupancyType.StartsWith("School"))
            {
            }
            else if (OccupancyType.StartsWith("Police"))
            {
            }
            else if (OccupancyType.StartsWith("Fire"))
            {
            }
            else if (OccupancyType.StartsWith("Hospi") || OccupancyType.StartsWith("Heal"))
            {
            }
            else if (OccupancyType.StartsWith("Edu"))
            {
            }
            else if (OccupancyType.StartsWith("Apart"))
            {
            }

            return 0;
        }
    }
}
