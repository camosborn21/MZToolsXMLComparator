using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Data;
using MZToolsXMLComparator.Models;
using MZToolsXMLComparator.Utilities.ConflictTypes;
using MZToolsXMLComparator.ViewModels;

namespace MZToolsXMLComparator.Utilities
{
	public class Comparator
	{
		private readonly ICollection<FileToolViewModel> _templateFiles;
		private ICollection<CodeTemplate> uniqueCodeTemplates = new Collection<CodeTemplate>();
		private ICollection<IConflictTemplate> conflictTemplates = new Collection<IConflictTemplate>();
		private ConsoleUiTools _c;
		public Comparator(ICollection<FileToolViewModel> viewModels)
		{
			_templateFiles = viewModels;
			RunComparison();
			_c = new ConsoleUiTools();
		}

		public void ResolveConflicts()
		{
			while (conflictTemplates.Count(c => c.ResolutionTemplate == null) > 0)
			{
				conflictTemplates.First(c => c.ResolutionTemplate == null).Resolve();
			}
		}

		public void WriteOutputFile()
		{
			if (conflictTemplates.Count(c => c.ResolutionTemplate == null) > 0)
			{
				Console.WriteLine(@"Not all conflicts have been resolved. All conflicts must have a resolution template selected before an output file can be written.");
				Console.WriteLine(@"Would you like to inspect and resolve conflicts now? (Y/N)");
				char keyPressed = Console.ReadKey().KeyChar;
				while (keyPressed != 'Y' || keyPressed != 'N' || keyPressed != 'y' || keyPressed != 'n')
				{
					Console.WriteLine(@"Invalid Selection. Would you like to inspect and resolve conflicts now? (Y/N)");
					keyPressed = Console.ReadKey().KeyChar;
				}
				if (keyPressed == 'y' || keyPressed == 'Y')
				{
					ResolveConflicts();
				}
				return;
			}

			//[3/27/2018 17:39] Cameron Osborn: Collect all resolution templates from conflicts and all unique templates into new Collection
			Collection<CodeTemplate> finalTemplates = new Collection<CodeTemplate>();
			foreach (IConflictTemplate conflictTemplate in conflictTemplates)
			{
				finalTemplates.Add(conflictTemplate.ResolutionTemplate);
			}
			foreach (CodeTemplate uniqueCodeTemplate in uniqueCodeTemplates)
			{
				finalTemplates.Add(uniqueCodeTemplate);
			}


			FileWriter writer = new FileWriter(finalTemplates);
			writer.WriteOutputXmlFile();
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
				bool templateIsUnique = true;
				foreach (CodeTemplate comparisonTemplate in mergedTemplates)
				{

					//[3/26/2018 13:21] camerono: If the code template and comparison template are not the same object but have the same description
					if (comparisonTemplate != codeTemplate && comparisonTemplate.Description == codeTemplate.Description && codeTemplate.Language == comparisonTemplate.Language)
					{
						//[3/26/2018 13:22] camerono: Check to see if they have the same text
						if (comparisonTemplate.Text != codeTemplate.Text)
						{
							if (conflictTemplates.OfType<ImperfectCopyConflict>().Any(c => c.Description == codeTemplate.Description))
							{
								if (!conflictTemplates.OfType<ImperfectCopyConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Contains(codeTemplate))
									conflictTemplates.OfType<ImperfectCopyConflict>().Single(c => c.Description == codeTemplate.Description)
										.ConflictedTemplates.Add(codeTemplate);
								if (!conflictTemplates.OfType<ImperfectCopyConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Contains(comparisonTemplate))
									conflictTemplates.OfType<ImperfectCopyConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Add(comparisonTemplate);
							}
							else
							{
								//[3/26/2018 13:26] camerono: Create a new ImperfectCopyConflict
								ICollection<CodeTemplate> conflictCopies = new Collection<CodeTemplate>();
								conflictCopies.Add(codeTemplate);
								conflictCopies.Add(comparisonTemplate);
								conflictTemplates.Add(new ImperfectCopyConflict()
								{
									Description = codeTemplate.Description,
									ConflictedTemplates = conflictCopies
								});
							}
						} // End If Text is Different
						else
						{
							if (comparisonTemplate.Category != codeTemplate.Category)
							{
								//[3/26/2018 13:57] camerono: Check to see if a similar set of templates have been added already to the conflict list
								if (conflictTemplates.OfType<LocationConflict>().Any(c => c.Description == codeTemplate.Description))
								{
									if (!conflictTemplates.OfType<LocationConflict>().Single(c => c.Description == codeTemplate.Description)
										.ConflictedTemplates.Contains(codeTemplate))
										conflictTemplates.OfType<LocationConflict>().Single(c => c.Description == codeTemplate.Description)
											.ConflictedTemplates.Add(codeTemplate);
									if (!conflictTemplates.OfType<LocationConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Contains(comparisonTemplate))
										conflictTemplates.OfType<LocationConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Add(comparisonTemplate);
								}
								else
								{
									ICollection<CodeTemplate> conflictCopies = new Collection<CodeTemplate>();
									conflictCopies.Add(codeTemplate);
									conflictTemplates.Add(new LocationConflict()
									{
										Description = codeTemplate.Description,
										ConflictedTemplates = conflictCopies
									});
								}
							} // End if Category is different (Location Conflict)
							else
							{
								//[3/26/2018 15:20] camerono: Check to see if there is a conflict of accessor keys or expansion keywords.
								if (codeTemplate.CommandName != comparisonTemplate.CommandName ||
										codeTemplate.ExpansionKeyword != comparisonTemplate.ExpansionKeyword)
								{
									if (conflictTemplates.OfType<AccessConflict>().Any(c => c.Description == codeTemplate.Description))
									{
										if (!conflictTemplates.OfType<AccessConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Contains(codeTemplate))
											conflictTemplates.OfType<AccessConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Add(codeTemplate);
										if (!conflictTemplates.OfType<AccessConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Contains(comparisonTemplate))
											conflictTemplates.OfType<AccessConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Add(comparisonTemplate);
									}
									else
									{
										ICollection<CodeTemplate> conflictCopies = new Collection<CodeTemplate>();
										conflictCopies.Add(codeTemplate);
										conflictCopies.Add(comparisonTemplate);
										conflictTemplates.Add(new AccessConflict()
										{
											Description = codeTemplate.Description,
											ConflictedTemplates = conflictCopies
										});
									}
								} // End if accesor keying is different
								else
								{
									//[3/26/2018 15:21] camerono: At this point, all else being the same, we have a duplicate method.
									if (conflictTemplates.OfType<DuplicateConflict>().Any(c => c.Description == codeTemplate.Description))
									{
										if (!conflictTemplates.OfType<DuplicateConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Contains(codeTemplate))
											conflictTemplates.OfType<DuplicateConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Add(codeTemplate);
										if (!conflictTemplates.OfType<DuplicateConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Contains(comparisonTemplate))
											conflictTemplates.OfType<DuplicateConflict>().Single(c => c.Description == codeTemplate.Description).ConflictedTemplates.Add(comparisonTemplate);
									}
									else
									{
										ICollection<CodeTemplate> conflictCopies = new Collection<CodeTemplate>();
										conflictCopies.Add(codeTemplate);
										conflictCopies.Add(comparisonTemplate);
										conflictTemplates.Add(new DuplicateConflict()
										{
											Description = codeTemplate.Description,
											ConflictedTemplates = conflictCopies
										});

									}
								}
							}
						}

						templateIsUnique = false;
					}

					//[3/26/2018 13:24] camerono: Insert checks for different description but same code etc.
					if (codeTemplate != comparisonTemplate && codeTemplate.Text == comparisonTemplate.Text && codeTemplate.Description != comparisonTemplate.Description && codeTemplate.Language == comparisonTemplate.Language)
					{
						if (conflictTemplates.OfType<RenamingConflict>()
							.Any(c => c.ConflictedTemplates.Any(d => d.Text == codeTemplate.Text)))
						{
							if (!conflictTemplates.OfType<RenamingConflict>()
								.Any(c => c.ConflictedTemplates.Contains(codeTemplate)))
								conflictTemplates.OfType<RenamingConflict>()
									.Single(c => c.ConflictedTemplates.Any(d => d.Text == codeTemplate.Text)).ConflictedTemplates.Add(codeTemplate);
							if (!conflictTemplates.OfType<RenamingConflict>()
								.Any(c => c.ConflictedTemplates.Contains(comparisonTemplate)))
								conflictTemplates.OfType<RenamingConflict>()
									.Single(c => c.ConflictedTemplates.Any(d => d.Text == comparisonTemplate.Text)).ConflictedTemplates.Add(comparisonTemplate);
						}
						else
						{
							ICollection<CodeTemplate> conflictCopies = new Collection<CodeTemplate>();
							conflictCopies.Add(codeTemplate);
							conflictCopies.Add(comparisonTemplate);
							conflictTemplates.Add(new RenamingConflict()
							{
								Description = "Renaming Conflict Like: " + codeTemplate.Description,
								ConflictedTemplates = conflictCopies
							});
						}
						templateIsUnique = false;
					}

				}
				if (templateIsUnique == true)
					uniqueCodeTemplates.Add(codeTemplate);
			}
			PrintComparisonFindings();
		}

		public void PrintComparisonFindings()
		{
			Console.WriteLine(@"Displaying Conflict Procedures (" + conflictTemplates.Count + @" conflicts)");
			foreach (IConflictTemplate conflictTemplate in conflictTemplates)
			{
				conflictTemplate.PrintInfo();
			}
			Console.WriteLine(@"Displaying Unique Procedures (" + uniqueCodeTemplates.Count + @")");
			foreach (CodeTemplate uniqueCodeTemplate in uniqueCodeTemplates)
			{
				Console.WriteLine(@"	Unique Procedure: " + uniqueCodeTemplate.Description + @" from " + uniqueCodeTemplate.ParentGuid.ToString() + @"_" + uniqueCodeTemplate.Id);
			}
		}
	}
}
