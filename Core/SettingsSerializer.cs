using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Core
{	
	public interface IMySettings
	{
		string SheetFormat { get; set; }
		MappingTree MappingTree { get; }
		
		void Load();
		void Save();
	}
	public class MySettings : IMySettings
	{
		private readonly IMySettingsSerializer _serializer;
		private readonly ILogger _logger;
		private IMappingTreeConverter _mappingTreeConverter;
		private IPatternsListConverter _patternsListConverter;
		
		public string SheetFormat { get; set; }
		private PatternsList PatternsList { get; set; }
		public MappingTree MappingTree { get; private set; }
		
		// CTOR
		public MySettings(IMySettingsSerializer serializer, ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			if (serializer == null)
				throw new ArgumentNullException("serializer");
			
			_serializer = serializer;	
			_logger = logger;
		}
		
		public void Load()
		{
			if (this.PatternsList != null || this.MappingTree != null)
				throw new Exception("Settings already loaded");
						
			try
			{
				var settings = _serializer.Deserialize();
				
				_patternsListConverter = new PatternsListConverter();
				this.PatternsList = _patternsListConverter.Restore(settings.PatternsTable);
				
				_mappingTreeConverter = new MappingTreeConverter(this.PatternsList);
				this.MappingTree = _mappingTreeConverter.Restore(settings.MappingTable);
								
				this.SheetFormat = settings.SheetFormat;
			}
			catch
			{
				_logger.WriteLine("Can't load Settings");
				
				// Если загрузить данные не получилось, то создадим контейнер шаблонов вручную
				this.PatternsList = new PatternsList();
				this.MappingTree = new MappingTree(this.PatternsList);
				this.SheetFormat = "";
			}
		}
		public void Save()
		{
			try
			{
				var pt = _patternsListConverter.Convert(this.PatternsList);
				var mt = _mappingTreeConverter.Convert(this.MappingTree);
				string sf = this.SheetFormat;
				
				var ss = new MySettingsSerializationClass()
				{
					PatternsTable = pt,
					MappingTable = mt,
					SheetFormat = sf,
				};
				_serializer.Serialize(ss);
			}
			catch
			{
				_logger.WriteLine("Can't deserialize PatternsList");
				var ss = new MySettingsSerializationClass()
				{
					SheetFormat = this.SheetFormat,
				};
				_serializer.Serialize(ss);
			}
		}
	}
		
	public interface IMappingTreeConverter
	{
		MappingTable Convert(MappingTree tree);
		MappingTree Restore(MappingTable mappingTable);
	}
	public class MappingTreeConverter : IMappingTreeConverter
	{
		private readonly PatternsList _patternsList;
		
		public MappingTreeConverter(PatternsList patternsList)
		{
			_patternsList = patternsList;
		}
		
		public MappingTable Convert(MappingTree tree)
		{
			var ret = new MappingTable();

			foreach (var root in tree.Roots)
				foreach (var devName in root.Devices)
					ret.Add(devName, root.PlatePattern.Name);
			
			return ret;
		}		
		public MappingTree Restore(MappingTable mappingTable)
		{
			var ret = new MappingTree(_patternsList);
			
			var groups = mappingTable.Pairs.GroupBy(x => x.PatternName);
			foreach (var grp in groups)
			{
				var pat = _patternsList.Get(grp.Key);
				
				if (pat == null)
					continue;
				
				var devNames = grp.Select(x => x.DeviceName);
				var newRoot = new MappingTree.Root(pat, devNames);
				ret.AddRoot(newRoot);
			}
			
			return ret;
		}
	}
	
	public interface IPatternsListConverter
	{
		PatternsTable Convert(PatternsList patternsList);
		PatternsList Restore(PatternsTable patternsTable);
	}
	public class PatternsListConverter : IPatternsListConverter
	{
		public PatternsTable Convert(PatternsList patternsList)
		{
			var ret = new PatternsTable();
			
			foreach (var pat in patternsList.Values)
				ret.Add(pat.Name, pat);
			
			return ret;
		}
		public PatternsList Restore(PatternsTable patternsTable)
		{
			var ret = new PatternsList();
			
			foreach (var pat in patternsTable.Values)
				ret.Add(pat);
			
			return ret;
		}
	}
	
	public interface IMySettingsSerializer
	{
		void Serialize(MySettingsSerializationClass mySettings);
		MySettingsSerializationClass Deserialize();
	}
	public class MySettingsSerializer : IMySettingsSerializer
	{
		private readonly string _filePath;
		private readonly ILogger _logger;
		
		public MySettingsSerializer(string filePath, ILogger logger)
		{
			if (filePath == null)
				throw new ArgumentNullException("filePath");
			if (logger == null)
				throw new ArgumentNullException("logger");
			
			this._filePath = filePath;
			this._logger = logger;
		}
		
    	public void Serialize(MySettingsSerializationClass mySettings)
	    {	 
			try
			{
	    		var formatter = new XmlSerializer(typeof(MySettingsSerializationClass));
		        using (var fs = new FileStream(_filePath, FileMode.Create))
		        {
		            formatter.Serialize(fs, mySettings);
		        }
			}
			catch
			{
				_logger.WriteLine("Settings on write error");
			}
	    }
	    public MySettingsSerializationClass Deserialize()
	    {
	    	MySettingsSerializationClass ret = null;
	    	
	    	try
	    	{
	    		var formatter = new XmlSerializer(typeof(MySettingsSerializationClass));
		        using (var fs = new FileStream(_filePath, FileMode.OpenOrCreate))
		        {
		            ret = (MySettingsSerializationClass)formatter.Deserialize(fs);
		            if (ret.MappingTable == null)
		            	ret.MappingTable = new MappingTable();
		            if (ret.PatternsTable == null)
		            	ret.PatternsTable = new PatternsTable();
		        }
	    	}
	    	catch
	    	{
	    		_logger.WriteLine("Settings on write error");
	    		ret = new MySettingsSerializationClass();
	    	}
	    	
	    	return ret;
	    } 
	}
	
	/// <summary>
	/// Настройки в форме удобной для сериализации
	/// </summary>
	[Serializable]
	public class MySettingsSerializationClass
	{
		public string        SheetFormat   { get; set; }
		public PatternsTable PatternsTable { get; set; }
		public MappingTable  MappingTable  { get; set; }
	}
	
	[XmlRoot("PatternsTable")]
	public class PatternsTable : XmlSerializableDictionary<string, PlatePattern> { }
		
	[Serializable, XmlRoot("MappingTable")]
	public class MappingTable
	{		
		[XmlElement("Pair")]
		public List<Pair> Pairs { get; private set; }
		
		// CTOR
		public MappingTable()
		{
			Pairs = new List<Pair>();
		}
		
		public void Add(string devName, string patName)
		{				
			Pairs.Add(new Pair { DeviceName = devName, PatternName = patName } );
		}
		public void Remove(Pair pair)
		{
			Pairs.Remove(pair);
		}
		
		public class Pair
		{
			public string DeviceName { get; set; }
			public string PatternName { get; set; }
		}
	}
	
	[XmlRoot("Dictionary")]
	public class XmlSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
	{
	    public System.Xml.Schema.XmlSchema GetSchema()
	    {
	        return null;
	    }
	  
	    public void ReadXml(System.Xml.XmlReader reader)
	    {
	        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
	        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
	        bool wasEmpty = reader.IsEmptyElement;
	        reader.Read();
	        if (wasEmpty)
	            return;
	        while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
	        {
	            reader.ReadStartElement("item");
	            reader.ReadStartElement("key");
	            TKey key = (TKey)keySerializer.Deserialize(reader);
	            reader.ReadEndElement();
	            reader.ReadStartElement("value");
	            TValue value = (TValue)valueSerializer.Deserialize(reader);
	            reader.ReadEndElement();
	            this.Add(key, value);
	            reader.ReadEndElement();
	            reader.MoveToContent();
	        }
	        reader.ReadEndElement();
	    }
	  
	    public void WriteXml(System.Xml.XmlWriter writer)
	    {
	        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
	        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
	        foreach (TKey key in this.Keys)
	        {
	            writer.WriteStartElement("item");
	            writer.WriteStartElement("key");
	            keySerializer.Serialize(writer, key);
	            writer.WriteEndElement();
	            writer.WriteStartElement("value");
	            TValue value = this[key];
	            valueSerializer.Serialize(writer, value);
	            writer.WriteEndElement();
	            writer.WriteEndElement();
	        }
	    }
	}
}
