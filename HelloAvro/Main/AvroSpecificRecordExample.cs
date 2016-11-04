using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ps.platform.test;

namespace HelloAvro.Main
{


    public class AvroSpecificRecordExample
    {
        const string topic = "ps.platform.test.kay.v8";
        const string consumerGroup = "kay-consumer-group-test6";
        const string kafkaRestProxyUrl = "http://kafka-rest.stage:8082";
        const string commandContentType = "application/vnd.kafka.v1+json";
        const string messageContentType = "application/vnd.kafka.avro.v1+json";

        public void Produce()
        {
            var user = new User2(42, "kay", null);
            var url = $"{kafkaRestProxyUrl}/topics/{topic}";
            var request = new ProduceRequestSpecific
            {
                value_schema = User2.Schema,
                records = new[]
                              {
                                  new ProduceRequestRecordSpecific { value = user }
                              }
            };
            var payload = JsonConvert.SerializeObject(request);
            var response = HttpHelper.Post(url, messageContentType, payload);
            var resp = JsonConvert.DeserializeObject<ProduceResponse>(response.ResponseBody);
        }
    }

    public class ProduceRequestSpecific
    {
        public string value_schema;
        public ProduceRequestRecordSpecific[] records;
    }

    public class ProduceRequestRecordSpecific
    {
        public User2 value;
    }
}