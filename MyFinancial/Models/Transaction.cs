using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyFinancial.Models
{
    public class Transaction
    {
        public int Id { get; set; } //每筆紀錄的識別編號

        [Required]
        [StringLength(100)]
        public string Title { get; set; }=string.Empty; //交易名稱
        [Range(0.01, 99999999)]
        public decimal Amount { get; set; } //金額
        [Required]
        public string Type { get; set; } = "Expense"; //收入支出分類

        [Required]
        public string Category { get; set; } = string.Empty;

        public DateTime Date { get; set; }= DateTime.Today; //日期

        public string? Note {  get; set; } //備註
    }
}
