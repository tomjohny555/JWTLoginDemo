using System;
using System.Linq;
using System.Web;
using LoginDemo.Interface;
using LoginDemo.Models;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Security.Claims;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Tokens;


namespace LoginDemo.Services
{
    public class EmployeeServices : IEmployee
    {
        private readonly string connection = null;
        #region Constructor
        public EmployeeServices()
        {
            connection = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
        }
        #endregion

        #region Register customer
        public int RegisterCustomer(Signin objLog)
        {
            int LoginID = 0;
            try
            {
                using (SqlConnection objConnection = new SqlConnection(connection))
                {
                    objConnection.Open();
                    SqlCommand objSqlCommand = new SqlCommand("SP_Create_Employee", objConnection);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = objLog.employeeName;
                    objSqlCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = objLog.userName;
                    objSqlCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = objLog.email;
                    objSqlCommand.Parameters.Add("@password", SqlDbType.VarChar).Value = objLog.password;
                    SqlDataReader objReader = objSqlCommand.ExecuteReader();
                    while (objReader.Read())
                    {
                        LoginID = objReader["LoginId"] == DBNull.Value ? 0 : Convert.ToInt32(objReader["LoginId"]);
                    }
                }
                return LoginID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region create token
        public string GenerateToken(Signin objLog,int LoginId, string base64Secret)
        {
            try
            {
                var identity = new ClaimsIdentity("JWT");
                identity.AddClaim(new Claim(ClaimTypes.Name, "User"));
                identity.AddClaim(new Claim("UserID", LoginId.ToString()));
                identity.AddClaim(new Claim("UserName", objLog.userName));
                var _issuer = "afsalmh";
                var keyByteArray = TextEncodings.Base64Url.Decode(base64Secret);
                var signingKey = new HmacSigningCredentials(keyByteArray);
                var issued = DateTime.UtcNow;
                var expires = DateTime.UtcNow.AddDays(1);
                Lifetime lf = new Lifetime(issued, expires);
                var token = new JwtSecurityToken(_issuer, objLog.userName, identity.Claims, lf, signingKey);
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.WriteToken(token);
                return jwt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}