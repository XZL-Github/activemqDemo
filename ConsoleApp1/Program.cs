using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary2;

namespace ConsoleApp1
{
    class Program
    {
        static object lll = new object();
        static void Main(string[] args)
        {
            var sss = BasicActive.BasicActiveIntance();
            sss.UserName = "system";
            sss.Password = "manager";
            sss.BrokerUri = "tcp://192.168.65.133:61616";
            sss.TopicName = "hhhh";
            //var aaaa = ActiveHelper.ActiveHelperInstance(sss);
            //for (int j = 0; j < 1000; j++)
            //{
            //    aaaa.SendMsg("hello-world");
            //}
            List<Task> tasks = new List<Task>();
            Task.Run(() =>
            {

                for (int i = 0; i < 10; i++)
            {
                int j = i;
                    //Task.Run(() =>
                    //{
                        try
                        {
                            //lock (lll)
                            //{


                                int k = j;
                        sss.TopicName = $"Test{k}";
                            var aaaa = ActiveHelper.ActiveHelperInstance(sss);
                            aaaa.ReceiveMsg(msg => { Console.WriteLine($"{((Apache.NMS.ITextMessage)msg).Text}"); });
                            //aaaa.ReceiveMsg(msg => { Console.WriteLine($"{((Apache.NMS.ITextMessage)msg).Text}"); });
                        }
                        //}
                        catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                            //throw;
                        }

            //});

        }
            });
            //Task.Run(()=> {
            //    for (int i = 0; i < 10; i++)
            //    {
            //        //Task.Run(()=> {

            //            try
            //            {
            //                sss.TopicName = $"Test{++i}";
            //                var aaaa = ActiveHelper.ActiveHelperInstance(sss);
            //                for (int j = 0; j < 10; j++)
            //                {
            //                    aaaa.SendMsg($"hello-world");
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex.ToString());

            //                //throw;
            //            }
            //        //});
            //    }

            //});
            //Task.WaitAll(tasks.ToArray());
            //try
            //{

            //}
            //catch (AggregateException ex)
            //{
            //    foreach (var item in ex.InnerExceptions)
            //    {
            //        Console.WriteLine(item.Message);
            //    }
            //}
            //Task.Run(() => {

            //    for (int i = 0; i < 10; i++)
            //    {
            //        try
            //        {
            //            sss.UserName = $"{i}";
            //            var aaaa = ActiveHelper.ActiveHelperInstance(sss);

            //            aaaa.ReceiveMsg(msg => { Console.WriteLine($"{((Apache.NMS.ITextMessage)msg).Text}"); });
            //        }
            //        catch (Exception ex)
            //        {

            //            //throw;
            //        }
            //    }
            //});

            //for (int i = 0; i < 100; i++)
            //{
            //    sss.UserName = $"{i}";
            //    var aaaa = ActiveHelper.ActiveHelperInstance(sss);

            //    Task.Run(() =>
            //    {

            //        try
            //        {

            //            aaaa.ReceiveMsg(msg => { Console.WriteLine($"{((Apache.NMS.ITextMessage)msg).Text}"); });
            //        }
            //        catch (Exception ex)
            //        {

            //            throw;
            //        }
            //    });
            //}

            //for (int i = 0; i < 1000; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        try
            //        {
            //            var aaaa = ActiveHelper.ActiveHelperInstance(sss);
            //            //for (int j = 0; j < 10; j++)
            //            //{
            //                aaaa.SendMsg("hello-world");
            //            //}
            //        }
            //        catch (Exception ex)
            //        {

            //            throw;
            //        }
            //    }
            //    );
            //}

            //Task.Run(() =>
            //{

            //    try
            //    {
            //        //sss.Password = "1111";
            //        var aaaa = ActiveHelper.ActiveHelperInstance(sss);
            //        aaaa.ReceiveMsg(msg => { Console.WriteLine($"{msg.ToString()}"); });
            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }
            //});
            Console.ReadKey();
        }
       
    }
}
