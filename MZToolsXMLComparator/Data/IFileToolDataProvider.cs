using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Models;

namespace MZToolsXMLComparator.Data
{
	public interface IFileToolDataProvider
	{
		ICollection<CodeTemplate> GetTemplates(string xmlFilePath);

	}
}
