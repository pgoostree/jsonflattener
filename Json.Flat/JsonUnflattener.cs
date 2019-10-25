using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Json.Flat
{
    public class JsonUnflattener
    {
        public JObject Unflatten(
            IDictionary<string, object> keyValues)
        {
            JContainer result = null;
            var setting = new JsonMergeSettings();

            setting.MergeArrayHandling = MergeArrayHandling.Merge;

            foreach (var pathValue in keyValues)
                if (result == null)
                {
                    result = UnflatenSingle(pathValue);
                }
                else
                {
                    var node = UnflatenSingle(pathValue);
                    result.Merge(node, setting);
                }

            return result as JObject;
        }

        private JContainer UnflatenSingle(
            KeyValuePair<string, object> keyValue)
        {
            var path = keyValue.Key;
            var value = keyValue.Value;
            var pathSegments = SplitPath(path);
            JContainer lastItem = null;

            // Build from leaf to root
            foreach (var pathSegment in pathSegments.Reverse())
            {
                var type = GetJsonType(pathSegment);
                switch (type)
                {
                    case JsonType.Object:
                        var obj = new JObject();
                        if (null == lastItem)
                        {
                            JToken token = null;
                            if (value.ToString() != string.Empty)
                            {
                                token = JToken.FromObject(value);
                            }

                            obj.Add(pathSegment, token);
                        }
                        else
                        {
                            obj.Add(pathSegment, lastItem);
                        }

                        lastItem = obj;
                        break;
                    case JsonType.Array:
                        var array = new JArray();
                        var index = GetArrayIndex(pathSegment);
                        array = FillEmpty(array, index);
                        if (lastItem == null)
                        {
                            array[index] = JToken.FromObject(value);
                        }
                        else
                        {
                            array[index] = lastItem;
                        }

                        lastItem = array;
                        break;
                }
            }

            return lastItem;
        }

        private IList<string> SplitPath(
            string path)
        {
            IList<string> result = new List<string>();
            var regex = new Regex(@"(?!\.)([^. ^\[\]]+)|(?!\[)(\d+)(?=\])");

            foreach (Match match in regex.Matches(path)) result.Add(match.Value);
            return result;
        }

        private JArray FillEmpty(
            JArray array,
            int index)
        {
            for (var i = 0; i <= index; i++) array.Add(null);
            return array;
        }

        private JsonType GetJsonType(
            string pathSegment)
        {
            return int.TryParse(pathSegment, out _) ? JsonType.Array : JsonType.Object;
        }

        private int GetArrayIndex(
            string pathSegment)
        {
            if (int.TryParse(pathSegment, out var result))
            {
                return result;
            }

            throw new ApplicationException("Unable to parse array index: " + pathSegment);
        }
    }
}