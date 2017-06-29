using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using uPLibrary.Networking.M2Mqtt; //pamiętajcie o NuGecie M2MqttDotnetCore

namespace Room
{
    [Activity(Label = "Room", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        bool state = true;
        private MqttClient client;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { SwitchLampState(); };
        }

        public void SwitchLampState()
        {
            if (state)
            {
                TurnOnLamp();
                state = false;
            }
            else
            {
                TurnOffLamp();
                state = true;
            }
        }

        public void TurnOffLamp()
        {
            this.client = new MqttClient("192.168.0.169");//ip waszego brokera, u mnie ip lokalne mojego raspberry pi

            this.client.Connect(Guid.NewGuid().ToString());
            byte[] msg = new byte[1];
            msg[0] = 49;
            this.client.Publish("inTopic", msg);//topic taki jak przyjeśliście wszędzie w tym projekcie
        }

        public void TurnOnLamp()
        {
            this.client = new MqttClient("192.168.0.169");//ip waszego brokera, u mnie ip lokalne mojego raspberry pi

            this.client.Connect(Guid.NewGuid().ToString());
            byte[] msg = new byte[1];
            msg[0] = 50;
            this.client.Publish("inTopic", msg);//topic taki jak przyjeśliście wszędzie w tym projekcie
        }
    }
}

