using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace MVC.Controllers.Util
{
    public static class ToastrUtil
    {
        public const string ERROR = "error";
        public const string INFO = "info";
        public const string SUCCESS = "success";
        public const string WARNING = "warning";

        public static void Toastr(Controller controller, string title, string message, string type)
        {
            var toastrs = new List<Toastr>();
            if (controller.TempData["Toastrs"] != null)
            {
                toastrs = JsonSerializer.Deserialize<List<Toastr>>(controller.TempData["Toastrs"].ToString());
            }

            toastrs.Add(new Toastr { Title = title, Message = message, Type = type });

            controller.TempData["Toastrs"] = JsonSerializer.Serialize(toastrs);
        }

        public static void ToastrError(Controller controller, string message)
        {
            Toastr(controller, "Error", message, ERROR);
        }

        public static void ToastrInfo(Controller controller, string message)
        {
            Toastr(controller, "Info", message, INFO);
        }

        public static void ToastrSuccess(Controller controller, string message)
        {
            Toastr(controller, "Success", message, SUCCESS);
        }

        public static void ToastrWarning(Controller controller, string message)
        {
            Toastr(controller, "Warning", message, WARNING);
        }
    }
}
