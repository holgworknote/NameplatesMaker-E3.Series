using System;

namespace Core
{
	public interface ILogger
	{
		string Output { get; }
		
		void WriteLine(string txt);
		void Clear();
	}
	public class Logger : ILogger
	{
		public string Output { get; private set; }
		
		public void WriteLine(string txt)
		{
			this.Output = this.Output + Environment.NewLine + txt;
		}
		public void Clear()
		{
			this.Output = "";
		}
	}
}
