using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_lab_Val
{
    public partial class Form1 : Form
    {
        private BigInteger usualN;
        private BigInteger usualF;
        private BigInteger usualE;
        private BigInteger usualD;
        private int usualP;
        private int usualQ;

        private BigInteger kemN;
        private BigInteger kemF;
        private BigInteger kemE;
        private BigInteger kemD;
        private int kemP;
        private int kemQ;
        public Form1()
        {
            InitializeComponent();
        }
        private int getBigSimple(int maxVal)
        {
            for(int i = maxVal-1; i > 1; i--)
            {
                if (isSimple(i))
                {
                    return i;
                }
            }
            return 1;
        }
        private static bool isSimple(int N)
        {
            //чтоб убедится простое число или нет достаточно проверить не делитсья ли 
            //число на числа до его половины
            for (int i = 2; i < (int)(N / 2); i++)
            {
                if (N % i == 0)
                    return false;
            }
            return true;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0)
                {
                    usualP = Convert.ToInt32(textBox1.Text.ToString());
                    usualQ = Convert.ToInt32(textBox2.Text.ToString());
                    usualN = usualP * usualQ;
                    usualF = (usualP - 1) * (usualQ - 1);
                    usualE = getBigSimple((int)(usualF - 1));
                    usualD = modInverse(usualE, usualF);

                    textBox3.Text = usualE.ToString();
                    textBox4.Text = usualD.ToString();
                    textBox5.Text = usualN.ToString();



                    //генерируем параметры для инкапсуляции ключа
                    Random rn = new Random();
                    kemP = getBigSimple(rn.Next(100,200));
                    kemQ = getBigSimple(rn.Next(100,200));
                    Console.WriteLine(kemP + " " + kemQ);
                    kemN = kemP * kemQ;
                    kemF = (kemP - 1) * (kemQ - 1);
                    kemE = getBigSimple((int)(kemF - 1));
                    kemD = modInverse(kemE, kemF);
                }
            } catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string inString = textBox6.Text;
                string outString = "";
                int eKey = Convert.ToInt32(textBox9.Text.ToString());
                int nKey = Convert.ToInt32(textBox10.Text.ToString());
                string keyString = kemD.ToString() + "|" + kemN.ToString();

                for (int i = 0; i < keyString.Length; i++)
                {
                    outString += BigInteger.ModPow(keyString[i], eKey, nKey).ToString();
                    if (i != keyString.Length - 1)
                    {
                        outString += " ";
                    }
                }
                textBox7.Text = outString;

                outString = "";
                for (int i = 0; i < inString.Length; i++)
                {
                    outString += BigInteger.ModPow(inString[i], kemE, kemN).ToString();
                    if (i != inString.Length - 1)
                    {
                        outString += " ";
                    }
                }
                textBox8.Text = outString;


            } catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
        BigInteger modInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string kemString = textBox12.Text.ToString();
                string demString = textBox15.Text.ToString();

                int dKey = Convert.ToInt32(textBox13.Text.ToString());
                int nKey = Convert.ToInt32(textBox14.Text.ToString());

                string[] kemArray = kemString.Split();
                string outString = "";
                for (int i = 0; i < kemArray.Length; i++)
                {
                    outString += (char)BigInteger.ModPow(Convert.ToInt32(kemArray[i]), dKey, nKey);
                }
                kemArray = outString.Split('|');
                if(kemArray.Length == 2)
                {
                    string[] demArray = demString.Split();
                    outString = "";
                    for (int i = 0; i < demArray.Length; i++)
                    {
                        outString += (char)BigInteger.ModPow(Convert.ToInt32(demArray[i]), Convert.ToInt32(kemArray[0]), Convert.ToInt32(kemArray[1]));
                    }
                    textBox11.Text = outString;
                } else
                {
                    MessageBox.Show("Невозможно расшифровать данные");
                }
            } catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
    }
}
