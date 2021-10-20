namespace Core.Common
{
    public static class SettingsVariables
    {
        public const string PasswordExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$";
    }
}
