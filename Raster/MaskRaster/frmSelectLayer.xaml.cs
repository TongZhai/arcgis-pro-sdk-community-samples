using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Core.Data.Raster;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Core.Data;
using System.IO;
using ArcGIS.Desktop.Core;
using Microsoft.Win32;

namespace MaskRaster
{
    /// <summary>
    /// Interaction logic for frmSelectLayer.xaml
    /// </summary>
    public partial class frmSelectLayer : Window
    {
        List<Alternative> _Alternatives;

        public frmSelectLayer(List<Alternative> alternatives)
        {
            _Alternatives = alternatives;
            InitializeComponent();
            listAlternatives();
        }

        private void listAlternatives()
        {
            this.cboAlternatives.DisplayMemberPath = "Name";
            this.cboAlternatives.Items.Clear();
            foreach (var alt in _Alternatives)
            {
                this.cboAlternatives.Items.Add(alt);
            }
            txtFIAOutputDir.Text = Alternative.basefolderfia;
        }

        private void btnLoadGridLayers_Click(object sender, RoutedEventArgs e)
        {
            if (MapView.Active == null)
            {
                System.Windows.MessageBox.Show("No active ArcGIS Pro Mapview available.");
                return;
            }
            if (_Alternatives == null || _Alternatives.Count == 0)
            {
                System.Windows.MessageBox.Show("No Alternatives available.");
                return;
            }

            var layers = MapView.Active.Map.GetLayersAsFlattenedList(); //.OfType<FeatureLayer>().Where(fl => fl.Name.Contains(xlsLayerName)).FirstOrDefault();
            int numLayersAdded = 0;

            foreach (var alt in _Alternatives)
            {
                var datatype = GridDataType.WSEMAX;
                if (rdoDepthMax.IsChecked == true)
                {
                    datatype = GridDataType.DEPTHMAX;
                }
                if (rdoGridTerrain.IsChecked == true)
                {
                    datatype = GridDataType.TERRAIN;
                }

                if (alt.isPathSet(datatype) && layers.Where(fl => fl.Name == alt.layerName(datatype)).FirstOrDefault() == null)
                {
                    AddLayer(alt.fullpath(datatype));
                    numLayersAdded++;
                }
            }
            System.Windows.MessageBox.Show($"Number of layers added: {numLayersAdded}.");
            //this.Close();
        }

        public Task<Layer> AddLayer(string uri)
        {
            return QueuedTask.Run(() =>
            {
                Map map = MapView.Active.Map;
                return LayerFactory.Instance.CreateLayer(new Uri(uri), map);
            });
        }

        private void btnReadGridLayers_Click(object sender, RoutedEventArgs e)
        {
            var mapView = MapView.Active;
            FeatureLayer buildingFootprint = null;
            if (cboVectorLayers.SelectedItem != null)
            {
                if (cboVectorLayers.Items.Count == 1)
                {
                    buildingFootprint = (cboVectorLayers.SelectedItem as List<FeatureLayer>)[0] as FeatureLayer;
                }
                else
                {
                    buildingFootprint = cboVectorLayers.SelectedItem as FeatureLayer;
                }

            }
            if (buildingFootprint == null)
            {
                System.Windows.MessageBox.Show("Select a building footprint vector layer first.");
                return;
            }
            var gridDataType = rdoDepthMax.IsChecked == true ? GridDataType.DEPTHMAX : GridDataType.WSEMAX;
            if (rdoGridTerrain.IsChecked == true)
            {
                gridDataType = GridDataType.TERRAIN;
            }
            MaskRasterVM.ReadRaster(buildingFootprint, gridDataType);

        }

        private void btnGetVectors_Click(object sender, RoutedEventArgs e)
        {
            var mapView = MapView.Active;
            var lyr_list = mapView.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>().ToList();
            this.cboVectorLayers.DisplayMemberPath = "Name";
            this.cboVectorLayers.Items.Clear();
            foreach (FeatureLayer layer in lyr_list)
            {
                this.cboVectorLayers.Items.Add(layer);
            }
        }

