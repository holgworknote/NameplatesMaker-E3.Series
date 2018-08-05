/*
 * Created by SharpDevelop.
 * User: OHozhatelev
 * Date: 31.07.2018
 * Time: 16:44
 */
using System;
using System.Windows.Forms;
using Core;

namespace GUI
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			string pathPatternTable = Environment.CurrentDirectory + @"\PatternTable.xml";
			string pathMappingTable = Environment.CurrentDirectory + @"\MappingTable.xml";
			var logger = new Logger();
						
			var settingsManager = new SettingsManager(pathPatternTable, pathMappingTable, logger);
			settingsManager.Load();
			
			var e3Connector = new E3Connector(logger);
			var e3Reader = new E3Reader();
			var e3Writer = new E3Writer(settingsManager.MappingTree, "A4 верт.", logger);
			var worker = new Worker(e3Writer, e3Reader, e3Connector);
			
			var model = new Model(settingsManager, worker, logger);
			
			Application.Run(new MainForm(model));
		}
		
	}
}
