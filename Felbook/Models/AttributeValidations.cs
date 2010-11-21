using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Felbook.Models;


namespace Felbook.Models
{

    public class EmailExistAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            return true;
            //string email = (string)value;
            ///UserService usrService = new UserService();

            /*if (usrService.existEmail(email))
            {
                return false;
            }
            else 
            {
                return true;
            }*/
        }

    }

    public class EmailAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            string email = (string)value;
            Regex emailPattern=new Regex(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$");
            return emailPattern.IsMatch(email);
        }
            
    }

    public class ICQAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            ValidationMethods valMethods = new ValidationMethods();
            if (value == null)
            {
                return true;
            }
            string stringICQ = (string)value;
            
            if (valMethods.IsNumeric(stringICQ) && stringICQ.Length > 6 && stringICQ.Length < 10)
            {
                return true;
            }
            else {
                return false;
            }          
        }
    }
    public class PhoneAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            ValidationMethods valMethods = new ValidationMethods();
            if (value == null)
            {
                return true;
            }
            string stringICQ = (string)value;

            if (valMethods.IsNumeric(stringICQ) && stringICQ.Length > 8 && stringICQ.Length < 16)
            {
                return true;
            }
            else
            {
                return false;
            }   
        }
    }
    #region validation methods
    class ValidationMethods {
        public bool IsNumeric(string s)
        {
            bool isnumeric = false;
            if (!IsEmptyString(s))
            {
                try
                {
                    System.Double.Parse(s, System.Globalization.NumberStyles.Any, null);
                    isnumeric = true;
                }
                catch
                {
                    isnumeric = false;
                }
            }
            else
            {
                isnumeric = false;
            }
            return isnumeric;
        }

        public bool IsEmptyString(string s)
        {
            bool isemptystring = false;
            if (s != null && s.Length > 0)
            {
                isemptystring = false;
            }
            else
            {
                isemptystring = true;
            }
            return isemptystring;
        }
    }
    #endregion











   
}