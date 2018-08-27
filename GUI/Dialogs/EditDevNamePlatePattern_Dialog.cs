using System;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace GUI.EditDevNamePlatePatternDialog
{
	/// <summary>
	/// Форма для редактирования шаблона таблички для позиционных обозначений
	/// </summary>
	public partial class View : Form
	{
		public View()
		{
			InitializeComponent();
		}
		
		public double InputWidth    { get { return Convert.ToDouble(nudWidth.Value); } }
		public double InputHeight   { get { return Convert.ToDouble(nudHeight.Value); } }
		public double InputFontSize { get { return Convert.ToDouble(nudFontSize.Value); } }
		
		public void Set(DeviceNamePattern platePattern)
		{
			nudWidth.Value    = Convert.ToDecimal(platePattern.Width);
			nudHeight.Value   = Convert.ToDecimal(platePattern.Height);
			nudFontSize.Value = Convert.ToDecimal(platePattern.FontHeight);
		}
		
		void BtApplyClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		void BtCancelClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
