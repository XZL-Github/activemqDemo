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

namespace Subscriber
{
    public partial class Form1 : Form
    {
        private const string DESTINATION = "Com.FirstSolver";

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建公共消息连接工厂
                IConnectionFactory Factory = new ConnectionFactory(this.textBox1.Text) {
                    UserName = this.textBox4.Text,
                    Password=this.textBox5.Text
                };
                using (IConnection Connection = Factory.CreateConnection())
                {
                    using (ISession Session = Connection.CreateSession())
                    {
                        using (IMessageProducer Producer = Session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(this.textBox3.Text)))
                        {
                            ITextMessage Message = Producer.CreateTextMessage();
                            Message.Text = this.textBox2.Text;
                            //Message.Properties.SetString("filter", "demo");
                            Producer.Send(Message, MsgDeliveryMode.Persistent, MsgPriority.Normal, TimeSpan.MinValue);

                            MessageBox.Show("消息发送成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.textBox2.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Clear();
        }
    }
}
