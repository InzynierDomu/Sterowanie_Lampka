using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uPLibrary.Networking.M2Mqtt; //pamiętajcie o NuGecie M2MqttDotnetCore


namespace Mqtt_wpf_test
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MqttClient client;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.client = new MqttClient("192.168.0.169");//ip waszego brokera, u mnie ip lokalne mojego raspberry pi

            this.client.Connect(Guid.NewGuid().ToString());
            byte[] msg = new byte[1];
            msg[0] = 49;
            this.client.Publish("inTopic", msg);//topic taki jak przyjeśliście wszędzie w tym projekcie
        }

        private void btnTurnOn_Click(object sender, RoutedEventArgs e)
        {
            this.client = new MqttClient("192.168.0.169");//ip waszego brokera, u mnie ip lokalne mojego raspberry pi

            this.client.Connect(Guid.NewGuid().ToString());
            byte[] msg = new byte[1];
            msg[0] = 50;
            this.client.Publish("inTopic", msg);//topic taki jak przyjeśliście wszędzie w tym projekcie
        }
    }
}
