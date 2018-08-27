using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Core
{
	public static class Extensions
	{
		public static Rectangle Measure(this string str, double fontHeight, string fontFamily)
		{			
			using (var graphics = Graphics.FromImage(new Bitmap(1, 1)))
			{
				var font = new Font(fontFamily, (float)fontHeight, FontStyle.Regular, GraphicsUnit.Millimeter);
			    SizeF size = graphics.MeasureString(str, font);
			    
			    // Переведем дюймы в милиметры (25.4 - мм в дюйме)
			    double width = size.Width / graphics.DpiX*25.4; 
				double height = size.Height / graphics.DpiX*25.4;
				
				return new Rectangle(new Point(), width, height);
			}
		}
		
		public static bool CompareByMask(this string str, string wildCard, string mask)
		{
			// Приведем маску к виду удобному для работы
			mask = mask.Replace(wildCard, @"\w*");
						
			Regex regex = new Regex(mask);
			MatchCollection matches = regex.Matches(str);
			
			return matches.Count > 0;
		}
		
		public static IEnumerable<string> SplitByWidth(this string str, double fontHeight, string fontFamily, double maxWidth)
		{
			var ret = new List<string>();
			
			var words = str.Split(' ').ToList();
			string retStr = "";
			string subStr = "";
			int i = 0;
			bool flag = true;
			while (flag && i < words.Count)
			{
				subStr = retStr + " " + words.ElementAt(i);
				double width = subStr.Measure(fontHeight, fontFamily).Width;				
			    retStr = subStr;			    
			    flag = width < maxWidth;
			    i++;
			}
			
			ret.Add(retStr);
			
			int c = retStr.Split(' ').Count();
			if (c < words.Count)
			{
				var lastWords = words.Where(x => words.IndexOf(x) >= i);
				string crop = String.Join(" ", lastWords).Trim();
				ret.AddRange(SplitByWidth(crop, fontHeight, fontFamily, maxWidth));
			}
			
			return ret;
		}
	}
}
