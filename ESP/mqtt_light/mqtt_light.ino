#include <ESP8266WiFi.h>
#include <PubSubClient.h> //należy pobrać i zainstalować bibliotekę

const char* ssid = "Nazwa_sieci"; //tu nalezy wpisac nazwe swoje sieci wi-fi
const char* password = "haslo"; //tutaj hasło do tej sieci
const char* mqtt_server = "192.168.0.169"; //ip naszego brokera

#define DIGITAL_PIN  14 //pin do którego podpięty jest przekaźnik
WiFiClient espClient;
PubSubClient client(espClient);
long lastMsg = 0;
char msg[50];
int value = 0;

void setup() {
  pinMode(DIGITAL_PIN, OUTPUT);    
  digitalWrite(DIGITAL_PIN, LOW); 
  Serial.begin(115200);
  setup_wifi();
  client.setServer(mqtt_server, 1883);
  client.setCallback(callback);
}

void setup_wifi() {

  delay(10);
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
}

void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();

  if ((char)payload[0] == '1') {
    digitalWrite(DIGITAL_PIN, LOW);   

  } else if ((char)payload[0] == '2') {
    digitalWrite(DIGITAL_PIN, HIGH); 
  }

}

void reconnect() {
  while (!client.connected()) {
    Serial.print("Attempting MQTT connection...");
    if (client.connect("ESP8266Client")) {
      Serial.println("connected");
      client.publish("outTopic", "test");
      client.subscribe("inTopic");
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      delay(5000);
    }
  }
}
void loop() {

  if (!client.connected()) {
    reconnect();
  }
  client.loop();

  long now = millis();
  if (now - lastMsg > 2000) {
    lastMsg = now;
    ++value;
    //testowo ESP cały czas na OutTopic wysyła wiadomości
    snprintf (msg, 75, "test #%ld", value);
    Serial.print("Publish message: ");
    Serial.println(msg);
    client.publish("outTopic", msg);
  }
}
