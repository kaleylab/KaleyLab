using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using KaleyLab.Data;
using KaleyLab.Data.EntityFramework;
using KaleyLab.Data.Sample;
using KaleyLab.Data.Sample.Models;
using KaleyLab.Data.Sample.Repositories;

namespace KaleyLab.Data.SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IRepositoryContext  context = new EntityFrameworkRepositoryContext<SampleEFDbContext>() )
            {
                //Order Repository
                OrderEFRepository orderRepository = new OrderEFRepository(context);

                //Insert
                /*
                Order orderEntity = new Order()
                {
                    CreateUser = "Codi",
                    OrderNo = "OR-1000",
                    CreateDate = DateTime.Now,
                    Comment = "TEST"
                };
                orderRepository.Add(orderEntity);
                context.Commit();
                */

                //Single Query
                Guid orderId = Guid.Parse("c5b83f00-bf04-4c05-bc3a-ca0a06c04bae");
                Order singleOrderEntity = orderRepository.Get(orderId);

                //Apply Current Values
                singleOrderEntity.Comment = "TEST " + DateTime.Now.ToString();
                orderRepository.ApplyCurrentValues(singleOrderEntity);
                context.Commit();


            }

            //Console.WriteLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
            Console.WriteLine("Yes,We did it!");
            Console.ReadKey();
        }
    }
}
