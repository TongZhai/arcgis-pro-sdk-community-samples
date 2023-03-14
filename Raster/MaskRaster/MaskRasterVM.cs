//   Copyright 2019 Esri
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       https://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Windows.Media.Animation;
using ArcGIS.Core.Internal.Geometry;

namespace MaskRaster
{
    /// <summary>
    /// Viewmodel class that allows functions to mask raster pixels to be UI agnostic.
    /// </summary>
    static class MaskRasterVM
    {
        static int _bandindex = 0;

        public static List<Alternative> Alternatives;

        public static PixelBlock GetPixelBlock(Raster inputRaster, Geometry geometry)
        {
            // Use the MapToPixel method of the input raster to get the row and column values for the 
            // points of the rectangle.
            bool ispoint = geometry.GeometryType == GeometryType.Point;
            double buffer = ispoint ? 1.0 : 0.0;
            var XMin = geometry.Extent.XMin - buffer;
            var XMax = geometry.Extent.XMax + buffer;
            var YMin = geometry.Extent.YMin - buffer;
            var YMax = geometry.Extent.YMax + buffer;
            Tuple<int, int> tlcTuple = inputRaster.MapToPixel(XMin, YMin);
            Tuple<int, int> lrcTuple = inputRaster.MapToPixel(XMax, YMax);

            int minCol = (int)tlcTuple.Item1;
            int minRow = (int)tlcTuple.Item2;
            int maxCol = (int)lrcTuple.Item1;
            int maxRow = (int)lrcTuple.Item2;

            // Ensure the min's are less than the max's.
            if (maxCol < minCol)
            {
                int temp = maxCol;
                maxCol = minCol;
                minCol = temp;
            }

            if (maxRow < minRow)
            {
                int temp = maxRow;
                maxRow = minRow;
                minRow = temp;
            }

            // Ensure the mins and maxs are within the raster.
            minCol = (minCol < 0) ? 0 : minCol;
            minRow = (minRow < 0) ? 0 : minRow;
            maxCol = (maxCol > inputRaster.GetWidth()) ? inputRaster.GetWidth() : maxCol;
            maxRow = (maxRow > inputRaster.GetHeight()) ? inputRaster.GetHeight() : maxRow;

            // Calculate the width and height of the pixel block to create.
            int pbWidth = maxCol - minCol;
            int pbHeight = maxRow - minRow;

            // Check to see if the output raster can be edited.
            /*
            if (!inputRaster.CanEdit())
            {
                // If not, show a message box with the appropriate message.
                MessageBox.Show("Cannot edit raster :(");
                return null;
            }
            */

            // Create a new pixel block from the output raster of the height and width calculated above.
            try
            {
                PixelBlock currentPixelBlock = inputRaster.CreatePixelBlock(pbWidth, pbHeight);
                return currentPixelBlock;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Mask raster pixels based on the rectangle given and save the output in the 
        /// current project folder.
        /// </summary>
        /// <param name="geometry">Rectangle to use to mask raster pixels.</param>
        public static async void MaskRaster(Geometry geometry)
        {
            //string lyrname = "BuildingFootprints_Centers_SMC";
            string lyrname = "BuildingFootprints_SMC";
            try
            {
                // Check if there is an active map view.
                if (MapView.Active != null)
                {
                    // Get the active map view.
                    var mapView = MapView.Active;
                    var list = mapView.Map.FindLayers(lyrname);
                    var lyr_list = mapView.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>().ToList();

                    if (list == null || list.Count == 0)
                    {
                        MessageBox.Show("No Building Footprint Layer found. Please add it.");
                        return;
                    }
                    var buildingfp = list.First();
                    var featbuildingfp = lyr_list.Where(ll => ll.Name == lyrname).FirstOrDefault();
                    BasicFeatureLayer lyr_fp = buildingfp as BasicFeatureLayer;



                    // Get the list of selected layers.
                    IReadOnlyList<Layer> selectedLayerList = mapView.GetSelectedLayers();
                    if (selectedLayerList.Count == 0)
                    {
                        // If no layers are selected show a message box with the appropriate message.
                        MessageBox.Show("No Layers selected. Please select one Raster layer.");
                    }
                    else
                    {
                        // Get the most recently selected layer.                
                        Layer firstSelectedLayer = mapView.GetSelectedLayers().First();
                        if (firstSelectedLayer is RasterLayer)
                        {
                            // Working with rasters requires the MCT.
                            await QueuedTask.Run(() =>
                            {

                                if (buildingfp.ConnectionStatus == ConnectionStatus.Broken)
                                    throw new ApplicationException("Footprint layer connection broken");
                                //GeodatabaseType? gdbType = GeodatabaseType.FileSystem;

                                #region Get the raster dataset from the currently selected layer
                                // Get the raster layer from the selected layer.
                                RasterLayer currentRasterLayer = firstSelectedLayer as RasterLayer;
                                // Get the raster from the current selected raster layer.
                                Raster inputRaster = currentRasterLayer.GetRaster();
                                // Get the basic raster dataset from the raster.
                                BasicRasterDataset basicRasterDataset = inputRaster.GetRasterDataset();
                                if (!(basicRasterDataset is RasterDataset))
                                {
                                    // If the dataset is not a raster dataset, show a message box with the appropriate message.
                                    MessageBox.Show("No Raster Layers selected. Please select one Raster layer.");
                                    return;
                                }
                                // Get the input raster dataset from the basic raster dataset.
                                RasterDataset rasterDataset = basicRasterDataset as RasterDataset;
                                #endregion

                                FeatureClass featfp = featbuildingfp.GetFeatureClass();
                                long nCount = featfp.GetCount();

                                #region Save a copy of the raster dataset in the project folder and open it
                                // Create a full raster from the input raster dataset.
                                inputRaster = rasterDataset.CreateFullRaster();

                                // If the map spatial reference is different from the spatial reference of the input raster,
                                // set the map spatial reference on the input raster. This will ensure the map points are 
                                // correctly reprojected to image points.
                                if (mapView.Map.SpatialReference.Name != inputRaster.GetSpatialReference().Name)
                                    inputRaster.SetSpatialReference(mapView.Map.SpatialReference);


                                using (var rc = featfp.Search())
                                {
                                    while (rc.MoveNext())
                                    {
                                        using (var record = rc.Current)
                                        {
                                            // var s = record[1].ToString(); <- this is wrong
                                            Feature f = record as Feature;
                                            Geometry shape = f.GetShape();
                                            /* 
                                            s = Convert.ToString(record["Location"]);
                                            Console.WriteLine(s);
                                            IReadOnlyList<Field> fs = f.GetFields();
                                            Console.WriteLine("got to here"); 
                                            */
                                            int pixelBlockWidth = Convert.ToInt32(shape.Extent.XMax - shape.Extent.XMin);
                                            int pixelBlockHeight = Convert.ToInt32(shape.Extent.YMax - shape.Extent.YMin);

                                            RasterBand rb = inputRaster.GetBand(0);
                                            //var rt = rb.GetAttributeTable();
                                            //PixelBlock pb = inputRaster.CreatePixelBlock(pixelBlockWidth, pixelBlockHeight);

                                            //Method 1: read raster data within a polygon shape geometry
                                            // determine the cursor position in mapping coordinates
                                            //var pixelLocationAtRaster = inputRaster.MapToPixel(shape_pt.X, shape_pt.Y);
                                            PixelBlock pb = GetPixelBlock(inputRaster, shape);

                                            var shpTopLeftCornerAtRaster = inputRaster.MapToPixel(shape.Extent.XMin, shape.Extent.YMax);
                                            // fill the pb (PixelBlock) with the pointer location
                                            inputRaster.Read(shpTopLeftCornerAtRaster.Item1, shpTopLeftCornerAtRaster.Item2, pb);
                                            var rasValueArray = pb.GetPixelData(0, false);
                                            Console.WriteLine("Stop here");
                                            /*
                                            if (pb != null)
                                            {
                                                inputRaster.Read(pixelLocationAtRaster.Item1, pixelLocationAtRaster.Item2, pb);
                                                double v = Convert.ToDouble(pb.GetValue(0, 0, 0));
                                                Array va = pb.GetPixelData(0, false);
                                                double sum = 0.0;
                                                double num = 0.0;
                                                foreach (float v1 in va)
                                                {
                                                    if (v1 == -9999) continue;
                                                    num++; sum+= v1;
                                                }
                                                if (num > 0)
                                                {
                                                    v = sum/ num;
                                                }
                                                //double vaa = (from val in va select val).Average();
                                                //double vaa = va.Where(d => !double.IsNaN(d.Value)).Average();
                                                Console.WriteLine(v.ToString());
                                            }
                                            */

                                            //Method 2: read raster data by a point with fixed window
                                            // create a pixelblock representing a 3x3 window to hold the raster values
                                            var pixelBlock = inputRaster.CreatePixelBlock(3, 3);

                                            // determine the cursor position in mapping coordinates
                                            //var shape_x = shape.Extent.CenterCoordinate.ToMapPoint().X;
                                            //var shape_y = shape.Extent.CenterCoordinate.ToMapPoint().Y;

                                            //var shape_pt = shape.Extent.CenterCoordinate.ToMapPoint();
                                            var shape_ctrpt = shape.Extent.CenterCoordinate.ToMapPoint();

                                            /*
                                            var clientCoords = new System.Windows.Point(e.ClientPoint.X, e.ClientPoint.Y);
                                            if (mapView == null) return;
                                            var mapPointAtCursor = mapView.ClientToMap(clientCoords);
                                            if (mapPointAtCursor == null) return;
                                            */

                                            // create a container to hold the pixel values
                                            //Array pixelArray = new object[pixelBlock.GetWidth(), pixelBlock.GetHeight()];
                                            Array pixelArray = new object[pixelBlock.GetWidth(), pixelBlock.GetHeight()];

                                            // reproject the raster envelope to match the map spatial reference
                                            var rasterEnvelope = GeometryEngine.Instance.Project(inputRaster.GetExtent(), inputRaster.GetSpatialReference());

                                            // if the cursor is within the extent of the raster
                                            if (GeometryEngine.Instance.Contains(rasterEnvelope, shape_ctrpt))
                                            {
                                                // find the map location expressed in row,column of the raster
                                                var pixelLocationAtRaster = inputRaster.MapToPixel(shape_ctrpt.X, shape_ctrpt.Y);

                                                // fill the pixelblock with the pointer location
                                                inputRaster.Read(pixelLocationAtRaster.Item1 - 1, pixelLocationAtRaster.Item2 - 1, pixelBlock);

                                                var _bandindex = 0;

                                                if (_bandindex != -1)
                                                {
                                                    // retrieve the actual pixel values from the pixelblock representing the red raster band
                                                    pixelArray = pixelBlock.GetPixelData(_bandindex, false);
                                                    Console.WriteLine("Stop here");
                                                }
                                            }
                                            else
                                            {
                                                // fill the container with 0s
                                                Array.Clear(pixelArray, 0, pixelArray.Length);
                                            }


                                            int[] intNumbers = new int[] { 60, 80, 50, 90, 10, 30, 70, 40, 20, 100 };
                                            //Using Method Syntax
                                            var MSAverageValue = intNumbers.Where(num => num > 50).Average();
                                        }
                                    }
                                }

                                // Setup the paths and name of the output file and folder inside the project folder.
                                string ouputFolderName = "MaskedOuput";
                                string outputFolder = Path.Combine(Project.Current.HomeFolderPath, ouputFolderName); ;
                                string outputName = "MaskedRaster.tif";
                                // Delete the output directory if it exists and create it.
                                // Note: You will need write access to the project directory for this sample to work.
                                if (Directory.Exists(outputFolder))
                                    Directory.Delete(outputFolder, true);
                                Directory.CreateDirectory(outputFolder);

                                // Create a new file system connection path to open raster datasets using the output folder path.
                                FileSystemConnectionPath outputConnectionPath = new FileSystemConnectionPath(
                                new System.Uri(outputFolder), FileSystemDatastoreType.Raster);
                                // Create a new file system data store for the connection path created above.
                                FileSystemDatastore outputFileSytemDataStore = new FileSystemDatastore(outputConnectionPath);
                                // Create a new raster storage definition. 
                                RasterStorageDef rasterStorageDef = new RasterStorageDef();
                                // Set the pyramid level to 0 meaning no pyramids will be calculated. This is required 
                                // because we are going to change the pixels after we save the raster dataset and if the 
                                // pyramids are calculated prior to that, the pyramids will be incorrect and will have to
                                // be recalculated.
                                rasterStorageDef.SetPyramidLevel(0);
                                // Save a copy of the raster using the file system data store and the raster storage definition.
                                inputRaster.SaveAs(outputName, outputFileSytemDataStore, "TIFF", rasterStorageDef);

                                // Open the raster dataset you just saved.
                                rasterDataset = OpenRasterDataset(outputFolder, outputName);
                                // Create a full raster from it so we can modify pixels.
                                Raster outputRaster = rasterDataset.CreateFullRaster();
                                #endregion

                                #region Get the Min/Max Row/Column to mask
                                PixelBlock currentPixelBlock = GetPixelBlock(inputRaster, geometry);
                                // Iterate over the bands of the output raster.
                                for (int plane = 0; plane < currentPixelBlock.GetPlaneCount(); plane++)
                                {
                                    // For each band, clear the pixel block.
                                    currentPixelBlock.Clear(plane);
                                    //Array noDataMask = currentPixelBlock.GetNoDataMask(plane, true);
                                    //for (int i = 0; i < noDataMask.GetLength(0); i++)
                                    //    noDataMask.SetValue(Convert.ToByte(0), i);
                                    //currentPixelBlock.SetNoDataMask(plane, noDataMask);
                                }
                                // Write the cleared pixel block to the output raster dataset.
                                //outputRaster.Write(minCol, minRow, currentPixelBlock);
                                outputRaster.Write(0, 0, currentPixelBlock);
                                // Refresh the properties of the output raster dataset.
                                outputRaster.Refresh();
                                #endregion

                                // Create a new layer from the masked raster dataset and add it to the map.
                                LayerFactory.Instance.CreateLayer(new Uri(Path.Combine(outputFolder, outputName)),
                                mapView.Map);
                                // Disable the layer representing the original raster dataset.
                                firstSelectedLayer.SetVisibility(false);
                            });
                        }
                        else
                        {
                            // If the selected layer is not a raster layer show a message box with the appropriate message.
                            MessageBox.Show("No Raster layers selected. Please select one Raster layer.");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception caught in MaskRaster: " + exc.Message);
            }
        }

        //this only works for polygons
        public static List<MapPoint> GetPolygonSegmentOffsetPoints(Geometry shape)
        {
            List<MapPoint> list = new List<MapPoint>();
            var polygon = shape as Polygon;
            var shape_centroid = shape.Extent.CenterCoordinate.ToMapPoint();
            double buf_dist = 3.5;
            double slope;
            double xdiff;
            double ydiff;
            double angle;
            MapPoint mp;
            for (int i = 0; i < polygon.Points.Count - 1; i++)
            {
                var p1 = polygon.Points[i];
                var p2 = polygon.Points[i + 1];
                var ctrpt = MapPointBuilderEx.CreateMapPoint((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);

                slope = 0;
                angle = 0;
                xdiff = 0;
                ydiff = 0;
                if (Math.Abs(p2.X - p1.X) > 1e-4)
                {
                    slope = (p2.Y - p1.Y) / (p2.X - p1.X); //slope could be zero here
                    angle = Math.Atan(Math.Abs(p1.Y - p2.Y) / Math.Abs(p1.X - p2.X));
                    xdiff = Math.Sin(angle) * buf_dist;
                    ydiff = Math.Cos(angle) * buf_dist;
                }
                else
                {
                    slope = double.NaN;
                }

                
                if (slope > 0 && Math.Abs(slope) > 1e-4)
                {
                    mp = MapPointBuilderEx.CreateMapPoint(ctrpt.X - xdiff, ctrpt.Y + ydiff);
                    if (GeometryEngine.Instance.Intersects(shape, mp))
                    {
                        list.Add(MapPointBuilderEx.CreateMapPoint(ctrpt.X + xdiff, ctrpt.Y - ydiff));
                    }
                    else
                    {
                        list.Add(mp);
                    }
                }
                else if (slope < 0 && Math.Abs(slope) > 1e-4)
                {
                    mp = MapPointBuilderEx.CreateMapPoint(ctrpt.X + xdiff, ctrpt.Y + ydiff);
                    if (GeometryEngine.Instance.Intersects(shape, mp))
                    {
                        list.Add(MapPointBuilderEx.CreateMapPoint(ctrpt.X - xdiff, ctrpt.Y - ydiff));
                    } 
                    else
                    {
                        list.Add(mp);
                    }
                }
                else if (double.IsNaN(slope))
                {
                    //vertical line
                    if (ctrpt.X < shape_centroid.X)
                    {
                        list.Add(MapPointBuilderEx.CreateMapPoint(ctrpt.X - buf_dist, ctrpt.Y));
                    }
                    else if (ctrpt.X > shape_centroid.X)
                    {
                        list.Add(MapPointBuilderEx.CreateMapPoint(ctrpt.X + buf_dist, ctrpt.Y));
                    }
                }
                else //if (slope == 0)
                {
                    //horizontal line
                    if (ctrpt.Y < shape_centroid.Y)
                    {
                        list.Add(MapPointBuilderEx.CreateMapPoint(ctrpt.X, ctrpt.Y - buf_dist));
                    }
                    else if (ctrpt.Y > shape_centroid.Y)
                    {
                        list.Add(MapPointBuilderEx.CreateMapPoint(ctrpt.X, ctrpt.Y + buf_dist));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Read raster pixels values per building footprint, based on the building footprint geometries,
        /// collate and output into textfile in user specified folder
        /// </summary>
        /// <param name="geometry">Nominal (i.e., null) rectangle geometry from the Add-in trigger.</param>
        public static async void ReadRaster(FeatureLayer footprintLayer, GridDataType gridDataType, int bandindex = 0)
        {
            if (MapView.Active == null)
            {
                return;
            }
            _bandindex = bandindex;
            if (_bandindex < 0)
            {
                MessageBox.Show($"Should only read from first band of raster dataset.");
                return;
            }

            try
            {
                BasicFeatureLayer lyr_fp = footprintLayer as BasicFeatureLayer;
                var numBuildings = await QueuedTask.Run(() => { return (footprintLayer as FeatureLayer).GetTable().GetCount(); });
                //get all raster layers
                var lyr_rasters = MapView.Active.Map.GetLayersAsFlattenedList().OfType<RasterLayer>().ToList();

                /*
                ProgressorSource ps = new ProgressorSource($"Reading grids: ", false);
                //ps.Max = await QueuedTask.Run(() => { return (uint)((footprintLayer as FeatureLayer).GetTable().GetCount()); });
                ps.Max = (uint) lyr_rasters.Count;
                ps.Progressor.Value = 0;
                */
                // iterate through all grids
                foreach (Layer firstSelectedLayer in lyr_rasters)
                {
                    // Working with rasters requires the MCT.
                    //CancelableProgressorSource ps = new CancelableProgressorSource($"Reading {firstSelectedLayer.Name} grid: ", "Cancel", false);
                    ProgressorSource ps = new ProgressorSource($"Reading {firstSelectedLayer.Name} grid: ", false);
                    await QueuedTask.Run(() =>
                    {
                        var srlatlong = new SpatialReferenceBuilder(4326);

                        ps.Max = (uint)numBuildings;
                        ps.Progressor.Value = 0;
                        if (footprintLayer.ConnectionStatus == ConnectionStatus.Broken)
                            throw new ApplicationException("Footprint layer connection broken");

                        #region Get the raster dataset from the currently selected layer
                        // Get the raster layer from the selected layer.
                        Raster inputRaster = (firstSelectedLayer as RasterLayer).GetRaster();
                        // Get the basic raster dataset from the raster.
                        BasicRasterDataset basicRasterDataset = inputRaster.GetRasterDataset();
                        // If the map spatial reference is different from the spatial reference of the input raster,
                        // set the map spatial reference on the input raster. This will ensure the map points are 
                        // correctly reprojected to image points.
                        if (MapView.Active.Map.SpatialReference.Name != inputRaster.GetSpatialReference().Name)
                            inputRaster.SetSpatialReference(MapView.Active.Map.SpatialReference);

                        // reproject the raster envelope to match the map spatial reference
                        var rasterEnvelope = GeometryEngine.Instance.Project(inputRaster.GetExtent(), inputRaster.GetSpatialReference());
                        #endregion

                        //get the matching alternative
                        var alt = Alternatives.Where(a => a.layerName(gridDataType) == firstSelectedLayer.Name).FirstOrDefault();
                        if (alt != null)
                        {
                            //if the alt associated with a raster data layer is present, i.e. there is no alt that has that raster data layer
                            #region read raster values


                            Building b;
                            FeatureClass featfp = footprintLayer.GetFeatureClass();
                            bool readAlready = false;
                            using (var rc = featfp.Search())
                            {
                                while (rc.MoveNext())
                                {
                                    using (var record = rc.Current)
                                    {
                                        Feature f = record as Feature;
                                        Geometry shape = f.GetShape();
                                        ReadOnlyPointCollection shape_vertices = null;
                                        if (shape.GeometryType == GeometryType.Polygon && Alternative.evalmethod == EINUNDATIONEVALUATIONLOCATION.STRUCTURESURROUND)
                                        {
                                            var polygon = shape as Polygon;
                                            shape_vertices = polygon.Points;
                                            var lm = GetPolygonSegmentOffsetPoints(shape);
                                        }

                                        int buildingid = Convert.ToInt32(record["BID"]);
                                        if (!BCA.Buildings.ContainsKey(buildingid))
                                        {
                                            b = new Building();
                                            b.BID = buildingid;
                                            BCA.Buildings.Add(buildingid, b);
                                        }
                                        else
                                        {
                                            b = BCA.Buildings[buildingid];
                                        }

                                        readAlready = false;
                                        if (gridDataType == GridDataType.WSEMAX)
                                        {
                                            if (b.WSEmax.ContainsKey(alt.Name))
                                            {
                                                readAlready = true;
                                            }
                                        }
                                        else if (gridDataType == GridDataType.DEPTHMAX)
                                        {
                                            if (b.Depthmax.ContainsKey(alt.Name))
                                            {
                                                readAlready = true;
                                            }
                                        }
                                        else if (gridDataType == GridDataType.TERRAIN)
                                        {
                                            if (b.Terrain.ContainsKey(alt.Name))
                                            {
                                                readAlready = true;
                                            }
                                        }

                                        if (!readAlready)
                                        {
                                            //Method 2: read raster data by a point with fixed window
                                            // create a pixelblock representing a 3x3 window to hold the raster values
                                            var pixelBlock = inputRaster.CreatePixelBlock(3, 3);
                                            var shape_ctrpt = shape.Extent.CenterCoordinate.ToMapPoint();

                                            var list_points = new List<MapPoint>();
                                            if (shape_vertices != null && shape_vertices.Count > 0)
                                            {
                                                foreach(var sv in shape_vertices)
                                                {
                                                    list_points.Add(sv.Extent.CenterCoordinate.ToMapPoint());
                                                }
                                            }
                                            else
                                            {
                                                list_points.Add(shape_ctrpt);
                                            }

                                            var list_array = new List<Array>();
                                            foreach (var pt in list_points)
                                            {
                                                // create a container to hold the pixel values
                                                Array pixelArray = new object[pixelBlock.GetWidth(), pixelBlock.GetHeight()];
    
                                                // if the cursor is within the extent of the raster
                                                if (GeometryEngine.Instance.Contains(rasterEnvelope, pt))
                                                {



                                                    Coordinate2D p2d = new Coordinate2D(pt);
                                                    p2d.GetPerpendicular(true);

                                                    // find the map location expressed in row,column of the raster
                                                    var pixelLocationAtRaster = inputRaster.MapToPixel(pt.X, pt.Y);

                                                    var (mapPtTupleX, mapPtTupleY) = inputRaster.PixelToMap(pixelLocationAtRaster.Item1 - 2, pixelLocationAtRaster.Item2 - 2 );
                                                    var mapPt = MapPointBuilderEx.CreateMapPoint(mapPtTupleX, mapPtTupleY);
                                                    bool insidePolygon = GeometryEngine.Instance.Intersects(shape, mapPt);
                                                    // fill the pixelblock with the pointer location
                                                    // (-2, -2) then read at index=4, will read 1 grid cell away from the vertices
                                                    // (+2, +2) then read at index=4, will read 1 grid cell away from the vertices
                                                    if (insidePolygon)
                                                    {
                                                        inputRaster.Read(pixelLocationAtRaster.Item1 + 2, pixelLocationAtRaster.Item2 + 2, pixelBlock);
                                                        (mapPtTupleX, mapPtTupleY) = inputRaster.PixelToMap(pixelLocationAtRaster.Item1 + 2, pixelLocationAtRaster.Item2 + 2 );
                                                        mapPt = MapPointBuilderEx.CreateMapPoint(mapPtTupleX, mapPtTupleY);
                                                        insidePolygon = GeometryEngine.Instance.Intersects(shape, mapPt);
                                                        if (insidePolygon)
                                                        {
                                                            string stop = "now";
                                                        }
                                                    } 
                                                    else
                                                    {
                                                        inputRaster.Read(pixelLocationAtRaster.Item1 - 2, pixelLocationAtRaster.Item2 - 2, pixelBlock);
                                                    }
    
                                                    // retrieve the actual pixel values from the pixelblock representing the red raster band
                                                    pixelArray = pixelBlock.GetPixelData(_bandindex, false);
                                                }
                                                else
                                                {
                                                    // fill the container with 0s
                                                    Array.Clear(pixelArray, 0, pixelArray.Length);
                                                }

                                                list_array.Add(pixelArray);
                                            }

                                            double ras_val = 0;
                                            int num = 0;
                                            //int method = 1; // 0 will be a direct read; 1 will be an average
                                            int ind = 0;
                                            int centerIndex = 4; // in a 3 by 3 pixel block, starting at -1 row and -1 column
                                                                 //int centerIndex = 0; // in a 3 by 3 pixel block, starting at 0 row and 0 column
                                            var list_values = new List<double>();
                                            foreach(var pt_pixelArray in list_array)
                                            {
                                                ind = 0;
                                                foreach (float v in pt_pixelArray)
                                                {
                                                    if (v < 0) { }
                                                    else { list_values.Add(v); }

                                                    if (Alternative.readmethod == EREADRASTERMETHOD.POINTDIRECT)
                                                    {
                                                        if (ind == centerIndex)
                                                        {
                                                            if (ras_val < 0)
                                                            {
                                                                // skip points outside of flood water
                                                            }
                                                            else
                                                            {
                                                                ras_val += v;
                                                                num++;
                                                            }
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // skip points outside of flood water
                                                        if (v >= 0)
                                                        {
                                                            ras_val += v;
                                                            num++;
                                                        }
                                                    }
                                                    ind++;
                                                }
                                            }

                                            if (num > 0)
                                            {
                                                ras_val /= num;
                                            }
                                            else
                                            {
                                                ras_val = -9999;
                                            }

                                            //record depth values, later on used for statistics
                                            if (!b.BCADepthmaxStatistics.ContainsKey(alt.Name))
                                            {
                                                b.BCADepthmaxStatistics.Add(alt.Name, new BCAMATH());
                                            }
                                            b.BCADepthmaxStatistics[alt.Name].SetData(list_values);

                                            //clean up
                                            foreach(var pt_pixelArray in list_array)
                                            {
                                                Array.Clear(pt_pixelArray, 0, pt_pixelArray.Length);
                                            }
                                            list_array.Clear();
                                            list_points.Clear();
                                            list_values.Clear();

                                            //record result
                                            if (b.latitude == null || b.longitude == null)
                                            {
                                                //only save one copy of the building location x,y (lat-long) and footprint size
                                                //assuming all alternatives are using the same set of building footprints

                                                /* Try to calculate lat-long from Stateplane x,y
                                                 * looks like it is not possible... */
                                                /*
                                                var llcoord = new Coordinate2D(shape_ctrpt.X, shape_ctrpt.Y);
                                                var mp = llcoord.ToMapPoint(srlatlong.BaseGeographic);
                                                var g = srlatlong.BaseGeographic.GcsWkid;
                                                var g1 = srlatlong.IsGeographic;
                                                var g2 = srlatlong.BaseGeographic.LeftLongitude;
                                                var g3 = srlatlong.BaseGeographic.RightLongitude;
                                                */

                                                //Alternative.BuildingXY.Add(buildingid, (shape_ctrpt.X, shape_ctrpt.Y));
                                                double latitude = Convert.ToDouble(record["Latitude"]);
                                                double longitude = Convert.ToDouble(record["Longitude"]);
                                                double buildingfootprintsqft = Convert.ToDouble(record["Shape_Area"]);
                                                string buildingtype = Convert.ToString(record["BType"]);
                                                string location = Convert.ToString(record["location"]);
                                                string parcel_id = Convert.ToString(record["Parcel_ID"]);
                                                string parcel_hyp = Convert.ToString(record["Parcel_Hyp"]);
                                                b.latitude = latitude;
                                                b.longitude = longitude;
                                                b.FirstFloorAreaSqFt = buildingfootprintsqft;
                                                b.OccupancyType = buildingtype;
                                                b.Address = location;
                                                if (!string.IsNullOrEmpty(parcel_id))
                                                {
                                                    b.ParcelID = parcel_id;
                                                }
                                                else
                                                {
                                                    if (!string.IsNullOrEmpty(parcel_hyp))
                                                    {
                                                        b.ParcelID = parcel_hyp.Replace("-", "");
                                                    }
                                                }

                                                Parcel p = BCA.Parcels.Where(pc => pc.ParcelID == b.ParcelID).FirstOrDefault();
                                                if (p == null)
                                                {
                                                    p = new Parcel(b.ParcelID);
                                                    BCA.Parcels.Add(p);
                                                }
                                                p.AddBuilding(b);
                                            }
                                            if (gridDataType == GridDataType.WSEMAX)
                                            {
                                                if (!b.WSEmax.ContainsKey(alt.Name))
                                                {
                                                    b.WSEmax.Add(alt.Name, ras_val);
                                                }
                                            }
                                            else if (gridDataType == GridDataType.DEPTHMAX)
                                            {
                                                if (!b.Depthmax.ContainsKey(alt.Name))
                                                {
                                                    b.Depthmax.Add(alt.Name, ras_val);
                                                }
                                            }
                                            else if (gridDataType == GridDataType.TERRAIN)
                                            {
                                                if (!b.Terrain.ContainsKey(alt.Name))
                                                {
                                                    b.Terrain.Add(alt.Name, ras_val);
                                                }
                                            }
                                        }
                                    }
                                    ps.Progressor.Value++;
                                    ps.Progressor.Status = (ps.Progressor.Value * 100 / ps.Max) + @" % Completed";
                                    ps.Progressor.Message = $"Read {firstSelectedLayer.Name} per Building #{ps.Progressor.Value}";
                                }
                            }
                            #endregion
                        }
                    }, ps.Progressor);
                    System.Diagnostics.Debug.WriteLine($"Read Raster: {firstSelectedLayer.Name}\n");
                    /*
                    ps.Progressor.Value++;
                    ps.Progressor.Status = (ps.Progressor.Value * 100 / ps.Max) + @" % Completed";
                    ps.Progressor.Message = $"Finished reading {firstSelectedLayer.Name}";
                    */
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception caught in ReadRaster: " + exc.Message);
            }
        }

        /// <summary>
        /// Open a Raster Dataset given a folder and a dataset name.
        /// </summary>
        /// <param name="folder">Full path to the folder containing the raster dataset.</param>
        /// <param name="name">Name of the raster dataset to open.</param>
        /// <returns></returns>
        public static RasterDataset OpenRasterDataset(string folder, string name)
        {
            // Create a new raster dataset which is set to null
            RasterDataset rasterDatasetToOpen = null;
            try
            {
                // Create a new file system connection path to open raster datasets using the folder path.
                FileSystemConnectionPath connectionPath = new FileSystemConnectionPath(new System.Uri(folder), FileSystemDatastoreType.Raster);
                // Create a new file system data store for the connection path created above.
                FileSystemDatastore dataStore = new FileSystemDatastore(connectionPath);
                // Open the raster dataset.
                rasterDatasetToOpen = dataStore.OpenDataset<RasterDataset>(name);
                // Check if it is not null. If it is show a message box with the appropriate message.
                if (rasterDatasetToOpen == null)
                    MessageBox.Show("Failed to open raster dataset: " + name);
            }
            catch (Exception exc)
            {
                // If an exception occurs, show a message box with the appropriate message.
                MessageBox.Show("Exception caught in OpenRasterDataset for raster: " + name + exc.Message);
            }
            return rasterDatasetToOpen;
        }
    }
}
