package com.pluralsight.hello_avro

import java.util.Properties

import org.apache.avro.Schema
import org.apache.avro.generic.{GenericData, GenericRecord}
import org.apache.kafka.clients.producer.{KafkaProducer, ProducerRecord}

import scala.io.Source

/**
  * Created by ps-dev on 11/2/2016.
  */
object ProducerApp {
  def main(args: Array[String]): Unit = {
    println("Hello Avro")

    val schemaSource = Source.fromFile("src/main/avro/User.avsc")
    val schemaStr = schemaSource.getLines mkString "\n"
    val schema: Schema = new Schema.Parser().parse(schemaStr)

    val user: GenericRecord = new GenericData.Record(schema)
    user.put("id", 2)
    user.put("name", "becca")
    user.put("email", "rebecca@test.com")

    val props = new Properties()
    props.put("bootstrap.servers", "10.107.219.195:9092")
    props.put("key.serializer", "io.confluent.kafka.serializers.KafkaAvroSerializer")
    props.put("value.serializer", "io.confluent.kafka.serializers.KafkaAvroSerializer")
    props.put("schema.registry.url", "http://10.107.220.101:8081")

    val topic = "ps.platform.test.kay.v3"

    val producer = new KafkaProducer[String, GenericRecord](props)
    val record = new ProducerRecord[String, GenericRecord](topic, user)
    producer.send(record)

    producer.close()
  }
}
