using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using e3;

namespace Core
{
	public interface IWorker
	{		
		IEnumerable<Device> Execute();
	}
	public class Worker : IWorker
	{
		
		private readonly INameplateWriter _nameplateBuilder;
		
		public Worker(INameplateWriter nameplateBuilder)
		{
			if (nameplateBuilder == null)
				throw new ArgumentNullException("nameplateBuilder");
			_nameplateBuilder = nameplateBuilder;
		}
		
		public IEnumerable<Device> Execute()
		{
            // Получаем количество процессов E3.series
            int procCount = Process.GetProcessesByName("E3.series").Length;

            if (procCount == 0)
            	throw new Exception("Не обнаружено ни одного открытого проекта E3Series");
            
            // Пройдемся по всем запущенным проектам e3Series
            var ret = new List<Device>();
            int i = 0;
            while (i < procCount) 
            {
            	string msg = "Файл обработан успешно";
            	string prjName = null;
            	e3Application e3App = null;
            	e3Job e3Job = null;
            	dynamic disp = null;
            	
            	try
            	{
		            disp = Activator.CreateInstance(Type.GetTypeFromProgID("CT.Dispatcher"));
		            e3App = (e3Application)disp.GetE3ByProcessId(Process.GetProcessesByName("E3.series")[i].Id);
		            e3Job = (e3Job)e3App.CreateJobObject();
		            prjName = e3Job.GetName();
		            
		           // FIXME: !!! ===========================
		            var devReader = new DevicesReader();
		            var devices = devReader.GetDevices(e3Job);
		            // =====================================
		            
		            _nameplateBuilder.Execute(e3Job, devices);
            	}
            	catch (Exception ex) 
            	{ 
            		msg = "ERROR: " + ex.Message; 
            		throw ex;
            	}
            	         
            	disp = null;
	        	e3App = null;
    			e3Job = null;
            	
				i++;
            }
            
            return ret;
		}
	}

	public interface INameplateWriter
	{
		void Execute(e3Job e3Job, IEnumerable<Device> devices);
	}
	public class NameplateWriter : INameplateWriter
	{		
		private readonly MappingTree _mappingTree; // Таблица сопоставлений DeviceName -> PatternName
		private readonly string _sheetSymbolName;
		
		public NameplateWriter(MappingTree mappingTree, string sheetSymbolName)
		{
			_mappingTree = mappingTree;
			_sheetSymbolName = sheetSymbolName;
		}
		
		public void Execute(e3Job e3Job, IEnumerable<Device> devices)
		{
			var e3Sht = (e3Sheet)e3Job.CreateSheetObject();
			var e3Txt = (e3Text)e3Job.CreateTextObject();
			
			dynamic xmin = null;
			dynamic ymin = null;
			dynamic xmax = null;
			dynamic ymax = null;
			
			// Необходимо создать новый лист выбранного формата, чтобы получить его рабочую зону
			e3Sht.Create(0, "demo", _sheetSymbolName, 0, 0);
			e3Sht.GetWorkingArea(ref xmin, ref ymin, ref xmax, ref ymax);
			Point startPoint = new Point(xmin, ymin);
			Point endPoint = new Point(xmax, ymax);
			
			// Удалим лист, потому что он больше не нужен
			e3Sht.Delete();
			
			var placementCalculator = new PlacementCalculator(startPoint, endPoint);
			
			var groups = _mappingTree.Roots.SelectMany(x => x.Devices.Select(y => new { Pat = x.PlatePattern, DevName = y, } ))
				.GroupBy(x => x.Pat);
			
			foreach (var grp in groups)
			{
				var devs = devices.Where(x => grp.Any(y => y.DevName == x.Name));
				var sheets = placementCalculator.Calculate(grp.Key, devs, _sheetSymbolName);
				foreach (var sht in sheets)
					new WriteSheetCommand(sht).Execute(e3Job);
			}
						
			e3Sht = null;
			e3Txt = null;
		}
	}
	
	public interface IPlacementCalculator
	{
		IEnumerable<PlatesSheet> Calculate(PlatePattern pat, IEnumerable<Device> devices, string sheetSymbolName);
	}
	public class PlacementCalculator : IPlacementCalculator
	{
		private readonly Point _startPoint;
		private readonly Point _endPoint;
		
		public PlacementCalculator(Point startPoint, Point endPoint)
		{
			_startPoint = startPoint;
			_endPoint = endPoint;
		}
		
		public IEnumerable<PlatesSheet> Calculate(PlatePattern pat, IEnumerable<Device> devices, string sheetSymbolName)
		{			
			var ret = new List<PlatesSheet>();
			
			string shtName = pat.Name + ret.Count;
			ret.Add(new PlatesSheet(sheetSymbolName, shtName));
			
			Point p = _startPoint;
			foreach (var dev in devices)
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
					ret.Add(new PlatesSheet(sheetSymbolName, newShtName));
				}
				
				rect = new Rectangle(p, pat.Width, pat.Height);
				var newPlate = new Plate(dev.Function, rect, pat.ShowPositions, dev.GetPositions());
				ret.Last().Add(newPlate);
				
				p.Add(pat.Width, 0);
			}
			
