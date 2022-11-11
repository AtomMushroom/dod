using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Windows.Forms;
using Client.Chat;

namespace Client
{
    public partial class Form1 : Form, IServiceCallback
    {
        bool isConnected = false;
        ServiceClient client;
        int ID;

        public Form1()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceClient(new InstanceContext(this));
                
                ID = client.Connect(textBox1.Text);
                textBox1.Enabled = false;
                isConnected = !isConnected;
                button1.Text = "Отключиться";
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                textBox1.Enabled = true;
                isConnected = !isConnected;
                button1.Text = "Подключиться";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MessageCallback(string message)
        {
            listBox1.Items.Add(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectUser();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(client != null)
                {
                    client.SendMessage(textBox2.Text, ID);
                    textBox2.Text = String.Empty;
                }
            }
        }
    }
}
