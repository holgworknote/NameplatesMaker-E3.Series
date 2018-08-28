using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using e3;

namespace Core
{
	/// <summary>
	/// TOP LEVEL EXECUTOR
	/// </summary>
	public interface IWorker
	{		
		void Execute(string fontFamily, string sheetSymbolName, string txtFilePath);
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
		
		public void Execute(string fontFamily, string sheetSymbolName, string txtFilePath)
		{
			_connector.Execute(e3Job => 
			{
	            var res = _reader.Execute(e3Job);
	            _writer.Execute(e3Job, res.DevicesWithFunctions, res.AllDevices, fontFamily, 
	                            sheetSymbolName, txtFilePath);
			});
		}
	}

	/// <summary>
	/// Класс для формирования листов с табличками в проекте E3.Series
	/// </summary>
	public interface IE3Writer
	{
		void Execute(e3Job e3Job, IEnumerable<Device> devices, IEnumerable<Device> allDevs, 
		             string fontFamily, string sheetSymbolName, string txtFilePath);
	}
	public class E3Writer : IE3Writer
	{		
		private readonly MappingTree _mappingTree; // Таблица сопоставлений DeviceName -> PatternName
		private readonly string      _sheetName;
		private readonly ILogger     _logger;
		
		public E3Writer(MappingTree mappingTree, string shtName, ILogger logger)
		{
			_mappingTree = mappingTree;
			_sheetName   = shtName;
			_logger      = logger;
		}
		
		public void Execute(e3Job e3Job, IEnumerable<Device> devices, IEnumerable<Device> allDevs, 
		                    string fontFamily, string sheetSymbolName, string txtFilePath)
		{
			var e3Sht = (e3Sheet)e3Job.CreateSheetObject();
			var e3Txt = (e3Text)e3Job.CreateTextObject();
			
			try
			{
				dynamic xmin = null;
				dynamic ymin = null;
				dynamic xmax = null;
				dynamic ymax = null;
				
				// Проверим, все ли изделия находятся в MappingTree
				var notExistedDevices = new List<Device>();
				foreach (var dev in devices.Where(x => x.PlatePattern == null))
				{
					bool check = _mappingTree.Roots.Any(x => x.Devices.Any(y => y == dev.Name));
					
					if (!check)
						notExistedDevices.Add(dev);
				}
				
				if (notExistedDevices.Any())
				{
					string combine = String.Join("," + Environment.NewLine, notExistedDevices.Select(x => x.ToString()));
					_logger.WriteLine("Изделия, для которых не обнаружен шаблон: " + Environment.NewLine + combine);
				}
				
				// Необходимо создать новый лист выбранного формата, чтобы получить его рабочую зону
				e3Sht.Create(0, "Таблички", sheetSymbolName, 0, 0);
				e3Sht.GetWorkingArea(ref xmin, ref ymin, ref xmax, ref ymax);
				Point startPoint = new Point(xmin, ymin);
				Point endPoint = new Point(xmax, ymax);
				
				// Удалим лист, потому что он больше не нужен
				e3Sht.Delete();
				
				var plateBuilder = new PlateBuilder(_logger);
				var sheetBuilder = new SheetBuilder(plateBuilder, startPoint, endPoint);
				                                    
				sheetBuilder.Calculate(devices, sheetSymbolName, fontFamily, _sheetName)
					.AsParallel()
					.ForAll(x => x.Draw(e3Job, e3Sht));
								
				sheetBuilder.Calculate(allDevs, sheetSymbolName, fontFamily, _sheetName)
					.AsParallel()
					.ForAll(x => x.Draw(e3Job, e3Sht));
				
				// Запишем текст в файл (если пользователь указал путь)
				if (!String.IsNullOrEmpty(txtFilePath))
				{
					// Скинем все объекты в кучу и преобразуем в текст
					var lines = devices.Union(allDevs).Select(x => x.GetText());
					string text = String.Join(Environment.NewLine, lines);
					System.IO.File.WriteAllText(txtFilePath, text);
				}
			}
			catch (Exception ex)
			{
				string errMsg = "Ошибка при записи листов с табличками в проект: " + ex.Message;
				throw new Exception(errMsg, ex);
			}
			finally
			{
				e3Sht = null;
				e3Txt = null;	
			}
		}
	}
	
