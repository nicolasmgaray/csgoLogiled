using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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

namespace CSGO_Logitech_Integration
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static GameState gameState;
         static HttpListener httpListener = new HttpListener();
         static SimpleServer httpServer = new SimpleServer(httpListener, "http://127.0.0.1:3000/", processResponse);
        static bool serverStarted = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

            if (serverStarted)
            {
                httpServer.Stop();
                serverStarted = false;
            }
            else
            {
                gameState = new GameState();
                httpServer.Start();
                serverStarted = true;
            }

            btnStart.Content = !serverStarted ? "Start Server" : "Stop Server" ;






        }
        public  static byte[] processResponse(string request)
        {
            gameState.updateState(request);
            return new byte[0]; 
        }
    }

  

}
