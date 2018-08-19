using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
	/// <summary>
	/// MessageBox Form
	/// </summary>
	public partial class MessageBox_Form : Form
	{
		public MessageBox_Form(string title, string body, EventType eventType)
		{
			InitializeComponent();
			
			this.Text = title;
			txtBody.Text = body;
			
			switch (eventType)
			{
				case (EventType.Error) : lbImage.Image = imageList.Images["error"]; break;
				case (EventType.Information) : lbImage.Image = imageList.Images["info"]; break;
			}				
		}
	}
	
	public static class MyMessageBox
	{
		public static void ShowInfo(string body)
		{
		    var dlg = new MessageBox_Form("Information", body, EventType.Information);
		    dlg.ShowDialog();
		}
	}
	
	public enum EventType
	{
		Error,
		Information,
	}
}
