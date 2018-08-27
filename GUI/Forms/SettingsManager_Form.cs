using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using BrightIdeasSoftware;
using Core;

namespace GUI.SettingsManager
{
	/// <summary>
	/// Форма для отображения и редактирования списка шаблонов табличек.
	/// </summary>
	 
	public interface IView
	{
		// OPERATIONS
		void SetSheetName(string sheetName);
		void SetFontFamily(string fontFam);
		void SetFontsList(IEnumerable<string> fonts);
		void SetPatternsList(IEnumerable<PlatePattern> patterns);
		void SetMappingTree(IEnumerable<MappingTree.IRoot> roots);
		void UpdatePattern(PlatePattern pat);
		void UpdateMappingRoot(MappingTree.IRoot root);
		
		// QUERIES
		string GetSheetName();
		string GetFontFamily();
		PlatePattern GetSelectedPlatePattern();
		IEnumerable<PlatePattern> GetSelectedPlatePatterns();
		MappingTree.IRoot GetSelectedMappingRoot();
		string GetSelectedDevice();
		IEnumerable<string> GetSelectedDevices();
		MappingTree.IRoot GetParent();
		MappingTree.IRoot GetParent(string dev);
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
			olvPatterns.Columns.Add(new OLVColumn("Размер шрифта", "FontHeight"));
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
		
