using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace bds.web.smtp {
    public class SmtpHandler : TcpClientHandler {

        public SmtpHandler(TcpClient client) : base(client){}

        protected override void Process(byte[] bytes) {
            Console.WriteLine(bytes.Length);
        }
    }
}
