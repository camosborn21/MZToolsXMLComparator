using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Data;
using MZToolsXMLComparator.Models;
using MZToolsXMLComparator.Utilities;
using MZToolsXMLComparator.ViewModels;

namespace ComparatorConsole
{
	public class ProgramUI
	{
		private ICollection<FileToolViewModel> viewModels;

		public ProgramUI()
		{
			viewModels = new Collection<FileToolViewModel>();
		}
		public void Debug_Print()
		{
			Console.WriteLine("Enter the Xml Code File to Parse");
			string fileName = Console.ReadLine();
			FileToolViewModel toolView = new FileToolViewModel(new FileToolDataProvider(), fileName);
			foreach (CodeTemplate template in toolView.Templates.OrderBy(c => c.Category))
			{
				Console.WriteLine("Template");
				Console.WriteLine("		" + template.Category);
				Console.WriteLine("		" + template.Description);
			}
			Console.ReadLine();
		}
		public void StartInterface()
		{
			bool inMenu = true;
			char keyPressed;
			while (inMenu)
			{
				Lines(2);
				HorizontalRule();
				Console.WriteLine(@"**  MENU:(press a character to select an option)  **");
				HorizontalRule();
				WriteLoadedFileList();
				Console.WriteLine(@"Q. [QUIT]" + GetAlignmentSpacing(4) + @"Quit.");
				Console.WriteLine(@"L. [LOAD]" + GetAlignmentSpacing(4) + @"Load an additional xml file for comparison.");
				Console.WriteLine(@"D. [DISPLAY]" + GetAlignmentSpacing(7) +
				                  @"Display overview or full contents of a loaded file.");
				Console.WriteLine(@"C. [COMPARE]"+GetAlignmentSpacing(7)+@"Compare all files loaded to the buffer.");
				HorizontalRule();
				Lines(2);
				Console.Write(@"Your choice is: ");
				keyPressed = Console.ReadKey().KeyChar;
				Line();
				switch (keyPressed)
				{
					case 'Q':
					case 'q':
						inMenu = false;
						break;

					case 'L':
					case 'l':
						LoadXMLFile();
						break;

					case 'D':
					case 'd':
						DisplayXmlFileContents();
						break;

					case 'c':
					case 'C':
						CompareXmlFilesInBuffer();
						break;
				}
			}
		}

		private void CompareXmlFilesInBuffer()
		{
			Line();
			if (viewModels.Count < 2)
			{
				Console.WriteLine(@"There aren't enough files loaded to run a comparison. You need at least 2...duh.");
				return;
			}
			Lines(2);
			Console.WriteLine(@"Running Comparison of Loaded Files");
			Comparator comparator = new Comparator(viewModels);
			comparator.PrintUniqueTemplateDetails();
		}

		private void DisplayXmlFileContents()
		{
			int selectedFile = -1;
			if (viewModels.Count > 1)
			{
				Console.WriteLine(@"Which file would you like to display?");
				int i = 1;
				foreach (FileToolViewModel viewModel in viewModels)
				{
					Console.WriteLine(i + @". " + viewModel.FileName());
					i++;
				}
				Console.Write(@"Your choice is: ");

				while(selectedFile-1 < 0 || selectedFile-1 >= viewModels.Count)
				{
					Console.WriteLine(@"Invalid selection. Please choose a file from the list above.");
					selectedFile = Int32.Parse(Console.ReadKey().KeyChar.ToString());
				}
				selectedFile -= 1;
				Lines(2);
			} else if (viewModels.Count == 1)
			{
				selectedFile = 0;
				Lines(2);
			} else if (viewModels.Count == 0)
			{
				Console.WriteLine(@"No files have yet been loaded to be displayed. First load a file.");
				return;
			}

			Line();
			Console.WriteLine(@"Display Xml file: " + viewModels.ElementAt(selectedFile).FileName());
			Console.WriteLine(@"Would you like to display: ");
			Console.WriteLine(@"1. Basic Information and Template Names");
			Console.WriteLine(@"2. Detailed Information Including Template Text");
			Console.Write(@"Your choice is: ");
			int selectedDetail = Int32.Parse(Console.ReadKey().KeyChar.ToString());
			Line();
			while (selectedDetail != 1 && selectedDetail != 2)
			{
				Console.WriteLine(@"Invalid option. Please choose your level of detail: 1 or 2.");
				selectedDetail = Int32.Parse(Console.ReadKey().KeyChar.ToString());
				Line();
			}
			Line();
			switch (selectedDetail)
			{
				case 1:
					viewModels.ElementAt(selectedFile).PrintBasicInformation();
					break;
				case 2:
					viewModels.ElementAt(selectedFile).PrintDetailedInformation();
					break;
			}
		}

		private void LoadXMLFile()
		{
			Console.WriteLine(@"Enter the Xml Code File to Parse");
			string fileName = Console.ReadLine();
			FileToolViewModel toolView = new FileToolViewModel(new FileToolDataProvider(), fileName);
			viewModels.Add(toolView);
			Line();
			Console.WriteLine(@"File successfully parsed. " + toolView.Templates.Count + @" code templates found");
		}
		private void WriteLoadedFileList()
		{
			if (viewModels.Count > 0)
			{
				Console.WriteLine(@"FILES LOADED:");
				foreach (FileToolViewModel model in viewModels)
				{
					Console.WriteLine(model.FileName());
				}
				HorizontalRule();
			}
		}

		private  string GetAlignmentSpacing(int titleLen)
		{
			string result = "";
			for (int i = 0; i < 20 - titleLen; i++)
			{
				result = result + " ";
			}
			return result;
		}

		 void Line()
		{
			Console.WriteLine();
		}
		 void HorizontalRule()
		{
			Console.WriteLine(@"****************************************************");
		}
		 void Lines(int lineCount)
		{
			for (int i = 1; i < lineCount; i++)
			{
				Console.WriteLine();
			}
		}
	}
}
