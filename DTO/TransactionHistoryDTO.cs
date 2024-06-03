
﻿using DAL.Classes;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class TransactionHistoryDTO
    {
        [Required(ErrorMessage = "TransactionHistoryId is required")]
        public int TransactionHistoryId { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        public int AccountId { get; set; }

        public string? AccountUsername { get; set; }

        public DateTime? DateTime { get; set; }

        [Required (ErrorMessage = "Src is required")]
        [Range(0,3, ErrorMessage = "The given Src Type is improper")]
        public Src Src { get; set; }

        [Required(ErrorMessage = "TransactionType is required")]
        [Range(0, 2, ErrorMessage = "The given TransactionType Type is improper")]
        public TransactionType TransactionType { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(-1000, 1000, ErrorMessage = "Please enter a value between {1} and {2}")]
        public decimal Amount { get; set; }

        public int? ConversionId { get; set; }

        public string? ConversionName { get; set; }

        public decimal? ConversionValue { get; set; }
    }
}