        private void btnBrowseBCATemplate_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse BCA Riverine Flood Template File",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xlsx",
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (fd.ShowDialog() == true)
            {
                txtBCARiverineFloodTemplateFilePath.Text = fd.FileName;
            }
        }

        private void btnSetupBCAInputsv5_Click(object sender, RoutedEventArgs e)
        {
            BCA.OpenBCATemplateFile(txtBCARiverineFloodTemplateFilePath.Text);
            BCA.SetupBCAInputs(_Alternatives, cboAlternatives.SelectedItem as Alternative);
        }

        private void btnSetupBCAInputsv6_Click(object sender, RoutedEventArgs e)
        {
            /*
            foreach(var ddf in BCA.DDFs.Values)
            {
                var depths_s = ddf.DDFStructure.Keys;
                var values_s = ddf.DDFStructure.Values;
                var depths_c = ddf.DDFContent.Keys;
                var values_c = ddf.DDFContent.Values;
                var depths_d = ddf.DDFDisplacement.Keys;
                var values_d = ddf.DDFDisplacement.Values;
            }
            foreach(var b in BCA.Buildings.Values)
            {
                foreach(var alt in _Alternatives)
                {
                    var min = b.BCADepthmaxStatistics[alt.Name].Min();
                    var max = b.BCADepthmaxStatistics[alt.Name].Max();
                    var mean = b.BCADepthmaxStatistics[alt.Name].Mean();
                    var median = b.BCADepthmaxStatistics[alt.Name].Median();
                    var std = b.BCADepthmaxStatistics[alt.Name].StandardDeviation();
                    var pct90 = b.BCADepthmaxStatistics[alt.Name].Percentile(90);
                }
            }
            foreach(var p in BCA.Parcels)
            {
                foreach(var alt in _Alternatives)
                {
                    var min = p.BCAMaths[alt.Name].Min();
                    var max = p.BCAMaths[alt.Name].Max();
                    var mean = p.BCAMaths[alt.Name].Mean();
                    var median = p.BCAMaths[alt.Name].Median();
                    var std = p.BCAMaths[alt.Name].StandardDeviation();
                    var pct90 = p.BCAMaths[alt.Name].Percentile(90);
                }
            }
            */
            BCA.SetupBCAv6Worksheet(_Alternatives, cboAlternatives.SelectedItem as Alternative);
        }

