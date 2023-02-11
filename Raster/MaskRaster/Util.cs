using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MaskRaster
{
	public enum GridDataType
	{
		WSEMAX,
		DEPTHMAX
	}

    internal class Util
    {
		static string _configfile = @"C:\dev\arcgis-pro-sdk-community-samples\Raster\MaskRaster\Config.daml";
		

		/// <summary>
		/// Get the current add-in module's daml / AddInInfo Id tag (which is the same as the Assembly GUID)
		/// </summary>
		/// <returns></returns>
		public static string GetAddInId()
		{
			// Module.Id is internal, but we can still get the ID from the assembly
			var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			//var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
			//var fileName = Path.Combine($@"{{{attribute.Value.ToString()}}}", $@"{assembly.FullName.Split(',')[0]}.esriAddInX");
			//return fileName;
			return "";
		}

        /// <summary>
		/// returns a tuple with version and desktopVersion using the given addin file path
		/// </summary>
		/// <param name="fileName">file path (partial) of esriAddinX package</param>
		/// <returns>tuple: version, desktopVersion</returns>
		public static List<Alternative> GetConfigDamlSMCAlternatives()
		{
			XmlDocument xDoc = new XmlDocument();
			try
			{
				string daml = string.Empty;
				using (StreamReader sr = new StreamReader(_configfile))
				{
					daml = sr.ReadToEnd();
					xDoc.LoadXml(daml); // @"<?xml version=""1.0"" encoding=""utf - 8""?>" + 
				}
				
				XmlNodeList alt_list_block = xDoc.GetElementsByTagName("SMCAlternatives");
				Alternative.basefolder = alt_list_block[0].Attributes["basefolder"].Value;
				List<Alternative> alts = new List<Alternative>();
				foreach (XmlNode xalt in alt_list_block[0])
				{
					string alt_id = xalt.Attributes["id"].Value;
					string alt_data_type = xalt.Attributes["type"].Value;
					string alt_path = xalt.ChildNodes[0].InnerText;
					Alternative alt = alts.Where(a => a.Name == alt_id).FirstOrDefault();
					if (alt == null)
					{
						alt = new Alternative(alt_id);
						alts.Add(alt);
					}
					switch (alt_data_type)
					{
						case nameof(GridDataType.WSEMAX):
							alt.PathWSEMAX = alt_path;
							break;
						case nameof(GridDataType.DEPTHMAX):
							alt.PathDEPTHMAX = alt_path;
							break;
					}
				}
				return alts;
			}
			catch (Exception ex)
			{
				throw new Exception($@"Unable to parse config.daml {_configfile}: {ex.Message}");
			}
			return null;
		}
    }
}
