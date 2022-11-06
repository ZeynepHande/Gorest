using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using static System.Net.Http.HttpClient;

namespace Gorest1
{
    class Program
    {
        static void Main(string[] args)
        {
            // string connection = "https://gorest.co.in/public/v2/users";
            string baglanti = "https://gorest.co.in/public/v2/users";

            var data = HttpHelper.GetDataFromApi<List<Urun>>(baglanti).Result;
           // Console.Read();
            WebRequest istek = HttpWebRequest.Create(baglanti);
            WebResponse cevap;

            cevap = istek.GetResponse();

            StreamReader donenBilgi = new StreamReader(cevap.GetResponseStream());

            string bilgiAl = donenBilgi.ReadToEnd().Trim();

            List<Urun> urunBilgisi = JsonConvert.DeserializeObject<List<Urun>>(bilgiAl);
            // List<Urun> urunBilgisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Urun>(bilgiAl);
            ////JsonConvert.DeserializeObject
            // //XDocument gorest = XDocument.Load(connection);

            //var id = urunBilgisi.Descendants("id").ElementAt(0);
            // for (int i = 0; i < items[0].)
           // var items = JsonConvert.DeserializeObject<List<Urun>>(bilgiAl);
            Console.WriteLine("List1:");
            foreach (var item in urunBilgisi)
            {
                
                if (item.status == "active")
                {
                    Console.WriteLine(item.id);
                }
            }
            Console.WriteLine("List2:");
            foreach (var item in urunBilgisi)
            {

                if (item.status == "inactive")
                {
                    Console.WriteLine(item.id);
                }
            }

            //Console.WriteLine(bilgiAl.ToString());

            Console.WriteLine("miktar" + ":" + data.Count.ToString());
            Console.ReadLine();

            //List<Urun> urun = bilgiAl.Select(c => new Urun
            //{
            //    id = c.ToString()
            //    if (id )
            //}).ToList();

            
        }
        public class HttpHelper
        {
            public static async Task<T> GetDataFromApi<T>(string url)
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    var result = await client.GetAsync(url);
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                    return resultContent;
                }
            }
        }

    }
}
