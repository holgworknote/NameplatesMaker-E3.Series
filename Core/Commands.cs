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
		private readonly ILogger _logger;
		
		public WriteSheetCommand(PlatesSheet platesSheet, ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			if (platesSheet == null)
				throw new ArgumentNullException("platesSheet");
			
			_platesSheet = platesSheet;
			_logger = logger;
		}
		
		public void Execute(e3Job e3Job)
		{
			var e3Sht = (e3Sheet)e3Job.CreateSheetObject();
			e3Sht.Create(0, "Plates", _platesSheet.SymbolName, 0, 0);
			
			foreach (var plate in _platesSheet.Plates)
				new WritePlateCommand(plate, _logger).Execute(e3Job, e3Sht);
			
			foreach (var txtField in _platesSheet.TextFields)
				new WriteTextFieldCommand(txtField).Execute(e3Job, e3Sht);
		}
	}
	
	public interface IWriteTextFieldCommand
	{
		void Execute(e3Job e3job, e3Sheet e3sheet);
	}
	public class WriteTextFieldCommand : IWriteTextFieldCommand
	{
		private readonly TextField _txtField;
		
		public WriteTextFieldCommand(TextField txtField)
		{
			_txtField = txtField;
		}
		
		public void Execute(e3Job e3job, e3Sheet e3sheet)
		{
			var txt = (e3Text)e3job.CreateTextObject();
			var graph = (e3Graph)e3job.CreateGraphObject();
			
			// Построим прямоугольник рамки
			int shtId = e3sheet.GetId();			
			
			// Создадим текстовое поле
			string val = _txtField.Value;
			double x = _txtField.Point.X;
			double y = _txtField.Point.Y;
			int ret = graph.CreateText(shtId, val, x, y);
			
			txt.SetId(graph.GetId());
			txt.SetFontName("GOST type A");
			txt.SetHeight(3.5);			
			
			txt = null;
			graph = null;
		}
	}
	
	public interface IWritePlateCommand
	{
		void Execute(e3Job e3job, e3Sheet e3sheet);
	}
	public class WritePlateCommand : IWritePlateCommand
	{
		private readonly Plate   _plate;
		private readonly ILogger _logger;
		private readonly string  _fontFamily  = "Consolas";
		
		public WritePlateCommand(Plate plate, ILogger logger)
		{
			if (plate == null)
				throw new ArgumentNullException("plate");
			if (logger == null)
				throw new ArgumentNullException("logger");
			
			_plate = plate;
			_logger = logger;
		}
		
		public void Execute(e3Job e3job, e3Sheet e3sheet)
		{
			var txt = (e3Text)e3job.CreateTextObject();
			var graph = (e3Graph)e3job.CreateGraphObject();
			
			// Построим прямоугольник рамки
			int shtId = e3sheet.GetId();			
			double startx = _plate.Rectangle.StartPoint.X;
			double starty = _plate.Rectangle.StartPoint.Y;
			double endy = _plate.Rectangle.GetEndPoint().Y;
			double endx = _plate.Rectangle.GetEndPoint().X;
			graph.CreateRectangle(shtId, startx, starty, endx, endy);
			graph.SetColour(7);

			var txtBounds = new BoundsCalculator(_plate).Calculate();
			var txtRect = new TextCalculator(_plate.FontSize, _plate.MaxLength, txtBounds, _logger).GetRectangle(_plate.Header); // FIXME: !!!
			
			double x = txtRect.StartPoint.X;
			double y = txtRect.StartPoint.Y;
			
			graph.CreateText(shtId, _plate.Header, x, y);
			txt.SetId(graph.GetId());
			txt.SetAlignment(2); // Выравнивание по центру
			txt.SetFontName("Consolas");
			txt.SetHeight(_plate.FontSize);
			
			txt.SetBox(txtRect.Width, txtRect.Height);
			
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
			double h = _plate.Rectangle.Height/8; // 8 - эксперементальный коэффициент
			
			int i = 0;
			double newPosX = _plate.Rectangle.StartPoint.X + w/2;
			while (i < 9)
			{
				// Создадим текстовое поле
				graph.CreateText(shtId, _plate.Positions[i], newPosX, _plate.Rectangle.StartPoint.Y + h/2);
				txt.SetId(graph.GetId());
				txt.SetAlignment(2); // Выравнивание по центру
				txt.SetFontName(_fontFamily);
				txt.SetHeight(_plate.FontSize);
				txt.SetBox(w, h);
								
				newPosX = newPosX + w;				
				i++;
			}
			
			txt = null;
			graph = null;
		}			
	}
	
	// === UTILITY============================================
	
	/// <summary>
	/// Класс для расчета фактических размеров текстового поля
	/// </summary>
	public class TextCalculator
	{
		private readonly double    _fontHeight;       // Высота шрифта
		private readonly double    _lineSymbolsCount; // Максимальное кол-во символов в строке
		private readonly double    _lineSpacing;      // Междустрочное расстояние
		private readonly Rectangle _bounds;           // Границы текстового поля
		private readonly ILogger   _logger;           // Логгер
		
		public TextCalculator(double fontHeight, double lineSymbolsCount, Rectangle bounds, ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			
			_fontHeight       = fontHeight;
			_lineSymbolsCount = lineSymbolsCount;
			_lineSpacing      = 0.4*fontHeight;
			_bounds           = bounds;
			_logger           = logger;
		}
		
		public Rectangle GetRectangle(string text)
		{
			// Получим полное кол-во строк
			double res = (double)(text.Length)/_lineSymbolsCount;
			int c = (int)(Math.Ceiling(res));
						
			double h = (_fontHeight + _lineSpacing)*c;
			double w = _bounds.Width;
			
			// Рассчитаем начальную точку
			double x = _bounds.StartPoint.X;
			double y = _bounds.StartPoint.Y - 0.5*_bounds.Height + 0.75*h;
			var p = new Point(x, y);
			
			// Если границы таблички нарушены, то напишем это в логгер
			if (h > _bounds.Height)
				_logger.WriteLine("Нарушены границы таблички: " + text);
			
			return new Rectangle(p, w, h);
		}
	}
	
	/// <summary>
	/// Класс для расчета границ текстового поля для таблички
	/// </summary>
	public class BoundsCalculator
	{
		private readonly Plate _plate;
		
		public BoundsCalculator(Plate plate)
		{
			if (plate == null)
				throw new ArgumentNullException("plate");
			_plate = plate;
		}
		
		public Rectangle Calculate()
		{
			/* 	Все цифры, использованные в этом методе сугубо волшебные и подобраны опытным путем ¯\_(ツ)_/¯
				Олсо, сдвиг по оси X - не бред, а фактическая необходимость, т.к. E3 при выравнивании 
				текстового объекта какого-то хера сдвигает его */
			
			if (_plate.GotPositions)
			{
				double x = _plate.Rectangle.StartPoint.X + _plate.Rectangle.Width/2;
				double y = _plate.Rectangle.StartPoint.Y + 0.58*_plate.Rectangle.Height;
				return new Rectangle(new Point(x, y), _plate.Rectangle.Width, _plate.Rectangle.Height*2/3);
			}	
			else
			{
				double x = _plate.Rectangle.StartPoint.X + _plate.Rectangle.Width/2;
				double y = _plate.Rectangle.StartPoint.Y + _plate.Rectangle.Height/2;
				return new Rectangle(new Point(x, y), _plate.Rectangle.Width, _plate.Rectangle.Height);
			}	
		}
	}
}
