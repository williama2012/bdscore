using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace bds.core.data {
    public class Security {

        public Security() { }

        public NetworkCredential CreateNetworkCredential(string userName, string passWord) {
            
            NetworkCredential credential = new NetworkCredential(userName,passWord);
            userName = null;
            passWord = null;
            return credential;
        }

        public byte[] CreateMD5(object obj) {
            var md5Provider = new MD5CryptoServiceProvider();
            var serializedObject = SerializeObject(obj);
            
            return md5Provider.ComputeHash(serializedObject);
        }

        public byte[] SerializeObject(object obj) {
            IFormatter formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        public object DeserializeObject(byte[] obj) {
            IFormatter formatter = new BinaryFormatter();
            var stream = new MemoryStream(obj);
            return formatter.Deserialize(stream);
        }

        public string ProtectPassword(string password, bool currentUser){
            byte[] bytes = Encoding.Unicode.GetBytes(password.ToString());
            byte[] protectedBytes = ProtectedData.Protect(bytes, null, currentUser ? DataProtectionScope.CurrentUser : DataProtectionScope.LocalMachine);

            var protectedPassword = Convert.ToBase64String(protectedBytes);
            //var protectedPassword = CreateSecureString(Convert.ToBase64String(protectedBytes));
            bytes = null;
            protectedBytes = null;
            
            return protectedPassword;
        }

        public string UnprotectPassword(string password, bool currentUser) {
            byte[] bytes = Convert.FromBase64String(password.ToString());
            byte[] unprotectedBytes = ProtectedData.Unprotect(bytes, null, currentUser ? DataProtectionScope.CurrentUser : DataProtectionScope.LocalMachine);

            var unprotectedPassword = Encoding.Unicode.GetString(unprotectedBytes);
            //var unprotectedPassword = CreateSecureString(Encoding.Unicode.GetString(unprotectedBytes));
            bytes = null;
            unprotectedBytes = null;

            return unprotectedPassword;
        }


        private SecureString CreateSecureString(string str) {
            SecureString secureString = new SecureString();
            foreach (char c in str.Cast<char>()) {
                secureString.AppendChar(c);
            }
            str = null;
            return secureString;
        }


    }
}
