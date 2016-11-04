package com.pluralsight.hello_avro

import java.util
import java.util.Properties

import org.apache.avro.generic.GenericRecord
import org.apache.kafka.clients.consumer.{ConsumerRecord, ConsumerRecords, KafkaConsumer}

/**
  * Created by ps-dev on 11/2/2016.
  */

// See https://github.com/confluentinc/examples/tree/kafka-0.10.0.1-cp-3.0.1/kafka-clients
object ConsumerApp {
  def main(args: Array[String]): Unit = {

    val props = new Properties()
    props.put("bootstrap.servers", "10.107.219.195:9092")
    props.put("group.id", "kay-consumer-group-test4")
    props.put("key.deserializer", "org.apache.kafka.common.serialization.StringDeserializer")
    props.put("value.deserializer", "io.confluent.kafka.serializers.KafkaAvroDeserializer")
    props.put("schema.registry.url", "http://10.107.220.101:8081")
//    props.put("specific.avro.reader", "true")
    props.put("auto.offset.reset", "earliest")
    props.put("auto.commit.enable", "false")

    val consumer = new KafkaConsumer[String, GenericRecord](props)

    val topic = "ps.platform.test.kay.v3"

    consumer.subscribe(util.Arrays.asList(topic))

    try {
      while (true) {
        val records: ConsumerRecords[String, GenericRecord] = consumer.poll(200)
        val iter = records.iterator()
        while(iter.hasNext()) {
          val record : ConsumerRecord[String,GenericRecord] = iter.next()
          val user =  record.value()
          println(user.get("id"))
          println(user.get("name"))
          println(user.get("email"))
          println
        }
      }
    } finally {
      consumer.close()
    }


  }
}
