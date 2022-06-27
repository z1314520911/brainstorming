using System;
using System.Runtime.Serialization;

namespace Commons
{
    /// <summary>
    /// 操作结果类
    /// </summary>
    [DataContract]
    public class Result
    {
        [IgnoreDataMember]
        private string _detail = string.Empty;

        [IgnoreDataMember]
        private DateTime _oprTime = DateTime.Now;

        /// <summary>
        /// 操作结果返回码
        /// 默认代码：
        /// < 0、异常；
        /// 0、操作失败；
        /// > 0、操作成功；
        /// 可根据实际业务进行调整
        /// [ScriptIgnore],使用JavaScriptSerializer序列化时不序列化此字段
        /// [IgnoreDataMember],使用DataContractJsonSerializer序列化时不序列化此字段
        /// [JsonIgnore],使用JsonConvert序列化时不序列化此字段  
        /// </summary>
        [DataMember(Name = "Code")]
        public int Code
        {
            get;
            set;
        }

        /// <summary>
        /// 操作结果信息
        /// </summary>
        [DataMember(Name = "Message")]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// 操作结果详细说明
        /// </summary>
        [DataMember(Name = "Detail")]
        public string Detail
        {
            get
            {
                return _detail;
            }

            set
            {
                _detail = (string.IsNullOrEmpty(value) ? string.Empty : value);
            }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OprTime
        {
            get
            {
                return _oprTime;
            }

            set
            {
                _oprTime = ((value == null) ? DateTime.Now : value);
            }
        }
        public Result()
        {
            this.Detail = string.Empty;
            this._oprTime = DateTime.Now;
        }
        public Result(int code, string message = "", string detail = "")
        {
            this.Code = code;
            this.Message = message;
            this.Detail = detail;
            this._oprTime = DateTime.Now;
        }
    }
}
