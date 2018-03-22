using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using MZToolsXMLComparator.Models;
using System.Xml;
namespace MZToolsXMLComparator.Data
{
	public class FileToolDataProvider : IFileToolDataProvider
	{
		public ICollection<CodeTemplate> GetTemplates(string xmlFilePath)
		{
			//XmlTextReader reader = new XmlTextReader(xmlFilePath);
			ICollection<CodeTemplate> templates = new Collection<CodeTemplate>();
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlFilePath);
			if (doc.DocumentElement != null)
			{
				XmlNode xmlNodeList = doc.DocumentElement.SelectSingleNode("/Options/CodeTemplates");
				if (xmlNodeList != null)
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
							//XmlNode selectSingleNode = node.SelectSingleNode("/Description");
							//if (selectSingleNode != null)
							templates.Add(template);
						}
					}
			}
			return templates;
		}
	}
}
