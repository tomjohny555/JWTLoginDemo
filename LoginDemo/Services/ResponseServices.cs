using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginDemo.Interface;
using LoginDemo.Models;
using System.Web.Script.Serialization;

namespace LoginDemo.Services
{
    public class ResponseServices:IResponseServices
    {
      
        public string CreateErrorResponse(string Description, int statusCode, string Status)
        {
            try
            {

                ResponseStatus objResponseStatus = new ResponseStatus();
                ErrResponse objErrorResponse = new ErrResponse();
                objResponseStatus.Description = Description;
                objResponseStatus.status = Status;
                objResponseStatus.statusCode = statusCode;
                objErrorResponse.responseStatus = objResponseStatus;
                var serializer = new JavaScriptSerializer();
                var responseMessage = serializer.Serialize(objErrorResponse);
                return responseMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateResponse(string Description, int StatusCode, dynamic Content, string Status)
        {
            try
            {
                // Response obj
                Response objResponse = new Response();
                ResponseStatus objResponseStatus = new ResponseStatus();
                objResponseStatus.Description = Description;
                objResponseStatus.statusCode = StatusCode;
                objResponseStatus.status = Status;
                objResponse.responseStatus = objResponseStatus;
                objResponse.responseContent = Content;
                var serializer = new JavaScriptSerializer();
                var responseMessage = serializer.Serialize(objResponse);
                return responseMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }


     
    }
}