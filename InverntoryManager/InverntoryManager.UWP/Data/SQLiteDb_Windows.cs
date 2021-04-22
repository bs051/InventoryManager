using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using InverntoryManager.Data;
using Windows.Storage;
using InverntoryManager.UWP.Data;

[assembly: Dependency(typeof(SQLiteDb))]

namespace InverntoryManager.UWP.Data
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(documentsPath, "InventoryDb.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}
