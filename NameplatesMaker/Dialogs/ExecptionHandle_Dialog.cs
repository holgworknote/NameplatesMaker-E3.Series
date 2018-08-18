using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
	public partial class ExecptionHandle_Dialog : Form
	{
		public ExecptionHandle_Dialog(string msg, string stackTrace)
		{
			InitializeComponent();
			txtMessage.Text = msg;
			txtStackTrace.Text = stackTrace;
		}
		
		void BtOk_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
	}
	
	public static class ExceptionsHandler
	{
		public static void Handle(this Exception exc)
		{
			var dlg = new ExecptionHandle_Dialog(exc.Message, exc.StackTrace);
			dlg.ShowDialog();
		}
	}
}
