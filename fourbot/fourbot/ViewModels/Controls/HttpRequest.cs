using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fourbot.ViewModels.Controls
{
    public class HttpRequest
    {
        
        public HttpClient httpClient = new HttpClient();
        public string HmacSha256Digest(string message, string secret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            System.Security.Cryptography.HMACSHA256 cryptographer = new System.Security.Cryptography.HMACSHA256(keyBytes);

            byte[] bytes = cryptographer.ComputeHash(messageBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        public async Task<string> BinanceOpenOrders(string api, string secret)
        {
            CrossToastPopUp.Current.ShowToastMessage("aОшибка при получении данных!", Plugin.Toast.Abstractions.ToastLength.Long);

            string timeStamp;
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://testnet.binance.vision/api/v3/time"))
            {
                var response = await httpClient.SendAsync(request);
                string snapshot = await response.Content.ReadAsStringAsync();

                string[] mainSnapshot1 = snapshot.Split(':');
                string[] ss = mainSnapshot1[1].Split('}');
                timeStamp = ss[0];


            }
            string req = "https://fapi.binance.com/fapi/v1/openOrders?recvWindow=5000&timestamp=" + timeStamp + "&signature=" + HmacSha256Digest("recvWindow=5000&timestamp=" + timeStamp, secret);
           

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), req))
            {
                httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", api);
                var response = await httpClient.SendAsync(request);
                string snapshot = await response.Content.ReadAsStringAsync();
                return snapshot;
            }
        }
    }
}
