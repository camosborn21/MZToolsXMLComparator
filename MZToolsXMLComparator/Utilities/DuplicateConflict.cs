using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Utilities
{
	public class DuplicateConflict : IConflictTemplate
	{
		//[3/22/2018 15:12] camerono: Used for tracking all exact duplicates of code
		public string Description { get; set; }
		public ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		public CodeTemplate ResolutionTemplate { get; set; }
		public void Resolve()
		{
			throw new NotImplementedException();
		}
	}
}
