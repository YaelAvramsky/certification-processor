using CertificationProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CertificationProcessor.Services;

/// <summary>
/// Service responsible for calculating final scores for candidates.
/// </summary>
public class ScoreCalculator
{
    private const double PracticalWeight = 0.6;
    private const double TheoryWeight = 0.4;

    /// <summary>
    /// Calculates final score for each candidate.
    /// </summary>
    /// <param name="candidates">Enumerable of cleaned Candidate objects</param>
    /// <returns>Enumerable of Candidate with FinalScore populated</returns>
    /// <exception cref="ArgumentNullException">Thrown when candidates is null</exception>
    public IEnumerable<Candidate> CalculateFinalScores(IEnumerable<Candidate> candidates)
    {
        if (candidates == null) throw new ArgumentNullException(nameof(candidates));

        foreach (var candidate in candidates)
        {
            // Weighted sum: 60% Practical, 40% Theory
            candidate.FinalScore = (int)Math.Round(
                candidate.PracticalScore * PracticalWeight +
                candidate.TheoreticalScore * TheoryWeight
            );
        }

        return candidates;
    }

    /// <summary>
    /// Filters candidates that passed the training.
    /// </summary>
    /// <param name="candidates">Enumerable of Candidate</param>
    /// <param name="passThreshold">Pass threshold (inclusive)</param>
    /// <returns>Enumerable of candidates that passed</returns>
    public IEnumerable<Candidate> GetPassedCandidates(IEnumerable<Candidate> candidates, int passThreshold = 70)
    {
        if (candidates == null) throw new ArgumentNullException(nameof(candidates));

        return candidates.Where(c => c.FinalScore >= passThreshold);
    }
}
