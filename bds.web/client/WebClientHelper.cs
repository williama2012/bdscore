using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace bds.web {
    public class WebClientHelper {

        public static void DownloadFile(string UriSource, string UriDestination) {
            using (var client = new WebClient()) {
                client.DownloadFile(UriSource, UriDestination);
            }
        }
    }
}
