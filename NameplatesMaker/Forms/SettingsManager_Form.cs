using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using BrightIdeasSoftware;
using Core;

namespace NameplatesMaker.SettingsManager
{
	/// <summary>
	/// Форма для отображения и редактирования списка шаблонов табличек.
	/// </summary>
	 
	public interface IView
	{
		void SetPatternsList(IEnumerable<PlatePattern> patterns);
		void SetMappingTree(List<MappingTree.IRoot> roots);
		void UpdatePattern(PlatePattern pat);
		void UpdateMappingRoot(MappingTree.IRoot root);
		PlatePattern GetSelectedPlatePattern();
		MappingTree.IRoot GetSelectedMappingRoot();
		string GetSelectedDevice();
		MappingTree.IRoot GetParent();
	}
	public partial class View : Form, IView
	{
		private readonly Presenter _presenter;
		
		// CTOR
		public View(IModel model)
		{
			InitializeComponent();
			this.BuildPatternsOlv();
			this.BuildMappingTreeOlv();
			
			_presenter = new Presenter(this, model);
		}
		
		private void BuildPatternsOlv()
		{
			olvPatterns.Columns.Add(new OLVColumn("Наименование", "Name"));
			olvPatterns.Columns.Add(new OLVColumn("Ширина", "Width"));
			olvPatterns.Columns.Add(new OLVColumn("Высота", "Height"));
			olvPatterns.Columns.Add(new OLVColumn("Отображение положений", "ShowPositions"));
		}
		private void BuildMappingTreeOlv()
		{
            // Колонка наименования
            var idCol = new OLVColumn();
            idCol.Text = "Name";
            idCol.AspectGetter = x => 
            {
            	string ret = null;
            	
            	if (x is MappingTree.Root)
            		ret = (x as MappingTree.Root).PlatePattern.Name;
            	else if (x is string)
            		ret = x as string;
            	
            	return ret;
            };
            idCol.ImageGetter = x =>
            {
            	string ret = null;
            	
            	if (x is  MappingTree.Root)
            		ret = "PlatePattern";
            	else if (x is string)
            		ret = "Device";
            	
            	return ret;
            };
            idCol.FillsFreeSpace = true;
            olvMappingTree.Columns.Add(idCol);
            
			olvMappingTree.CanExpandGetter = x => 
			{
				if (x is MappingTree.Root)
					return (x as  MappingTree.Root).Devices.Any();
				else
					return false;
			};
			olvMappingTree.ChildrenGetter = x => 
			{
				if (x is MappingTree.Root)
					return (x as MappingTree.Root).Devices;
				else
					return null;
			};
		}
		
		// IVIEW MEMBERS
		public void SetPatternsList(IEnumerable<PlatePattern> patterns)
		{
			olvPatterns.SetObjects(patterns.ToList());
			olvPatterns.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		public void SetMappingTree(List<MappingTree.IRoot> roots)
		{
			olvMappingTree.SetObjects(roots.ToList());
		}
		public void UpdatePattern(PlatePattern pat)
		{
			olvPatterns.UpdateObject(pat);
		}
		public void UpdateMappingRoot(MappingTree.IRoot root)
		{
			olvMappingTree.UpdateObject(root);
		}
		public PlatePattern GetSelectedPlatePattern()
		{
			PlatePattern ret = null;
			
			var obj = olvPatterns.SelectedObject;
			if (obj != null)
				ret = obj as PlatePattern;
			
			return ret;
		}
		public MappingTree.IRoot GetSelectedMappingRoot()
		{
			MappingTree.IRoot ret = null;
			
			var obj = olvMappingTree.SelectedObject;
			if (obj != null)
				if (obj is MappingTree.IRoot)
					ret = obj as MappingTree.IRoot;
			
			return ret;
		}
		public string GetSelectedDevice()
		{
			string ret = null;
			
			var obj = olvMappingTree.SelectedObject;
			if (obj != null)
				if (obj is string)
					ret = obj as string;
			
			return ret;
		}
		public MappingTree.IRoot GetParent()
		{
			MappingTree.IRoot ret = null;
			
			var obj = olvMappingTree.SelectedObject;
			string device = this.GetSelectedDevice();
			
			if (device != null)
				ret = olvMappingTree.GetParent(obj) as MappingTree.IRoot;
			
			return ret;
		}
		
		void BtAddPattern_Click(object sender, EventArgs e)
		{
			_presenter.CreatePattern();
		}
		void BtRemove_Click(object sender, EventArgs e)
		{
			_presenter.RemovePattern();
		}
		void OlvPatterns_DoubleClick(object sender, EventArgs e)
		{
			_presenter.EditPattern();
		}
		void btSaveSettings_Click(object sender, EventArgs e)
		{
			_presenter.SaveSettings();
		}
		void btCreateMappingRule_Click(object sender, EventArgs e)
		{
			_presenter.CreateNewMapping();
		}
		void BtAddMappingItem_Click(object sender, EventArgs e)
		{
			_presenter.AddMappingItem();
		}
		void BtRemoveMapping_ItemClick(object sender, EventArgs e)
		{
			_presenter.RemoveMapping();
		}
		void OlvMappingTreeDoubleClick(object sender, EventArgs e)
		{
			_presenter.EditMapping();
		}
	}
	
	public interface IModel
	{
		ISettingsManager SettingsManager { get; }
	}
	public class Model : IModel
	{
		private readonly ISettingsManager _settingsManager;
		
