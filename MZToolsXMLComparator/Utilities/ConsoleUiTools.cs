using System;

namespace MZToolsXMLComparator.Utilities
{
	public class ConsoleUiTools
	{
		public ConsoleUiTools()
		{
		}

		public void Line()
		{
			Console.WriteLine();
		}

		public void HorizontalRule()
		{
			Console.WriteLine(@"****************************************************");
		}

		public void Lines(int lineCount)
		{
			for (int i = 1; i < lineCount; i++)
			{
				Console.WriteLine();
			}
		}
		public string GetAlignmentSpacing(int titleLen, int spacingLen)
		{
			string result = "";
			for (int i = 0; i < spacingLen - titleLen; i++)
			{
				result = result + " ";
			}
			return result;
		}
	}
}