using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using InverntoryManager.iOS;

using Foundation;
using UIKit;
using InverntoryManager.iOS.Data;
using InverntoryManager.Data;

[assembly: Dependency(typeof(SQLiteDb))]

namespace InverntoryManager.iOS.Data
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
            var path = Path.Combine(documentsPath, "InventoryDB.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}