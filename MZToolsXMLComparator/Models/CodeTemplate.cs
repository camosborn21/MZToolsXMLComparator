using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZToolsXMLComparator.Models
{
	public class CodeTemplate
	{
		public string Description { get; set; }
		public string Text { get; set; }
		public string Author { get; set; }
		public string Comment { get; set; }
		public string ExpansionKeyword { get; set; }
		public string CommandName { get; set; }
		public string Category { get; set; }
		public int Language { get; set; }
		public int Id { get; set; }
		public Guid ParentGuid { get; set; }
	}
}
