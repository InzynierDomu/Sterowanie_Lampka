using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt; //pamiętajcie o NuGecie M2MqttDotnetCore

namespace test_app_for_cortana
{
    class Mqtt_commands
    {
        private MqttClient client;

        public void turningOnLightMqtt()
        {
            this.client = new MqttClient("192.168.0.169");//ip waszego brokera, u mnie ip lokalne mojego raspberry pi

            this.client.Connect(Guid.NewGuid().ToString());
            byte[] msg = new byte[1];
            msg[0] = 50;
            this.client.Publish("inTopic", msg);//topic taki jak przyjeśliście wszędzie w tym projekcie
        }

        public void turningOffLightMqtt()
        {
            this.client = new MqttClient("192.168.0.169");//ip waszego brokera, u mnie ip lokalne mojego raspberry pi

            this.client.Connect(Guid.NewGuid().ToString());
            byte[] msg = new byte[1];
            msg[0] = 49;
            this.client.Publish("inTopic", msg);//topic taki jak przyjeśliście wszędzie w tym projekcie
        }

    }
}
