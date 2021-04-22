using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using InverntoryManager.Droid.Data;
using InverntoryManager.Data;

[assembly: Dependency(typeof(SQLiteDb))]

namespace InverntoryManager.Droid.Data
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "InventoryDb.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}