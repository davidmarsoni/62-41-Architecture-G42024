using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.Util
{
    public class ToastrUtil
    {
        public static String ERROR = "error";
        public static String INFO = "info";
        public static String SUCCESS = "success";
        public static String WARNING = "warning";

        public static void Toastr(Controller controller, String title, String message, String type)
        {
            controller.TempData["ToastrTitle"] = title;
            controller.TempData["ToastrMessage"] = message;
            controller.TempData["ToastrType"] = type;
        }

        public static void ToastrError(Controller controller, String message)
        {
            Toastr(controller, "Error", message, ERROR);
        }

        public static void ToastrInfo(Controller controller, String message)
        {
            Toastr(controller, "Info", message, INFO);
        }

        public static void ToastrSuccess(Controller controller, String message)
        {
            Toastr(controller, "Success", message, SUCCESS);
        }

        public static void ToastrWarning(Controller controller, String message)
        {
            Toastr(controller, "Warning", message, WARNING);
        }
    }
}
