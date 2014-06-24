﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using blackSheep.Helper;
using System.Configuration;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace blackSheep
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ForgotPassword));
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnForgotPwd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                bool exist = false;
                UserRepository objUserRepo = new UserRepository();
                Registration regObject = new Registration();
              
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    string strUrl = string.Empty;
                   // c.customer_email = txtEmail.Text.Trim();
                    // exist = custrepo.ExistedCustomerEmail(c);
                    User usr = objUserRepo.getUserInfoByEmail(txtEmail.Text);

                    if (usr!=null)
                    {
                        string URL = Request.Url.AbsoluteUri;
                        //strUrl = Server.MapPath("~/ChangePassword.aspx") + "?str=" + txtEmail.Text + "&type=forget";
                        strUrl = URL.Replace("ForgetPassword.aspx", "ChangePassword.aspx" + "?str=" + regObject.MD5Hash(txtEmail.Text) + "&type=forget");

                        string MailBody = "<body bgcolor=\"#FFFFFF\"><!-- Email Notification from socialcrowd.com-->" +
                    "<table id=\"Table_01\" style=\"margin-top: 50px; margin-left: auto; margin-right: auto;\"" +
                    " align=\"center\" width=\"650px\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr>" +
                   "<td height=\"20px\" style=\"background-color: rgb(222, 222, 222); text-align: center; font-size: 15px; font-weight: bold; font-family: Arial; color: rgb(51, 51, 51); float: left; width: 100%; margin-top: 7px; padding-top: 10px; border-bottom: 1px solid rgb(204, 204, 204); padding-bottom: 10px;\">" +
                       "blackSheep</td></tr><!--Email content--><tr>" +
                   "<td style=\"background-color: #dedede; padding-top: 10px; padding-left: 25px; padding-right: 25px; padding-bottom: 30px; font-family: Tahoma; font-size: 14px; color: #181818; min-height: auto;\"><p>Hi , " + usr.UserName + "</p><p>" +
                       "As your request, please find below Reset Password information to Click This Link : <a href=" + strUrl + " style=\"text-decoration:none;\">Reset Password</a></td></tr><tr>" +
                   "<td style=\"background-color: rgb(222, 222, 222); margin-top: 10px; padding-left: 20px; height: 20px; color: rgb(51, 51, 51); font-size: 15px; font-family: Arial; border-top: 1px solid rgb(204, 204, 204); padding-bottom: 10px; padding-top: 10px;\">Thanks" +
                   "</td></tr></table><!-- End Email Notification From blackSheep.com --></body>";

                        string username = ConfigurationManager.AppSettings["userName"];
                        string host = ConfigurationManager.AppSettings["host"];
                        string port = ConfigurationManager.AppSettings["port"];
                        string pass = ConfigurationManager.AppSettings["pasword"];

                     //   string Body = mailformat.VerificationMail(MailBody, txtEmail.Text.ToString(), "");
                        string Subject = "Forget Password WootSuite Social account";
                        MailHelper.SendMailMessage(host, int.Parse(port.ToString()), username, pass, txtEmail.Text.ToString(), string.Empty, string.Empty, Subject, MailBody);


                        lblerror.Text = "Your Password Changes info to send in your Email";
                    }
                    else
                    {
                        lblerror.Text = "Your Email is wrong Please try another one";
                    }


                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.StackTrace);
            }
        }
    }
}