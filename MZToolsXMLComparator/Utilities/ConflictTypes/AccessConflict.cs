using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using MZToolsXMLComparator.Models;


namespace MZToolsXMLComparator.Utilities.ConflictTypes
{
	public class AccessConflict : IConflictTemplate
	{
		public string Description { get; set; }
		public ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		public CodeTemplate ResolutionTemplate { get; set; }
		private ConsoleUiTools _c;
		public AccessConflict()
		{
			_c = new ConsoleUiTools();
		}
		public void PrintInfo()
		{
			Console.WriteLine(@"	Access Conflict: ");
			foreach (CodeTemplate conflictedTemplate in ConflictedTemplates)
			{
				Console.WriteLine(@"		Conflicted Template: " + conflictedTemplate.Description + @" from " + conflictedTemplate.ParentGuid + @"_" + conflictedTemplate.Id);
			}
		}

		public void Resolve()
		{
			//bool finished = false;
			//while (finished == false)
			//{
			_c.Lines(2);
			_c.HorizontalRule();
			Console.WriteLine(@"** RESOLVE ACCESS CONFLICT (press a key to select an option) **");
			Console.WriteLine(Description);
			_c.Line();
			int count = 1;
			int selectedTemplate = -1;
			foreach (CodeTemplate template in ConflictedTemplates)
			{
				Console.WriteLine(count + @". " + template.Description + @" from " + template.ParentGuid + @"-" + template.Id);
				Console.WriteLine(@"	Expansion Keyword : " + template.ExpansionKeyword);
				Console.WriteLine(@"	Command Name      : " + template.CommandName);

				count++;
			}
			Console.Write(@"Your choice is: ");
			selectedTemplate = Int32.Parse(Console.ReadKey().KeyChar.ToString());
			while (selectedTemplate - 1 < 0 || selectedTemplate - 1 >= ConflictedTemplates.Count)
			{
				Console.WriteLine(@"Invalid Selection. Choose a template from the list above.");
				selectedTemplate = Int32.Parse(Console.ReadKey().KeyChar.ToString());
			}
			ResolutionTemplate = ConflictedTemplates.ElementAt(selectedTemplate - 1);


			//Console.WriteLine(@"A. [ALL]" + _c.GetAlignmentSpacing(3,20) + @"Display all conflict templates current access settings");
			//Console.WriteLine(@"S. [SELECT]" + _c.GetAlignmentSpacing(6,20) + @"Select which template will resolve this conflict");
			//char keyPressed;
			//Console.Write(@"Your choice is: ");
			//keyPressed = Console.ReadKey().KeyChar;
			//_c.Line();
			//switch (keyPressed)
			//{
			//	case 'A':
			//	case 'a':
			//		DisplayAccessConflicts();
			//		break;
			//	case 'S':
			//	case 's':
			//		SelectResolution();
			//		break;
			//	default:
			//		Console.WriteLine(@"Invalid selection.");
			//		break;
			//}
			//	finished = true;
			//}
		}

		//private void SelectResolution()
		//{
		//	int count = 1;
		//	foreach (CodeTemplate template in ConflictedTemplates)
		//	{
		//		Console.WriteLine(count + @". " + template.Description + @" from " + template.ParentGuid + @"-" + template.Id);
		//		Console.WriteLine(@"	Expansion Keyword : " + template.ExpansionKeyword);
		//		Console.WriteLine(@"	Command Name      : " + template.CommandName);

		//		count++;
		//	}
		//}

		//private void DisplayAccessConflicts()
		//{
		//	Console.WriteLine(@"Conflict Template Information");
		//	int count = 1;
		//	foreach (CodeTemplate template in ConflictedTemplates)
		//	{
		//		Console.WriteLine(count + @". " + template.Description + @" from " + template.ParentGuid + @"-" + template.Id);
		//		Console.WriteLine(@"	Expansion Keyword : " + template.ExpansionKeyword);
		//		Console.WriteLine(@"	Command Name      : " + template.CommandName);

		//		count++;
		//	}
		//}


	}
}
