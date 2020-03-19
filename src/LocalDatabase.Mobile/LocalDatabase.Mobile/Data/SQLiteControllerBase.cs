﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LocalDatabase.Mobile.Data
{
    public class SQLiteControllerBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static readonly Lazy<SQLiteConnection> _lazyInitializer = new Lazy<SQLiteConnection>(() =>
        {
            return new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        });

        public static SQLiteAsyncConnection DatabaseAsync => lazyInitializer.Value;

        public static SQLiteConnection Database = _lazyInitializer.Value;
    }
}