using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Catalog.Entities;

namespace Catalog.Repository
{
    public class dataBillRepository
    {
        SerialPort ComPort = new SerialPort("COM9", 9600, Parity.None, StopBits.One);
        internal delegate void SerialDataReceivedEventHandlerDelegate(
            object sender, SerialDataReceivedEventArgs e);

        delegate void setTextCallBack(string text);
        string inputData = String.Empty;
        const int moneySetPoint = 50;
        char charValue, tempChar;
        int entryMoney, sum, remains;
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

        private readonly List<Money> moneys = new()
        {
            InitializationComponents();
            ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived_1);
        }

        private readonly List<Accept> accept = new()
        {
            string acc = Encoding.ASCII.GetString(new byte[] {0x02});
            ComPort.Write(acc);
        }
        
        private readonly List<Reject> reject = new()
        {
            string rej = Encoding.ASCII.GetString(new byte[] {0X0F});
        } 

        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            var callChar = new List<char>{'A', 'B', 'C', 'D', 'E', 'F'};
            inputData = "";
            inputData = ComPort.ReadExisting();
            foreach (char tempChar in InputData)
            {
                charValue = tempChar;
            }

            for (int i = 0; i <= 4; i++)
            {
                if (charValue == callChar[i])
                {
                    if (charValue == 'F')
                    {
                        entryMoney = 100;
                        sum += entryMoney;
                    }
                    else if (charValue == 'E')
                    {
                        entryMoney = 50;
                        sum += entryMoney;
                    }
                    else if (charValue == 'D')
                    {
                        entryMoney = 20;
                        sum += entryMoney;
                    }
                    else if (charValue == 'C')
                    {
                        entryMoney = 10;
                        sum += entryMoney;
                    }
                    else if (charValue == 'B')
                    {
                        entryMoney = 5;
                        sum += entryMoney;
                    }
                    else if (charValue = 'A')
                    {
                        entryMoney = 2;
                        sum += entryMoney;
                    }
                    if (sum < moneySetPoint)
                    {
                        Console.Write("Pas!");
                        accept();
                    }
                    else if (sum > moneySetPoint)
                    {
                        Console.Write("Lebih");
                        reject();
                    }

                    Console.Write("\t");
                    Console.Write("Data: ");
                    Console.write(charValue);
                    Console.Write("\t");
                    Console.Write("Current Money: ");
                    Console.Write(sum);

                    Console.WriteLine();
                    inputData = "";

                }
            }

        }

        private void form1_load(object sender, EventArgs e)
        {
            reset();
        }
        
        public IEnumerable<Money> GetMoney()
        {
            return moneys;
        }

        // public IEnumerable<Accept> GetItems()
        // {
        //     return accept;
        // }

        // public IEnumerable<Reject> GetItems()
        // {
        //     return reject;
        // }

        public Money GetMoney(int moneyId)
        {
            return moneys.Where(money => money.Id == id).SingleOrDefault();
        }
    }
}