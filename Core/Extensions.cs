using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

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
	}
}
