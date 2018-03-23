using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Models;
using MZToolsXMLComparator.ViewModels;

namespace MZToolsXMLComparator.Utilities
{
	public class Comparator
	{
		private readonly ICollection<FileToolViewModel> _templateFiles;
		private ICollection<CodeTemplate> uniqueCodeTemplates = new Collection<CodeTemplate>();
		private ICollection<IConflictTemplate> conflictTemplates = new Collection<IConflictTemplate>();
		public Comparator(ICollection<FileToolViewModel> viewModels)
		{
			_templateFiles = viewModels;
			RunComparison();
		}

		public void RunComparison()
		{
			ICollection<CodeTemplate> mergedTemplates = new List<CodeTemplate>();
			foreach (FileToolViewModel file in _templateFiles)
			{
				foreach (CodeTemplate template in file.Templates)
				{
					mergedTemplates.Add(template);
				}
			}
			foreach (CodeTemplate codeTemplate in mergedTemplates)
			{
				bool templateIsUnique=true;
				foreach (CodeTemplate comparisonTemplate in mergedTemplates)
				{

					if (comparisonTemplate != codeTemplate && comparisonTemplate.Description == codeTemplate.Description)
					{
						if (comparisonTemplate.Text != codeTemplate.Text)
						{
							if (conflictTemplates.Any(c => c.Description == codeTemplate.Description))
							{
								//[3/22/2018 20:54] camerono: Left off here trying to check if a conflicted template has been added already.
							}
							else
							{
								ICollection<CodeTemplate> conflictCopies = new Collection<CodeTemplate>();
								conflictCopies.Add(codeTemplate);
								conflictCopies.Add(comparisonTemplate);
								conflictTemplates.Add(new ImperfectCopyConflict()
								{
									Description = codeTemplate.Description,
									ConflictedTemplates = conflictCopies
								});
							}
						}
						templateIsUnique = false;						
					}


				}
				if (templateIsUnique == true)
					uniqueCodeTemplates.Add(codeTemplate);
				//if (mergedTemplates.Select(c =>
				//	c.Description == codeTemplate.Description && c.Text == codeTemplate.Text && 
				//	(c.ParentGuid != codeTemplate.ParentGuid)).Count()==0)
				//{
				//	uniqueCodeTemplates.Add(codeTemplate);
				//}


			}

		}

		public void PrintUniqueTemplateDetails()
		{
			Console.WriteLine(@"Displaying Unique Procedures (" + uniqueCodeTemplates.Count +@")");
			foreach (CodeTemplate uniqueCodeTemplate in uniqueCodeTemplates)
			{
				Console.WriteLine(@"	Unique Procedure: " + uniqueCodeTemplate.Description + @" from " + uniqueCodeTemplate.ParentGuid.ToString() + @"_" + uniqueCodeTemplate.Id);
			}
		}
	}
}
