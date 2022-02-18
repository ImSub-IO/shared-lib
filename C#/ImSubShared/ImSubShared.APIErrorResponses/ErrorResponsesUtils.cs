using ImSubShared.APIErrorResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;

namespace ImSubShared.APIErrorResponses
{
    public static class ErrorResponsesUtils
    {
        /// <summary>
        /// Creates a custom 500 <see cref="ErrorResponse{T}"/>, with <see cref="SubCode.INTERNAL_ERROR"/> as subcode and "Internal error" as
        /// <see cref="ErrorResponse{T}.ErrorDescription"/>
        /// </summary>
        /// <returns></returns>
        public static ContentResult Create500Response()
        {
            return new ContentResult
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ContentType = MediaTypeNames.Application.Json,
                Content = JsonConvert.SerializeObject(new ErrorResponse<string>
                {
                    SubCode = SubCode.INTERNAL_ERROR,
                    ErrorDescription = "Internal error"
                })
            };
        }

        /// <summary>
        /// Used for creating a <see cref="Custom400ErrorResponse"/> for a single field
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Custom400ErrorResponse Create400ErrorResponse(string key, string value, ActionContext context)
        {
            context.ModelState.AddModelError(key, value);
            return new Custom400ErrorResponse(context);
        }

        /// <summary>
        /// Creates an <see cref="ErrorResponse{T}"/>, with the specified <see cref="SubCode"/> and the specified error description
        /// as <see cref="ErrorResponse{T}.ErrorDescription"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subCode"></param>
        /// <param name="errorDescription"></param>
        /// <returns></returns>
        public static ErrorResponse<T> CreateErrorResponse<T>(SubCode subCode, T errorDescription) where T : class
        {
            return new ErrorResponse<T>
            {
                SubCode = subCode,
                ErrorDescription = errorDescription
            };
        }

    }
}
