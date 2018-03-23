using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Utilities
{
	public class LocationConflict : IConflictTemplate
	{
		public string Description { get; set; }
		public ICollection<CodeTemplate> ConflictedTemplates { get; set; }
		public CodeTemplate ResolutionTemplate { get; set; }
		public void Resolve()
		{
			throw new NotImplementedException();
		}
	}
}
