using System;
using System.Collections.Generic;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Utilities.ConflictTypes
{
	public class ImperfectCopyConflict : IConflictTemplate
	{
		//[3/22/2018 15:11] camerono: Used for code templates where the title is the same but the code text is different.
		public string Description { get; set; }
		public ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		public CodeTemplate ResolutionTemplate { get; set; }
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
			throw new NotImplementedException();
		}
	}
}
