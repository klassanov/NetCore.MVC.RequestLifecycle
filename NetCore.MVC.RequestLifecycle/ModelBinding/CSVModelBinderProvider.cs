using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCore.MVC.RequestLifecycle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.MVC.RequestLifecycle.ModelBinding
{
    public class CSVModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(List<Order>))
            {
                return new CSVModelBinder();
            }

            //Means that mvc should keep searching through the other providers
            return null;
        }
    }
}
