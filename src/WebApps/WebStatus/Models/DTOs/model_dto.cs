namespace WebStatus.Models.DTOs
{
    // Models/CustomerDto.cs
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }

    // Models/AccountDto.cs
    public class AccountDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Balance { get; set; }
    }

    // Models/TransactionDto.cs
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }

    // Models/TransactionType.cs
    public enum TransactionType
    {
        Adding,
        Withdrawing
    }
}
