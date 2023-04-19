using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HrApi
{
    public class CancellationTokenExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => throw new NotImplementedException();

        // This will run AFTER the controller returns a response.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception is TaskCanceledException)
            {
                Console.WriteLine("Got that cancellation");
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }

        // This will run BEFORE the controller action is called.
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Nothing to see here. Carry on.
        }
    }
}
