/*
 * Created by SharpDevelop.
 * User: OHozhatelev
 * Date: 01.08.2018
 * Time: 16:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace GUI.SettingsManager
{
	partial class View
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.GroupBox groupBox2;
		private BrightIdeasSoftware.ObjectListView olvPatterns;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox1;
		private BrightIdeasSoftware.TreeListView olvMappingTree;
		private System.Windows.Forms.ToolStripMenuItem btSaveSettings;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtSheetName;
		private System.Windows.Forms.MenuStrip menuStripMappingtable;
		private System.Windows.Forms.ToolStripMenuItem menuItemMappingRules;
		private System.Windows.Forms.ToolStripMenuItem menuItemCreateNewRule;
		private System.Windows.Forms.ToolStripMenuItem menuItemRemoveMappingRule;
		private System.Windows.Forms.ToolStripMenuItem menuItemDevice;
		private System.Windows.Forms.ToolStripMenuItem menuItemCreateDevice;
		private System.Windows.Forms.ToolStripMenuItem menuItemRemoveDevice;
		private System.Windows.Forms.MenuStrip menuStripPatterns;
		private System.Windows.Forms.ToolStripMenuItem menuItemPatterns;
		private System.Windows.Forms.ToolStripMenuItem menuItemCreateNewPattern;
		private System.Windows.Forms.ToolStripMenuItem menuItemRemovePattern;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.menuStripPatterns = new System.Windows.Forms.MenuStrip();
			this.menuItemPatterns = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCreateNewPattern = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRemovePattern = new System.Windows.Forms.ToolStripMenuItem();
			this.olvPatterns = new BrightIdeasSoftware.ObjectListView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtSheetName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.olvMappingTree = new BrightIdeasSoftware.TreeListView();
			this.menuStripMappingtable = new System.Windows.Forms.MenuStrip();
			this.menuItemMappingRules = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCreateNewRule = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRemoveMappingRule = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemDevice = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemCreateDevice = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRemoveDevice = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.btSaveSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox2.SuspendLayout();
			this.menuStripPatterns.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvPatterns)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvMappingTree)).BeginInit();
			this.menuStripMappingtable.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.menuStripPatterns);
			this.groupBox2.Controls.Add(this.olvPatterns);
			this.groupBox2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.Location = new System.Drawing.Point(0, 48);
			this.groupBox2.MinimumSize = new System.Drawing.Size(193, 50);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBox2.Size = new System.Drawing.Size(363, 396);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "PATTERNS LIST";
			// 
			// menuStripPatterns
			// 
			this.menuStripPatterns.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuStripPatterns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.menuItemPatterns});
			this.menuStripPatterns.Location = new System.Drawing.Point(3, 16);
			this.menuStripPatterns.Name = "menuStripPatterns";
			this.menuStripPatterns.Size = new System.Drawing.Size(357, 32);
			this.menuStripPatterns.TabIndex = 7;
			this.menuStripPatterns.Text = "menuStrip1";
			// 
			// menuItemPatterns
			// 
			this.menuItemPatterns.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuItemPatterns.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.menuItemCreateNewPattern,
			this.menuItemRemovePattern});
			this.menuItemPatterns.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuItemPatterns.Image = ((System.Drawing.Image)(resources.GetObject("menuItemPatterns.Image")));
			this.menuItemPatterns.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemPatterns.Name = "menuItemPatterns";
			this.menuItemPatterns.Size = new System.Drawing.Size(85, 28);
			this.menuItemPatterns.Text = "ШАБЛОНЫ";
			// 
			// menuItemCreateNewPattern
			// 
			this.menuItemCreateNewPattern.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuItemCreateNewPattern.Image = ((System.Drawing.Image)(resources.GetObject("menuItemCreateNewPattern.Image")));
			this.menuItemCreateNewPattern.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemCreateNewPattern.Name = "menuItemCreateNewPattern";
			this.menuItemCreateNewPattern.Size = new System.Drawing.Size(232, 30);
			this.menuItemCreateNewPattern.Text = "СОЗДАТЬ НОВЫЙ ШАБЛОН";
			this.menuItemCreateNewPattern.Click += new System.EventHandler(this.MenuItemCreateNewPatternClick);
			// 
			// menuItemRemovePattern
			// 
			this.menuItemRemovePattern.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuItemRemovePattern.Image = ((System.Drawing.Image)(resources.GetObject("menuItemRemovePattern.Image")));
			this.menuItemRemovePattern.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemRemovePattern.Name = "menuItemRemovePattern";
			this.menuItemRemovePattern.Size = new System.Drawing.Size(232, 30);
			this.menuItemRemovePattern.Text = "УДАЛИТЬ ВЫДЕЛЕННЫЙ ШАБЛОН";
			this.menuItemRemovePattern.Click += new System.EventHandler(this.MenuItemRemovePatternClick);
			// 
			// olvPatterns
			// 
			this.olvPatterns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.olvPatterns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.olvPatterns.CellEditUseWholeCell = false;
			this.olvPatterns.Cursor = System.Windows.Forms.Cursors.Default;
			this.olvPatterns.FullRowSelect = true;
			this.olvPatterns.GridLines = true;
			this.olvPatterns.Location = new System.Drawing.Point(6, 53);
			this.olvPatterns.Name = "olvPatterns";
			this.olvPatterns.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
			this.olvPatterns.SelectedForeColor = System.Drawing.Color.Black;
			this.olvPatterns.ShowGroups = false;
			this.olvPatterns.Size = new System.Drawing.Size(351, 337);
			this.olvPatterns.TabIndex = 0;
			this.olvPatterns.UseCompatibleStateImageBehavior = false;
			this.olvPatterns.View = System.Windows.Forms.View.Details;
			this.olvPatterns.DoubleClick += new System.EventHandler(this.OlvPatterns_DoubleClick);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(12, 35);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.AutoScrollMargin = new System.Drawing.Size(1, 1);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(795, 444);
			this.splitContainer1.SplitterDistance = 363;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 3;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtSheetName);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(363, 42);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "SHEET FORMAT";
			// 
			// txtSheetName
			// 
			this.txtSheetName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSheetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSheetName.Location = new System.Drawing.Point(6, 16);
			this.txtSheetName.Name = "txtSheetName";
			this.txtSheetName.Size = new System.Drawing.Size(351, 20);
			this.txtSheetName.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.olvMappingTree);
			this.groupBox1.Controls.Add(this.menuStripMappingtable);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.MinimumSize = new System.Drawing.Size(377, 50);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBox1.Size = new System.Drawing.Size(426, 444);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "MAPPING TABLE";
			// 
			// olvMappingTree
			// 
			this.olvMappingTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.olvMappingTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.olvMappingTree.CellEditUseWholeCell = false;
			this.olvMappingTree.FullRowSelect = true;
			this.olvMappingTree.GridLines = true;
			this.olvMappingTree.IsSimpleDragSource = true;
			this.olvMappingTree.IsSimpleDropSink = true;
			this.olvMappingTree.Location = new System.Drawing.Point(6, 51);
			this.olvMappingTree.Name = "olvMappingTree";
			this.olvMappingTree.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
			this.olvMappingTree.SelectedForeColor = System.Drawing.Color.Black;
			this.olvMappingTree.ShowGroups = false;
			this.olvMappingTree.ShowHeaderInAllViews = false;
			this.olvMappingTree.Size = new System.Drawing.Size(414, 387);
			this.olvMappingTree.TabIndex = 5;
			this.olvMappingTree.UseCompatibleStateImageBehavior = false;
			this.olvMappingTree.View = System.Windows.Forms.View.Details;
			this.olvMappingTree.VirtualMode = true;
			this.olvMappingTree.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.OlvMappingTreeModelCanDrop);
			this.olvMappingTree.ModelDropped += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.OlvMappingTreeModelDropped);
			this.olvMappingTree.DoubleClick += new System.EventHandler(this.OlvMappingTreeDoubleClick);
			// 
			// menuStripMappingtable
			// 
			this.menuStripMappingtable.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuStripMappingtable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.menuItemMappingRules,
			this.menuItemDevice});
			this.menuStripMappingtable.Location = new System.Drawing.Point(3, 16);
			this.menuStripMappingtable.Name = "menuStripMappingtable";
			this.menuStripMappingtable.Size = new System.Drawing.Size(420, 32);
			this.menuStripMappingtable.TabIndex = 6;
			this.menuStripMappingtable.Text = "menuStrip1";
			// 
			// menuItemMappingRules
			// 
			this.menuItemMappingRules.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuItemMappingRules.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.menuItemCreateNewRule,
			this.menuItemRemoveMappingRule});
			this.menuItemMappingRules.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuItemMappingRules.Image = ((System.Drawing.Image)(resources.GetObject("menuItemMappingRules.Image")));
			this.menuItemMappingRules.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemMappingRules.Name = "menuItemMappingRules";
			this.menuItemMappingRules.Size = new System.Drawing.Size(85, 28);
			this.menuItemMappingRules.Text = "ПРАВИЛА";
			// 
			// menuItemCreateNewRule
			// 
			this.menuItemCreateNewRule.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuItemCreateNewRule.Image = ((System.Drawing.Image)(resources.GetObject("menuItemCreateNewRule.Image")));
			this.menuItemCreateNewRule.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemCreateNewRule.Name = "menuItemCreateNewRule";
			this.menuItemCreateNewRule.Size = new System.Drawing.Size(238, 30);
			this.menuItemCreateNewRule.Text = "СОЗДАТЬ НОВОЕ ПРАВИЛО";
			this.menuItemCreateNewRule.Click += new System.EventHandler(this.menuItemCreateNewRule_Click);
			// 
			// menuItemRemoveMappingRule
			// 
			this.menuItemRemoveMappingRule.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuItemRemoveMappingRule.Image = ((System.Drawing.Image)(resources.GetObject("menuItemRemoveMappingRule.Image")));
			this.menuItemRemoveMappingRule.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemRemoveMappingRule.Name = "menuItemRemoveMappingRule";
			this.menuItemRemoveMappingRule.Size = new System.Drawing.Size(238, 30);
			this.menuItemRemoveMappingRule.Text = "УДАЛИТЬ ВЫДЕЛЕННОЕ ПРАВИЛО";
			this.menuItemRemoveMappingRule.Click += new System.EventHandler(this.MenuItemRemoveMappingRule_Click);
			// 
			// menuItemDevice
			// 
			this.menuItemDevice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.menuItemCreateDevice,
			this.menuItemRemoveDevice});
			this.menuItemDevice.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuItemDevice.Image = ((System.Drawing.Image)(resources.GetObject("menuItemDevice.Image")));
			this.menuItemDevice.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemDevice.Name = "menuItemDevice";
			this.menuItemDevice.Size = new System.Drawing.Size(103, 28);
			this.menuItemDevice.Text = "УСТРОЙСТВА";
			// 
			// menuItemCreateDevice
			// 
			this.menuItemCreateDevice.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuItemCreateDevice.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuItemCreateDevice.Image = ((System.Drawing.Image)(resources.GetObject("menuItemCreateDevice.Image")));
			this.menuItemCreateDevice.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemCreateDevice.Name = "menuItemCreateDevice";
			this.menuItemCreateDevice.Size = new System.Drawing.Size(340, 30);
			this.menuItemCreateDevice.Text = "ДОБАВИТЬ НОВОЕ У-ВО ДЛЯ ВЫДЕЛЕННОГО ПРАВИЛА";
			this.menuItemCreateDevice.Click += new System.EventHandler(this.MenuItemCreateDeviceClick);
			// 
			// menuItemRemoveDevice
			// 
			this.menuItemRemoveDevice.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuItemRemoveDevice.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuItemRemoveDevice.Image = ((System.Drawing.Image)(resources.GetObject("menuItemRemoveDevice.Image")));
			this.menuItemRemoveDevice.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menuItemRemoveDevice.Name = "menuItemRemoveDevice";
			this.menuItemRemoveDevice.Size = new System.Drawing.Size(340, 30);
			this.menuItemRemoveDevice.Text = "УДАЛИТЬ ВЫДЕЛЕННОЕ У-ВО";
			this.menuItemRemoveDevice.Click += new System.EventHandler(this.MenuItemRemoveDeviceClick);
			// 
			// menuStrip
			// 
			this.menuStrip.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuStrip.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.btSaveSettings});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip.Size = new System.Drawing.Size(819, 32);
			this.menuStrip.TabIndex = 4;
			this.menuStrip.Text = "menuStrip1";
			// 
			// btSaveSettings
			// 
			this.btSaveSettings.Image = ((System.Drawing.Image)(resources.GetObject("btSaveSettings.Image")));
			this.btSaveSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btSaveSettings.Name = "btSaveSettings";
			this.btSaveSettings.Size = new System.Drawing.Size(197, 28);
			this.btSaveSettings.Text = "Перезаписать настройки";
			this.btSaveSettings.Click += new System.EventHandler(this.btSaveSettings_Click);
			// 
			// View
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(819, 491);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStripMappingtable;
			this.Name = "View";
			this.Text = "Настройки";
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.menuStripPatterns.ResumeLayout(false);
			this.menuStripPatterns.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvPatterns)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvMappingTree)).EndInit();
			this.menuStripMappingtable.ResumeLayout(false);
			this.menuStripMappingtable.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
