using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCore.MVC.RequestLifecycle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.MVC.RequestLifecycle.ModelBinding
{
    public class CSVModelBinder : IModelBinder
    {
        private const string requestKey = "csvcontent";

        //Model Validation can be added, because now it assumes consistent csv data
        //But this is out of the scope of the course

        public Task BindModelAsync(ModelBindingContext context)
        {
            var rawCsv = context.ValueProvider.GetValue(requestKey).ToString();
            //1.Separate by new line
            var orderListCsv = rawCsv.Split(Environment.NewLine.ToCharArray());

            var ordersList = new List<Order>();

            foreach (var csvOrder in orderListCsv)
            {
                //2. Separate by comma
                var csvOrderValues = csvOrder.Split(",");
                var order = new Order()
                {
                    ProductName = csvOrderValues[0],
                    Count = csvOrderValues[1],
                    Description = csvOrderValues[2]
                };

                ordersList.Add(order);
            }

            context.Result = ModelBindingResult.Success(ordersList);
            return Task.CompletedTask;
        }
    }
}
