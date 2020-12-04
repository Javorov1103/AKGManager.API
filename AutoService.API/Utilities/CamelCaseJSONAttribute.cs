using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.API.Utilities
{
    //using System;

    //using Newtonsoft.Json.Serialization;

    //public class CamelCaseJsonAttribute : Attribute, IControllerConfiguration
    //{
    //    public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
    //    {
    //        var formatter = controllerSettings.Formatters.JsonFormatter;

    //        controllerSettings.Formatters.Remove(formatter);

    //        formatter = new JsonMediaTypeFormatter
    //        {
    //            SerializerSettings =
    //            {
    //                ContractResolver = new CamelCasePropertyNamesContractResolver()
    //            }
    //        };

    //        controllerSettings.Formatters.Insert(0, formatter);
    //    }
    //}

    //public class CamelCaseJsonFormatter : TextOutputFormatter
    //{
    //    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

}