			return ret;
		}
	}
		
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
	
	public interface IDevicesReader
	{
		IEnumerable<Device> GetDevices(e3Job e3Job);
	}
	public class DevicesReader : IDevicesReader
	{
		public IEnumerable<Device> GetDevices(e3Job e3Job)
		{
			var ret = new List<Device>();
			var dev = (e3Device)e3Job.CreateDeviceObject();
			var cmp = (e3Component)e3Job.CreateComponentObject();
			
			dynamic devIds = null;
			e3Job.GetAllDeviceIds(ref devIds);
			foreach (var devId in devIds)
			{
    			if (devId == 0 || devId == null)
    				continue;
    			
    			dev.SetId(devId);
    			cmp.SetId(devId);
    			
    			// Попробуем получить значение атрибута "Функция устройства"
    			var func = dev.GetAttributeValue("Функция устройства");
    			
    			// Если устройство не обладает этим атрибутом, значит у него нет таблички
    			// И это изделие нас не интересует.
    			if (String.IsNullOrEmpty(func))
    				continue;
    			    			
    			// Зафиксируем изделие и его атрибуты
    			var newDevice = new Device()
    			{
    				Name      = cmp.GetName(),
					Function  = func, 
					Position0 = this.GetAttValue(dev, pos0Atts),
					Position1 = this.GetAttValue(dev, pos1Atts),
					Position2 = this.GetAttValue(dev, pos2Atts),
					Position3 = this.GetAttValue(dev, pos3Atts),
					Position4 = this.GetAttValue(dev, pos4Atts),
					Position5 = this.GetAttValue(dev, pos5Atts),
					Position6 = this.GetAttValue(dev, pos6Atts),
					Position7 = this.GetAttValue(dev, pos7Atts),
					Position8 = this.GetAttValue(dev, pos8Atts),
    			};
    			
    			ret.Add(newDevice);
			}
			
			dev = null;
			cmp = null;
			
			return ret;
		} 
		
		private string GetAttValue(e3Device e3dev, string[] attsNames)
		{
			string ret = null;
			
			var values = attsNames.Select(x => e3dev.GetAttributeValue(x))
				.Where(x => !String.IsNullOrEmpty(x));
			
			if (values.Any())
				ret = values.First();
			
			if (!String.IsNullOrEmpty(ret))
				Debug.WriteLine(ret);
				
			return ret;
		}
		
		#region ATTS ARRAYS
		private string[] pos7Atts = new string[]
		{
			"Положение 1 (+135 вправо)",
			"Положение 2 (+135 вправо)",
			"Положение 3 (+135 вправо)",
			"Положение 4 (+135 вправо)",
			"Положение 5 (+135 вправо)",
			"Положение 6 (+135 вправо)",
			"Положение 7 (+135 вправо)",
			"Положение 8 (+135 вправо)",
		};
		private string[] pos8Atts = new string[]
		{
			"Положение 1 (+180 вниз)",
			"Положение 2 (+180 вниз)",
			"Положение 3 (+180 вниз)",
			"Положение 4 (+180 вниз)",
			"Положение 5 (+180 вниз)",
			"Положение 6 (+180 вниз)",
			"Положение 7 (+180 вниз)",
			"Положение 8 (+180 вниз)",
		};
		private string[] pos5Atts = new string[]
		{
			"Положение 1 (+45 вправо)",
			"Положение 2 (+45 вправо)",
			"Положение 3 (+45 вправо)",
			"Положение 4 (+45 вправо)",
			"Положение 5 (+45 вправо)",
			"Положение 6 (+45 вправо)",
			"Положение 7 (+45 вправо)",
			"Положение 8 (+45 вправо)",
		};
		private string[] pos6Atts = new string[]
		{
			"Положение 1 (+90 вправо)",
			"Положение 2 (+90 вправо)",
			"Положение 3 (+90 вправо)",
			"Положение 4 (+90 вправо)",
			"Положение 5 (+90 вправо)",
			"Положение 6 (+90 вправо)",
			"Положение 7 (+90 вправо)",
			"Положение 8 (+90 вправо)",
		};
		private string[] pos1Atts = new string[]
		{
			"Положение 1 (-135 влево)",
			"Положение 2 (-135 влево)",
			"Положение 3 (-135 влево)",
			"Положение 4 (-135 влево)",
			"Положение 5 (-135 влево)",
			"Положение 6 (-135 влево)",
			"Положение 7 (-135 влево)",
			"Положение 8 (-135 влево)",
		};
		private string[] pos0Atts = new string[]
		{
			"Положение 1 (-180 вниз)",
			"Положение 2 (-180 вниз)",
			"Положение 3 (-180 вниз)",
			"Положение 4 (-180 вниз)",
			"Положение 5 (-180 вниз)",
			"Положение 6 (-180 вниз)",
			"Положение 7 (-180 вниз)",
			"Положение 8 (-180 вниз)",
		};
		private string[] pos3Atts = new string[]
		{
			"Положение 1 (-45 влево)",
			"Положение 2 (-45 влево)",
			"Положение 3 (-45 влево)",
			"Положение 4 (-45 влево)",
			"Положение 5 (-45 влево)",
			"Положение 6 (-45 влево)",
			"Положение 7 (-45 влево)",
			"Положение 8 (-45 влево)",
		};
		private string[] pos2Atts = new string[]
		{
			"Положение 1 (-90 влево)",
			"Положение 2 (-90 влево)",
			"Положение 3 (-90 влево)",
			"Положение 4 (-90 влево)",
			"Положение 5 (-90 влево)",
			"Положение 6 (-90 влево)",
			"Положение 7 (-90 влево)",
			"Положение 8 (-90 влево)",
		};
		private string[] pos4Atts = new string[]
		{
			"Положение 1 (0 вверх)",
			"Положение 2 (0 вверх)",
			"Положение 3 (0 вверх)",
			"Положение 4 (0 вверх)",
			"Положение 5 (0 вверх)",
			"Положение 6 (0 вверх)",
			"Положение 7 (0 вверх)",
			"Положение 8 (0 вверх)",
		};
		#endregion
	}
}