        private async void cboVectorLayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FeatureLayer buildingFootprint = null;
            if (cboVectorLayers.SelectedItem != null)
            {
                if (cboVectorLayers.Items.Count == 1)
                {
                    buildingFootprint = (cboVectorLayers.SelectedItem as List<FeatureLayer>)[0] as FeatureLayer;
                }
                else
                {
                    buildingFootprint = cboVectorLayers.SelectedItem as FeatureLayer;
                }

            }
            if (buildingFootprint == null)
            {
                return;
            }
            listAttributes.Items.Clear();
            IReadOnlyList<Field> fields = null;
            await QueuedTask.Run(async () =>
            {
                try
                {
                    fields = buildingFootprint.GetTable().GetDefinition().GetFields();
                }
                catch (Exception ex)
                {
                    fields = null;
                }
            });
            while (fields != null)
            {
                foreach (var f in fields)
                {
                    listAttributes.Items.Add(f.Name);
                }
                fields = null;
            }
        }

        private void btnReadFIAOutputs_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(txtFIAOutputDir.Text)) 
            {
                System.Windows.MessageBox.Show($"Please specify the top level FIA analysis folder that contains the \\run folder.");
                //txtFIAOutputDir.Focus();
                return; 
            }
            BCA.ReadFIAOutputs(_Alternatives);
        }

        private void btnReadDDFs_Click(object sender, RoutedEventArgs e)
        {
            Util.GetConfigDamlDDFs();
        }

        private async Task<string> ReadTRCNs()
        {
            /*
            BCAInputsProgress.Minimum = 0;
            BCAInputsProgress.Maximum = 100;
            BCAInputsProgress.Value = 0;
            var myprogress = new Progress<MyProgress>();
            myprogress.ProgressChanged += ( s, e ) =>
            {
               BCAInputsProgress.Value = e.ProgressPercentage;
               //txtResult.Text += e.Text;
            };
            //return await BCA.SetupParcels(myprogress);
            */
            return "";
        }

        private void btnReadParcelTRCNs_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BCA.FilepathParcelTRCN) || !File.Exists(BCA.FilepathParcelTRCN))
            {
                System.Windows.MessageBox.Show("Need to specify Parcel TRCN data file path, please read the Depth-Damage Function data first.");
                return;
            }
            if (!File.Exists(BCA.FilepathParcelTRCN))
            {
                System.Windows.MessageBox.Show("Need to specify Parcel TRCN data file path, suggest double-check the 'parcel' setting in the Depth-Damage Function data configuration.");
                return;
            }

            BCA.SetupParcels(null);
        }

        /***
         * Perform one time tasks on as needed basis
         * ***/
        private void btnCustomOpn_Click(object sender, RoutedEventArgs e)
        {
            var mapView = MapView.Active;

            /* Task 1. cross-check building's Parcel_ID and Parcel_Hyp againt City provided Parcel shapefile
            var lyr_list_footprint = mapView.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>().ToList().Where(f => f.Name.StartsWith("BuildingFootprints_SMC"));
            var lyr_list_parcel = mapView.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>().ToList().Where(f => f.Name.StartsWith("MissingParcels_RESPEC"));
            if (lyr_list_footprint.Any() && lyr_list_parcel.Any())
            {
                foreach(var lf in lyr_list_footprint)
                {
                    foreach(var lp in lyr_list_parcel)
                    {
                        MaskRasterVM.CrosscheckParcelIDs(lf, lp);
                        break;
                    }
                    break;
                }
            }
            */

            /* Task 2. For floodway analysis, read the alternative floodway model's WSEmax at all of the profile locations */
            var lyr_profile = mapView.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>().ToList().Where(f => f.Name.StartsWith("Floodway_Profiles")).FirstOrDefault();
            if (lyr_profile != null)
            {
                MaskRasterVM.ReadWSEs(lyr_profile, GridDataType.WSEMAX);
                System.Windows.MessageBox.Show("Done.");
            }
        }

        private void btnLoadFWWSEmax_Click(object sender, RoutedEventArgs e)
        {
            if (MapView.Active == null)
            {
                System.Windows.MessageBox.Show("No active ArcGIS Pro Mapview available.");
                return;
            }
            if (MaskRasterVM.Alternatives_FW == null || MaskRasterVM.Alternatives_FW.Count == 0)
            {
                System.Windows.MessageBox.Show("No Floodway Alternatives available.");
                return;
            }

            var layers = MapView.Active.Map.GetLayersAsFlattenedList(); //.OfType<FeatureLayer>().Where(fl => fl.Name.Contains(xlsLayerName)).FirstOrDefault();
            int numLayersAdded = 0;

            var datatype = GridDataType.WSEMAX;
            foreach (var alt in MaskRasterVM.Alternatives_FW)
            {
                if (alt.isPathSet(datatype) && layers.Where(fl => fl.Name == alt.layerName(datatype)).FirstOrDefault() == null)
                {
                    AddLayer(alt.fullpathfloodway(datatype));
                    numLayersAdded++;
                }
            }
            System.Windows.MessageBox.Show($"Number of layers added: {numLayersAdded}.");
        }

        private void btnReportWSEmax_Click(object sender, RoutedEventArgs e)
        {
            txtReport.Text = MaskRasterVM.WriteWSEMaxTable();
        }
    }

}
