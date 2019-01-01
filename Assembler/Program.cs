using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assembler
{
	class Program
	{
		static SymbolTable symbolTable = new SymbolTable();

		static int variableCount = 0;

		static void Main(string[] args)
		{
			var inputFileName = args[1];
			if (!inputFileName.EndsWith(".asm"))
			{
				Console.WriteLine("拡張子が.asmではありません。");
				return;
			}
			var outputFileName = inputFileName.Substring(0,inputFileName.Length - 4) + ".hack";
			var fs = new FileStream(outputFileName, FileMode.Create);
			// バイナリデータの書き込み
			var byteList = new List<byte>();
			var labelParser = new Parser(inputFileName);
			while (labelParser.hasMoreCommand())
			{
				labelParser.advance();
				if (!labelParser.CanParse)
				{
					continue;
				}
				if (labelParser.currentCommandType == Parser.cType.L_COMMAND)
				{
					symbolTable.addEntry(labelParser.symbol(), variableCount);
				}
				else
				{
					variableCount++;
				}
			}
			var parser = new Parser(inputFileName);
			while (parser.hasMoreCommand())
			{
				parser.advance();
				if (!parser.CanParse)
				{
					continue;
				}
				if (parser.currentCommandType == Parser.cType.A_COMMAND)
				{
					var bytes = ConvertA(parser.symbol());
					byteList.Add(bytes[0]);
					byteList.Add(bytes[1]);
				}
				else if (parser.currentCommandType == Parser.cType.C_COMMAND)
				{
					var comp = Code.comp(parser.comp());
					var dest = Code.dest(parser.dest());
					var jump = Code.jump(parser.jump());
					var bytes = ConvertC(comp, dest, jump);
					byteList.Add(bytes[0]);
					byteList.Add(bytes[1]);
				}
			}
			var data = byteList.ToArray();
			fs.Write(data, 0, data.Length);

			fs.Close();
		}

		static byte[] ConvertA(string symbol)
		{
			int value;
			if (int.TryParse(symbol, out value))
			{
				return ConvertIntToBytes(value);
			}

			// 数字ではなくラベルであるとき
			if (symbolTable.contains(symbol))
			{
				value = symbolTable.getAddress(symbol);
			}
			else
			{
				// 見つからないときは変数用の場所へ入れる。
				symbolTable.addEntry(symbol, 16 + variableCount);
				value = symbolTable.getAddress(symbol);
			}
			return ConvertIntToBytes(value);
		}

		static byte[] ConvertC(string comp, string dest, string jump)
		{
			var biteString = "111" + comp + dest + jump;

			return ConvertStringToBytes(biteString);
		}

		static byte[] ConvertIntToBytes(int value)
		{
			var biteString = Convert.ToString(value, 2);
			while (biteString.Length != 16)
			{
				biteString = "0" + biteString;
			}
			return ConvertStringToBytes(biteString);
		}

		static byte[] ConvertStringToBytes(string biteString)
		{
			var zenhan = biteString.Substring(0, 8);
			var kouhan = biteString.Substring(8, 8);
			var zenByte = (byte)Convert.ToInt32(zenhan, 2);
			var kouByte = (byte)Convert.ToInt32(kouhan, 2);
			var ret = new byte[] { zenByte, kouByte };
			return ret;
		}
	}
}
