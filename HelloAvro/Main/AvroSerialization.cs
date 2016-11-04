using System.IO;
using Microsoft.Hadoop.Avro;

namespace HelloAvro.Main
{
    public class AvroSerialization
    {
        public static byte[] SerializeUser(int id, string name, string email)
        {
            var schemaPath = @"Avro\User.avsc";
            var schemaStr = File.ReadAllText(schemaPath);
            var serializer = AvroSerializer.CreateGeneric(schemaStr);

            using (var stream = new MemoryStream())
            {
                dynamic user = new AvroRecord(serializer.WriterSchema);
                user.id = id;
                user.name = name;
                user.email = email;
                serializer.Serialize(stream, user);
                return stream.ToArray();
            }
        }

        public static dynamic DeserializeUser(byte[] serialized)
        {
            var schemaPath = @"Avro\User.avsc";
            var schemaStr = File.ReadAllText(schemaPath);
            var serializer = AvroSerializer.CreateGeneric(schemaStr);

            using (var stream = new MemoryStream(serialized))
            {
                dynamic user = serializer.Deserialize(stream);
                return user;
            }
        }
    }
}