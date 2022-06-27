using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Serialization;

namespace Controllers.Helpers
{
    public class XmlResult : ActionResult
    {
        public XmlResult(Object data)
        {
            this.Data = data;
        }
        public Object Data { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Data == null)
            {
                //new EmptyResult().ExecuteResult(context);  // 这句代码可有可无  
                return;
            }
            context.HttpContext.Response.ContentType = "application/xml";
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(Data.GetType());
                xs.Serialize(ms, Data); // 把数据序列化到内存流中  
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    //读取流对象  
                    context.HttpContext.Response.Output.Write(sr.ReadToEnd());
                }
            }
        }
    }
}
