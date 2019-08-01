using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Amount { get; set; }

        public SaleStatus Status { get; protected set; }

        public Seller Seller { get; set; }

        //Construtor
        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }

        public SalesRecord(DateTime date, double amount, SaleStatus status)
        {
            Date = date;
            Amount = amount;
            Status = status;
        }
    }
}
