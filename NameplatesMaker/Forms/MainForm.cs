using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Core;

namespace GUI
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	
	public interface IView
	{
		
	}
	public partial class MainForm : Form, IView
	{
		private readonly Presenter _presenter;
		
		public MainForm(IModel model)
		{
			InitializeComponent();
			
			_presenter = new Presenter(this, model);
		}
		

		
		/* root - наименование PlatePattern, childs - список устройств */
		
		public void Program()
		{
			try
			{
//				Console.Write("Пдключаюсь к E3 ..");
//							
//				var nameplateBuilder = new NameplateWriter(null, null);
//				var worker = new Worker(nameplateBuilder);
//				var reports = worker.Execute();
//				
//				Console.WriteLine(" DONE");
//				Console.WriteLine("");
				
//				foreach (var report in reports)
//				{
//					Console.WriteLine(report.ProjectName);
//					Console.WriteLine(report.Message);
//					
//					int c = report.Entries.Count();
//					
//					if (c == 0)
//						Console.WriteLine("Дублирующихся проводов не обнаружено!");						
//					else
//					{
//						Console.WriteLine(String.Format("В проекте обнаружены дублирующиеся провода [{0} шт.]:", c));
//						foreach (var entry in report.Entries)
//							Console.WriteLine(entry);
//					}
//					
//					Console.WriteLine("");	
//				}
			}
			catch(Exception ex) 
			{ 
				Console.WriteLine(" FAIL");
				Console.WriteLine("");
				Console.WriteLine("ERROR: " + ex.Message); 
			}
			
			GC.Collect();
			Console.Write("Нажмите любую кнопку, чтобы завершить работу... ");
			Console.ReadKey(true);
		}
		
		void BtShowSettings_Click(object sender, EventArgs e)
		{
			_presenter.ShowsettingsManager();
		}
		void BtStartClick(object sender, EventArgs e)
		{	
			_presenter.Start();
		}
	}
	
	public interface IModel
	{
		ISettingsManager SettingsManager { get; }
		
		void Start();
	}
	public class Model : IModel
	{
		private readonly ISettingsManager _settingsManager;
		private readonly IWorker _worker;
		
		public ISettingsManager SettingsManager { get { return _settingsManager; } }
		
		public Model(ISettingsManager settingsManager, IWorker worker)
		{
			if (worker == null)
				throw new ArgumentNullException("worker");
			if (settingsManager == null)
				throw new ArgumentNullException("settingsManager");
			
			_settingsManager = settingsManager;
			_worker = worker;
		}
		
		public void Start()
		{
			_worker.Execute();
			GC.Collect();
		}
	}
	
	public class Presenter
	{
		private readonly IView _view;
		private readonly IModel _model;
		
		public Presenter(IView view, IModel model)
		{
			if (view == null)
				throw new ArgumentNullException("view");
			if (model == null)
				throw new ArgumentNullException("model");
			
			_view = view;
			_model = model;
		}
			
		public void ShowsettingsManager()
		{
			var model = new NameplatesMaker.SettingsManager.Model(_model.SettingsManager);
			var frm = new NameplatesMaker.SettingsManager.View(model);
			frm.Show();
		}
		public void Start()
		{
			_model.Start();
		}
	}
}