		public ISettingsManager SettingsManager { get { return _settingsManager; } }
		
		public Model(ISettingsManager settingsManager)
		{
			if (settingsManager == null)
				throw new ArgumentNullException("settingsManager");
			_settingsManager = settingsManager;
		}
	}
	
	public class Presenter
	{
		private readonly IView _view;
		private readonly IModel _model;
		
		// CTOR
		public Presenter(IView view, IModel model)
		{
			if (view == null)
				throw new ArgumentNullException("view");
			if (model == null)
				throw new ArgumentNullException("model");
			
			_view = view;
			_model = model;
			
			// Отобразим настройки на форме
			var patterns = _model.SettingsManager.PatternsList.Values;
			_view.SetPatternsList(patterns);
			var mappingTree = _model.SettingsManager.MappingTree.Roots;
			_view.SetMappingTree(mappingTree);
		}
		
		// Работа с шаблонами
		public void CreatePattern()
		{
			var dlg = new EditPlatePatternDialog.View();
			dlg.ShowDialog();
			
			if (dlg.DialogResult != DialogResult.OK)
				return;
			
			// Создадим новый паттерн и передадим его в модель
			var pat = new PlatePattern()
			{
				Name          = dlg.InputName,         
				Width         = dlg.InputWidth,
				Height        = dlg.InputHeight,
				ShowPositions = dlg.InputShowPositions,
			};
			_model.SettingsManager.PatternsList.Add(pat);
			
			var patterns = _model.SettingsManager.PatternsList.Values;
			_view.SetPatternsList(patterns);
		}
		public void RemovePattern()
		{
			var pat = _view.GetSelectedPlatePattern();
			
			if (pat == null)
				return;
			
			_model.SettingsManager.PatternsList.Remove(pat);
			
			var patterns = _model.SettingsManager.PatternsList.Values;
			_view.SetPatternsList(patterns);
		}
		public void EditPattern()
		{
			var pat = _view.GetSelectedPlatePattern();
			
			if (pat == null)
				return;
			
			var dlg = new EditPlatePatternDialog.View();
			dlg.Set(pat);
			dlg.ShowDialog();
			
			if (dlg.DialogResult != DialogResult.OK)
				return;
			
			pat.Name          = dlg.InputName;
			pat.Width         = dlg.InputWidth;
			pat.Height        = dlg.InputHeight;
			pat.ShowPositions = dlg.InputShowPositions;
			
			_view.UpdatePattern(pat);
		}
		
		// Работа с MappingTree
		public void CreateNewMapping()
		{
			var patterns = _model.SettingsManager.PatternsList.Values;
			var dlg = new PlatePatternSelector.View(patterns);
			dlg.ShowDialog();
			
			if (dlg.DialogResult != DialogResult.OK)
				return;
			
			// Создадим новый паттерн и передадим его в модель
			var pat = dlg.GetInput();
			var map = new MappingTree.Root(pat, Enumerable.Empty<string>());
			_model.SettingsManager.MappingTree.Add(map);
			
			var mappings = _model.SettingsManager.MappingTree.Roots;
			_view.SetMappingTree(mappings);
		}
		public void AddMappingItem()
		{
			var mRoot = _view.GetSelectedMappingRoot();
			
			if (mRoot == null)
				return;
			
			var dlg = new NameplatesMaker.TextInput.View();
			dlg.ShowDialog();
			
			if (dlg.DialogResult != DialogResult.OK)
				return;
			
			// Передадим пользовательский ввод в модель
			mRoot.Devices.Add(dlg.GetInput());
			
			// Обновим отображение
			var mappingTree = _model.SettingsManager.MappingTree.Roots;
			_view.SetMappingTree(mappingTree);
		}
		public void RemoveMapping()
		{
			var mRoot = _view.GetSelectedMappingRoot();
			var device = _view.GetSelectedDevice();
			
			if (device != null)
				mRoot = _view.GetParent();
			
			if (mRoot != null)
				_model.SettingsManager.MappingTree.Remove(mRoot);
			
			// Обновим отображение
			var mappingTree = _model.SettingsManager.MappingTree.Roots;
			_view.SetMappingTree(mappingTree);
		}
		public void EditMapping()
		{
			// FIXME: !!!!
			
//			var mRoot = _view.GetSelectedMappingRoot();
//			var device = _view.GetSelectedDevice();
//			
//			if (mRoot != null)
//			{
//				var patterns = _model.SettingsManager.PatternsList.Values;
//				var dlg = new PlatePatternSelector.View(patterns);
//				dlg.ShowDialog();
//				
//				if (dlg.DialogResult != DialogResult.OK)
//					return;
//				
//				// Создадим новый паттерн и передадим его в модель
//				var pat = dlg.GetInput();
//				mRoot.PlatePattern = pat;
//				
//				_view.UpdateMappingRoot(mRoot);
//			}
			
//			if (device != null)
//			{
//				var dlg = new NameplatesMaker.TextInput.View(device);
//				dlg.ShowDialog();
//				
//				if (dlg.DialogResult != DialogResult.OK)
//					return;
//				
//				// Передадим пользовательский ввод в модель
//				mRoot.Devices.Add(dlg.GetInput());
//			}
		}
		
		public void SaveSettings()
		{
			_model.SettingsManager.Save();
			MessageBox.Show("Настройки успешно сохранены!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
