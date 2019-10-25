using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Json.Flat
{
    public class JsonFlattener
    {
        public static IDictionary<string, string> Flatten(
            JObject json)
        {
            if (json == null)
            {
                return null;
            }

            // By design Json.Net will parse date formatted string to a date type.  
            // Set DateParseHandling to none so that date formatted strings are not modified.
            var reader = new JsonTextReader(new StringReader(json.ToString()))
            {
                DateParseHandling = DateParseHandling.None
            };

            return JObject.Load(reader).Descendants()
                .Where(j => !j.Children().Any())
                .Aggregate(
                    new Dictionary<string, string>(),
                    (
                        props,
                        jtoken) =>
                    {
                        props.Add(jtoken.Path, jtoken.ToString());
                        return props;
                    });
        }
    }
}