using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace ZeenaPower.Helper
{
    public class StorageHelper
    {
        static string localFileName = "savedData.txt";
        static string FilePath = Path.Combine(FileSystem.AppDataDirectory, localFileName);
        public static Shared StoredShared = new Shared();

        //public StorageHelper()
        //{
        //    FilePath
        //}
        public static void SaveStorageObjectToFile()
        {
            try
            {
                string JsonSavedData = JsonConvert.SerializeObject(StoredShared);
                File.WriteAllText(FilePath, JsonSavedData);
                Shared.LogToConsole("STORAGE SAVED");
            }
            catch (Exception ex)
            {
                Shared.LogToConsole("ERROR SAVING: " + ex.ToString());
            }
        }
        public static void RetrieveStorageObject()
        {
            try
            {
                Shared.LogToConsole("Retrieve Stored Data");
                var s = File.ReadAllText(FilePath);
                StoredShared = JsonConvert.DeserializeObject<Shared>(s);
                Shared.LogToConsole("Success Retrieving stored data!");
            }
            catch (Exception ex)
            {
                Shared.LogToConsole("StoredRetreving Error:\n" + ex.ToString());
            }
        }

        //public static bool CanRefreshPage(string pageName)
        //{
        //    try
        //    {
        //        if (StorageHelper.StorageObject.SavedDate == null)
        //        {
        //            StorageHelper.StorageObject.SavedDate = new Dictionary<string, DateTime>();
        //            StorageHelper.SaveStorageObjectToFile();
        //            return false;
        //        }
        //        var hasKey = StorageHelper.StorageObject.SavedDate.ContainsKey(pageName);
        //        if (hasKey && (DateTime.Now - StorageHelper.StorageObject.SavedDate[pageName]).Minutes > 15)
        //            return true;
        //    }
        //    catch
        //    {
        //        Shared.LogToConsole("CanRefreshPage Fuction");
        //    }
        //    return false;
        //}
        //public static void SaveLastLoadTime(string pageName)
        //{
        //    //update saved time
        //    if (StorageHelper.StorageObject.SavedDate == null)
        //    {
        //        StorageHelper.StorageObject.SavedDate = new Dictionary<string, DateTime>();
        //    }
        //    if (StorageHelper.StorageObject.SavedDate.ContainsKey(pageName))
        //    {
        //        StorageHelper.StorageObject.SavedDate[pageName] = DateTime.Now;
        //    }
        //    else
        //    {
        //        StorageHelper.StorageObject.SavedDate.Add(pageName, DateTime.Now);
        //    }
        //    StorageHelper.SaveStorageObjectToFile();
        //}
    }
}
