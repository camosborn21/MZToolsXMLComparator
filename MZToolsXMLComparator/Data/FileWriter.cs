using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MZToolsXMLComparator.Models;
using MZToolsXMLComparator.Utilities;
namespace MZToolsXMLComparator.Data
{
	public class FileWriter
	{
		
		private ICollection<CodeTemplate> templates;
		public FileWriter(ICollection<CodeTemplate> resolved)
		{
			templates = resolved;
		}

		public void WriteOutputXmlFile()
		{
			XmlDocument doc = new XmlDocument();
			XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
			XmlElement root = doc.DocumentElement;
			doc.InsertBefore(xmlDeclaration, root);

			//[3/27/2018 17:33] Cameron Osborn: Create wrapper
			XmlElement options = doc.CreateElement(string.Empty, "Options", string.Empty);
			doc.AppendChild(options);
			XmlElement templates = doc.CreateElement(string.Empty, "CodeTemplates", string.Empty);
			options.AppendChild(templates);

			//[3/27/2018 17:36] Cameron Osborn: Write all templates
			foreach (CodeTemplate template in templates)
			{
				XmlElement templateElement = doc.CreateElement(string.Empty, "CodeTemplate", string.Empty);

				XmlElement descElement = doc.CreateElement(string.Empty, "Description", string.Empty);
				XmlText descText = doc.CreateTextNode(template.Description);
				descElement.AppendChild(descText);
				templateElement.AppendChild(descElement);

				XmlElement textElement = doc.CreateElement(string.Empty, "Text", string.Empty);
				XmlText textText = doc.CreateTextNode(template.Text);
				textElement.AppendChild(textText);
				templateElement.AppendChild(textElement);

				XmlElement authorElement = doc.CreateElement(string.Empty, "Author", string.Empty);
				XmlText authorText = doc.CreateTextNode(template.Author);
				authorElement.AppendChild(authorText);
				templateElement.AppendChild(authorElement);

				XmlElement commentElement = doc.CreateElement(string.Empty, "Comment", string.Empty);
				XmlText commentText = doc.CreateTextNode(template.Comment);
				commentElement.AppendChild(commentText);
				templateElement.AppendChild(commentElement);


			}

		}
	}
}

