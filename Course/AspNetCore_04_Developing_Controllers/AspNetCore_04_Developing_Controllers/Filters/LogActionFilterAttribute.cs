using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_04_Developing_Controllers.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        #region Fields
        private readonly IHostingEnvironment _environment;
        private readonly string _contentRootPath;
        private readonly string _logPath;
        private readonly string _fileName;
        private readonly string _fullPath;
        #endregion

        #region Constructor
        public LogActionFilterAttribute(IHostingEnvironment environment)
        {
            _environment = environment;
            _contentRootPath = _environment.ContentRootPath;
            _logPath = _contentRootPath + "\\LogFile\\";
            _fileName = $"log {DateTime.Now.ToString("MM-dd-yyyy-H-mm")}.txt";
            _fullPath = _logPath + _fileName;
        }
        #endregion

        #region Methods
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.RouteValues["action"];
            var controllerName = filterContext.ActionDescriptor.RouteValues["controller"];

            using (var fs = new FileStream(_fullPath, FileMode.Append))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"The action {actionName} in {controllerName} controller finished, event fired: OnActionExecuted");
                }
            }
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Directory.CreateDirectory(_logPath);
            var actionName = filterContext.ActionDescriptor.RouteValues["action"];
            var controllerName = filterContext.ActionDescriptor.RouteValues["controller"];

            using (var fs = new FileStream(_fullPath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"The action {actionName} in {controllerName} controller started, event fired: OnActionExecuting");
                }
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.RouteValues["action"];
            var controllerName = filterContext.ActionDescriptor.RouteValues["controller"];

            var result = (ViewResult)filterContext.Result;

            using (var fs = new FileStream(_fullPath, FileMode.Append))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"The action {actionName} in {controllerName} controller has the following viewData : {result.ViewData.Values.FirstOrDefault()}, event fired: OnResultExecuted");
                }
            }
            base.OnResultExecuted(filterContext);
        }
        #endregion
    }
}
