using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using MZToolsXMLComparator.Models;
using System.Xml;
using MZToolsXMLComparator.Utilities;
using MZToolsXMLComparator.ViewModels;

namespace MZToolsXMLComparator.Data
{
	public class FileToolDataProvider : IFileToolDataProvider
	{
		public ICollection<CodeTemplate> GetTemplates(FileToolViewModel parentModel)
		{
			//FileToolViewModel parent;

			//XmlTextReader reader = new XmlTextReader(xmlFilePath);
			ICollection<CodeTemplate> templates = new Collection<CodeTemplate>();
			XmlDocument doc = new XmlDocument();
			doc.Load(parentModel.XmlFilePath);
			if (doc.DocumentElement != null)
			{
				XmlNode xmlNodeList = doc.DocumentElement.SelectSingleNode("/Options/CodeTemplates");
				if (xmlNodeList != null)
				{
					int id = 0;
					foreach (XmlNode node in xmlNodeList.ChildNodes)
					{
						if (node.Name == "CodeTemplate")
						{
							CodeTemplate template = new CodeTemplate();
							foreach (XmlNode childNode in node.ChildNodes)
							{


								if (childNode.Name == "Description")
									template.Description = childNode.InnerText.Trim();
								if (childNode.Name == "Text")
									template.Text = childNode.InnerText;
								if (childNode.Name == "Author")
									template.Author = childNode.InnerText;
								if (childNode.Name == "Comment")
									template.Comment = childNode.InnerText;
								if (childNode.Name == "ExpansionKeyword")
									template.ExpansionKeyword = childNode.InnerText;
								if (childNode.Name == "CommandName")
									template.CommandName = childNode.InnerText;
								if (childNode.Name == "Category")
									template.Category = childNode.InnerText.Trim();
								if (childNode.Name == "Language")
									template.Language = Convert.ToInt32(childNode.InnerText);
								

							}
							template.ParentGuid = parentModel.Guid;
							template.Id = id;
							templates.Add(template);
							id+=1;
						}
					}
				}
			}
			return templates;
		}


	}
}
