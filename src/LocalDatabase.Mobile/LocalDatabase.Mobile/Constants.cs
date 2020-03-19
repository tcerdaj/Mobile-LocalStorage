using System;
using System.IO;
using SQLite;

namespace LocalDatabase.Mobile
{
    public class Constants
    {
        public const string DatabaseFilename = "LocalSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename);
    }
}
