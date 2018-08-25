using System;
using System.Collections.Generic;
using System.Linq;
using e3;

namespace Core
{
	public interface IE3Drawing
	{
		void Draw(e3Job e3job, e3Sheet e3sheet);
	}
	
	public class E3Rectangle : IE3Drawing
	{
		public Rectangle Bounds { get; set; }
		
		public E3Rectangle(Rectangle bounds)
		{
			this.Bounds = bounds;	
		}
		
		public void Draw(e3Job e3job, e3Sheet e3sheet)
		{
			var drawer = new E3RectangleDrawer(this);
			drawer.Execute(e3job, e3sheet);
		}
	}	
	public class E3TextField : IE3Drawing
	{
		public string     Value         { get; set; }
		public Point      Point         { get; set; }
		public Alignment  TextAlignment { get; set; }
		public double     FontHeight    { get; set; }
		public string     FontFamily    { get; set; }
		public Rectangle? Bounds        { get; set; }
	
		public E3TextField(string val, Point p, double h, string font, Alignment textAlignment = E3TextField.Alignment.Left, 
		                   Rectangle? bounds = null)
		{
			this.Value         = val;
			this.Point         = p;
			this.TextAlignment = textAlignment;
			this.FontHeight    = h;
			this.FontFamily    = font;
			this.Bounds        = bounds;
		}	

		public void Draw(e3Job e3job, e3Sheet e3sheet)
		{
			var drawer = new E3TextFieldDrawer(this);
			drawer.Execute(e3job, e3sheet);
		}

		public enum Alignment 
		{
			Right, 
			Center, 
			Left
		}
	}
	public class E3Plate : IE3Drawing
	{
		public E3Rectangle       Border     { get; set; }
		public E3TextField       Header     { get; set; }
		public List<E3TextField> TextFields { get; set; }
		
		public E3Plate(E3Rectangle border, E3TextField header)
		{
			this.Border = border;
			this.Header = header;
			this.TextFields = new List<E3TextField>();
		}
		
		public void Draw(e3Job e3job, e3Sheet e3sheet)
		{
			var drawer = new E3PlateDrawer(this);
			drawer.Execute(e3job, e3sheet);
		}
	}	
	public class E3Sheet : IE3Drawing
	{		
		public string           Format   { get; set; }
		public string           Name     { get; set; }
		public List<IE3Drawing> Drawings { get; private set; }
		
		public E3Sheet(string format, string name)
		{
			this.Format = format;
			this.Name = name;
			this.Drawings = new List<IE3Drawing>();
		}
		
		public void Draw(e3Job e3job, e3Sheet e3sheet)
		{
			var drawer = new E3SheetDrawer(this);
			drawer.Execute(e3job, e3sheet);
		}	
	}
}
