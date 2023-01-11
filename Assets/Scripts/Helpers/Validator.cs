using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mail;
using System.Text.RegularExpressions;

public static class Validator
{
    public static bool CheckMail(string mailString)
    {
        if (string.IsNullOrEmpty(mailString) || string.IsNullOrWhiteSpace(mailString) || mailString.Length < 6) return false;
        try
        {
            MailAddress m = new MailAddress(mailString);
            return true;
        }
        catch
        {
            Debug.Log("Mail Failed");
            return false;
        }
    }
    public static bool CheckName(string nameString)
    {
        if (nameString.Length < 6) return false;
        string pattern = @"^[a-zA-Z][a-zA-Z0-9]{5,11}$";
        Regex regex = new Regex(pattern);
        Debug.Log("CheckName" + regex.IsMatch(nameString));
        return regex.IsMatch(nameString);
    }
    public static bool CheckPassword(string passString)
    {
        // Regex hasNumber = new Regex(@"[0-9]+");
        // Regex hasUpperChar = new Regex(@"[A-Z]+");
        Regex hasMinimum6Chars = new Regex(@".{6,}");
        bool result = hasMinimum6Chars.IsMatch(passString);
        return result;
    }
}
