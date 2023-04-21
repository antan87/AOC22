using Common.Parsing;
using Common.Parsing.Interfaces;

namespace Day13;

public sealed class PairParser : IParser<List<Pair>>
{
    List<Pair> IParser<List<Pair>>.Parse(string value)
    {
        var rows = ParseHelper.Parse(value, new string[] { Environment.NewLine }, ParserCreator.StringParser, StringSplitOptions.RemoveEmptyEntries);

        int taker = 0;
        int index = 1;
        List<Pair> pairs = new List<Pair>();
        while (taker < rows.Length)
        {
            var pairRows = rows.Skip(taker).Take(2);
            var firstPart = GetPart(pairRows.First());
            var secondPart = GetPart(pairRows.Last());

            pairs.Add(new Pair(index, firstPart, secondPart));
            taker += 2;
            index++;
        }

        return pairs;
    }

    private ListPart GetPart(string content)
    {
        var rootList = new List<IPart>();

        var rootPart = new ListPart(rootList, null);
        TakeWhile(rootPart, content.Skip(1));

        return rootPart;

        void TakeWhile(ListPart? listPart, IEnumerable<char> content)
        {
            if (listPart == null)
                return;

            char? part = content.Take(1).FirstOrDefault();
            switch (part)
            {
                case ',':
                    TakeWhile(listPart, content.Skip(1));
                    break;

                case '[':
                    var newPart = new ListPart(new List<IPart>(), listPart);
                    listPart.Parts.Add(newPart);
                    TakeWhile(newPart, content.Skip(1));
                    break;

                case ']':
                    TakeWhile(listPart.ParentListPart, content.Skip(1));
                    break;

                case null:
                    break;

                default:
                    var charNumbers = content.TakeWhile(ch => int.TryParse(ch.ToString(), out int _)).ToList();
                    var intNumber = ParserCreator.Int32Parser.Parse(string.Join("", charNumbers));
                    listPart.Parts.Add(new IntPart(intNumber));
                    TakeWhile(listPart, content.Skip(charNumbers.Count()));
                    break;
            }
        }
    }
}