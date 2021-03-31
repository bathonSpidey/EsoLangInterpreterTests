using System.Linq;

namespace EsoLangInterpreterTests
{
	public class SmallFuck
	{
		public string Compile(string code, string tape)
		{
			var tapeArray = tape.ToCharArray();
			Execute(code, tapeArray, 0);
			return new string(tapeArray);
		}

		private static void Execute(string code, char[] tapeArray, int pointer)
		{
			var lookUpTable = LookUpTable.GetClosingBrackets(code);
			for (var index = 0; index < code.Length; index++)
			{
				switch (code[index])
				{
				case '*':
					FlipCharacter(tapeArray, pointer);
					break;
				case '>':
					pointer++;
					break;
				case '<':
					pointer--;
					break;
				case '[':
					if (tapeArray[pointer] == '0')
						index = lookUpTable[index];
					break;
				case ']':
					if (tapeArray[pointer] != '0')
						index = lookUpTable.FirstOrDefault(x=>x.Value==index).Key;
					break;
				}
				if (pointer >= tapeArray.Length || pointer < 0)
					break;
			}
		}

		private static void FlipCharacter(char[] tapeArray, int i)
		{
			if (tapeArray[i] == '0')
				tapeArray[i] = '1';
			else
				tapeArray[i] = '0';
		}
	}
}