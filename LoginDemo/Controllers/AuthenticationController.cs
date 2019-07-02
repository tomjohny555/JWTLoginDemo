using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoginDemo.Interface;
using LoginDemo.Services;
using LoginDemo.Models;
using System.Text;
using System.Web.Http.Cors;
namespace LoginDemo.Controllers

{
    [RoutePrefix("Api/V1.0")]
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AuthenticationController : ApiController
    {

        private IResponseServices _objIResponse = null;
        private IEmployee _iEmp = null;
        string secretToken = "oGPUpC6GxgmR3j0MWPqKhrAE5wZkkVhfZfRw-qlZKAg";

        #region Constructor
        public AuthenticationController() : this(new ResponseServices()) { }
        public AuthenticationController(ResponseServices objIResponse)
        {
            _objIResponse = objIResponse;
            _iEmp = new EmployeeServices();
        }
        #endregion
        #region Login
        [Route("Authentication")]
        [HttpPost]
        public HttpResponseMessage Login(Signin objLog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int LoginID = _iEmp.RegisterCustomer(objLog);
                    if (LoginID > 0)
                    {
                        string token = _iEmp.GenerateToken(objLog, LoginID, secretToken);
                        if (token != null)
                        {
                            dynamic objResponse = new
                            {
                                Token = token,
                                LoginID = LoginID,
                                UserName = objLog.userName
                            };
                            var response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(_objIResponse.GenerateResponse("Added new user", 200, objResponse, "Success").ToString(), Encoding.UTF8, "Application/json");
                            return response;
                        }
                        else
                        {
                            var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                            response.Content = new StringContent(_objIResponse.GenerateResponse("Token creation failed", 500, "Internal server error", "Failed").ToString(), Encoding.UTF8, "Application/json");
                            return response;
                        }
                    }
                    else
                    {
                        var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                        response.Content = new StringContent(_objIResponse.GenerateResponse("User registration failed", 500, "DB server updation failed", "Failed").ToString(), Encoding.UTF8, "Application/json");
                        return response;
                    }
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                    var response = Request.CreateResponse(HttpStatusCode.NoContent);
                    response.Content = new StringContent(_objIResponse.GenerateResponse("Invalid Parameters", 204, errors, "Failed").ToString(), Encoding.UTF8, "Application/json");
                    return response;
                }

            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(_objIResponse.CreateErrorResponse("Internal Server Error", 500, ex.Message).ToString(), Encoding.UTF8, "Application/json");
                return response;
            }
        }
        #endregion
    }
}
