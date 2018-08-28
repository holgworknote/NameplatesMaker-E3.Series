using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;

namespace Core
{
	public static class Extensions
	{
		public static Rectangle Measure(this string str, double fontHeight, string fontFamily, double w)
		{			
			using (var graphics = Graphics.FromImage(new Bitmap(1, 1)))
			{
				graphics.PageUnit = GraphicsUnit.Millimeter;
				var font = new Font(fontFamily, (float)fontHeight, FontStyle.Regular, GraphicsUnit.Millimeter);
				SizeF size = graphics.MeasureString(str, font, (int)(Math.Round(w)));
				return new Rectangle(new Point(), size.Width, size.Height);
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
		
		public static bool IsPlaced(this e3.e3Device dev)
		{
			dynamic x     = null;
			dynamic y     = null; 
			dynamic z     = null; 
			dynamic rot   = null; 
			
			int ret = dev.GetPanelLocation(out x, out y, out z, out rot);
			
			return (ret == 1);
		}
	}
}
