using System;
using System.Configuration;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace Senparc.Weixin.CommonService
{
    public class WeiXinUtilis
    {
        private string _appid = string.Empty;
        private string _appSecret = string.Empty;

        public WeiXinUtilis()
        {
            _appid = ConfigurationManager.AppSettings["AppId"];
            _appSecret = ConfigurationManager.AppSettings["Secret"];
        }

        public WeiXinUtilis(string appId, string appSecret)
        {
            _appid = appId;
            _appSecret = appSecret;
        }

        public SendTemplateMessageResult SendFMYCompleteTemplate(string openId, string first = "", string keyword1 = "", string keyword2 = "", string remark = "", string url = "", string topcolor = "#173177", string templateId = "po732kPdzGfcqO6ZSB_ZfJZ0hL4FCLkvOw1qKqzK944" )
        {
            #region 构造模板数据
            //{{first.DATA}}
            //订单号：{{keyword1.DATA}}
            //完成时间：{{keyword2.DATA}}
            //{{remark.DATA}}
            var data = new
            {
                first = new
                {
                    value = first,
                    color = "#173177"
                },
                keyword1 = new
                {
                    value = keyword1,
                    color = "#173177"
                },
                keyword2 = new
                {
                    value = keyword2,
                    color = "#173177"
                },
                remark = new
                {
                    value = remark,
                    color = "#173177"
                }
            };

            #endregion 构造模板数据

            return SendTemplate(openId, templateId, data, url, topcolor);
        }

        public SendTemplateMessageResult T_Withdraw_Send(string openId, string first = "", string keyword1 = "", string keyword2 = "", string remark = "", string url = "", string topcolor = "#173177", string templateId = "__ufY0fIsCMPtfJ2zEfwhDV7iTQ3NHkxAnhLPy2AVOw")
        {
            #region 构造模板数据
            //{ { first.DATA} }
            //金额：{ { keyword1.DATA} }
            //时间：{ { keyword2.DATA} }
            //{ { remark.DATA} }
            var data = new
            {
                first = new
                {
                    value = first,
                    color = "#173177"
                },
                keyword1 = new
                {
                    value = keyword1,
                    color = "#173177"
                },
                keyword2 = new
                {
                    value = keyword2,
                    color = "#173177"
                },
                remark = new
                {
                    value = remark,
                    color = "#173177"
                }
            };

            #endregion 构造模板数据

            return SendTemplate(openId, templateId, data, url, topcolor);
        }

        public SendTemplateMessageResult RefundNotice(string openId, string first = "", string reason = "", string refund = "", string remark = "", string url = "", string topcolor = "#173177", string templateId = "Myw3XYZYSUJJqOGpAS_rsopJEt727g_kIVMeuM1S91g")
        {
            #region 构造模板数据
            //{ { first.DATA} }
            //退款原因：{ { reason.DATA} }
            //退款金额：{ { refund.DATA} }
            //{ { remark.DATA} }
            var data = new
            {
                first = new
                {
                    value = first,
                    color = "#173177"
                },
                reason = new
                {
                    value = reason,
                    color = "#173177"
                },
                refund = new
                {
                    value = refund,
                    color = "#173177"
                },
                remark = new
                {
                    value = remark,
                    color = "#173177"
                }
            };

            #endregion 构造模板数据

            return SendTemplate(openId, templateId, data, url, topcolor);
        }

        public SendTemplateMessageResult SendCompleteTemplate(string openId, string first = "", string keyword1 = "", string keyword2 = "", string remark = "", string url = "", string topcolor = "#173177", string templateId = "Yl20u-uV8N4VHBnBnL7Ha_bRhZZmbSevGaiRto2WSxc")
        {
            #region 构造模板数据
            //{{first.DATA}}
            //订单号：{{keyword1.DATA}}
            //完成时间：{{keyword2.DATA}}
            //{{remark.DATA}}
            var data = new
            {
                first = new
                {
                    value = first,
                    color = "#173177"
                },
                keyword1 = new
                {
                    value = keyword1,
                    color = "#173177"
                },
                keyword2 = new
                {
                    value = keyword2,
                    color = "#173177"
                },
                remark = new
                {
                    value = remark,
                    color = "#173177"
                }
            };

            #endregion 构造模板数据

            return SendTemplate(openId, templateId, data, url, topcolor);
        }

        public SendTemplateMessageResult SendNoticeTemplate(string openId, string first = "", string keyword1 = "", string keyword2 = "", string keyword3 = "", string keyword4 = "", string remark = "", string url = "", string topcolor = "#173177", string templateId = "7Xlg2Kl7e441KkAqOfRmrM4r1DZ0gTmJeYCqCTuXsYk")
        {
            #region 构造模板数据
            //{{first.DATA}}
            //升级用户：{{keyword1.DATA}}
            //升级计划：{{keyword2.DATA}}
            //升级日期：{{keyword3.DATA}}
            //{{remark.DATA}}
            var data = new
            {
                first = new
                {
                    value = first,
                    color = topcolor
                },
                keyword1 = new
                {
                    value = keyword1,
                    color = topcolor
                },
                keyword2 = new
                {
                    value = keyword2,
                    color = topcolor
                },
                keyword3 = new
                {
                    value = keyword3,
                    color = topcolor
                },
                keyword4 = new
                {
                    value = keyword4,
                    color = topcolor
                },
                remark = new
                {
                    value = remark,
                    color = topcolor
                }
            };

            #endregion 构造模板数据

            return SendTemplate(openId, templateId, data, url, topcolor);
        }

        public SendTemplateMessageResult SendTemplate(string openId, string templateId, object data, string url = "", string topcolor = "#173177")
        {
            try
            {
                string token = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(_appid, _appSecret).access_token;
                SendTemplateMessageResult result = Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(token, openId, templateId, url, data);

                return result;
            }
            catch (Exception ex)
            {
                Commons.WriteLog.CreateLog(ex);
                return null;
            }
        }
    }
}
