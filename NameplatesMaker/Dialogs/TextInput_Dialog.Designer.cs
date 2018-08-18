/*
 * Created by SharpDevelop.
 * User: OHozhatelev
 * Date: 02.08.2018
 * Time: 10:39
 */
namespace GUI.TextInput
{
	partial class View
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btApply;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.TextBox txtInput;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View));
			this.btApply = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btApply
			// 
			this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.btApply.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
			this.btApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btApply.Location = new System.Drawing.Point(4, 30);
			this.btApply.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
			this.btApply.Name = "btApply";
			this.btApply.Size = new System.Drawing.Size(192, 30);
			this.btApply.TabIndex = 17;
			this.btApply.Text = "OK";
			this.btApply.UseVisualStyleBackColor = true;
			this.btApply.Click += new System.EventHandler(this.BtApply_Click);
			// 
			// btCancel
			// 
			this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
			this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btCancel.Location = new System.Drawing.Point(202, 30);
			this.btCancel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(30, 30);
			this.btCancel.TabIndex = 16;
			this.btCancel.Text = "X";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// txtInput
			// 
			this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInput.Location = new System.Drawing.Point(4, 4);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(228, 20);
			this.txtInput.TabIndex = 18;
			// 
			// View
			// 
			this.AcceptButton = this.btApply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.CancelButton = this.btCancel;
			this.ClientSize = new System.Drawing.Size(236, 63);
			this.Controls.Add(this.txtInput);
			this.Controls.Add(this.btApply);
			this.Controls.Add(this.btCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "View";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Введите наименование устройства:";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
