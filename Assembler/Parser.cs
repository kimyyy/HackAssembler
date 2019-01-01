using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
	class Parser
	{
		private StreamReader streamReader;

		private string currentScript;

		public enum cType
		{
			A_COMMAND, C_COMMAND, L_COMMAND
		}

		public cType currentCommandType;

		public bool CanParse
		{
			get { return currentScript != ""; }
		}

		#region private method

		/// <summary>
		/// 一行を要素に分解します。
		/// </summary>
		/// <param name="str">一行の文字列</param>
		/// <returns>分解した要素</returns>
		private string ParseOneCol(string str)
		{
			// コメントを取り除く
			int? commentIndex = null;
			for (var i = 0; i < str.Length - 1; i++)
			{
				if (str[i] == str[i + 1] && str[i] == '/')
				{
					commentIndex = i;
					break;
				}
			}
			if (commentIndex != null)
			{
				str = str.Remove((int)commentIndex);
			}

			// 空白を取り除く
			str = str.Replace(" ", String.Empty);
			str = str.Replace("	", String.Empty);

			// もし空行なら次へ

			// 文を分ける
			if (str.Contains("=") || str.Contains(";"))
			{
				currentCommandType = cType.C_COMMAND;
			}
			else if (str.Contains("@"))
			{
				currentCommandType = cType.A_COMMAND;
			}
			else if (str.Contains("(") && str.Contains(")"))
			{
				currentCommandType = cType.L_COMMAND;
			}
			return str;
		}

		#endregion

		#region public method

		/// <summary>
		/// 入力ファイルやストリームを開きます
		/// </summary>
		public Parser(string inputFileName)
		{
			streamReader = new StreamReader(inputFileName, Encoding.GetEncoding("SHIFT-JIS"));
		}

		/// <summary>
		/// 入力において、さらにコマンドが存在するか
		/// </summary>
		/// <returns></returns>
		public bool hasMoreCommand()
		{
			return streamReader.Peek() != -1;
		}

		/// <summary>
		/// 入力から次のコマンドを読み、それを現コマンドとするhasMoreCommandsがtrueのときのみ呼ばれる。
		/// </summary>
		public void advance()
		{
			currentScript = streamReader.ReadLine();
			currentScript = ParseOneCol(currentScript);
		}

		public cType commandType()
		{
			return currentCommandType;
		}

		public string symbol()
		{
			switch (currentCommandType)
			{
				case cType.C_COMMAND:
					throw new Exception("symbol : 対応していないコマンドが呼ばれました。");
				case cType.A_COMMAND:
					var aSymbol = currentScript.Replace("@", "");
					return aSymbol;
				case cType.L_COMMAND:
					var cSymbol = currentScript;
					cSymbol = cSymbol.Replace("(", "");
					cSymbol = cSymbol.Replace(")", "");
					return cSymbol;

			}
			throw new Exception("symbol : 対応していないコマンドが呼ばれました");
		}

		public string dest()
		{
			if (currentCommandType != cType.C_COMMAND)
			{
				throw new Exception("対応していないコマンドです。");
			}
			if (!currentScript.Contains("="))
			{
				return "";
			}
			var eqIndex = currentScript.IndexOf('=');
			var dest = currentScript.Remove(eqIndex);
			return dest;
		}

		public string comp()
		{
			if (currentCommandType != cType.C_COMMAND)
			{
				throw new Exception("対応していないコマンドです。");
			}
			string comp = currentScript;
			if (currentScript.Contains(";"))
			{
				var compIndex = currentScript.IndexOf(';');
				comp = currentScript.Remove(compIndex);
			}

			if (currentScript.Contains("="))
			{
				var eqIndex = currentScript.IndexOf('=');
				comp = comp.Substring(eqIndex + 1);
			}
			return comp;
		}

		public string jump()
		{
			if (currentCommandType != cType.C_COMMAND)
			{
				throw new Exception("対応していないコマンドです。");
			}
			if (!currentScript.Contains(";"))
			{
				return "";
			}
			var compIndex = currentScript.IndexOf(';');
			var jump = currentScript.Substring(compIndex + 1);
			return jump;
		}

		public void AllCol()
		{
			while (hasMoreCommand())
			{
				advance();
				// ここにコード
			}
		}

		#endregion
	}
}
