using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Komoduino
{
    class Program
    {
        // The selected port.
        SerialPort port;

        // Use 9600 baud

        static string cost;
        static string content;

        static string portNumber;

        static void Main(string[] args)
        {
            // Get com port before entering while loop.
            portNumber = "COM" + getPort();

            Thread beta = new Thread(new ThreadStart(Beta));
            beta.Start();
            
        }

        private static void Beta()
        {
            // Get price from online
            using (WebClient client = new WebClient())
            {
                string url = "https://coinmarketcap.com/currencies/komodo/";
                content = client.DownloadString(url);
            }
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            var ost = htmlDoc.GetElementbyId("quote_price");
            cost = ost.InnerText;
            Console.WriteLine(cost);
            Console.ReadLine();
            // Send to arduino

            // Wait 5 min
            // Repeat
        }

        private static int getPort()
        {
            // List ports
            if (SerialPort.GetPortNames().Count() >= 0)
            {
                foreach (string p in SerialPort.GetPortNames())
                {
                    Console.WriteLine(p);
                }
            }
            else
            {
                Console.WriteLine("No Ports available, press any key to exit.");
                Console.ReadLine();
            }

            Console.WriteLine("Port: ");
            int selected = int.Parse(Console.ReadLine());
            Console.WriteLine("Set active port to COM" + selected);
            return selected;
        }
    }
}
