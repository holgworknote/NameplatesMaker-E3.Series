using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
	/// <summary>
	/// Класс для хранения списка паттернов, обеспечивающий уникальность имен паттернов
	/// </summary>
	public class PatternsList
	{
		private readonly List<PlatePattern> _values;
		
		public IEnumerable<PlatePattern> Values { get { return _values; } }
		
		public PatternsList()
		{
			_values = new List<PlatePattern>();
		}
		
		public void Add(PlatePattern platePattern)
		{
			// Убедимся, что паттерна с таким именем в списке еще нет
			if (_values.Any(x => x.Name == platePattern.Name))
				throw new ArgumentException("List already contains pattern named as " + platePattern.Name);
			
			_values.Add(platePattern);
		}
		public void Remove(PlatePattern platePattern)
		{
			_values.Remove(platePattern);
		}
		public bool IsExisted(string patName)
		{
			return _values.Any(x => x.Name == patName);
		}
		public PlatePattern Get(string patName)
		{
			return _values.Find(x => x.Name == patName);
		}
	}
	
	/// <summary>
	/// Класс для хранения схемы связей вида Pattern->Devices
	/// </summary>
	public class MappingTree
	{
		private readonly PatternsList _patternsList;
		private readonly List<IRoot> _roots;
		
		public IEnumerable<IRoot> Roots { get { return _roots; } }
		public IEnumerable<PlatePattern> PatternsCollection { get { return _patternsList.Values; } }
		
		public MappingTree(PatternsList patternsList)
		{
			if (patternsList == null)
				throw new ArgumentNullException("patternsList");
			
			_patternsList = patternsList;
			_roots = new List<IRoot>();
		}
				
		// OPERATIONS
		public void AddRoot(IRoot root)
		{
			// Убедимся, что паттерн существует в списке
			bool check = _patternsList.Values.All(x => x != root.PlatePattern);
			if (check)
				throw new Exception("Pattern not existed");
			
			// Убедимся, что паттерн в списке уникален
			if (_roots.Any(x => x.PlatePattern == root.PlatePattern))
				throw new Exception("Pattern already exists in MappingTree");
			
			_roots.Add(root);
		}
		public void RemoveRoot(IRoot root)
		{
			_roots.Remove(root);
		}
		public void AddPattern(PlatePattern platePattern)
		{
			_patternsList.Add(platePattern);
		}
		public void RemovePattern(PlatePattern platePattern)
		{
			// Удалим корни, привязанные к выбранному паттерну
			_roots.RemoveAll(x => x.PlatePattern == platePattern);
			
			_patternsList.Remove(platePattern);
		}
		public void RemoveDevice(string dev)
		{
			_roots.ForEach(x => x.Devices.RemoveAll(y => y == dev));
		}
		
		public interface IRoot
		{
			PlatePattern PlatePattern { get; }
			List<string> Devices { get; }
		}
		public class Root : IRoot
		{
			private readonly PlatePattern _platePattern;
			private readonly List<string> _devices;
			
			public PlatePattern PlatePattern { get { return _platePattern; } }
			public List<string> Devices { get { return _devices; } }
			
			public Root(PlatePattern platePattern, IEnumerable<string> devices)
			{
				if (devices == null)
					throw new ArgumentNullException("devices");
				
				_platePattern = platePattern;
				_devices = new List<string>();
				_devices.AddRange(devices);
			}
		}		
	}
	
	/// <summary>
	/// Лист с  длями
	/// </summary>
	public class PlatesSheet
	{
		public string          Name       { get; set; }
		public string          SymbolName { get; set; }
		public List<Plate>     Plates     { get; private set; }
		public List<TextField> TextFields { get; private set; }
		
		public PlatesSheet(string sheetSymbolName, string name = "test")
		{
			this.SymbolName = sheetSymbolName;
			this.Name       = name;
			this.Plates     = new List<Plate>();
			this.TextFields = new List<TextField>();
		}
	}
	
	/// <summary>
	/// Табличка
	/// </summary>
	public class Plate
	{
		public string    Header       { get; set; }
		public Rectangle Rectangle    { get; set; }
		public bool      GotPositions { get; set; }
		public double    FontSize     { get; set; } // Высота шрифта 
		public string[]  Positions    { get; set; }
		
		public Plate() { }
		public Plate(string header, Rectangle rectangle, bool gotPositions, double fontSize, string[] positions)
		{
			this.Header       = header;
			this.Rectangle    = rectangle;
			this.GotPositions = gotPositions;
			this.FontSize     = fontSize;
			this.Positions    = positions;
		}
	}
	
	/// <summary>
	/// Шаблон таблички
	/// </summary>
	public class PlatePattern
	{
		public string Name          { get; set; }
		public double Width         { get; set; }
		public double Height        { get; set; }
		public double FontHeight    { get; set; } 
		public bool   ShowPositions { get; set; }
	}	
	
	/// <summary>
	/// Объект текстового поля
	/// </summary>
	public struct TextField
	{
		public string Value { get; set; }
		public Point Point { get; set; }
	}
	
	/// <summary>
	/// Устройство, считанное из проекта E3.Series
	/// </summary>
	public struct Device
	{
		public string       Name         { get; set; } // FIXME: DELETE ME!!!
		public string       Function     { get; set; }
		public string       Position0    { get; set; }
		public string       Position1    { get; set; }
		public string       Position2    { get; set; }
		public string       Position3    { get; set; }
		public string       Position4    { get; set; }
		public string       Position5    { get; set; }
		public string       Position6    { get; set; }
		public string       Position7    { get; set; }
		public string       Position8    { get; set; }
		public string       Location     { get; set; }
		public PlatePattern PlatePattern { get; set; }
		
		public override string ToString()
		{
			return string.Format("[Device Name={0}, Function={1}]", Name, Function);
		}
		public string GetText()
		{
			string ret = this.Function;
			
			var pos = this.GetPositions();
			if (pos.Any(x => x != null))
				ret = ret + "|" + String.Join("|", pos);
			
			return ret;
		}
		
		public string[] GetPositions()
		{
			return new string[] 
			{
				Position0,
				Position1,
				Position2,
				Position3,
				Position4,
				Position5,
				Position6,
				Position7,
				Position8,
			};
		}
	}
		
	/// <summary>
	/// 2D DRAWING: Прямоугольник
	/// </summary>
	public struct Rectangle
	{
		public Point  StartPoint { get; set; }
		public double Width      { get; set; }
		public double Height     { get; set; }
		
		public Rectangle(Point startPoint, double width, double height) : this()
		{
			this.StartPoint = startPoint;
			this.Width = width;
			this.Height = height;
		}
		
		public Point GetEndPoint()
		{
			double x = this.StartPoint.X + this.Width;
			double y = this.StartPoint.Y + this.Height;
			return new Point(x, y);
		}
		
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			return (obj is Rectangle) && Equals((Rectangle)obj);
		}

		public bool Equals(Rectangle other)
		{
			return this.StartPoint == other.StartPoint && object.Equals(this.Width, other.Width) && object.Equals(this.Height, other.Height);
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				hashCode += 1000000007 * StartPoint.GetHashCode();
				hashCode += 1000000009 * Width.GetHashCode();
				hashCode += 1000000021 * Height.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(Rectangle lhs, Rectangle rhs) {
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Rectangle lhs, Rectangle rhs) {
			return !(lhs == rhs);
		}

		#endregion
	}
	
	/// <summary>
	/// 2D DRAWING: Точка на плоскости
	/// </summary>
	public struct Point
	{
		public double X { get; set; }
		public double Y { get; set; }
		
		// CTOR
		public Point(double x, double y) : this()
		{
			this.X = x;
			this.Y = y;
		}
		
		public void Add(Point p)
		{
			this.X = this.X + p.X;
			this.Y = this.Y + p.Y;
		}
		public void Add(double width, double height)
		{
			this.X = this.X + width;
			this.Y = this.Y + height;
		}
		
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			return (obj is Point) && Equals((Point)obj);
		}

		public bool Equals(Point other)
		{
			return object.Equals(this.X, other.X) && object.Equals(this.Y, other.Y);
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				hashCode += 1000000007 * X.GetHashCode();
				hashCode += 1000000009 * Y.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(Point lhs, Point rhs) {
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Point lhs, Point rhs) {
			return !(lhs == rhs);
		}
		#endregion
	}
}
