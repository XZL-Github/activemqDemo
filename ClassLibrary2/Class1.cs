using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace ClassLibrary2
{
    public class ActiveHelper
    {
        private ActiveHelper()
        {
            Console.WriteLine("初始化");
        }
        private static BasicActive _BasicActive {
            get;set;
        }
        private static volatile ActiveHelper _ActiveHelper = null;
        private static readonly object _ActiveHelper_Lock = new object();


        public static ActiveHelper ActiveHelperInstance(BasicActive basicActive)
        {
            _BasicActive = basicActive;
            if (_ActiveHelper == null)
            {
                lock (_ActiveHelper_Lock)
            {
                //_BasicActive = basicActive;
                if (_ActiveHelper == null)
                {
                    _ActiveHelper = new ActiveHelper();
                }
            }
            }

            return _ActiveHelper;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        public  void ReceiveMsg(MessageListener OnMessageReceived)
        {
                //_BasicActive = basicActive;
                Console.WriteLine(_BasicActive.TopicName);
                // 创建公共消息连接工厂
                IConnectionFactory Factory = new ConnectionFactory(_BasicActive.BrokerUri)
                {
                    UserName = _BasicActive.UserName,
                    Password = _BasicActive.Password
                };
                var Connection = Factory.CreateConnection();

                Connection.ClientId = $"{_BasicActive.TopicName}.ActiveMQ.Listener";
                Connection.Start(); // 开启侦听

                var Session = Connection.CreateSession();
                var Consumer = Session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(_BasicActive.TopicName), "Customer", null, false);
                    Consumer.Listener += OnMessageReceived;
                
            }
        }

    public class BasicActive
    {
        private BasicActive(){
            }
        public static  BasicActive BasicActiveIntance()
        {
            lock (_BasicActive_Lock)
            {

            }
            return new BasicActive();
        }

        private static readonly object _BasicActive_Lock = new object();
        /// <summary>
        /// 用户名
        /// </summary>
        public  string UserName { get; set; } = "admin";
        /// <summary>
        /// 密码
        /// </summary>
        public  string Password { get; set; } = "admin";
        /// <summary>
        /// 地址
        /// </summary>
        public  string BrokerUri { get; set; } = "tcp://127.0.1:61616";
        /// <summary>
        /// 主题名
        /// </summary>
        //public  string TopicName {
        //    get => TopicName;
        //    set => TopicName = value ??throw new ArgumentNullException(nameof(value), "TopicName cannot be null");
        //}
        public string TopicName { get; set; }


    }
}
