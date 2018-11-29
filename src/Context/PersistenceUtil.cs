using System;
using System.IO;
using Newtonsoft.Json;

namespace Arvan.PowerShell.Context
{
    public class PersistenceUtil
    {
        private const string ArvanUserFolderName = ".Arvan";
        private const string PsSubFolderName = "arvan-powershell";

        public static string PersistenceRootFolder => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ArvanUserFolderName,
            PsSubFolderName);

        public static TEntity Load<TEntity>(string fileName)
        {
            string filePath = Path.Combine(PersistenceRootFolder, fileName);
            if (!File.Exists(filePath))
                return default(TEntity);

            try
            {
                return JsonConvert.DeserializeObject<TEntity>(File.ReadAllText(filePath));
            }
            catch (Exception e)
            {
                return default(TEntity);
            }
        }

        public static void Save<TEntity>(string fileName, TEntity entity)
        {
            string filePath = Path.Combine(PersistenceRootFolder, fileName);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(entity));
        }
    }
}