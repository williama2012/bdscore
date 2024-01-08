using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace bds.web.smtp {

    public delegate void TcpClientConnectionClosedHandler(object sender, string message);

    public abstract class TcpClientHandler : ITcpClientHandler {
        public TcpClient Client { get; set; }

        /// <summary>
        /// Get or Set weither the 
        /// </summary>
        public bool IsAlive { get; private set; }

        private Thread NetworkStreamThread;

        public event TcpClientConnectionClosedHandler OnClientClosed;

        public TcpClientHandler(TcpClient client) {
            Client = client;
        }

        public void ListenToClient() {
            NetworkStreamThread = new Thread(new ThreadStart(this.Process));
            NetworkStreamThread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Process(){
            Console.WriteLine("Started Client Handler.");
            IsAlive = true;
            long bytesLeftInArray;
            while (IsAlive) {

                if (!Client.Connected && OnClientClosed != null) {
                    OnClientClosed(this, "Client Closed.");
                    return;
                }
                try {
                    byte[] bytes = ReadByteChunk(out bytesLeftInArray);
                    //Console.WriteLine(string.Format("bytes:{0}",bytes.Length));
                    //Process(bytes);
                    //if (bytesLeftInArray == 0)
                        Thread.Sleep(200);
                    Console.WriteLine(DateTime.Now.ToLongTimeString());
                } catch (Exception e) {
                    //IsAlive = false;
                    Console.WriteLine(e.InnerException);
                }
            }

        }
        
        /// <summary>
        /// Override this method to have execute when ListenToClient is executed.
        /// </summary>
        /// <param name="bytes"></param>
        protected virtual void Process(byte[] bytes){}

        private int _chunkSize = 8192;

        /// <summary>
        /// Get or Set the chunk byte length to attempt retrieve from the client stream at a time.
        /// </summary>
        public int ChunkSize {
            get { return _chunkSize; }
            set { _chunkSize = value; }
        }

        private byte[] ReadByteChunk(out long leftInStream) {
            var networkStream = Client.GetStream();
            leftInStream = networkStream.Length;
            if (networkStream.Length == 0) {
                return new byte[0];
            }

            var bytes = new byte[ChunkSize];
            var bytesRead = networkStream.Read(bytes, 0, ChunkSize);
            leftInStream -= bytesRead;

            //if (bytesRead == ChunkSize) 
                return bytes;
            //var smallerArray = new byte[bytesRead];
            //System.Buffer.BlockCopy(bytes,0,smallerArray,0,bytesRead);
            //return smallerArray;
        }

        public void CloseConnection(string message) {
            IsAlive = false;
            Thread.Sleep(100);
            if (Client != null && !Client.Connected)
                Client.Close();
            this.OnClientClosed(this, message);

        }

        public void Dispose() {
            CloseConnection("Handler disposed.");
        }
    }
}
