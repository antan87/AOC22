using Common.Parsing;
using Common.Parsing.Interfaces;

namespace Day9
{
    internal class StepParser : IParser<Step>
    {
        public Step Parse(string value)
        {
            var valuesAsArray = value.ToArray();
            MovmentKind kind = GetMovmentKind(valuesAsArray[0].ToString());
            var digits = valuesAsArray.Skip(2).Select(letter => letter.ToString());

            var digit = string.Join("", digits);

            int steps = ParseHelper.Parse(ParserCreator.Int32Parser, digit);

            return new Step(kind, steps);
        }

        private static MovmentKind GetMovmentKind(string letter)
        {
            switch (letter)
            {
                case "L":
                    return MovmentKind.Left;

                case "R":
                    return MovmentKind.Right;

                case "U":
                    return MovmentKind.Up;

                case "D":
                    return MovmentKind.Down;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}