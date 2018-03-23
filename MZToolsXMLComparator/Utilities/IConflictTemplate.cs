using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Utilities
{
	public interface IConflictTemplate
	{ 
		string Description { get; set; }
		ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		CodeTemplate ResolutionTemplate { get; set; }
		void Resolve();
	}
}
