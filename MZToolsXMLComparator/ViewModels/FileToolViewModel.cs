using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Data;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.ViewModels
{
	public class FileToolViewModel
	{
		private IFileToolDataProvider dataProvider;
		private ICollection<CodeTemplate> templates;
		private string xmlFilePath;

		public ICollection<CodeTemplate> Templates => templates ?? (templates = dataProvider.GetTemplates(xmlFilePath));

		//public ICollection<CodeTemplate> Templates => templates ?? dataProvider.GetTemplates(xmlFilePath);

		public string FileName()
		{
			string result = "";
			DirectoryInfo dir=new DirectoryInfo(xmlFilePath);
			result = dir.Name;
			return result;
		}

		public void PrintBasicInformation()
		{
			Console.WriteLine(@"Overview information for: " + FileName());
			Console.WriteLine(templates.Count + @" templates in this file.");
			Console.WriteLine(@"Contents:");
			foreach (CodeTemplate template in templates)
			{
				Console.WriteLine("	"+template.Category+@" -- " + template.Description);
			}
		}

		public void PrintDetailedInformation()
		{
			Console.WriteLine("Detailed information for: " + FileName());
			Console.WriteLine(templates.Count + @" templates in this file.");
			Console.WriteLine(@"Contents:");
			foreach (CodeTemplate template in templates)
			{
				Console.WriteLine(@"Template");
				Console.WriteLine(@"	" + template.Category + @" -- " + template.Description);
				Console.WriteLine(@"	Template by: "+template.Author);
				Console.WriteLine(@"	Expansion Keyword: " + template.ExpansionKeyword);
				Console.WriteLine(@"	Hotkey: " + template.CommandName);
				Console.WriteLine(@"	Comment: " + template.Comment);
				Console.WriteLine(@"	Template Text: ");
				Console.WriteLine(template.Text);
				Console.WriteLine();
				Console.WriteLine();
			}
		}
		public FileToolViewModel(IFileToolDataProvider data, string xmlFile)
		{
			xmlFilePath = xmlFile;
			dataProvider = data;
		}

	}
}
