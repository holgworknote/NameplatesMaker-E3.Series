﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
	public interface IPlacementCalculator
	{
		IEnumerable<PlatesSheet> Calculate(IEnumerable<Device> devices, string sheetSymbolName);
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
		
		public IEnumerable<PlatesSheet> Calculate(IEnumerable<Device> devices, string sheetSymbolName)
		{			
			var ret = new List<PlatesSheet>();
						
			string shtName = "xxx";
			ret.Add(new PlatesSheet(sheetSymbolName, shtName));
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
						ret.Add(new PlatesSheet(sheetSymbolName, newShtName));
					}
					
					rect = new Rectangle(p, pat.Width, pat.Height);
					var newPlate = new Plate(dev.Function, rect, pat.ShowPositions, dev.GetPositions());
					ret.Last().Plates.Add(newPlate);
					
					p.Add(pat.Width, 0);
				}
				
				// Перейдем на следующую строку
				double botMargin = 2; // отступ снизу
				p.X = _startPoint.X;
				p.Y = p.Y + pat.Height + botMargin;
				
				// Создадим поясняющую надпись поверности
				var txtField = new TextField()
				{
					Value = grp.Key.Location,
					Point = p,
				};
				ret.Last().TextFields.Add(txtField);
				
				// Перейдем на следующую строку
				double topMargin = 8; // Отступ сверху
				p.Y = p.Y + topMargin;
			}
			
			return ret;
		}
	}
}
