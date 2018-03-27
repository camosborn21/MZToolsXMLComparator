using System;
using System.Collections.Generic;
using System.Linq;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Utilities.ConflictTypes
{
	public class DuplicateConflict : IConflictTemplate
	{
		//[3/22/2018 15:12] camerono: Used for tracking all exact duplicates of code
		public string Description { get; set; }
		public ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		public CodeTemplate ResolutionTemplate { get; set; }
		private ConsoleUiTools _c;
		public DuplicateConflict()
		{
			_c = new ConsoleUiTools();
		}
		public void PrintInfo()
		{
			Console.WriteLine(@"	Duplicate Conflict: ");
			foreach (CodeTemplate conflictedTemplate in ConflictedTemplates)
			{
				Console.WriteLine(@"		Conflicted Template: " + conflictedTemplate.Description + @" from " + conflictedTemplate.ParentGuid + @"_" + conflictedTemplate.Id);
			}
		}

		public void Resolve()
		{
			ResolutionTemplate = ConflictedTemplates.First();
			_c.Lines(2);
			_c.HorizontalRule();
			Console.WriteLine(@"** DUPLICATE CODE CONFLICT (Autoresolved) **");
			Console.WriteLine(ResolutionTemplate.Description);
		}
	}
}
