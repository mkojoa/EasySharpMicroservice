using EasySharp.Core.Messages.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EasySharp.Core.Messages
{
    public static class ApiGenericMsg
    {
        /// <summary>
        /// Return server error message
        /// </summary>
        public static string ServerError => "Server Error Occurred";

        /// <summary>
        /// Return processing error message
        /// </summary>
        public static string ProcessingError =>
            "An error occured while processing your request. Contact your administrator";

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="tbody"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static ApiGenericResponse<TBody> OnEntityCreateError<TBody>(TBody tbody, string entityName)
        {
            return new ApiGenericResponse<TBody>()
            {
                Status = 0,
                Message = $"An error occured while creating new {entityName}. Contact your administrator.",
                Data = tbody
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="result"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static ApiGenericResponse<TBody> OnEntityCreateSuccess<TBody>(TBody result, string entityName)
        {
            return new ApiGenericResponse<TBody>()
            {
                Status = (int)HttpStatusCode.OK,
                Message = $"New {entityName} Created Successfully.",
                Data = result
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="tbody"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static ApiGenericResponse<TBody> OnEntityUpdateSuccess<TBody>(TBody tbody, string entityName)
        {
            return new ApiGenericResponse<TBody>()
            {
                Status = (int)HttpStatusCode.OK,
                Message = $"{entityName} Updated Successfully.",
                Data = tbody
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="tbody"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static ApiGenericResponse<TBody> OnEntityUpdateError<TBody>(TBody tbody, string entityName)
        {
            return new ApiGenericResponse<TBody>()
            {
                Status = 0,
                Message = $"An error occured while delteting {entityName}. Contact your administrator.",
                Data = tbody
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="tbody"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static ApiGenericResponse<TBody> OnEntityDeleteSuccess<TBody>(TBody tbody, string entityName)
        {
            return new ApiGenericResponse<TBody>()
            {
                Status = (int)HttpStatusCode.OK,
                Message = $"{entityName} Deleted Successfully.",
                Data = tbody
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="tbody"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static ApiGenericResponse<TBody> OnEntityDeleteError<TBody>(TBody tbody, string entityName) 
        {
            return new ApiGenericResponse<TBody>()
            {
                Status = 0,
                Message = $"An error occured while delteting {entityName}. Contact your administrator.",
                Data = tbody
            };
        }
    }
}
