using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Publisher
{
    public partial class Form1 : Form
    {
        private const string DESTINATION = "Com.FirstSolver";
        private bool IsServerRunning = false;
        private IConnection Connection = null;
        private ISession Session = null;
        private IMessageConsumer Consumer = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsServerRunning)
            {
                if (Consumer != null)
                {
                    Consumer.Close();
                    Consumer = null;
                }

                if (Session != null)
                {
                    Session.Close();
                    Session = null;
                }

                if (Connection != null)
                {
                    Connection.Stop();  // 停止侦听
                    Connection.Close(); // 关闭连接

                    Connection = null;
                }

                IsServerRunning = false;
                this.button2.Text = "Start Listener";
            }
            else
            {
                try
                {
                    // 创建公共消息连接工厂
                    IConnectionFactory Factory = new ConnectionFactory(this.textBox1.Text)
                    {
                        UserName = this.textBox4.Text,
                        Password = this.textBox5.Text
                    };
                    Connection = Factory.CreateConnection();

                    Connection.ClientId = "mq.first.demo.ActiveMQ.Listener";
                    Connection.Start(); // 开启侦听

                    Session = Connection.CreateSession();
                    Consumer = Session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(this.textBox3.Text), "Customer", null, false);
                    Consumer.Listener += OnMessageReceived;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                IsServerRunning = true;
                this.button2.Text = "Stop Listener";
            }
        }

        private void OnMessageReceived(IMessage message)
        {
            if (message is ITextMessage)
            {
                this.BeginInvoke(new Action<string>((msg) => { this.textBox2.AppendText(msg + "\r\n"); }), ((ITextMessage)message).Text);
            }
        }

        private void FormSubscriber_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsServerRunning)
            {
                if (Consumer != null)
                {
                    Consumer.Close();
                    Consumer = null;
                }

                if (Session != null)
                {
                    Session.Close();
                    Session = null;
                }

                if (Connection != null)
                {
                    Connection.Stop();  // 停止侦听
                    Connection.Close(); // 关闭连接

                    Connection = null;
                }

                IsServerRunning = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Clear();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
