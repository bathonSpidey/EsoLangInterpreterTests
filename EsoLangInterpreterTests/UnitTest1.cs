using NUnit.Framework;

namespace EsoLangInterpreterTests
{
	public class SmallFuckTests
	{
		[Test]
		public void FlippingCodeShouldFlipTape()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile("*", "00101100");
			Assert.That(result, Is.EqualTo("10101100"));
		}

		[Test]
		public void FlipsSecondAndThirdPositionOfTheCell()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile(">*>*", "00101100");
			Assert.That(result, Is.EqualTo("01001100"));
		}

		[Test]
		public void FlipsAllInTheCell()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile("*>*>*>*>*>*>*>*", "00101100");
			Assert.That(result, Is.EqualTo("11010011"));
		}

		[Test]
		public void FlipsZeroBits()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile("*>*>>*>>>*>*", "00101100");
			Assert.That(result, Is.EqualTo("11111111"));
		}

		[Test]
		public void FlipsOneBits()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile(">>>>>*<*<<*", "00101100");
			Assert.That(result, Is.EqualTo("00000000"));
		}

		[Test]
		public void JumpPastBracketIfZero()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile("*>[>*]*", "00101100");
			Assert.That(result, Is.EqualTo("11101100"));
		}

		[Test]
		public void LoopShouldExecuteProperly()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile("*[>*]",
				"0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
			Assert.That(result,
				Is.EqualTo(
					"1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111"));
		}

		[Test]
		public void FailingTestsInKata()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile("[[]*>*>*>]", "000");
			Assert.That(result, Is.EqualTo("000"));
		}

		[Test]
		public void AnotherFailingTestsInKata()
		{
			var interpreter = new SmallFuck();
			var result = interpreter.Compile("[*>[>*>]>]", "11001");
			Assert.That(result, Is.EqualTo("01100"));
		}
	}

	public class SmallFuck
	{
		public string Compile(string code, string tape)
		{
			var pointer = 0;
			var tapeArray = tape.ToCharArray();
			var bracketStartIndex = -1;
			Execute(code, tapeArray, pointer, bracketStartIndex);
			return new string(tapeArray);
		}

		private static void Execute(string code, char[] tapeArray, int pointer, int bracketStartIndex)
		{
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
						index = code.LastIndexOf(']');
					else
						bracketStartIndex = index;
					break;
				case ']':
					if (tapeArray[pointer] != '0')
						index = bracketStartIndex;
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