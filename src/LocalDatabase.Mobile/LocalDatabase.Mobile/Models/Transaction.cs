using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace LocalDatabase.Mobile.Models
{
    public class Transaction: EntityBase
    {
        [NotNull]
        [MaxLength(250)]
        public  string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public  Category Category { get; set; }
        public  VendorType Vendor { get; set; }
        [NotNull]
        public  double Amount { get; set; }
        public  byte[] Photo { get; set; }
        public Guid ContactId { get; set; }
    }

    public enum TransactionType
    {
        Expense,
        Income
    }

    public enum Category
    {
        Groseries,
        Payroll,
        OtherIncome,
        Laundry,
        HouseMortgage,
        HouseImprovement,
        CarMaintenance,
        CarGas,
        CarMortgage,
        CarWash,
        Clothing,
        LawnMaintenance,
        Utilities,
        Restaurants,
        CreditCardsPayments,
        LoanPayments,
        Phone,
        Internet
    }

    public enum VendorType
    {
        None,
        Walmart,
        Aldi,
        Publix,
        HomeDepot,
        Lowes,
        PolloTropical,
        MacDonals,
        DairyQueen,
        SevenEleven,
        Shell,
        SamsClub,
        Bravo,
        CvsPharmacy,
        Walgreens,
        Amazon,
        AutoZone,
        AdvanceAutoparts,
        AceHardware,
        MetroPcs
    }
}