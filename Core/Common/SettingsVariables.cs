namespace Core.Common
{
    public static class SettingsVariables
    {
        public const string PasswordExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$";

        public static string GetHtmlConfirmEmailPage(string link)
        {
            return "<h1>Welcome!!</h1> <hr>" +
                    "<h3>Your Registration is almost completed!</h3>" +
                    "<p>Thank you for registering in Travel Log!<br>" +
                    $"Please, confirm your email by this link<a href=\"{link}\"> confirm</a>.<br>" +
                    "If you have any questions contact us with this <a href=\"mailto: smtptestforproject @gmail.com\">email</a>.</p>";
        }
    }
}
