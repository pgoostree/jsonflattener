using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Json.Flat
{
    public abstract class JsonFlattenerBase
    {
        internal static IList<string> SplitPath(
            string path)
        {
            IList<string> result = new List<string>();
            var regex = new Regex(@"(?!\.)([^. ^\[\]]+)|(?!\[)(\d+)(?=\])");

            foreach (Match match in regex.Matches(path)) result.Add(match.Value);
            return result;
        }

        internal static JArray FillEmpty(
            JArray array,
            int index)
        {
            for (var i = 0; i <= index; i++) array.Add(null);
            return array;
        }

        internal static JsonType GetJsonType(
            string pathSegment)
        {
            return int.TryParse(pathSegment, out _) 
                ? JsonType.Array 
                : JsonType.Object;
        }

        internal static int GetArrayIndex(
            string pathSegment)
        {
            if (int.TryParse(pathSegment, out var result)) return result;

            throw new ApplicationException("Unable to parse array index: " + pathSegment);
        }
    }
}