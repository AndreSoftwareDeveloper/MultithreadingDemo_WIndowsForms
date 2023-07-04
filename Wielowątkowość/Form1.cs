using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace Wielowątkowość
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }  

        private int CalculatePrimeNumbers()
        {
            String currentPrimeNumber = label1.Text;
            for (int i = 1; true; i++)
            {
                if (i > Convert.ToInt32(currentPrimeNumber))
                {
                    if (IsPrime(i))
                    {
                        label1.Text = i.ToString();
                        Thread.Sleep(1000);
                    }
                }                                                           
            }
        }

        private void CalculatePowersOfTwo()
        {
            int currentPower = Convert.ToInt32(label2.Text);
            for (int i = 0; true; i++)
            {
                int newPower = (int)Math.Pow(2, i);
                if (newPower > currentPower)
                {
                    label2.Text = newPower.ToString();
                    Thread.Sleep(1000);                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(t =>
            {
                label1.Invoke(new Action(delegate 
                {
                    label1.Text = "1";
                    var timer = new System.Threading.Timer(_ => CalculatePrimeNumbers(), null, 0, 1000);
                }));               
            });
            thread1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(t =>
            {
                label1.Invoke(new Action(delegate
                {
                    label2.Text = "0";
                    var timer = new System.Threading.Timer(_ => CalculatePowersOfTwo(), null, 0, 1000);
                }));
            });
            thread2.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread3 = new Thread(t =>
            {
                label3.Invoke(new Action(delegate
                {
                    label3.Text = "0";
                    var timer = new System.Threading.Timer(_ => ShowUnixTime(), null, 0, 1000);
                }));
            });
            thread3.Start();
        }

        private void ShowUnixTime()
        {
            while (true)
            {
                String unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                label3.Text = unixTimestamp;
            }
        }
    }    
}
