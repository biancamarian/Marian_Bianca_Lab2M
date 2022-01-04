using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess = Marian_Bianca_Lab2.Data;
using ModelAccess = Marian_Bianca_Lab2.Models;

namespace GrpcCustomersService
{
    public class GrpcCRUDService : CustomerService.CustomerServiceBase
    {
        private DataAccess.LibraryContext db = null;
        public GrpcCRUDService(DataAccess.LibraryContext db)
        {
            this.db = db;
        }
       public override Task<CustomerList> GetAll(Empty empty, ServerCallContext context)
        {
            CustomerList pl = new CustomerList();
            var query = from cust in db.Customers
                        select new Customer()
                        {
                            CustomerId = cust.CustomerID,
                            Name = cust.Name,
                            Adress = cust.Adress
                        };
            pl.Item.AddRange(query.ToArray());
            return Task.FromResult(pl);
        }

        public override Task<Customer> Get(CustomerId requestData, ServerCallContext context)
        {
            var data = db.Customers.Find(requestData.Id);
            Customer cust = new Customer()
            {
                CustomerId = data.CustomerID,
                Name = data.Name,
                Adress = data.Adress
            };
            return Task.FromResult(cust);
        }


        public override Task<Empty> Insert(Customer requestData, ServerCallContext context)
        {
            db.Customers.Add(new ModelAccess.Customer
            {
                CustomerID = requestData.CustomerId,
                Name = requestData.Name,
                Adress = requestData.Adress,
                BirthDate = DateTime.Parse(requestData.Birthdate)
            });
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Update(Customer requestData, ServerCallContext context)
        {
            db.Customers.Update(new ModelAccess.Customer()
            {
                CustomerID = requestData.CustomerId,
                Name = requestData.Name,
                Adress = requestData.Adress,
                BirthDate = DateTime.Parse(requestData.Birthdate)
            });

            db.SaveChanges();

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Delete(CustomerId requestData, ServerCallContext context)
        {
            var data = db.Customers.Find(requestData.Id);
            db.Customers.Remove(data);

            db.SaveChanges();
            return Task.FromResult(new Empty());
        }




    }
}
