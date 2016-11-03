name := "HelloAvro"

version := "1.0"

scalaVersion := "2.11.8"

// for kafka-avro-serializer
resolvers ++=Seq("Confluent Maven Repo" at "http://packages.confluent.io/maven/")

// Latest avro is 1.8.1
libraryDependencies += "org.apache.avro"  %  "avro"  %  "1.7.7"
//libraryDependencies += "com.twitter" % "bijection-avro_2.11" % "0.9.2"
libraryDependencies += "org.apache.kafka" %% "kafka" % "0.10.0.0"
libraryDependencies += "io.confluent" % "kafka-avro-serializer" % "3.0.0"

sbtavrohugger.SbtAvrohugger.avroSettings