using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionProcessor.Application.DTOs
{
    public class ProcessResult
    {
        public int Processed { get; set; }

        public int Rejected { get; set; }

        public int Duplicates { get; set; }

        public int AccountsCreated { get; set; }

        public List<string> Messages { get; set; } = new();
    }
}
