namespace Arvan.PowerShell.Context.Authentication
{
    public class AuthenticationContext
    {
        private const string FileName = "account.json";
        
        public static AuthenticationContextEntity Current => InMemoryContext ?? PersistentContext;
        
        public static AuthenticationContextEntity InMemoryContext { get; set; }

        public static AuthenticationContextEntity PersistentContext
        {
            get => PersistenceUtil.Load<AuthenticationContextEntity>(FileName);
            set => PersistenceUtil.Save(FileName, value);
        }
    }
}