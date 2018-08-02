using System;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace NameplatesMaker.EditPlatePatternDialog
{
	/// <summary>
	/// Форма для создания иредактирования шаблонов табличек
	/// </summary>
	public partial class View : Form
	{
		public View()
		{
			InitializeComponent();
		}
		
		public string InputName          { get { return txtName.Text; } }
		public double InputWidth         { get { return Convert.ToDouble(nudWidth.Value); } }
		public double InputHeight        { get { return Convert.ToDouble(nudHeight.Value); } }
		public bool   InputShowPositions { get { return cbShowPositions.Checked; } }
		
		public void Set(PlatePattern platePattern)
		{
			txtName.Text            = platePattern.Name;
			nudWidth.Value          = Convert.ToDecimal(platePattern.Width);
			nudHeight.Value         = Convert.ToDecimal(platePattern.Height);
			cbShowPositions.Checked = platePattern.ShowPositions;
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
