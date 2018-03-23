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
		public string XmlFilePath { get; set; }
		public Guid Guid;
		public ICollection<CodeTemplate> Templates => templates ?? (templates = dataProvider.GetTemplates(this));

		//public ICollection<CodeTemplate> Templates => templates ?? dataProvider.GetTemplates(xmlFilePath);

		public string FileName()
		{
			string result = "";
			DirectoryInfo dir=new DirectoryInfo(XmlFilePath);
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
				Console.WriteLine("	"+(template.Id+1).ToString()+@". "+template.Category+@" -- " + template.Description);
			}
		}

		public void PrintDetailedInformation()
		{
			Console.WriteLine("Detailed information for: " + FileName());
			Console.WriteLine(templates.Count + @" templates in this file.");
			Console.WriteLine(@"Contents:");
			foreach (CodeTemplate template in templates)
			{
				Console.WriteLine(@"Template " + (template.Id+1).ToString());
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
			XmlFilePath = xmlFile;
			dataProvider = data;
			Guid=Guid.NewGuid();
		}

	}
}
