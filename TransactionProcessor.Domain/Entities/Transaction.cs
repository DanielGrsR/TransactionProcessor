using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TransactionProcessor.Domain.Enums;

namespace TransactionProcessor.Domain.Entities
{
    public class Transaction
    {
        public string TransactionId { get; set; } = string.Empty;

        public string AccountId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OperationType OperationType { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
