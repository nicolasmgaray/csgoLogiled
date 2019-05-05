
using System.Net;
using System.Windows;


namespace CSGO_Logitech_Integration
{ 

    public partial class MainWindow : Window
    {
        // GAMESTATE (For more reference go to GameState.cs)
        static GameState gameState;

        // WEBSERVER  (For more reference go to WebServer.cs)
        static HttpListener httpListener = new HttpListener();
        static SimpleServer httpServer = new SimpleServer(httpListener, "http://127.0.0.1:3000/", processResponse);
        static bool serverStarted = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Right now the only piece of UI, it just starts/stops the server and create a GameState instance.
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
