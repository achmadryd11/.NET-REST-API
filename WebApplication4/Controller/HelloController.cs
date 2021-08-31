using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Text;

namespace WebApplication4.Controller
{
    public class BillController : ApiController
    {
        SerialPort ComPort = new SerialPort("COM9", 9600, Parity.None, 8, StopBits.One);

        internal delegate void SerialDataReceivedEventHandlerDelegate(
                 object sender, SerialDataReceivedEventArgs e);


        delegate void SetTextCallback(string text);
        string InputData = String.Empty;

        const int duitPatokan = 50;
        int uangMasuk, sisa, jumlah, test;
        char value;
        bool continue_ = true;

        public void reset()
        {
            ComPort.Open();
            ComPort.Write("0");
            Task.Delay(2);

            string rst = Encoding.ASCII.GetString(new byte[] { 0x02 });
            ComPort.Write(rst);
            Task.Delay(2);

            ComPort.Write(rst);
        }

        [HttpGet]
        public int GetMoney()
        {
            // InitializeComponent();
            uangMasuk = 0;
            ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived_1);
            return uangMasuk;
        }

        public void accept()
        {
            // 0x02
            
            string acc = Encoding.ASCII.GetString(new byte[] { 0x02 });
            ComPort.Write(acc);
            // return "Money Accepted";
        }

        public void reject()
        {
            // 0x0f
            string rej = Encoding.ASCII.GetString(new byte[] { 0x0F });
            ComPort.Write(rej);
            // return "Money Rejected";
        }

        [HttpGet]
        public string GetAccept()
        {
            accept();
            return "Money Accepted";
        }

        [HttpGet]
        public string GetReject()
        {
            reject();
            return "Money Rejected";
        }

        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            var character = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F' };
            InputData = "";
            InputData = ComPort.ReadExisting();
            foreach (char tempValue in InputData)
            {
                value = tempValue;
            }

            for (int i = 0; i <= 5; i++)
            {
                if (value == character[i])
                {
                    if (value == 'F')
                    {
                        uangMasuk = 100;
                        jumlah += uangMasuk;
                    }
                    else if (value == 'E')
                    {
                        uangMasuk = 50;
                        jumlah += uangMasuk;
                    }
                    else if (value == 'D')
                    {
                        uangMasuk = 20;
                        jumlah += uangMasuk;
                    }
                    else if (value == 'C')
                    {
                        uangMasuk = 10;
                        jumlah += uangMasuk;
                    }
                    else if (value == 'B')
                    {
                        uangMasuk = 5;
                        jumlah += uangMasuk;
                    }
                    else if (value == 'A')
                    {
                        uangMasuk = 2;
                        jumlah += uangMasuk;
                    }

                    /*if (jumlah < duitPatokan)    // kurang
                    {
                        Console.Write("kurang!");
                        accept();
                    }
                    else if (jumlah == duitPatokan)
                    {
                        Console.Write("pas!");
                        accept();
                    }
                    else if (jumlah > duitPatokan)
                    {
                        Console.Write("lebih!");
                        reject();
                        jumlah -= uangMasuk;
                    }*/

                    Console.Write("\t");
                    Console.Write("data: ");
                    Console.Write(value);
                    Console.Write("\t");
                    Console.Write("uang sekarang: ");
                    Console.Write(jumlah);

                    Console.WriteLine();
                    InputData = "";
                }
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            reset();
        }
    }
}
