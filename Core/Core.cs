using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using e3;

namespace Core
{
	public interface IWorker
	{		
		void Execute();
	}
	public class Worker : IWorker
	{
		private readonly IE3Writer _writer;
		private readonly IE3Reader _reader;
		private readonly IE3Connector _connector;
		
		public Worker(IE3Writer writer, IE3Reader reader, IE3Connector connector)
		{
			if (connector == null)
				throw new ArgumentNullException("connector");
			if (reader == null)
				throw new ArgumentNullException("reader");
			if (writer == null)
				throw new ArgumentNullException("writer");
			
			_writer = writer;
			_reader = reader;
			_connector = connector;
		}
		
		public void Execute()
		{
			_connector.Execute(e3Job => 
			{
	            var devices = _reader.GetDevices(e3Job);
	            _writer.Execute(e3Job, devices);   	
			});
		}
	}

	/// <summary>
	/// Класс для формирования листов с табличками в проекте E3.Seriese
	/// </summary>
	public interface IE3Writer
	{
		void Execute(e3Job e3Job, IEnumerable<Device> devices);
	}
	public class E3Writer : IE3Writer
	{		
		private readonly MappingTree _mappingTree; // Таблица сопоставлений DeviceName -> PatternName
		private readonly string _sheetSymbolName;
		private readonly ILogger _logger;
		
		public E3Writer(MappingTree mappingTree, string sheetSymbolName, ILogger logger)
		{
			_mappingTree = mappingTree;
			_sheetSymbolName = sheetSymbolName;
			_logger = logger;
		}
		
		public void Execute(e3Job e3Job, IEnumerable<Device> devices)
		{
			var e3Sht = (e3Sheet)e3Job.CreateSheetObject();
			var e3Txt = (e3Text)e3Job.CreateTextObject();
			
			dynamic xmin = null;
			dynamic ymin = null;
			dynamic xmax = null;
			dynamic ymax = null;
			
			// Проверим, все ли изделия находятся в MappingTree
			var notExistedDevices = new List<Device>();
			foreach (var dev in devices)
			{
				bool check = _mappingTree.Roots.Any(x => x.Devices.Any(y => y == dev.Name));
				if (!check)
					notExistedDevices.Add(dev);
			}
			
			if (notExistedDevices.Any())
			{
				string combine = String.Join(", ", notExistedDevices.Select(x => x.ToString()));
				_logger.WriteLine("Изделия, дял которых не обнаружен шаблон: " + combine);
			}
			
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
	
	/// <summary>
	/// Класс, для чтения перечня устройств из проекта E3.Series (считываются только объекты с атрибутом "Функция устройства")
	/// </summary>
	public interface IE3Reader
	{
		IEnumerable<Device> GetDevices(e3Job e3Job);
	}
	public class E3Reader : IE3Reader
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
	
	/// <summary>
	/// Класс для подключения и поочередного обхода всех открытых проектов E3.Series
	/// </summary>
	public interface IE3Connector
	{
		void Execute(Action<e3Job> func);
	}
	public class E3Connector : IE3Connector
	{
		private readonly ILogger _logger;
		
		public E3Connector(ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			_logger = logger;
		}
		
        public void Execute(Action<e3Job> func)
        {
            // Получаем количество процессов E3.series
            int procCount = Process.GetProcessesByName("E3.series").Length;

            if (procCount == 0)
            	throw new Exception("Не обнаружено ни одного открытого проекта E3.Series");
            
            // Пройдемся по всем запущенным проектам e3Series
            var ret = new List<Device>();
            int i = 0;
            while (i < procCount) 
            {
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
		            
		            _logger.WriteLine("Обработка файла: " + prjName);
		            func(e3Job);
		            _logger.WriteLine("Обработка файла завершена");
            	}
            	catch (Exception ex) 
            	{ 
            		_logger.WriteLine("Ошибка: " + ex.Message);
            	}
            	         
            	disp = null;
	        	e3App = null;
    			e3Job = null;
            	
				i++;
            }
        }
	}
}