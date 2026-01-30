using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificationProcessor.Domain;

public class Candidate
{
    public string FullName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public int PracticalScore { get; set; }
    public int TheoreticalScore { get; set; }
    public int FinalScore { get; set; }
}
