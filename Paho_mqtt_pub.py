#!/usr/bin/python

import paho.mqtt.client as mqtt

client = mqtt.Client()
client.connect("127.0.0.1")

client.publish("/inTopic", "0" ,0)
#"0" zalacza lampke, zamaiana na "1" bedzie wylaczac lampke

