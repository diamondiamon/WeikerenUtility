using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Weikeren.PaymentTest.Utility;
using Weikeren.Utility.Payment.Enum;
using Weikeren.Utility.Payment.PayProcessor;

namespace Weikeren.PaymentTest.Controllers
{
    public class PayController : Controller
    {
        private IPaymentProcessor _paymentProcessor;

        public PayController()
        {
            var payWayString = System.Configuration.ConfigurationManager.AppSettings["PayWay"];
            PayWayEnum payEnum = (PayWayEnum)Enum.Parse(typeof(PayWayEnum), payWayString);
            _paymentProcessor = PayProcessorFactory.CreatePayProcessor(PayWayEnum.Alipay);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitPay(FormCollection form)
        {
            //decimal Amount = decimal.Parse(form["money"]);

            //PayRequestModelCreator requestModelCreator = new PayRequestModelCreator(_paymentProcessor.PayWay);
            //var requestModel = requestModelCreator.CreatePayRequestModel(Request, orderno, Amount, bankno, desc);
            //_paymentProcessor.PostPayment(requestModel);

            return null;
        }

    }
}
