using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Neofilia.DAL;
using Neofilia.DAL.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;

namespace Neofilia.BOT.Helpers
{
    public class CsvHandler
    {
        private readonly NeofiliaDbContext _context;

        public CsvHandler(NeofiliaDbContext context)
        {
            _context = context;
        }
        public bool BuildEntities(string entity)
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("configJson.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            var deserializedJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            var targetCsvPath = string.Empty;
            switch (entity)
            {
                case "question":
                    targetCsvPath = deserializedJson.QuestionCsvPath;
                    var records = new List<Question>();
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";",
                        HasHeaderRecord = true,
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        IgnoreBlankLines = true,
                        ShouldSkipRecord = args => args.Row.Parser.Record.All(string.IsNullOrWhiteSpace),
                    };
                    using (var reader = new StreamReader(targetCsvPath))
                    using (var csvReader = new CsvReader(reader, config))
                    {
                        csvReader.Context.RegisterClassMap<QuestionMap>();
                        records = csvReader.GetRecords<Question>().ToList();
                    }
                    foreach (var record in records)
                    {
                        _context.Set<Question>().AddIfNotExists(record);
                        
                    }
                    _context.SaveChanges();
                    return true;
                case "coupon":
                    targetCsvPath = deserializedJson.CouponCsvPath; break;
                case "bar":
                    targetCsvPath = deserializedJson.BarCsvPath; break;
                default:
                    return false;
            }
            return true;
        }
    }
    public static class DbSetExtensions
    {
        public static EntityEntry<T>? AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>>? predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }
    }

    public sealed class QuestionMap : ClassMap<Question> {
        public QuestionMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(x=>x.Id).Ignore();
        }
    }
}
