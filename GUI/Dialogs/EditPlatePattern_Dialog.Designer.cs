/*
 * Created by SharpDevelop.
 * User: OHozhatelev
 * Date: 01.08.2018
 * Time: 16:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace GUI.EditPlatePatternDialog
{
	partial class View
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox cbShowPositions;
		private System.Windows.Forms.NumericUpDown nudHeight;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudWidth;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.Button btApply;
		private System.Windows.Forms.NumericUpDown nudFontSize;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown nudMaxLineLength;
		private System.Windows.Forms.Label label5;
		
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.nudFontSize = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.cbShowPositions = new System.Windows.Forms.CheckBox();
			this.nudHeight = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.nudWidth = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.btCancel = new System.Windows.Forms.Button();
			this.btApply = new System.Windows.Forms.Button();
			this.nudMaxLineLength = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxLineLength)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.nudMaxLineLength);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.nudFontSize);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.cbShowPositions);
			this.groupBox1.Controls.Add(this.nudHeight);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.nudWidth);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtName);
			this.groupBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.groupBox1.Size = new System.Drawing.Size(453, 131);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "DETAILS";
			// 
			// nudFontSize
			// 
			this.nudFontSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nudFontSize.DecimalPlaces = 1;
			this.nudFontSize.Increment = new decimal(new int[] {
			1,
			0,
			0,
			65536});
			this.nudFontSize.Location = new System.Drawing.Point(188, 77);
			this.nudFontSize.Name = "nudFontSize";
			this.nudFontSize.Size = new System.Drawing.Size(94, 20);
			this.nudFontSize.TabIndex = 15;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(188, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(94, 16);
			this.label4.TabIndex = 14;
			this.label4.Text = "РАЗМЕР ШРИФТА:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbShowPositions
			// 
			this.cbShowPositions.Location = new System.Drawing.Point(6, 103);
			this.cbShowPositions.Name = "cbShowPositions";
			this.cbShowPositions.Size = new System.Drawing.Size(276, 24);
			this.cbShowPositions.TabIndex = 13;
			this.cbShowPositions.Text = "ОТОБРАЖАТЬ ПОЛОЖЕНИЯ";
			this.cbShowPositions.UseVisualStyleBackColor = true;
			// 
			// nudHeight
			// 
			this.nudHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nudHeight.Location = new System.Drawing.Point(97, 77);
			this.nudHeight.Name = "nudHeight";
			this.nudHeight.Size = new System.Drawing.Size(85, 20);
			this.nudHeight.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(97, 58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "ВЫСОТА:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// nudWidth
			// 
			this.nudWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nudWidth.Location = new System.Drawing.Point(6, 77);
			this.nudWidth.Name = "nudWidth";
			this.nudWidth.Size = new System.Drawing.Size(85, 20);
			this.nudWidth.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "ШИРИНА:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "НАЗВАНИЕ:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtName.Location = new System.Drawing.Point(6, 35);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(440, 20);
			this.txtName.TabIndex = 2;
			// 
			// btCancel
			// 
			this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
			this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btCancel.Location = new System.Drawing.Point(427, 142);
			this.btCancel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(30, 30);
			this.btCancel.TabIndex = 11;
			this.btCancel.Text = "X";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new System.EventHandler(this.BtCancelClick);
			// 
			// btApply
			// 
			this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btApply.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
			this.btApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btApply.Location = new System.Drawing.Point(292, 142);
			this.btApply.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
			this.btApply.Name = "btApply";
			this.btApply.Size = new System.Drawing.Size(131, 30);
			this.btApply.TabIndex = 12;
			this.btApply.Text = "OK";
			this.btApply.UseVisualStyleBackColor = true;
			this.btApply.Click += new System.EventHandler(this.BtApplyClick);
			// 
			// nudMaxLineLength
			// 
			this.nudMaxLineLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nudMaxLineLength.Location = new System.Drawing.Point(288, 77);
			this.nudMaxLineLength.Name = "nudMaxLineLength";
			this.nudMaxLineLength.Size = new System.Drawing.Size(158, 20);
			this.nudMaxLineLength.TabIndex = 17;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(288, 58);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(158, 16);
			this.label5.TabIndex = 16;
			this.label5.Text = "КОЛ-ВО СИМВОЛОВ В СТРОКЕ:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// View
			// 
			this.AcceptButton = this.btApply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.CancelButton = this.btCancel;
			this.ClientSize = new System.Drawing.Size(461, 176);
			this.Controls.Add(this.btApply);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "View";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Шаблон таблички:";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxLineLength)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
