using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberManager.Manager;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MemberManager.Controllers
{
    public class SendTypesController : Controller
    {
        private readonly SendTypesManager sendTypesManager;

        public SendTypesController(SendTypesManager _sendTypesManager)
        {
            sendTypesManager = _sendTypesManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public string Save(SendTypes sendTypes)
        {
            Dictionary<String, Object> valueObject = new Dictionary<string, object>();

            try
            {

                valueObject.Add("success", true);
                valueObject.Add("message", "儲存成功");
            }
            catch (Exception ex)
            {
                valueObject.Add("success", false);
                valueObject.Add("message", "儲存失敗，錯誤訊息:" + ex.Message);
            }

            return JsonConvert.SerializeObject(valueObject);
        }

        public IActionResult Removed(Int64 id)
        {
            return View();
        }
    }
}
