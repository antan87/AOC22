using System.Text;
using Common.Parsing;
using Common.Parsing.Interfaces;

namespace Day13
{
    public sealed class RowParser : IParser<List<List<IPart>>>
    {
        List<List<IPart>> IParser<List<List<IPart>>>.Parse(string value)
        {
            var rows = ParseHelper.Parse(value, new string[] { Environment.NewLine }, ParserCreator.StringParser, StringSplitOptions.RemoveEmptyEntries);
            int taker = 0;
            var list = new List<List<IPart>>();
            while (taker < rows.Length)
            {
                var pairRows = rows.Skip(taker).Take(1);
                var firstPart = GetListRow(pairRows.First());

                Console.WriteLine();

                list.Add(firstPart);
                taker += 1;
            }

            return list;
        }

        protected List<IPart> GetListRow(string content)
        {
            StringBuilder builder = new StringBuilder();
            var rootList = new List<IPart>();

            StringBuilder stringBuilder = new StringBuilder();
            List<IPart> list = new List<IPart>();
            TakeWhile(list, content, stringBuilder);

            Console.WriteLine(stringBuilder.ToString());

            return list;

            void TakeWhile(List<IPart> list, IEnumerable<char> content, StringBuilder builder)
            {
                char? part = content.Take(1).FirstOrDefault();
                switch (part)
                {
                    case ',':
                        builder.Append(",");
                        TakeWhile(list, content.Skip(1), builder);
                        break;

                    case '[':
                        builder.Append('[');
                        list.Add(new StartPart());
                        TakeWhile(list, content.Skip(1), builder);
                        break;

                    case ']':
                        builder.Append(']');
                        list.Add(new EndPart());
                        TakeWhile(list, content.Skip(1), builder);
                        break;

                    case null:
                        builder.Append("End");
                        break;

                    default:
                        var charNumbers = content.TakeWhile(ch => int.TryParse(ch.ToString(), out int _)).ToList();
                        if (!charNumbers.Any())
                            return;

                        var intNumber = ParserCreator.Int32Parser.Parse(string.Join("", charNumbers));
                        list.Add(new IntPart(intNumber));
                        builder.Append(intNumber);
                        TakeWhile(list, content.Skip(charNumbers.Count()), builder);
                        break;
                }
            }
        }
    }
}