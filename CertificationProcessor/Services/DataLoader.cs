using CertificationProcessor.Domain;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificationProcessor.Services;

public class DataLoader
{
    private readonly string _filePath;

    public DataLoader(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    public IEnumerable<CandidateCsvRecord> LoadCandidates()
    {
        if (!File.Exists(_filePath))
            throw new FileNotFoundException($"CSV file not found: {_filePath}");

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            IgnoreBlankLines = true,
            TrimOptions = TrimOptions.Trim,
            MissingFieldFound = null
        };

        using var reader = new StreamReader(_filePath);
        using var csv = new CsvReader(reader, config);

        var records = csv.GetRecords<CandidateCsvRecord>().ToList();

        return records;
    }
}
