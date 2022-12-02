﻿using Common.Parsing.Interfaces;
using System.Reflection;

namespace Common.Parsing;

public static class ParseHelper
{
    public static T[] Parse<T>(string[] separators, IParser<T> parser, string? input, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Array.Empty<T>();

        return input.Split(separators, stringSplitOptions).Select(number => parser.Parse(number)).ToArray();
    }

    public static T[] Parse<T>(this string? input, string[] separators, IParser<T> parser, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Array.Empty<T>();

        return input.Split(separators, stringSplitOptions).Select(number => parser.Parse(number)).ToArray();
    }

    public static async Task<string> GetInput(string resourceNamePath)
    {
        string assembly = resourceNamePath.Split('.')[0];
        using (Stream? stream = Assembly.LoadFrom(assembly).GetManifestResourceStream(resourceNamePath))
        using (StreamReader reader = new StreamReader(stream!))
        {
            return await reader.ReadToEndAsync();
        }
    }

    public static T Parse<T>(IParser<T> parser, string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new NullReferenceException();
        return parser.Parse(input);
    }

    public static IEnumerable<T> GetInput<T>(this IEnumerable<string> values, IParser2D<T> parser)
    {
        values.Reverse();
        return values.Select((input, y) => parser.Parse(y, input)).SelectMany(item => item);
    }
}