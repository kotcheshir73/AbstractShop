using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AbstractShopView
{
    public static class TSPClient<T>
    {
        public static string IPAddress { get { return ConfigurationManager.AppSettings["IPAddress"]; } }

        public static int IPPort { get { return Convert.ToInt32(ConfigurationManager.AppSettings["IPPort"]); } }

        public static ResponseModel<T> SendRequest(RequestModel model)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(IPAddress, IPPort);

                NetworkStream stream = tcpClient.GetStream();
                try
                {
                    var jsonSerRequest = new DataContractJsonSerializer(typeof(RequestModel));

                    jsonSerRequest.WriteObject(stream, model);

                    Byte[] bytes = new Byte[1024];
                    string bytesAsString = string.Empty;

                    do
                    {
                        stream.Read(bytes, 0, bytes.Length);
                        bytesAsString += Encoding.UTF8.GetString(bytes);
                    }
                    while (stream.DataAvailable);

                    var response = JsonConvert.DeserializeObject<ResponseModel<T>>(bytesAsString);

                    return response;
                }
                finally
                {
                    stream.Close();
                }
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}
