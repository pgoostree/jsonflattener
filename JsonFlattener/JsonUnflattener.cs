using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Json.Flat
{
    public class JsonUnflattener : JsonFlattenerBase
    {
        public static JObject Unflatten(
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

        private static JContainer UnflatenSingle(
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
                            if (value.ToString() != string.Empty) token = JToken.FromObject(value);

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
                            array[index] = JToken.FromObject(value);
                        else
                            array[index] = lastItem;

                        lastItem = array;
                        break;
                }
            }

            return lastItem;
        }
    }
}