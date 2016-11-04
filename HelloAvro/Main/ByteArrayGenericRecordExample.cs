using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Hadoop.Avro;
using Newtonsoft.Json;

namespace HelloAvro.Main
{
    public class ByteArrayGenericRecordExample
    {
        const string topic = "ps.platform.test.kay.v1";
        const string consumerGroup = "kay-consumer-group-test6";
        const string kafkaRestProxyUrl = "http://kafka-rest.stage:8082";
        const string commandContentType = "application/vnd.kafka.v1+json";
        const string messageContentType = "application/vnd.kafka.binary.v1+json";

        public void Produce()
        {
            const string key = "1";
            var user = AvroSerialization.SerializeUser(1, "mark", "mark@test.com");

            var url = $"{kafkaRestProxyUrl}/topics/{topic}";
            var request = new ProduceRequest
            {
                records = new [] {
                    new ProduceRequestRecord
                        {
                            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key)),
                            value = Convert.ToBase64String(user)
                        }}
            };
            var payload = JsonConvert.SerializeObject(request);
            var response = HttpHelper.Post(url, messageContentType, payload);
            var resp = JsonConvert.DeserializeObject<ProduceResponse>(response.ResponseBody);

        }

        public void Consume()
        {
            string url;
            var consumerId = CreateConsumer().Instance_Id;
            url = $"{kafkaRestProxyUrl}/consumers/{consumerGroup}/instances/{consumerId}/topics/{topic}";
            var response = HttpHelper.Get(url, messageContentType);
            var messages = JsonConvert.DeserializeObject<ConsumeMessageResponse[]>(response.ResponseBody);
            var values = messages.Select(m => Convert.FromBase64String(m.Value));
            var users = values.Select(v => AvroSerialization.DeserializeUser(v));

            foreach (var u in users)
            {
                var email = u.email ?? "";
                Console.Out.WriteLine($"User: {u.id} - {u.name} - {email}");
            }
        }

        private CreateConsumerResponse CreateConsumer()
        {
            var url = $"{kafkaRestProxyUrl}/consumers/{consumerGroup}";
            const string payload = @"{ ""format"": ""binary"", ""auto.offset.reset"": ""smallest"" }";
            var response = HttpHelper.Post(url, commandContentType, payload);
            return JsonConvert.DeserializeObject<CreateConsumerResponse>(response.ResponseBody);
        }

     
    }

    public class ProduceRequest
    {
        public string key_schema;
        public string key_schema_id;
        public string value_schema;
        public string value_schema_id;
        public ProduceRequestRecord[] records;
    }

    public class ProduceRequestRecord
    {
        public object key;
        public object value;
        public int? partition;
    }

    public class ProduceResponse
    {
        public int? Key_Schema_Id;
        public int? Value_Schema_Id;
        public ProduceResponseOffset[] Offsets;
    }

    public class ProduceResponseOffset
    {
        public int Partition;
        public long Offset;
        public long? Error_Code;
        public string Error;
    }

    internal class CreateConsumerResponse
    {
        public string Instance_Id { get; set; }
        public string Base_Uri { get; set; }
    }

    public class ConsumeMessageResponse
    {
//        public string Key { get; set; }
        public string Value { get; set; }
        public int Partition { get; set; }
        public int Offset { get; set; }
    }
}
