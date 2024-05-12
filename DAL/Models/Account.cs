using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Decimal Balance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
            }

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
            }

            if (Balance < amount)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }

            Balance -= amount;
        }
    }
}
