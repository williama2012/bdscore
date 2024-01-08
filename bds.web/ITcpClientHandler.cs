using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace bds.web {
    interface ITcpClientHandler : IDisposable{
        TcpClient Client { get; set;}
        int ChunkSize { get; set;}
        void ListenToClient();
        void CloseConnection(string message);
    }
}
