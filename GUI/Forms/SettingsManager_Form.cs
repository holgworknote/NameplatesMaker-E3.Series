﻿using System;
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
		string GetSheetName();
		void SetSheetName(string sheetName);
		void SetPatternsList(IEnumerable<PlatePattern> patterns);
		void SetMappingTree(IEnumerable<MappingTree.IRoot> roots);
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
		public string GetSheetName()
		{
			return txtSheetName.Text;
		}
		public void SetSheetName(string sheetName)
		{
			txtSheetName.Text = sheetName;
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
			var patterns = _model.SettingsManager.MappingTree.PatternsCollection;
			_view.SetPatternsList(patterns);
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
			var pat = _view.GetSelectedPlatePattern();
			
			// Если пользователь ничего не выделил, то сообщим ему об этом
			if (pat == null)
			{
				MyMessageBox.ShowInfo("Выделите шаблон, который вы хотите удалить!");
				return;
			}
			
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
				var mRoot = _view.GetSelectedMappingRoot();
				var device = _view.GetSelectedDevice();
				
				// Если пользователь ничего не выделил, то сообщим ему об этом
				if (device == null)
				{
					MyMessageBox.ShowInfo("Выделите изделие, которое вы хотите удалить!");
					return;
				}
				
				// Если пользователь выделил девайс, то удаляем соответсвенно деайс
				if (device != null)
				{
					mRoot = _view.GetParent();
					mRoot.Devices.Remove(device);
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