using System.IO;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Json.Flat.Tests
{
    public class JsonFlattenerTests
    {
        private ITestOutputHelper _outputHelper;

        public JsonFlattenerTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
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
        
        [Fact(DisplayName = "Flatten and write the name/value pairs to the console")]
        public void Fact2()
        {
            var data = JObject.Parse(File.ReadAllText(@"jsonStructure1.json"));

            var flattened = JsonFlattener.Flatten(data);

            foreach (var key in flattened.Keys)
            {
                _outputHelper.WriteLine($"{key}, {flattened[key]}");
            }
        }

    }
}