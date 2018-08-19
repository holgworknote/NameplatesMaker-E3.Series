/*
 * Created by SharpDevelop.
 * User: TL
 * Date: 15.08.2018
 * Time: 17:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace GUI
{
	partial class MessageBox_Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.Label lbImage;
		private System.Windows.Forms.TextBox txtBody;
		private System.Windows.Forms.ImageList imageList;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBox_Form));
			this.btOk = new System.Windows.Forms.Button();
			this.lbImage = new System.Windows.Forms.Label();
			this.txtBody = new System.Windows.Forms.TextBox();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// btOk
			// 
			this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
			this.btOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btOk.Location = new System.Drawing.Point(212, 67);
			this.btOk.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
			this.btOk.Name = "btOk";
			this.btOk.Size = new System.Drawing.Size(119, 27);
			this.btOk.TabIndex = 11;
			this.btOk.Text = "OK";
			this.btOk.UseVisualStyleBackColor = true;
			// 
			// lbImage
			// 
			this.lbImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.lbImage.Image = ((System.Drawing.Image)(resources.GetObject("lbImage.Image")));
			this.lbImage.Location = new System.Drawing.Point(4, 4);
			this.lbImage.Name = "lbImage";
			this.lbImage.Size = new System.Drawing.Size(65, 58);
			this.lbImage.TabIndex = 12;
			// 
			// txtBody
			// 
			this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBody.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtBody.Location = new System.Drawing.Point(74, 4);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.ReadOnly = true;
			this.txtBody.Size = new System.Drawing.Size(257, 58);
			this.txtBody.TabIndex = 13;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "error");
			this.imageList.Images.SetKeyName(1, "info");
			// 
			// MessageBox_Form
			// 
			this.AcceptButton = this.btOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.CancelButton = this.btOk;
			this.ClientSize = new System.Drawing.Size(335, 97);
			this.Controls.Add(this.txtBody);
			this.Controls.Add(this.lbImage);
			this.Controls.Add(this.btOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(343, 121);
			this.Name = "MessageBox_Form";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
