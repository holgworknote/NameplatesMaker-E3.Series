/*
 * Created by SharpDevelop.
 * User: OHozhatelev
 * Date: 03.07.2018
 * Time: 10:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Scada.Server.Modules
{
	partial class ExecptionHandle_Dialog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtStackTrace;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecptionHandle_Dialog));
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.btOk = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtStackTrace = new System.Windows.Forms.TextBox();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtMessage);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 5, 5);
			this.groupBox3.Size = new System.Drawing.Size(222, 146);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "MESSAGE";
			// 
			// txtMessage
			// 
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtMessage.Location = new System.Drawing.Point(4, 17);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(213, 124);
			this.txtMessage.TabIndex = 1;
			// 
			// btOk
			// 
			this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
			this.btOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btOk.Location = new System.Drawing.Point(4, 154);
			this.btOk.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
			this.btOk.Name = "btOk";
			this.btOk.Size = new System.Drawing.Size(149, 30);
			this.btOk.TabIndex = 10;
			this.btOk.Text = "HM.. OKAY";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new System.EventHandler(this.BtOk_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(4, 4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(448, 146);
			this.splitContainer1.SplitterDistance = 222;
			this.splitContainer1.TabIndex = 12;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtStackTrace);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 5, 5);
			this.groupBox1.Size = new System.Drawing.Size(222, 146);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "STACK TRACE";
			// 
			// txtStackTrace
			// 
			this.txtStackTrace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtStackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtStackTrace.Location = new System.Drawing.Point(4, 17);
			this.txtStackTrace.Multiline = true;
			this.txtStackTrace.Name = "txtStackTrace";
			this.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtStackTrace.Size = new System.Drawing.Size(213, 124);
			this.txtStackTrace.TabIndex = 1;
			// 
			// ExecptionHandle_Dialog
			// 
			this.AcceptButton = this.btOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.CancelButton = this.btOk;
			this.ClientSize = new System.Drawing.Size(456, 185);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.btOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ExecptionHandle_Dialog";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ShowIcon = false;
			this.Text = "I\'m an execption. Notice me";
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
