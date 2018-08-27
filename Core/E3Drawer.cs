using System;
using System.Linq;
using e3;

namespace Core
{
	public interface IE3Drawer
	{
		void Execute(e3Job e3job, e3Sheet e3sheet);
	}
	
	public class E3RectangleDrawer : IE3Drawer
	{
		private readonly E3Rectangle _rect;
		
		public E3RectangleDrawer(E3Rectangle rect)
		{
			if (rect == null)
				throw new ArgumentNullException("rect");
			_rect = rect;
		}
		
		public void Execute(e3Job e3job, e3Sheet e3sheet)
		{
			var graph = (e3Graph)e3job.CreateGraphObject();
			
			// Построим прямоугольник рамки
			int shtId = e3sheet.GetId();			
			double startx = _rect.Bounds.StartPoint.X;
			double starty = _rect.Bounds.StartPoint.Y;
			double endy = _rect.Bounds.GetEndPoint().Y;
			double endx = _rect.Bounds.GetEndPoint().X;
			graph.CreateRectangle(shtId, startx, starty, endx, endy);
			graph.SetColour(7);
			
			graph = null;
		}
	}
	public class E3TextFieldDrawer : IE3Drawer
	{
		private readonly E3TextField _txtField;
		
		public E3TextFieldDrawer(E3TextField txt)
		{
			_txtField = txt;
		}
		
		public void Execute(e3Job e3job, e3Sheet e3sheet)
		{
			var txt = (e3Text)e3job.CreateTextObject();
			var graph = (e3Graph)e3job.CreateGraphObject();
			
			int shtId = e3sheet.GetId();
			
			double x = _txtField.Point.X;
			double y = _txtField.Point.Y;
			
			graph.CreateText(shtId, _txtField.Value, x, y);
			txt.SetId(graph.GetId());
			txt.SetFontName(_txtField.FontFamily);
			txt.SetHeight(_txtField.FontHeight);
			txt.SetStyle(0);
			
			// Выравнивание по центру ()
			if (_txtField.TextAlignment == E3TextField.Alignment.Center)
			{
				txt.SetAlignment(2); 
				
				/* Такой вот нюанс - при выравнвании по центру E3 ведет себя неадекватно - тупо сдвигает текст зачем-то.
				   Так что нам придется восстановить изначальное положение объекта */
				
				if (_txtField.Bounds != null)
				{
					double w = _txtField.Bounds.Value.Width;
					double h = _txtField.Bounds.Value.Height;
					txt.SetBox(w, h);
							
					x = x + 0.5*w;
					txt.SetSchemaLocation(x, y);
				}
			}
			
			txt = null;
			graph = null;
		}
	}
	public class E3PlateDrawer : IE3Drawer
	{
		private readonly E3Plate _plate;
		
		public E3PlateDrawer(E3Plate plate)
		{
			if (plate == null)
				throw new ArgumentNullException("plate");
			
			_plate = plate;
		}
		
		public void Execute(e3Job e3job, e3Sheet e3sheet)
		{
			_plate.Border.Draw(e3job, e3sheet);
			_plate.Header.Draw(e3job, e3sheet);
			_plate.TextFields.ForEach(x => x.Draw(e3job, e3sheet));
		}
	}
	public class E3SheetDrawer : IE3Drawer
	{
		private readonly E3Sheet _sheet;
		
		public E3SheetDrawer(E3Sheet sheet)
		{
			if (sheet == null)
				throw new ArgumentNullException("sheet");
			_sheet = sheet;
		}
		
		public void Execute(e3Job e3job, e3Sheet e3sheet)
		{
			// Создадим новый лист
			e3sheet.Create(0, _sheet.Name, _sheet.Format, 0, 0);
			
			// Отрисуем все содержимое листа
			foreach (var drawing in _sheet.Drawings)
				drawing.Draw(e3job, e3sheet);
		}
	}
}
