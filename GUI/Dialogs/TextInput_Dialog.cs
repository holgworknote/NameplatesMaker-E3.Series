using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.TextInput
{
	/// <summary>
	/// Простая форма для ввода текстового значения
	/// </summary>
	public partial class View : Form
	{
		public View(string val = null)
		{
			InitializeComponent();
			txtInput.Text = val;
		}
		
		public string GetInput()
		{
			return txtInput.Text;
		}
		
		void BtApply_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		void BtCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
