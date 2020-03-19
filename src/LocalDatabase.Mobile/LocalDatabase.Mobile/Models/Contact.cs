using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LocalDatabase.Mobile.Controllers;
using LocalDatabase.Mobile.Helpers.SQLite.Models;
using LocalDatabase.Mobile.Utilities;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LocalDatabase.Mobile.Models
{
    public class Contact: EntityBase
    {
        static private AsyncLazy<List<Transaction>> lazyData;
        [Unique]
        [NotNull]
        public  string Name { get; set; }
        public  string Address { get; set; }
        public  string Phone { get; set; }
        public  byte[] Photo { get; set; }
    }
}
