using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace spinlock_test
{
    public partial class Form1 : Form
    {
        UI_Control uic = new UI_Control();
        ManualResetEvent mre = new ManualResetEvent(false);
        static int testnum = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(func_1);
            Thread t2 = new Thread(func_2);
            t1.Start(5);
            t2.Start(7);

        }

        private void func_1(object a)
        {
            for(int i = 0; i < 200; i++)
            {
                int temp = ((int)a * i);
                uic.writetextbox(textBox1, temp.ToString());
                testnum = temp;
                uic.writetextbox(textBox4, (testnum - temp).ToString());
                //Thread.Sleep(500);
            }
            
        }

        private void func_2(object a)
        {
            for (int i = 0; i < 200; i++)
            {
                int temp = ((int)a * i);
                uic.writetextbox(textBox2, temp.ToString());
                testnum = temp;
                uic.writetextbox(textBox5, (testnum - temp).ToString());
                //Thread.Sleep(500);
                
            }
        }
    }

    class UI_Control
    {
        public delegate void textboxhandler(TextBox tb, string text);
        public void writetextbox(TextBox tb, string text)
        {
            if (tb.InvokeRequired)
            {
                textboxhandler th = new textboxhandler(writetextbox);
                tb.Invoke(th, tb, text);
            }
            else
            {
                tb.Text = text;
            }
        }
    }
}
