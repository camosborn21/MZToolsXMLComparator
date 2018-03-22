using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MZToolsXMLComparator.Data;
using MZToolsXMLComparator.Models;
using MZToolsXMLComparator.ViewModels;

namespace ComparatorConsole
{
	class Program
	{
		
		static void Main(string[] args)
		{
			ProgramUI mainUi = new ProgramUI();
			//mainUi.Debug_Print();
			mainUi.StartInterface();
		}

		
	}
}
