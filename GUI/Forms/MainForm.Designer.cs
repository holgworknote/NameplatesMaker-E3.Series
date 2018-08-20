/*
 * Created by SharpDevelop.
 * User: OHozhatelev
 * Date: 31.07.2018
 * Time: 16:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace GUI
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btStart;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btClearOutput;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.ToolStripMenuItem btShowSettings;
		private System.Windows.Forms.MenuStrip menuStrip;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btStart = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btClearOutput = new System.Windows.Forms.Button();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.btShowSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.groupBox2.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// btStart
			// 
			this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btStart.BackColor = System.Drawing.Color.White;
			this.btStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btStart.Image = ((System.Drawing.Image)(resources.GetObject("btStart.Image")));
			this.btStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btStart.Location = new System.Drawing.Point(173, 224);
			this.btStart.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.btStart.Name = "btStart";
			this.btStart.Size = new System.Drawing.Size(127, 32);
			this.btStart.TabIndex = 1;
			this.btStart.Text = "   START";
			this.btStart.UseVisualStyleBackColor = false;
			this.btStart.Click += new System.EventHandler(this.BtStart_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.btClearOutput);
			this.groupBox2.Controls.Add(this.btStart);
			this.groupBox2.Controls.Add(this.txtOutput);
			this.groupBox2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.Location = new System.Drawing.Point(12, 35);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBox2.Size = new System.Drawing.Size(306, 262);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "OUTPUT";
			// 
			// btClearOutput
			// 
			this.btClearOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btClearOutput.BackColor = System.Drawing.Color.White;
			this.btClearOutput.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClearOutput.Image = ((System.Drawing.Image)(resources.GetObject("btClearOutput.Image")));
			this.btClearOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btClearOutput.Location = new System.Drawing.Point(4, 224);
			this.btClearOutput.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
			this.btClearOutput.Name = "btClearOutput";
			this.btClearOutput.Size = new System.Drawing.Size(166, 32);
			this.btClearOutput.TabIndex = 3;
			this.btClearOutput.Text = "Очистить окно вывода";
			this.btClearOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btClearOutput.UseVisualStyleBackColor = false;
			this.btClearOutput.Click += new System.EventHandler(this.BtClearOutputClick);
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutput.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOutput.Location = new System.Drawing.Point(6, 19);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutput.Size = new System.Drawing.Size(294, 201);
			this.txtOutput.TabIndex = 0;
			// 
			// btShowSettings
			// 
			this.btShowSettings.Image = ((System.Drawing.Image)(resources.GetObject("btShowSettings.Image")));
			this.btShowSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btShowSettings.Name = "btShowSettings";
			this.btShowSettings.Size = new System.Drawing.Size(106, 28);
			this.btShowSettings.Text = "Настройки";
			this.btShowSettings.Click += new System.EventHandler(this.BtShowSettings_Click);
			// 
			// menuStrip
			// 
			this.menuStrip.BackColor = System.Drawing.Color.WhiteSmoke;
			this.menuStrip.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.btShowSettings});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip.Size = new System.Drawing.Size(330, 32);
			this.menuStrip.TabIndex = 5;
			this.menuStrip.Text = "menuStrip1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(330, 309);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.groupBox2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(338, 336);
			this.Name = "MainForm";
			this.Text = "NameplatesMaker";
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
