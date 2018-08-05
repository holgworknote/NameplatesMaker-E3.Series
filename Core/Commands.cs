using System;
using System.Linq;
using e3;

namespace Core
{
	public interface IWriteSheetCommand
	{
		void Execute(e3Job e3job);
	}
	public class WriteSheetCommand : IWriteSheetCommand
	{
		private readonly PlatesSheet _platesSheet;
		
		public WriteSheetCommand(PlatesSheet platesSheet)
		{
			if (platesSheet == null)
				throw new ArgumentNullException("platesSheet");
			_platesSheet = platesSheet;
		}
		
		public void Execute(e3Job e3Job)
		{
			var e3Sht = (e3Sheet)e3Job.CreateSheetObject();
			e3Sht.Create(0, "demo", _platesSheet.SymbolName, 0, 0);
			
			foreach (var plate in _platesSheet)
				new WritePlateCommand(plate).Execute(e3Job, e3Sht);
		}
	}
	
	public interface IWritePlateCommand
	{
		void Execute(e3Job e3job, e3Sheet e3sheet);
	}
	public class WritePlateCommand
	{
		private readonly Plate _plate;
		
		public WritePlateCommand(Plate plate)
		{
			_plate = plate;
		}
		
		public void Execute(e3Job e3job, e3Sheet e3sheet)
		{
			var txt = (e3Text)e3job.CreateTextObject();
			var graph = (e3Graph)e3job.CreateGraphObject();
			
			// Построим прямоугольник рамки
			int shtId = e3sheet.GetId();			
			double startx = _plate.Rectangle.StartPoint.X;
			double starty = _plate.Rectangle.StartPoint.Y;
			double endx = _plate.Rectangle.GetEndPoint().X;
			double endy = _plate.Rectangle.GetEndPoint().Y;
			graph.CreateRectangle(shtId, startx, starty, endx, endy);
			graph.SetColour(7);
			
			// Создадим текстовое поле
			double x = startx + _plate.Rectangle.Width/2;
			double y = starty + _plate.Rectangle.Height/2;
			graph.CreateText(shtId, _plate.Header, x, y);
			txt.SetId(graph.GetId());
			txt.SetAlignment(2); // Выравнивание по центру
			txt.SetFontName("GOST type A");
			txt.SetHeight(3.5);
			txt.SetBox(_plate.Rectangle.Width, _plate.Rectangle.Height*2/3);
			
			if (_plate.GotPositions)
				this.BuildPositions(e3job, shtId);
			
			txt = null;
			graph = null;
		}
		
		private void BuildPositions(e3Job e3job, int shtId)
		{
			var txt = (e3Text)e3job.CreateTextObject();
			var graph = (e3Graph)e3job.CreateGraphObject();
			
			// Убедимся, что размерность массива = 9
			if (_plate.Positions.Count() != 9)
				throw new ArgumentException("Размерность массива позиций должна быть равна 9-ти!");
			
			// Рассчитаем ширину и высоту одной ячейки, в которую будет записываться значение
			double w = _plate.Rectangle.Width/9;
			double h = _plate.Rectangle.Height/3;
			
			int i = 0;
			double newPosX = _plate.Rectangle.StartPoint.X + w/2;
			while (i < 9)
			{
				// Создадим текстовое поле
				graph.CreateText(shtId, _plate.Positions[i], newPosX, _plate.Rectangle.StartPoint.Y + h/2);
				txt.SetId(graph.GetId());
				txt.SetAlignment(2); // Выравнивание по центру
				txt.SetFontName("GOST type A");
				txt.SetHeight(3.5);
				txt.SetBox(w, h);
								
				newPosX = newPosX + w;				
				i++;
			}
			
			txt = null;
			graph = null;
		}
	}
}
