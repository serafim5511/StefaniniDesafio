using Domain.Helpers;
using Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiDesafio.Config.Behaviors
{
    public class InvalidModelState
    {
        public IActionResult Configure(ActionContext actionContext)
        {
            var errorList = new List<Error>();

            int httpStatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

            actionContext.ModelState.Values.SelectMany(x => x.Errors).ToList().ForEach(modelError =>
            {
                var objectError = new Error(
                    Convert.ToString(httpStatusCode),
                    "A requisição foi malformada, omitindo atributos obrigatórios, seja no payload ou através de atributos na URL",
                    modelError.ErrorMessage
                );

                errorList.Add(objectError);
            });

            var objectResult = new ObjectResult(new
            {
                errorList,
                meta = new Meta(errorList.Count),
            });

            objectResult.StatusCode = httpStatusCode;

            return objectResult;
        }

    }
}
