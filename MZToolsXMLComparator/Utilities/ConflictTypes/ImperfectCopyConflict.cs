using System;
using System.Collections.Generic;
using System.Linq;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Utilities.ConflictTypes
{
	public class ImperfectCopyConflict : IConflictTemplate
	{
		//[3/22/2018 15:11] camerono: Used for code templates where the title is the same but the code text is different.
		public string Description { get; set; }
		public ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		public CodeTemplate ResolutionTemplate { get; set; }
		private ConsoleUiTools _c;

		public ImperfectCopyConflict()
		{
			_c= new ConsoleUiTools();
		}
		public void PrintInfo()
		{
			Console.WriteLine(@"	Imperfect Copy Conflict: ");
			foreach (CodeTemplate conflictedTemplate in ConflictedTemplates)
			{
				Console.WriteLine(@"		Conflicted Template: " + conflictedTemplate.Description + @" from " + conflictedTemplate.ParentGuid + @"_" + conflictedTemplate.Id);
			}
		}

		public void Resolve()
		{
			_c.Lines(2);
			_c.HorizontalRule();
			Console.WriteLine(@"** RESOLVE IMPERFECT COPY CONFLICT (select a template) **");
			Console.WriteLine(Description);
			_c.Line();
			int count = 1;
			int selectedTemplate = -1;
			foreach (CodeTemplate template in ConflictedTemplates)
			{
				Console.WriteLine(count + @". " + template.Description + @" from " + template.ParentGuid + @"-"+template.Id);
				Console.WriteLine(template.Text);
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
		}
	}
}
