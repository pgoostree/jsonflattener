using System.IO;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Json.Flat.Tests
{
    public class JsonFlattenerTests
    {
        public JsonFlattenerTests()
        {
        }
        
        [Fact(DisplayName = "When Json object is flattened it should contain expected keys")]
        public void Fact1()
        {
            var data = JObject.Parse(File.ReadAllText(@"jsonStructure1.json"));

            var flattened = JsonFlattener.Flatten(data);

            Assert.True(flattened.ContainsKey("firstName"));
            Assert.True(flattened.ContainsKey("lastName"));
            Assert.True(flattened.ContainsKey("address.street"));
            Assert.True(flattened.ContainsKey("address.city"));
            Assert.True(flattened.ContainsKey("address.state"));
            Assert.True(flattened.ContainsKey("address.phone[0].mobile"));
            Assert.True(flattened.ContainsKey("address.phone[0].home"));
            Assert.True(flattened.ContainsKey("address.phone[0].work"));
            Assert.True(flattened.ContainsKey("address.phone[1].mobile"));
            Assert.True(flattened.ContainsKey("address.phone[1].home"));
            Assert.True(flattened.ContainsKey("address.phone[1].work"));
        }
    }
}