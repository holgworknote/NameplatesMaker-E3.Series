/*
 * Created by SharpDevelop.
 * User: OHozhatelev
 * Date: 01.08.2018
 * Time: 16:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace NameplatesMaker.SettingsManager
{
	partial class View
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.GroupBox groupBox2;
		private BrightIdeasSoftware.ObjectListView olvPatterns;
		private System.Windows.Forms.Button btAddPattern;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox1;
		private BrightIdeasSoftware.TreeListView olvMappingTree;
		private System.Windows.Forms.Button btRemoveMappingItem;
		private System.Windows.Forms.Button btAddMappingItem;
		private System.Windows.Forms.Button btRemovePattern;
		private System.Windows.Forms.ToolStripMenuItem btSaveSettings;
		private System.Windows.Forms.Button btCreateMappingRule;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtSheetName;
		
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
			this.btRemovePattern = new System.Windows.Forms.Button();
			this.olvPatterns = new BrightIdeasSoftware.ObjectListView();
			this.btAddPattern = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btCreateMappingRule = new System.Windows.Forms.Button();
			this.olvMappingTree = new BrightIdeasSoftware.TreeListView();
			this.btRemoveMappingItem = new System.Windows.Forms.Button();
			this.btAddMappingItem = new System.Windows.Forms.Button();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.btSaveSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtSheetName = new System.Windows.Forms.TextBox();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvPatterns)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvMappingTree)).BeginInit();
			this.menuStrip.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.btRemovePattern);
			this.groupBox2.Controls.Add(this.olvPatterns);
			this.groupBox2.Controls.Add(this.btAddPattern);
			this.groupBox2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.Location = new System.Drawing.Point(0, 48);
			this.groupBox2.MinimumSize = new System.Drawing.Size(193, 50);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBox2.Size = new System.Drawing.Size(393, 396);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "PATTERNS LIST";
			// 
			// btRemovePattern
			// 
			this.btRemovePattern.BackColor = System.Drawing.Color.White;
			this.btRemovePattern.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btRemovePattern.Image = ((System.Drawing.Image)(resources.GetObject("btRemovePattern.Image")));
			this.btRemovePattern.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btRemovePattern.Location = new System.Drawing.Point(102, 17);
			this.btRemovePattern.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
			this.btRemovePattern.Name = "btRemovePattern";
			this.btRemovePattern.Size = new System.Drawing.Size(85, 32);
			this.btRemovePattern.TabIndex = 5;
			this.btRemovePattern.Text = "Удалить";
			this.btRemovePattern.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btRemovePattern.UseVisualStyleBackColor = false;
			this.btRemovePattern.Click += new System.EventHandler(this.BtRemove_Click);
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
			this.olvPatterns.Size = new System.Drawing.Size(381, 337);
			this.olvPatterns.TabIndex = 0;
			this.olvPatterns.UseCompatibleStateImageBehavior = false;
			this.olvPatterns.View = System.Windows.Forms.View.Details;
			this.olvPatterns.DoubleClick += new System.EventHandler(this.OlvPatterns_DoubleClick);
			// 
			// btAddPattern
			// 
			this.btAddPattern.BackColor = System.Drawing.Color.White;
			this.btAddPattern.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAddPattern.Image = ((System.Drawing.Image)(resources.GetObject("btAddPattern.Image")));
			this.btAddPattern.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btAddPattern.Location = new System.Drawing.Point(6, 17);
			this.btAddPattern.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
			this.btAddPattern.Name = "btAddPattern";
			this.btAddPattern.Size = new System.Drawing.Size(92, 32);
			this.btAddPattern.TabIndex = 4;
			this.btAddPattern.Text = "Добавить";
			this.btAddPattern.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btAddPattern.UseVisualStyleBackColor = false;
			this.btAddPattern.Click += new System.EventHandler(this.BtAddPattern_Click);
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
			this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(795, 444);
			this.splitContainer1.SplitterDistance = 393;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 3;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btCreateMappingRule);
			this.groupBox1.Controls.Add(this.olvMappingTree);
			this.groupBox1.Controls.Add(this.btRemoveMappingItem);
			this.groupBox1.Controls.Add(this.btAddMappingItem);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.MinimumSize = new System.Drawing.Size(377, 50);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBox1.Size = new System.Drawing.Size(396, 444);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "MAPPING TABLE";
			// 
			// btCreateMappingRule
			// 
			this.btCreateMappingRule.BackColor = System.Drawing.Color.White;
			this.btCreateMappingRule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btCreateMappingRule.Image = ((System.Drawing.Image)(resources.GetObject("btCreateMappingRule.Image")));
			this.btCreateMappingRule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btCreateMappingRule.Location = new System.Drawing.Point(6, 17);
			this.btCreateMappingRule.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
			this.btCreateMappingRule.Name = "btCreateMappingRule";
			this.btCreateMappingRule.Size = new System.Drawing.Size(179, 32);
			this.btCreateMappingRule.TabIndex = 6;
			this.btCreateMappingRule.Text = "Создать новое правило";
			this.btCreateMappingRule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btCreateMappingRule.UseVisualStyleBackColor = false;
			this.btCreateMappingRule.Click += new System.EventHandler(this.btCreateMappingRule_Click);
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
			this.olvMappingTree.Location = new System.Drawing.Point(6, 55);
			this.olvMappingTree.Name = "olvMappingTree";
			this.olvMappingTree.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
			this.olvMappingTree.SelectedForeColor = System.Drawing.Color.Black;
			this.olvMappingTree.ShowGroups = false;
			this.olvMappingTree.ShowHeaderInAllViews = false;
			this.olvMappingTree.Size = new System.Drawing.Size(384, 383);
			this.olvMappingTree.TabIndex = 5;
			this.olvMappingTree.UseCompatibleStateImageBehavior = false;
			this.olvMappingTree.View = System.Windows.Forms.View.Details;
			this.olvMappingTree.VirtualMode = true;
			this.olvMappingTree.DoubleClick += new System.EventHandler(this.OlvMappingTreeDoubleClick);
			// 
			// btRemoveMappingItem
			// 
			this.btRemoveMappingItem.BackColor = System.Drawing.Color.White;
			this.btRemoveMappingItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btRemoveMappingItem.Image = ((System.Drawing.Image)(resources.GetObject("btRemoveMappingItem.Image")));
			this.btRemoveMappingItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btRemoveMappingItem.Location = new System.Drawing.Point(285, 17);
			this.btRemoveMappingItem.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
			this.btRemoveMappingItem.Name = "btRemoveMappingItem";
			this.btRemoveMappingItem.Size = new System.Drawing.Size(85, 32);
			this.btRemoveMappingItem.TabIndex = 3;
			this.btRemoveMappingItem.Text = "Удалить";
			this.btRemoveMappingItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btRemoveMappingItem.UseVisualStyleBackColor = false;
			this.btRemoveMappingItem.Click += new System.EventHandler(this.BtRemoveMapping_ItemClick);
			// 
			// btAddMappingItem
			// 
			this.btAddMappingItem.BackColor = System.Drawing.Color.White;
			this.btAddMappingItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAddMappingItem.Image = ((System.Drawing.Image)(resources.GetObject("btAddMappingItem.Image")));
			this.btAddMappingItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btAddMappingItem.Location = new System.Drawing.Point(189, 17);
			this.btAddMappingItem.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
			this.btAddMappingItem.Name = "btAddMappingItem";
			this.btAddMappingItem.Size = new System.Drawing.Size(92, 32);
			this.btAddMappingItem.TabIndex = 2;
			this.btAddMappingItem.Text = "Добавить";
			this.btAddMappingItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btAddMappingItem.UseVisualStyleBackColor = false;
			this.btAddMappingItem.Click += new System.EventHandler(this.BtAddMappingItem_Click);
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
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.txtSheetName);
			this.groupBox3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(12, 35);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(393, 42);
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
			this.txtSheetName.Size = new System.Drawing.Size(381, 20);
			this.txtSheetName.TabIndex = 0;
			// 
			// View
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(819, 491);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "View";
			this.Text = "Настройки";
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.olvPatterns)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.olvMappingTree)).EndInit();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
