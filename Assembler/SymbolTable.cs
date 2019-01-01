using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
	class SymbolTable
	{
		private Dictionary<string, int> dictSymbol;

		public SymbolTable()
		{
			dictSymbol = new Dictionary<string, int>();
			dictSymbol.Add("SP", 0x0000);
			dictSymbol.Add("LCL", 0x0001);
			dictSymbol.Add("ARG", 0x0002);
			dictSymbol.Add("THIS", 0x0003);
			dictSymbol.Add("THAT", 0x0004);
			for (var i = 0; i < 16; i++)
			{
				dictSymbol.Add("R" + i.ToString(), i);
			}
			dictSymbol.Add("SCREEN", 0x4000);
			dictSymbol.Add("KBD", 0x6000);
		}

		public void addEntry(string symbol, int address)
		{
			dictSymbol.Add(symbol, address);
		}

		public bool contains(string symbol)
		{
			return dictSymbol.ContainsKey(symbol);
		}

		public int getAddress(string symbol)
		{
			return dictSymbol[symbol];
		}
	}
}
