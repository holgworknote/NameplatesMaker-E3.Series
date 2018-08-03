using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace GUI
{
	/// <summary>
	/// Main Form
	/// </summary>
	
	public interface IView
	{
		void ShowOutput(string txt);
	}
	public partial class MainForm : Form, IView
	{
		private readonly Presenter _presenter;
		
		public MainForm(IModel model)
		{
			InitializeComponent();
			
			_presenter = new Presenter(this, model);
		}
		
		public void ShowOutput(string txt)
		{
			txtOutput.Text = txt;
		}
		
		void BtShowSettings_Click(object sender, EventArgs e)
		{
			_presenter.ShowsettingsManager();
		}
		void BtStart_Click(object sender, EventArgs e)
		{	
			_presenter.Start();
		}
		void BtClearOutputClick(object sender, EventArgs e)
		{
			_presenter.ClearOutput();
		}
	}
	
	public interface IModel
	{
		ISettingsManager SettingsManager { get; }
		ILogger Logger { get; }
		
		void Start();
	}
	public class Model : IModel
	{
		private readonly ISettingsManager _settingsManager;
		private readonly IWorker _worker;
		private readonly ILogger _logger;
		
		public ISettingsManager SettingsManager { get { return _settingsManager; } }
		public ILogger Logger { get { return _logger; } }
		
		public Model(ISettingsManager settingsManager, IWorker worker, ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			if (worker == null)
				throw new ArgumentNullException("worker");
			if (settingsManager == null)
				throw new ArgumentNullException("settingsManager");
			
			_settingsManager = settingsManager;
			_worker = worker;
			_logger = logger;
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
		public void ClearOutput()
		{
			_model.Logger.Clear();
			_view.ShowOutput(_model.Logger.Output);
		}
		public void Start()
		{
			try
			{
				_model.Start();
			}
			catch(Exception ex) 
			{
				_model.Logger.WriteLine("Обработка завершена с ошибкой: " + ex.Message);
			}
			finally
			{
				_view.ShowOutput(_model.Logger.Output);
			}
		}
	}
}
