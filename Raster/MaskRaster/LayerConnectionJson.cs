using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MaskRaster
{
    public class LayerConnectionJson
    {
        public string type;
        public string workspaceConnectionString;
        public string workspaceFactory;
        public string dataset;
        public string datasetType;
    }
}
