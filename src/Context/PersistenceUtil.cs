using System;
using System.IO;

namespace Arvan.PowerShell.Context
{
    public class PersistenceUtil
    {
        private const string ArvanUserFolderName = ".Arvan";
        private const string PsSubFolderName = "arvan-powershell";

        public static string GetPersistenceRootFolder()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
                ArvanUserFolderName,
                PsSubFolderName);
        }
    }
}