using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Parsing.Interfaces;

namespace Common.Parsing
{
    public class Int32Parser2D : IParser2D<int>

    {
        public int[] Parse(int y, string value)
        {
            return ParseHelper.Parse(new string[] { }, ParserCreator.StringParser, value)
                .Select(item => ParseHelper.Parse(ParserCreator.Int32Parser, item))
                .ToArray();
        }
    }
}