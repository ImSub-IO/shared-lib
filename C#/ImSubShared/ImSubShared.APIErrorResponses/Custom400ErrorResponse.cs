using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ImSubShared.APIErrorResponses
{
    public class ValidationModelError
    {
        public string Key { get; set; }
        public List<string> Value { get; set; }
    }

    public class Custom400ErrorResponse
    {
        public List<ValidationModelError> Errors { get; set; }
        public Custom400ErrorResponse(ActionContext context)
        {
            Errors = context.ModelState.Select(x => new ValidationModelError
            {
                Key = x.Key,
                Value = x.Value.Errors.Select(x => x.ErrorMessage).ToList()
            }).ToList();
        }
    }
}
