using System.Collections.Generic;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Utilities.ConflictTypes
{
	public interface IConflictTemplate
	{ 
		string Description { get; set; }
		ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		CodeTemplate ResolutionTemplate { get; set; }
		void PrintInfo();
		void Resolve();
	}
}
