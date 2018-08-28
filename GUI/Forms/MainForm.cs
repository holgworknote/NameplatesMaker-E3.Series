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
		// QUERIES
		bool ImportText { get; }
		
		// OPERATIONS
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
		
		// QUERIES
		public bool ImportText { get { return cbImportText.Checked; } }
		
		// OPERATIONS
		public void ShowOutput(string txt)
		{
			txtOutput.Text = txt;
		}
		
		void BtShowSettings_Click(object sender, EventArgs e)
		{
			_presenter.ShowSettingsManager();
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
		IMySettings SettingsManager { get; }
		ILogger Logger { get; }
		
		void Start(string txtFilePath);
	}
	public class Model : IModel
	{
		private readonly IMySettings _settingsManager;
		private readonly IWorker _worker;
		private readonly ILogger _logger;
		
		public IMySettings SettingsManager { get { return _settingsManager; } }
		public ILogger Logger { get { return _logger; } }
		
		public Model(IMySettings settingsManager, IWorker worker, ILogger logger)
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
		
		public void Start(string txtFilePath)
		{
			string fontFam = _settingsManager.FontFamily;
			string shSym = _settingsManager.SheetFormat;
			_worker.Execute(fontFam, shSym, txtFilePath);
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
			_view.ShowOutput(_model.Logger.Output);
		}
			
		public void ShowSettingsManager()
		{
			try
			{
				var model = new SettingsManager.Model(_model.SettingsManager);
				var frm = new SettingsManager.View(model);
				frm.ShowDialog();
			}
			catch (Exception ex) { ex.Handle(); }
		}
		public void ClearOutput()
		{
			try
			{
				_model.Logger.Clear();
				_view.ShowOutput(_model.Logger.Output);
			}
			catch (Exception ex) { ex.Handle(); }
		}
		public void Start()
		{
			try
			{
				// Покажем пользователю диалог вы
				string txtFilePath = null;
				if (_view.ImportText)
				{
			        using (var sfd = new SaveFileDialog())
			        {
			            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			            sfd.FilterIndex = 1;
			
			            if (sfd.ShowDialog() == DialogResult.OK)
			            	txtFilePath = sfd.FileName;
			        }
				}
				
				_model.Start(txtFilePath);
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
