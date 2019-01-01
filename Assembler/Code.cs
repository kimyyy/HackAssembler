using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
	class Code
	{
		public static string dest(string nimonic)
		{
			string ret;
			switch (nimonic)
			{
				case "":
					ret = "000";
					break;
				case "null":
					ret = "000";
					break;
				case "M":
					ret = "001";
					break;
				case "D":
					ret = "010";
					break;
				case "MD":
					ret = "011";
					break;
				case "A":
					ret = "100";
					break;
				case "AM":
					ret = "101";
					break;
				case "AD":
					ret = "110";
					break;
				case "AMD":
					ret = "111";
					break;
				default:
					throw new Exception("dest : unexpected symbol");
			}
			return ret;
		}

		public static string comp(string nimonic)
		{
			string ret;
			string a0 = "0";
			string a1 = "1";
			switch (nimonic)
			{
				case "0":
					ret = "0101010";
					break;
				case "1":
					ret = "0111111";
					break;
				case "-1":
					ret = "0111010";
					break;
				case "D":
					ret = "0001100";
					break;
				case "A":
					ret = "0110000";
					break;
				case "!D":
					ret = "0001101";
					break;
				case "!A":
					ret = "0110001";
					break;
				case "-D":
					ret = "0001111";
					break;
				case "-A":
					ret = "0001111";
					break;
				case "D+1":
					ret = a0 + "011111";
					break;
				case "A+1":
					ret = a0 + "110111";
					break;
				case "D-1":
					ret = a0 + "001110";
					break;
				case "A-1":
					ret = a0 + "110010";
					break;
				case "D+A":
					ret = a0 + "000010";
					break;
				case "D-A":
					ret = a0 + "010011";
					break;
				case "A-D":
					ret = a0 + "000111";
					break;
				case "D&A":
					ret = a0 + "000000";
					break;
				case "D|A":
					ret = a0 + "010101";
					break;
				case "M":
					ret = a1 + "110000";
					break;
				case "!M":
					ret = a1 + "110001";
					break;
				case "M+1":
					ret = a1 + "110111";
					break;
				case "M-1":
					ret = a1 + "110010";
					break;
				case "D+M":
					ret = a1 + "000010";
					break;
				case "D-M":
					ret = a1 + "010011";
					break;
				case "M-D":
					ret = a1 + "000111";
					break;
				case "D&M":
					ret = a1 + "000000";
					break;
				case "D|M":
					ret = a1 + "010101";
					break;
				default:
					throw new Exception("comp: 未定義です。");
			}
			return ret.ToString();
		}

		public static string jump(string nimonic)
		{
			string ret;
			switch (nimonic)
			{
				case "":
					ret = "000";
					break;
				case "null":
					ret = "000";
					break;
				case "JGT":
					ret = "001";
					break;
				case "JEQ":
					ret = "010";
					break;
				case "JGE":
					ret = "011";
					break;
				case "JLT":
					ret = "100";
					break;
				case "JNE":
					ret = "101";
					break;
				case "JLE":
					ret = "110";
					break;
				case "JMP":
					ret = "111";
					break;
				default:
					throw new Exception("jump : unexpected symbol");
			}
			return ret;
		}
	}
}
