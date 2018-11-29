using Arvan.PowerShell.Common.Utils;

namespace Arvan.PowerShell.Common.Authentication
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