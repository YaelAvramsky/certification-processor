using CertificationProcessor.Services;

//var loader = new DataLoader("Data/Data.csv");
//var candidates = loader.LoadCandidates();

//foreach (var c in candidates)
//{
//    Console.WriteLine($"{c.FirstName} {c.LastName} | {c.Department} | {c.TheoryScore}/{c.PracticalScore}");
//}




var loader = new DataLoader("Data/Data.csv");
var rawCandidates = loader.LoadCandidates();

var cleaner = new DataCleaner();
var candidates = cleaner.Clean(rawCandidates);

foreach (var c in candidates)
{
    Console.WriteLine($"{c.FullName} | {c.Department} | {c.TheoreticalScore}/{c.PracticalScore}");
}