using Common.Parsing;
using Common.Parsing.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day11
{
    public class MonkeyParser : IParser<Monkey>
    {
        public Monkey Parse(string value)
        {
            var rows = ParseHelper.Parse(value, new string[] { Environment.NewLine }, ParserCreator.StringParser, StringSplitOptions.RemoveEmptyEntries);
            int index = GetIndexOfMonkey(rows[0]);
            var items = GetItems(rows[1]);
            var operationFunc = GetOperationFunc(rows[2]);
            var isDivisibleFunc = GetIsDivisibleFunc(rows[3]);
            var throwFunc = GetThrowFunc(rows[4], rows[5]);
            var monkey = new Monkey(index, operationFunc, isDivisibleFunc, throwFunc);
            monkey.Items.AddRange(items);

            return monkey;
        }

        private static int GetIndexOfMonkey(string text)
        {
            return ParseHelper.Parse(ParserCreator.Int32Parser, text.Split(" ")[1].First().ToString());
        }

        private static int[] GetItems(string text)
        {
            var parts = ParseHelper.Parse(new string[] { ":" }, ParserCreator.StringParser, text, StringSplitOptions.TrimEntries);
            var textNumbers = ParseHelper.Parse(new string[] { "," }, ParserCreator.StringParser, parts[1], StringSplitOptions.TrimEntries);

            return textNumbers.Select(textNumber => ParseHelper.Parse(ParserCreator.Int32Parser, textNumber)).ToArray();
        }

        private static Func<int, int> GetOperationFunc(string text)
        {
            var parts = ParseHelper.Parse(new string[] { "=" }, ParserCreator.StringParser, text, StringSplitOptions.TrimEntries);
            var operationParts = ParseHelper.Parse(new string[] { " " }, ParserCreator.StringParser, parts[1], StringSplitOptions.TrimEntries);

            var inputText = operationParts[0];
            var inputText2 = operationParts[2];
            var arithmeticText = operationParts[1];

            switch (arithmeticText)
            {
                case "+":
                    return (input) => (inputText == "old" ? input : ParseHelper.Parse(ParserCreator.Int32Parser, inputText)) + (inputText2 == "old" ? input : ParseHelper.Parse(ParserCreator.Int32Parser, inputText2));

                case "*":
                    return (input) => (inputText == "old" ? input : ParseHelper.Parse(ParserCreator.Int32Parser, inputText)) * (inputText2 == "old" ? input : ParseHelper.Parse(ParserCreator.Int32Parser, inputText2));

                default:
                    throw new NotImplementedException();
            }
        }

        private static Func<int, bool> GetIsDivisibleFunc(string text)
        {
            var parts = ParseHelper.Parse(new string[] { "by" }, ParserCreator.StringParser, text, StringSplitOptions.TrimEntries);

            return (input) => input % ParseHelper.Parse(ParserCreator.Int32Parser, parts[1]) == 0;
        }

        private static Func<bool, int> GetThrowFunc(string row, string row2)
        {
            var indexText = ParseHelper.Parse(new string[] { "monkey" }, ParserCreator.StringParser, row, StringSplitOptions.TrimEntries);
            var index2Text = ParseHelper.Parse(new string[] { "monkey" }, ParserCreator.StringParser, row2, StringSplitOptions.TrimEntries);

            return (predicate) => predicate ? ParseHelper.Parse(ParserCreator.Int32Parser, indexText[1]) : ParseHelper.Parse(ParserCreator.Int32Parser, index2Text[1]);
        }
    }
}