	/// <summary>
	/// Класс, для чтения перечня устройств из проекта E3.Series (считываются только объекты с атрибутом "Функция устройства")
	/// </summary>
	public interface IE3Reader
	{
		E3Reader.Results Execute(e3Job e3Job);
	}
	public class E3Reader : IE3Reader
	{
		private readonly MappingTree _mappingTree;
		private readonly IMappingTreePatternFinder _patternFinder;
		private readonly PlatePattern _devNamePlatePattern;
		
		public E3Reader(MappingTree mappingTree, IMappingTreePatternFinder patternFinder, PlatePattern devNamePlatePattern)
		{
			if (devNamePlatePattern == null)
				throw new ArgumentNullException("devNamePlatePattern");
			if (patternFinder == null)
				throw new ArgumentNullException("patternFinder");
			if (mappingTree == null)
				throw new ArgumentNullException("mappingTree");
			
			_mappingTree = mappingTree;
			_patternFinder = patternFinder;
			_devNamePlatePattern = devNamePlatePattern;
		}
		
		public Results Execute(e3Job e3Job)
		{
			try
			{
				var devs = new List<Device>();
				var allDevsNames = new List<Device>();
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
	    			
	    			Device? newDev = GetDevice(_patternFinder, dev, cmp, devId);
	    			
	    			if (newDev != null)
						devs.Add(newDev.Value);
	    			
	    			// Зафиксируем имя изделия
	    			if (dev.IsDevice() == 1 && 
	    				dev.IsTerminal() == 0 &&
	    			    dev.IsTerminalBlock() == 0 &&
	    			    dev.IsHose() == 0 &&
	    			    dev.IsCableDuct() == 0 &&
	    			   	dev.IsFormboard() == 0 &&
	    			   	dev.IsTube() == 0 &&
	    			   	dev.IsMount() == 0 &&
	    			   	dev.IsWireGroup() == 0 &&
	    			   	dev.IsCable() == 0 &&
	    			   	dev.IsView() == 0 &&
	    				dev.IsPlaced() &&
	    				dev.GetPinCount() > 0)
	    			{
	    				var d = new Device()
						{ 
							Function = dev.GetName(), 
							Location = "Позиционные обозначения",  
							PlatePattern = _devNamePlatePattern,
						};
						allDevsNames.Add(d);
	    			}
				}
				
				dev = null;
				cmp = null;
				
				var ret = new Results()
				{
					DevicesWithFunctions = devs,
					AllDevices = allDevsNames,
				};
				
				return ret;
			}
			catch (Exception ex) 
			{
				string errMsg = "Ошибка при обработке проекта: " + ex.Message;
				throw new Exception(errMsg, ex); 
			}
		} 
		
		private static Device? GetDevice(IMappingTreePatternFinder patternFinder, e3Device dev, e3Component cmp, int devId)
		{
			try
			{	
				// Попробуем получить значение атрибута "Функция устройства"
				string func = dev.GetAttributeValue("Функция устройства");
				func = func.Replace(Environment.NewLine, "  ");
				func = func.Replace("  ", " ");
				func = func.Trim().ToUpper();
				
				// Если устройство не обладает этим атрибутом, значит у него нет таблички
				// И это изделие нас не интересует.
				if (String.IsNullOrEmpty(func))
					return null;
				    			
				// Зафиксируем изделие и его атрибуты
				string devName = cmp.GetName();
				var newDevice = new Device()
				{
					Name         = devName,
					Function     = func, 
					Position0    = GetAttValue(dev, pos0Atts),
					Position1    = GetAttValue(dev, pos1Atts),
					Position2    = GetAttValue(dev, pos2Atts),
					Position3    = GetAttValue(dev, pos3Atts),
					Position4    = GetAttValue(dev, pos4Atts),
					Position5    = GetAttValue(dev, pos5Atts),
					Position6    = GetAttValue(dev, pos6Atts),
					Position7    = GetAttValue(dev, pos7Atts),
					Position8    = GetAttValue(dev, pos8Atts),
					Location     = dev.GetLocation(),
					PlatePattern = patternFinder.GetPattern(devName),
				};
				
				return newDevice;
			}
			catch (Exception ex) 
			{ 
				string devName = TryGetDevName(dev);
				string errMsg = String.Format("Не удалось обработать изделие {0}: {1}", devName, ex.Message);
				throw new Exception(errMsg, ex); 
			}
		}
		private static string TryGetDevName(e3Device dev)
		{
			string ret = null;
			try
			{
				ret = dev.GetName();
			}
			catch { ret = "<Не удалось получить имя изделия>"; }
			
			return ret;
		}
		private static string TryGetProjectName(e3Job e3Job)
		{
			string ret = null;
			
			try
			{
				ret = e3Job.GetName();
			}
			catch { ret = "<Не удалось получить имя проекта>"; }
			
			return ret;
		}
		private static string GetAttValue(e3Device e3dev, string[] attsNames)
		{
			try
			{
				string ret = null;
			
				var values = attsNames.Select(x => e3dev.GetAttributeValue(x))
					.Where(x => !String.IsNullOrEmpty(x));
				
				if (values.Any())
					ret = values.First();
					
				return ret;
			}
			catch (Exception ex)
			{
				const string err = "Ошибка при чтении атрибута";
				throw new Exception(err, ex);
			}
		}
		
