using CertificationProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificationProcessor.Services;

/// <summary>
/// Service responsible for cleaning raw candidate CSV data.
/// Performs capitalization of names and removes duplicate records.
/// </summary>
public class DataCleaner
{
    /// <summary>
    /// Cleans and normalizes raw candidate data.
    /// </summary>
    /// <param name="rawRecords">Enumerable of CandidateCsvRecord from CSV</param>
    /// <returns>Enumerable of Candidate with FullName set, duplicates removed, and names capitalized</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null</exception>
    public IEnumerable<Candidate> Clean(IEnumerable<CandidateCsvRecord> rawRecords)
    {
        if (rawRecords == null) throw new ArgumentNullException(nameof(rawRecords));

        var cleaned = rawRecords
            // Capitalize first and last names for consistency
            .Select(r => new
            {
                FirstName = Capitalize(r.FirstName),
                LastName = Capitalize(r.LastName),
                r.Department,
                r.PracticalScore,
                r.TheoryScore
            })
            // Remove duplicates based on all relevant fields
            .GroupBy(r => new
            {
                r.FirstName,
                r.LastName,
                r.Department,
                r.PracticalScore,
                r.TheoryScore
            })
            .Select(g => g.First())
            // Map to Candidate domain model with FullName
            .Select(r => new Candidate
            {
                FullName = $"{r.FirstName} {r.LastName}",
                Department = r.Department,
                PracticalScore = r.PracticalScore,
                TheoreticalScore = r.TheoryScore
            })
            .ToList();

        return cleaned;
    }

    /// <summary>
    /// Capitalizes the input string (e.g., "mira" → "Mira").
    /// Returns empty string if input is null or whitespace.
    /// </summary>
    /// <param name="input">Name to capitalize</param>
    /// <returns>Capitalized name</returns>
    private static string Capitalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(input.Trim().ToLower());
    }
}