		// IVIEW : OPERATIONS
		public void SetSheetName(string sheetName)
		{
			txtSheetName.Text = sheetName;
		}
		public void SetFontsList(IEnumerable<string> fonts)
		{
			cbFont.Items.AddRange(fonts.ToArray());
		}
		public void SetFontFamily(string fontFam)
		{
			cbFont.SelectedItem = fontFam;
		}
		public void SetPatternsList(IEnumerable<PlatePattern> patterns)
		{
			olvPatterns.SetObjects(patterns.ToList());
			olvPatterns.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		public void SetMappingTree(IEnumerable<MappingTree.IRoot> roots)
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

		// IVIEW : QUERIES
		public string GetSheetName()
		{
			return txtSheetName.Text;
		}
		public string GetFontFamily()
		{
			return cbFont.SelectedItem as string;
		}
		public PlatePattern GetSelectedPlatePattern()
		{
			PlatePattern ret = null;
			
			var obj = olvPatterns.SelectedObject;
			if (obj != null)
				ret = obj as PlatePattern;
			
			return ret;
		}
		public IEnumerable<PlatePattern> GetSelectedPlatePatterns()
		{
			return olvPatterns.SelectedObjects.Cast<PlatePattern>();
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
		public IEnumerable<string> GetSelectedDevices()
		{
			var ret = olvMappingTree.SelectedObjects.Cast<string>();
			
			var isAllDevs = ret.All(x => x != null);
			if (!isAllDevs)
				ret = Enumerable.Empty<string>();
			
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
		public MappingTree.IRoot GetParent(string dev)
		{
			return olvMappingTree.GetParent(dev) as MappingTree.IRoot;
		}
		
		void OlvPatterns_DoubleClick(object sender, EventArgs e)
		{
			_presenter.EditPattern();
		}
		void btSaveSettings_Click(object sender, EventArgs e)
		{
			_presenter.SaveSettings();
		}
		void OlvMappingTreeDoubleClick(object sender, EventArgs e)
		{
			_presenter.EditMapping();
		}
		
		// DRAG'N'DROP
		void OlvMappingTreeModelCanDrop(object sender, ModelDropEventArgs e)
		{
			var isAllDevs = e.SourceModels.Cast<string>().All(x => x != null);
			var root = e.TargetModel as MappingTree.IRoot;
			if (root == null && isAllDevs)
				e.Effect = DragDropEffects.None;
			else
				e.Effect = DragDropEffects.Move;
		}
		void OlvMappingTreeModelDropped(object sender, ModelDropEventArgs e)
		{
			_presenter.DragNDropHandler(sender, e);
		}
		void menuItemCreateNewRule_Click(object sender, EventArgs e)
		{
			_presenter.CreateNewMapping();
		}
		void MenuItemRemoveMappingRule_Click(object sender, EventArgs e)
		{
			_presenter.RemoveMapping();
		}
		void MenuItemCreateDeviceClick(object sender, EventArgs e)
		{
			_presenter.CreateDevice();
		}
		void MenuItemRemoveDeviceClick(object sender, EventArgs e)
		{
			_presenter.RemoveDevice();
		}
		void MenuItemCreateNewPatternClick(object sender, EventArgs e)
		{
			_presenter.CreatePattern();
		}
		void MenuItemRemovePatternClick(object sender, EventArgs e)
		{
			_presenter.RemovePattern();
		}
	}
	
	public interface IModel
	{
		IMySettings SettingsManager { get; }
		
		void Save();
	}
	public class Model : IModel
	{
		private readonly IMySettings _settingsManager;
		
		public IMySettings SettingsManager { get { return _settingsManager; } }
		
		public Model(IMySettings settingsManager)
		{
			if (settingsManager == null)
				throw new ArgumentNullException("settingsManager");
			
			_settingsManager = settingsManager;
		}
		
		public void Save()
		{
			SettingsManager.Save();
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
			var fontsList = System.Drawing.FontFamily.Families.Select(x => x.Name).ToList();;
			_view.SetFontsList(fontsList);
			string fontFamily = _model.SettingsManager.FontFamily;
			if (fontFamily != null)
				_view.SetFontFamily(fontFamily);
			
			// PATTERNS
			var patterns = _model.SettingsManager.MappingTree.PatternsCollection;
			_view.SetPatternsList(patterns);
			
			// MAPPING TREE
			var mappingTree = _model.SettingsManager.MappingTree.Roots;
			_view.SetMappingTree(mappingTree);
			_view.SetSheetName(_model.SettingsManager.SheetFormat);
		}
		
		// Работа с шаблонами
		public void CreatePattern()
		{
			var dlg = new GUI.EditPlatePatternDialog.View();
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
			_model.SettingsManager.MappingTree.AddPattern(pat);
			
			var patterns = _model.SettingsManager.MappingTree.PatternsCollection;
			_view.SetPatternsList(patterns);
		}
		public void RemovePattern()
		{
			foreach (var pat in _view.GetSelectedPlatePatterns())
				_model.SettingsManager.MappingTree.RemovePattern(pat);
			
			// Обновим отображение
			var patterns = _model.SettingsManager.MappingTree.PatternsCollection;
			_view.SetPatternsList(patterns);
			var mappings = _model.SettingsManager.MappingTree.Roots;
			_view.SetMappingTree(mappings);
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
			pat.FontHeight      = dlg.InputFontSize;
			pat.ShowPositions = dlg.InputShowPositions;
			
			_view.UpdatePattern(pat);
		}
		
		// Работа с MappingTree
		public void CreateNewMapping()
		{
			try
			{
				var patterns = _model.SettingsManager.MappingTree.PatternsCollection;
				var dlg = new PlatePatternSelector.View(patterns);
				dlg.ShowDialog();
				
				if (dlg.DialogResult != DialogResult.OK)
					return;
				
				// Создадим новый паттерн и передадим его в модель
				var pat = dlg.GetInput();
				var map = new MappingTree.Root(pat, Enumerable.Empty<string>());
				_model.SettingsManager.MappingTree.AddRoot(map);
				
				var mappings = _model.SettingsManager.MappingTree.Roots;
				_view.SetMappingTree(mappings);
			}
			catch (Exception ex) { ex.Handle(); }
		}
		public void CreateDevice()
		{
			try
			{
				var mRoot = _view.GetSelectedMappingRoot();
			
				if (mRoot == null)
					return;
				
				var dlg = new TextInput.View();
				dlg.ShowDialog();
				
				if (dlg.DialogResult != DialogResult.OK)
					return;
				
				// Передадим пользовательский ввод в модель
				mRoot.Devices.Add(dlg.GetInput());
				
				// Обновим отображение
				var mappingTree = _model.SettingsManager.MappingTree.Roots;
				_view.SetMappingTree(mappingTree);
			}
			catch (Exception ex) { ex.Handle(); }
		}
		public void RemoveMapping()
		{
			try
			{
				var mRoot = _view.GetSelectedMappingRoot();			
			
				// Если пользователь ничего не выделил, то сообщим ему об этом и дропнем операцию
				if (mRoot == null)
				{
					MyMessageBox.ShowInfo("Выделите правило, которое вы хотите удалить!");
					return;
				}
				
				// Если пользователь выделил корень, то надо удалить именно корень
				if (mRoot != null)
					_model.SettingsManager.MappingTree.RemoveRoot(mRoot);
				
				// Обновим отображение
				var mappingTree = _model.SettingsManager.MappingTree.Roots;
				_view.SetMappingTree(mappingTree);
			}
			catch (Exception ex) { ex.Handle(); }
		}
		public void RemoveDevice()
		{
			try
			{
				foreach (string dev in _view.GetSelectedDevices())
				{
					var root = _view.GetParent(dev);
					root.Devices.Remove(dev);
				}
				
				// Обновим отображение
				var mappingTree = _model.SettingsManager.MappingTree.Roots;
				_view.SetMappingTree(mappingTree);
			}
			catch (Exception ex) { ex.Handle(); }
		}
		public void EditMapping()
		{
			try
			{
				var device = _view.GetSelectedDevice();
				
				if (device != null)
					this.EditDevice(device);
			}
			catch(Exception ex) { ex.Handle(); }
		}
		public void DragNDropHandler(object sender, ModelDropEventArgs e)
		{
			try
			{
		    	// If they didn't drop on anything, then don't do anything
			    if (e.TargetModel == null)
			        return;
			
			    var root = e.TargetModel as MappingTree.IRoot;
			    
			    // Change the dropped people plus the target person to be married
			    foreach (string device in e.SourceModels)
			    {
					_model.SettingsManager.MappingTree.RemoveDevice(device);	
					root.Devices.Add(device);				
			    }
			
			    // Force them to refresh
			    e.RefreshObjects();
			}
			catch (Exception ex) { ex.Handle(); }
		}
		
		public void SaveSettings()
		{
			try
			{
				_model.SettingsManager.SheetFormat = _view.GetSheetName();
				_model.SettingsManager.FontFamily = _view.GetFontFamily();
				_model.SettingsManager.Save();
				MessageBox.Show("Настройки успешно сохранены!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex) { ex.Handle(); }
		}

		private void EditDevice(string device)
		{
			var dlg = new TextInput.View(device);
			dlg.ShowDialog();
			
			var root = _view.GetParent();
			
			if (dlg.DialogResult != DialogResult.OK)
				return;
			
			// Передадим пользовательский ввод в модель
			root.Devices.Add(dlg.GetInput());
			
			// Удалим выделенный элемент
			root.Devices.Remove(device);
		
			// Обновим отображение
			var mappingTree = _model.SettingsManager.MappingTree.Roots;
			_view.SetMappingTree(mappingTree);
		}
	}
}
