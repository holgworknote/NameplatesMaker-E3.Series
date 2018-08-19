using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace GUI.PlatePatternSelector
{
	/// <summary>
	/// Форма для выбора шаблона табличик из существующих
	/// </summary>	
	public partial class View : Form
	{
		public View(IEnumerable<PlatePattern> patterns)
		{
			InitializeComponent();
			cbPatterns.DataSource = patterns.ToList();
			cbPatterns.DisplayMember = "Name";
		}
		
		public PlatePattern GetInput()
		{
			PlatePattern ret = null;
			
			var obj = cbPatterns.SelectedValue;
			if (obj != null)
				ret = obj as PlatePattern;
			
			return ret;
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
