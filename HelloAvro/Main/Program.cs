using System;

namespace HelloAvro.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var example = new AvroSpecificRecordExample();
            example.Produce();

            Console.In.ReadLine();
        }
    }
}