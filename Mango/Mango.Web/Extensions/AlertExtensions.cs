using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Extensions
{
    public static class AlertExtensions
    {
        public static void Success(this Controller controller, string message)
        {
            controller.TempData["success"] = message;
        }

        public static void Error(this Controller controller, string message)
        {
            controller.TempData["error"] = message;
        }

        public static void Info(this Controller controller, string message)
        {
            controller.TempData["info"] = message;
        }

        public static void Warning(this Controller controller, string message)
        {
            controller.TempData["warning"] = message;
        }
    }
}
