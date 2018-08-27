using System;
using System.Linq;
using System.Collections.Generic;

namespace Core
{			
	/// <summary>
	/// Класс для расчета подписей для переключателей
	/// </summary>
	public class SwitcherTextFieldsCalculator
	{
		private readonly Rectangle _plateRect;
		private readonly string _fontFamily;
		private readonly ILogger _logger;
		
		public SwitcherTextFieldsCalculator(ILogger logger, Rectangle plateRect, string fontFamily)
		{
			_logger = logger;
			_plateRect = plateRect;
			_fontFamily = fontFamily;
		}
		
		public IEnumerable<E3TextField> Calculate(string[] values, double fontHeight)
		{
			var fields = new List<E3TextField>();
			
			// Убедимся, что размерность массива = 9
			if (values.Count() != 9)
				throw new ArgumentException("Размерность массива позиций должна быть равна 9-ти!");
			
			// Рассчитаем ширину и высоту одной ячейки, в которую будет записываться значение
			double w = _plateRect.Width/9;
			double h =_plateRect.Height/8; // 8 - эксперементальный коэффициент
			
			
			int i = 0;
			double newPosX = _plateRect.StartPoint.X + w/2;
			while (i < 9)
			{
				Point p = new Point(newPosX, _plateRect.StartPoint.Y + h/2);
				Rectangle rect = new Rectangle(p, w, h);
				var newField = new E3TextField(values[i], p, fontHeight, _fontFamily, E3TextField.Alignment.Center, rect);						
				
				fields.Add(newField);
				newPosX = newPosX + w;				
				i++;
			}
						
			return fields;
		}
	}
	
	public class TextFieldBuilder
	{
		private readonly ILogger _logger;
		
		public TextFieldBuilder(ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			
			_logger = logger;
		}
		
		public E3TextField Build(string val, Point p, double fontHeight, string fontFamily, 
		                         E3TextField.Alignment alignment = E3TextField.Alignment.Left, Rectangle? bounds = null)
		{
			if (bounds != null)
			{
				var lines = val.SplitByWidth(fontHeight, fontFamily, bounds.Value.Width);
				string wrappedStr = String.Join(Environment.NewLine, lines).Trim();
				
				return new E3TextField(wrappedStr, p, fontHeight, fontFamily, alignment, bounds);
			}
			else
				return new E3TextField(val, p, fontHeight, fontFamily);
		}
	}
	
	public class PlateBuilder
	{
		private readonly TextFieldBuilder _textFiledBuilder;
		private readonly ILogger _logger;
		
		public PlateBuilder(TextFieldBuilder textFiledBuilder, ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			if (textFiledBuilder == null)
				throw new ArgumentNullException("textFiledBuilder");
			
			_textFiledBuilder = textFiledBuilder;
			_logger = logger;
		}
		
		public E3Plate Build(Device dev, PlatePattern pat, Point p)
		{
			// Создадим прямоугольник, обозначающий границы таблички
			var bounds = new Rectangle(p, pat.Width, pat.Height);
			var rect = new E3Rectangle(bounds);
			
			// Рассчитаем геометрию текстового поля заголовка и сформируем его поле
			var headerBounds = CalculateTextFieldBounds(bounds, pat.FontHeight);	
			var header = _textFiledBuilder.Build(dev.Function, headerBounds.StartPoint, pat.FontHeight, pat.FontFamily, 
			                                     E3TextField.Alignment.Center, headerBounds);

			var newPlate = new E3Plate(rect, header);
			
			// Если высота отформатированной строки выходит за границы, то надо сделать соответствующую запись в логе
			double h = header.Value.Measure(pat.FontHeight, pat.FontFamily).Height;
			if (h > headerBounds.Height)
			{
				string logMsg = String.Format("Базовая надпись <{0}> выходит за гранцы таблички",
				                             dev.Function);
				_logger.WriteLine(logMsg);
			}
								
			// Сформируем подписи переключателя (если они нужны)			  
			if (pat.ShowPositions)
			{
				var calc = new SwitcherTextFieldsCalculator(_logger, bounds, pat.FontFamily);
				var posTxtFields = calc.Calculate(dev.GetPositions(), pat.FontHeight);
				newPlate.TextFields.AddRange(posTxtFields);
			}			
			
			return newPlate;
		}
		
