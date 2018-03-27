using System;
using System.Collections.Generic;
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
			bool finished = false;
			while (finished == false)
			{
				_c.Lines(2);
				_c.HorizontalRule();
				Console.WriteLine(@"** RESOLVE ACCESS CONFLICT (press a key to select an option) **");
				Console.WriteLine(Description);
				_c.Line();
				Console.WriteLine(@"A. [ALL]" + _c.GetAlignmentSpacing(3,20) + @"Display all conflict templates current access settings");
				Console.WriteLine(@"S. [SELECT]" + _c.GetAlignmentSpacing(6,20) + @"Select which template will resolve this conflict");
				char keyPressed;
				Console.Write(@"Your choice is: ");
				keyPressed = Console.ReadKey().KeyChar;
				_c.Line();
				switch (keyPressed)
				{
					case 'A':
					case 'a':
						DisplayAccessConflicts();
						break;
					case 'S':
					case 's':
						SelectResolution();
						break;
					default:
						Console.WriteLine(@"Invalid selection.");
						break;
				}
				finished = true;
			}
		}

		private void SelectResolution()
		{
			throw new NotImplementedException();
		}

		private void DisplayAccessConflicts()
		{
			throw new NotImplementedException();
		}


	}
}
