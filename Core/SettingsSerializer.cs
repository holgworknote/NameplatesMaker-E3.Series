using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Core
{	
	public interface ISettingsManager
	{
		PatternsList PatternsList { get; }
		MappingTree MappingTree { get; }
		
		void Load();
		void Save();
	}
	public class SettingsManager : ISettingsManager
	{
		private readonly string _pathPatternTable;
		private readonly string _pathMappingTable;
		private readonly ILogger _logger;
		
		private IPatternsListSerializer _patternsListSerializer;
		private IMappingTreeSerializer _mappingTreeSerializer;
		
		public PatternsList PatternsList { get; private set; }
		public MappingTree MappingTree { get; private set; }
		
		public SettingsManager(string pathPatternTable, string pathMappingTable, ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			if (pathPatternTable == null)
				throw new ArgumentNullException("pathPatternTable");
			if (pathMappingTable == null)
				throw new ArgumentNullException("pathMappingTable");
			
			_pathPatternTable = pathPatternTable;
			_pathMappingTable = pathMappingTable;
			_logger = logger;
		}
		
		public void Load()
		{
			if (this.PatternsList != null || this.MappingTree != null)
				throw new Exception("Settings already loaded");
			
			// Считаем список шаблонов
			var patternsListConverter = new PatternsListConverter();
			_patternsListSerializer = new PatternsListSerializer(_pathPatternTable, patternsListConverter, _logger);
			this.PatternsList = _patternsListSerializer.Deserialize();
			
			// Построим Mapping Tree
			var mappingTreeConverter = new MappingTreeConverter(this.PatternsList);
			_mappingTreeSerializer = new MappingTreeSerializer(_pathMappingTable, mappingTreeConverter, _logger);
			this.MappingTree = _mappingTreeSerializer.Deserialize();	
		}
		public void Save()
		{
			_patternsListSerializer.Serialize(this.PatternsList);
			_mappingTreeSerializer.Serialize(this.MappingTree);
		}
	}
	
	public interface IPatternsListSerializer
	{
		void Serialize(PatternsList patternsList);
		PatternsList Deserialize();
	}
	public class PatternsListSerializer : IPatternsListSerializer
	{
		private readonly string _filePath;
		private readonly IPatternsListConverter _converter;
		private readonly ILogger _logger;
		
		// CTOR
		public PatternsListSerializer(string filePath, IPatternsListConverter converter, ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			if (converter == null)
				throw new ArgumentNullException("converter");
			
			_filePath = filePath;
			_converter = converter;
			_logger = logger;
		}
		
    	public void Serialize(PatternsList patternsList)
	    {	    	
			// Преобразуем MappingTree в MappingTable для сериализации
			var mappingTable = _converter.Convert(patternsList);
			
    		var formatter = new XmlSerializer(typeof(PatternsTable));
	        using (var fs = new FileStream(_filePath, FileMode.Create))
	        {
	            formatter.Serialize(fs, mappingTable);
	        }
	    }
	    public PatternsList Deserialize()
	    {
	    	PatternsList ret = null;
	    	
	    	try
	    	{
	    		var formatter = new XmlSerializer(typeof(PatternsTable));
		        using (var fs = new FileStream(_filePath, FileMode.OpenOrCreate))
		        {
		            var mappingTable = (PatternsTable)formatter.Deserialize(fs);
		            ret = _converter.Restore(mappingTable);
		        }
	    	}
	    	catch (Exception ex) 
	    	{
	    		string errMsg = "Patterns List deserialization error";
	    		_logger.WriteLine(errMsg);
	    		ret = new PatternsList();
	    	}
	        return ret;
	    } 
	}
	
	public interface IMappingTreeSerializer
	{
		void Serialize(MappingTree mappingTree);
		MappingTree Deserialize();
	}
	public class MappingTreeSerializer : IMappingTreeSerializer
	{
		private readonly string _filePath;
		private readonly IMappingTreeConverter _mappingConverter;
		private readonly ILogger _logger;
		
		// CTOR
		public MappingTreeSerializer(string filePath, IMappingTreeConverter mappingConverter, ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException("logger");
			if (mappingConverter == null)
				throw new ArgumentNullException("mappingConverter");
			
			_filePath = filePath;
			_mappingConverter = mappingConverter;
			_logger = logger;
		}
		
    	public void Serialize(MappingTree mappingTree)
	    {	    	
			// Преобразуем MappingTree в MappingTable для сериализации
			var mappingTable = _mappingConverter.Convert(mappingTree);
			
    		var formatter = new XmlSerializer(typeof(MappingTable));
	        using (var fs = new FileStream(_filePath, FileMode.Create))
	        {
	            formatter.Serialize(fs, mappingTable);
	        }
	    }
	    public MappingTree Deserialize()
	    {
	    	MappingTree ret = null;
	    	
	    	try
	    	{
		    	var formatter = new XmlSerializer(typeof(MappingTable));
		        using (var fs = new FileStream(_filePath, FileMode.OpenOrCreate))
		        {
		            var mappingTable = (MappingTable)formatter.Deserialize(fs);
		            ret = _mappingConverter.Restore(mappingTable);
		        }
	    	}
	    	catch (Exception ex) 
	    	{
	    		string errMsg = "Mapping Tree deserialization error";
	    		_logger.WriteLine(errMsg);
	    		ret = new MappingTree();
	    	}
	        
	        return ret;
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
			var ret = new MappingTree();
			
			var groups = mappingTable.Pairs.GroupBy(x => x.PatternName);
			foreach (var grp in groups)
			{
				var pat = _patternsList.Get(grp.Key);
				
				if (pat == null)
					continue;
				
				var devNames = grp.Select(x => x.DeviceName);
				var newRoot = new MappingTree.Root(pat, devNames);
				ret.Add(newRoot);
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
