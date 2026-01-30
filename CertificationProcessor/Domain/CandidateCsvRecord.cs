using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificationProcessor.Domain;

public class CandidateCsvRecord
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int TheoryScore { get; set; }
    public int PracticalScore { get; set; }
}
