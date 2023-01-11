using Common.Parsing;
using Common.Parsing.Interfaces;

namespace Day12
{
    public sealed class GridParser : IParser<Grid>
    {
        Grid IParser<Grid>.Parse(string value)
        {
            List<string> elevations = new List<string>
            {
                "S","a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
                "n", "o", "p", "q", "r","s", "t", "u", "v", "w", "x", "y", "z", "E"
            };
            var rows = ParseHelper.Parse(value, new string[] { Environment.NewLine }, ParserCreator.StringParser);

            var positions = rows.Select((row, y) =>
            {
                return row
                .Select((letter, x) =>
                {
                    var elevation = elevations.IndexOf(letter.ToString().Trim());
                    if (elevation < 0)
                        throw new Exception();

                    if (letter.ToString() == "E")
                        return new Position(new Coordinate(y, x), elevation, Destination: true);
                    else if (letter.ToString() == "S")
                        return new Position(new Coordinate(y, x), elevation, Starting: true);

                    return new Position(new Coordinate(y, x), elevation);
                });
            }).SelectMany(pos => pos.ToList()).ToList();

            return new Grid(positions);
        }
    }
}