		/// <summary>
		/// Возвращает границы текстового поля заголовка таблички
		/// </summary>
		public static Rectangle CalculateTextFieldBounds(Rectangle bounds, double fontHeight)
		{			
			// Отступы от границы таблички
			double vPad = 0.05*bounds.Height; 
			double hPad = 0.05*bounds.Width;
			
			double x = bounds.StartPoint.X + hPad;
			double y = bounds.StartPoint.Y + bounds.Height - fontHeight - vPad;
			double width = bounds.Width - hPad;
			double height = bounds.Height - vPad;
			
			return new Rectangle(new Point(x, y), width, height);
		}
	}
	
	public class SheetBuilder
	{
		private readonly PlateBuilder     _plateBuilder;
		private readonly TextFieldBuilder _txtFieldBuilder;
		private readonly Point            _startPoint;
		private readonly Point            _endPoint;
		
		public SheetBuilder(PlateBuilder plateBuilder, TextFieldBuilder txtFieldBuilder, Point startPoint, Point endPoint)
		{
			if (plateBuilder == null)
				throw new ArgumentNullException("plateBuilder");
			if (txtFieldBuilder == null)
				throw new ArgumentNullException("txtFieldBuilder");
			
			_plateBuilder    = plateBuilder;
			_txtFieldBuilder = txtFieldBuilder;
			_startPoint      = startPoint;
			_endPoint        = endPoint;			
		}
		
		public IEnumerable<E3Sheet> Calculate(IEnumerable<Device> devices, string sheetSymbolName)
		{		
			try
			{
				var ret = new List<E3Sheet>();
							
				string shtName = "xxx"; // FIXME: !!!
				ret.Add(new E3Sheet(sheetSymbolName, shtName));
				Point p = _startPoint;
				var grps = devices.GroupBy(x => new { x.Location, x.PlatePattern });
				
				foreach (var grp in grps)
				{			
					var pat = grp.Key.PlatePattern;
					
					if (pat == null)
						continue;
					
					foreach (var dev in grp)
					{
						var rect = new Rectangle(p, pat.Width, pat.Height);
						Point endPoint = rect.GetEndPoint();
						
						// Если табличка пересекла границу листа по оси X, то перейдем на следующую строку
						if (endPoint.X > _endPoint.X)
						{
							p.X = _startPoint.X;
							p.Y = p.Y + pat.Height;
						}
						
						// Если табличка пересекла границу листа по оси Y, то надо создать новый лист
						if (endPoint.Y > _endPoint.Y)
						{
							p.X = _startPoint.X;
							p.Y = _startPoint.Y;
							string newShtName = pat.Name + (ret.Count + 1);
							ret.Add(new E3Sheet(sheetSymbolName, newShtName));
						}
						
						rect = new Rectangle(p, pat.Width, pat.Height);
						
						// Создадим новую табличку и добавим ее на крайний лист
						var newPlate = _plateBuilder.Build(dev, pat, p);
						ret.Last().Drawings.Add(newPlate);
						
						// Сместим точку по горизонтали для следующей таблички
						p.Add(pat.Width, 0);
					}
					
					// Перейдем на следующую строку
					double botMargin = 2; // отступ снизу
					p.X = _startPoint.X;
					p.Y = p.Y + pat.Height + botMargin;
					
					// Создадим поясняющую надпись поверности
					var surfaceTxtField = _txtFieldBuilder.Build(grp.Key.Location, p, pat.FontHeight, pat.FontFamily);
					ret.Last().Drawings.Add(surfaceTxtField);
					
					// Перейдем на следующую строку
					double topMargin = 8; // Отступ сверху
					p.Y = p.Y + topMargin;
				}
				
				return ret;
			}
			catch (Exception ex)
			{
				string errMsg = "Ошибка при расчете геометрических примитивов листа табличек:" + ex.Message;
				throw new Exception(errMsg, ex);
			}
		}
	}
}
