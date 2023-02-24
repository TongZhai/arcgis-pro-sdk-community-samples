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
            this.Close();
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

        private void btnSetupBCAInputs_Click(object sender, RoutedEventArgs e)
        {
            BCA.OpenBCATemplateFile(txtBCARiverineFloodTemplateFilePath.Text);
            BCA.SetupBCAInputs(BCAInputsProgress, _Alternatives, cboAlternatives.SelectedItem as Alternative);
        }
    }

}
