using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Utilities
{
	public class ImperfectCopyConflict : IConflictTemplate
	{
		//[3/22/2018 15:11] camerono: Used for code templates where the title is the same but the code text is different.
		public string Description { get; set; }
		public ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		public CodeTemplate ResolutionTemplate { get; set; }
		public void Resolve()
		{
			throw new NotImplementedException();
		}
	}
}
