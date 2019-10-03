using System;
using System.Collections.Generic;
using StackExchange.Redis;
using System.Threading;
using System.Linq;
using Newtonsoft.Json;


class TagInfo
{
    public int AntennaNo { get; set; }
    public string Epc { get; set; }
    public double Doppler { get; set; }
    public double Phase { get; set; }
    public double Rssi { get; set; }
}
namespace RedisSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // allow admin 
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost,allowAdmin=true");

            // get db
            IDatabase cache = redis.GetDatabase();

            // initialize db
            var server = redis.GetServer("localhost", 6379);
            server.FlushAllDatabases();

            var range = Enumerable.Range(1, 10);
            foreach (var index in range)
            {
                // read time
                var readTime = DateTime.Now;

                // moc
                List<TagInfo> tagInfoList = new List<TagInfo>{
                    new TagInfo{ AntennaNo = 1, Epc = "1234", Doppler = 0.0, Phase = 0.0, Rssi = -70},
                    new TagInfo{ AntennaNo = 2, Epc = "4567", Doppler = 0.0, Phase = 0.0, Rssi = -70},
                    new TagInfo{ AntennaNo = 3, Epc = "8910", Doppler = 0.0, Phase = 0.0, Rssi = -70},
                };

                // convert tag info json to string
                string jsonString = JsonConvert.SerializeObject(tagInfoList);
                Console.WriteLine(jsonString);

                // (KEY, VALUE) = (DateTime, tagInfoList)
                cache.StringSet($"{readTime.ToString("yyyy-MM-dd-HH:mm:ss.fff")}", $"{jsonString}");
                Thread.Sleep(100);
            }

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
