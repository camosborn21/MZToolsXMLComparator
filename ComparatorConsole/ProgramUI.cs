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
		private readonly ConsoleUiTools _c;

		public ProgramUI()
		{
			viewModels = new Collection<FileToolViewModel>();
			_c = new ConsoleUiTools();
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
			const int menuSpacing = 20;
			bool inMenu = true;
			char keyPressed;
			while (inMenu)
			{
				_c.Lines(2);
				_c.HorizontalRule();
				Console.WriteLine(@"**  MENU:(press a character to select an option)  **");
				_c.HorizontalRule();
				WriteLoadedFileList();
				Console.WriteLine(@"Q. [QUIT]" + _c.GetAlignmentSpacing(4, menuSpacing) + @"Quit.");
				Console.WriteLine(@"L. [LOAD]" + _c.GetAlignmentSpacing(4, menuSpacing) + @"Load an additional xml file for comparison.");
				Console.WriteLine(@"D. [DISPLAY]" + _c.GetAlignmentSpacing(7, menuSpacing) +
													@"Display overview or full contents of a loaded file.");
				Console.WriteLine(@"C. [COMPARE]" + _c.GetAlignmentSpacing(7, menuSpacing) + @"Compare all files loaded to the buffer.");
				_c.HorizontalRule();
				_c.Lines(2);
				Console.Write(@"Your choice is: ");
				keyPressed = Console.ReadKey().KeyChar;
				_c.Line();
				switch (keyPressed)
				{
					case 'Q':
					case 'q':
						inMenu = false;
						break;

					case 'L':
					case 'l':
						LoadXmlFile();
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
			_c.Line();
			if (viewModels.Count < 2)
			{
				Console.WriteLine(@"There aren't enough files loaded to run a comparison. You need at least 2...duh.");
				return;
			}
			_c.Lines(2);
			Console.WriteLine(@"Running Comparison of Loaded Files");
			Comparator comparator = new Comparator(viewModels);
			//comparator.PrintComparisonFindings();
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

				while (selectedFile - 1 < 0 || selectedFile - 1 >= viewModels.Count)
				{
					Console.WriteLine(@"Invalid selection. Please choose a file from the list above.");
					selectedFile = Int32.Parse(Console.ReadKey().KeyChar.ToString());
				}
				selectedFile -= 1;
				_c.Lines(2);
			}
			else if (viewModels.Count == 1)
			{
				selectedFile = 0;
				_c.Lines(2);
			}
			else if (viewModels.Count == 0)
			{
				Console.WriteLine(@"No files have yet been loaded to be displayed. First load a file.");
				return;
			}

			_c.Line();
			Console.WriteLine(@"Display Xml file: " + viewModels.ElementAt(selectedFile).FileName());
			Console.WriteLine(@"Would you like to display: ");
			Console.WriteLine(@"1. Basic Information and Template Names");
			Console.WriteLine(@"2. Detailed Information Including Template Text");
			Console.Write(@"Your choice is: ");
			int selectedDetail = Int32.Parse(Console.ReadKey().KeyChar.ToString());
			_c.Line();
			while (selectedDetail != 1 && selectedDetail != 2)
			{
				Console.WriteLine(@"Invalid option. Please choose your level of detail: 1 or 2.");
				selectedDetail = Int32.Parse(Console.ReadKey().KeyChar.ToString());
				_c.Line();
			}
			_c.Line();
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

		private void LoadXmlFile()
		{
			Console.WriteLine(@"Enter the Xml Code File to Parse");
			string fileName = Console.ReadLine();

			FileToolViewModel toolView = new FileToolViewModel(new FileToolDataProvider(), fileName);
			viewModels.Add(toolView);
			_c.Line();
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
				_c.HorizontalRule();
			}
		}

	}
}
