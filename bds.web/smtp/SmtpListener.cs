using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace bds.web.smtp {
    public class SmtpListener : IDisposable {
        private TcpListener listener;
        private List<ITcpClientHandler> clients = new List<ITcpClientHandler>();
        private Thread ListenThread;
        private bool _stayAlive;
        public bool IsAlive { get; private set; }

        public SmtpListener() : this(IPAddress.Any, 25) { }
        public SmtpListener(IPAddress address, int port) {
            listener = new TcpListener(new IPEndPoint(address, port));
            StartListeningForClients();
        }

        private void StartListeningForClients() {
            IsAlive = true;
            ListenThread = new Thread(new ThreadStart(WaitForConnection));
            ListenThread.Start();
        }

        public void StopListeningForClients() {
            IsAlive = false;
            if (ListenThread != null)
                ListenThread.Abort();
            listener.Stop();
        }

        private void CloseClientConnection(ITcpClientHandler client) {
            client.CloseConnection("Smtp Listener Closing.");
        }

        private void WaitForConnection() {
            listener.Start();
            while (IsAlive) {
                var client = listener.AcceptTcpClient();
                WhenClientConnected(client);
            }
        }

        private void WhenClientConnected(TcpClient client) {
            Console.WriteLine("Connection Made.");
            var handler = new SmtpHandler(client);
            clients.Add(handler);
            handler.OnClientClosed += new TcpClientConnectionClosedHandler(WhenClientClosed);
            handler.ListenToClient();
        }


        private void WhenClientClosed(object sender, string message) {
            DisposeClientHandler((SmtpHandler)sender);
            Console.WriteLine(string.Format("Client Closed: {0}", message));
        }

        private void DisposeClientHandler(ITcpClientHandler client) {
            client.Dispose();
            clients.Remove(client);
            Console.WriteLine("Client closed and removed from list.");
        }

        public void Dispose() {
            StopListeningForClients();
            clients.ForEach(c => c.Dispose());
        }

    }
}