		#region ATTS ARRAYS
		private static string[] pos7Atts = new string[]
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
		private static string[] pos8Atts = new string[]
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
		private static  string[] pos5Atts = new string[]
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
		private static string[] pos6Atts = new string[]
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
		private static string[] pos1Atts = new string[]
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
		private static string[] pos0Atts = new string[]
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
		private static string[] pos3Atts = new string[]
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
		private static string[] pos2Atts = new string[]
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
		private static string[] pos4Atts = new string[]
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
		
		public struct Results
		{
			public List<Device> DevicesWithFunctions { get; set; }
			public List<Device> AllDevices { get; set; }
		}
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
        	try
        	{
	            // Получаем количество процессов E3.series
	            int procCount = Process.GetProcessesByName("E3.series").Length;
	
	            if (procCount == 0)
	            	throw new Exception("Не обнаружено ни одного открытого проекта E3.Series");
	            
	            // Пройдемся по всем запущенным проектам e3Series
	            GetEnumerableInt(procCount).AsParallel()
	            	.ForAll(x => HandleProcess(x, _logger, func));
        	}
        	catch (Exception ex) { _logger.WriteLine("Возникли проблемы при обработке: " + ex.Message); }
        }
        
        private static void HandleProcess(int proc, ILogger logger, Action<e3Job> func)
        {
        	string prjName = null;
        	e3Application e3App = null;
        	e3Job e3Job = null;
        	dynamic disp = null;
        	
        	try
        	{
	            disp = Activator.CreateInstance(Type.GetTypeFromProgID("CT.Dispatcher"));
	            e3App = (e3Application)disp.GetE3ByProcessId(Process.GetProcessesByName("E3.series")[proc].Id);
	            e3Job = (e3Job)e3App.CreateJobObject();
	            prjName = e3Job.GetName();
	            
	            logger.WriteLine("Обработка файла: " + prjName);
	            func(e3Job);
	            logger.WriteLine("Обработка файла завершена");
        	}
        	catch (Exception ex) 
        	{ 
        		string errMsg = String.Format("Ошибка при обработке проекта {0}: {1}", TryGetProjectName(e3Job), ex.Message);
        		logger.WriteLine(errMsg);
        	}
        	         
        	disp = null;
        	e3App = null;
			e3Job = null;
        }
        private static string TryGetProjectName(e3Job e3Job)
        {
        	string ret = null;
        	
        	try
        	{
        		ret = e3Job.GetName();
        	}
        	catch { ret = "<Не удалось получить имя проекта>"; }
        	
        	return ret;
        }
        private static IEnumerable<int> GetEnumerableInt(int count)
        {
            var ret = new List<int>();
            int i = 0;
            while (i < count) 
            {
            	ret.Add(i);
            	i++;
            }
            return ret;
        }
	}
	
	/// <summary>
	/// Класс для поиска шаблона таблички в MappingTree
	/// </summary>
	public interface IMappingTreePatternFinder
	{
		PlatePattern GetPattern(string devName);
	}
	public class MappingTreePatternFinder : IMappingTreePatternFinder
	{
		private readonly MappingTree _tree;
		
		 public MappingTreePatternFinder(MappingTree tree)
		 {
			_tree = tree;
		 }
		
		public PlatePattern GetPattern(string devName)
		{
			var roots = _tree.Roots.ToList();
			PlatePattern ret = null;
			
			var q = roots.SelectMany(x => x.Devices.Select(y => new { Pat = x.PlatePattern, Dev = y }))
				.Where(x => devName.CompareByMask("*", x.Dev));
			
			if (q.Count() == 1)
				ret = q.ElementAt(0).Pat;
			else if (q.Count() > 1)
				ret = q.OrderBy(x => x.Dev.Count()).First().Pat;
						
			return ret;
		}
	}
}