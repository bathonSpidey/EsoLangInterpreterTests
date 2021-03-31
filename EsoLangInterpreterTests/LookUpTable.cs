using System.Collections.Generic;

namespace EsoLangInterpreterTests
{
	public static class LookUpTable
	{
		public static Dictionary<int, int> GetClosingBrackets(string code)
		{
			var lookUpTable = new Dictionary<int, int>();
			for (int index = 0; index < code.Length; index++)
			{
				if (code[index] == '[')
				{
					var closingIndex = GetClosingIndex(code, index);
					lookUpTable[index] = closingIndex;
				}
			}
			return lookUpTable;
		}

		private static int GetClosingIndex(string code, int index)
		{
			var stack = new Stack<int>();
			for (int start = index; start < code.Length; start++)
			{
				if (code[start]=='[')
					stack.Push(code[start]);
				else if (code[start] == ']')
				{
					stack.Pop();
					if (stack.Count == 0)
						return start;
				}
			}
			return -1;
		}
	}
}