using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageService.Application.Interfaces;
using LanguageService.Domain.Entities;
using LanguageService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LanguageService.Infrastructure.Repository
{
    public class LanguageRepository(AppDbContext context) : ILanguageRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CreateLanguage(Language language)
        {
            await _context.AddAsync(language);
            await _context.SaveChangesAsync();
        }

        public async Task<Language?> GetLanguageById(string id)
        {
            return await _context.Languages.FindAsync(id);

        }
        public async Task<Language?> GetLanguageByName(string name)
        {

            return await _context.Languages.Where(l => l.Name == name).FirstOrDefaultAsync();

        }

        public async Task<List<Language>?> GetLanguagesAsync()
        {
            return await _context.Languages.ToListAsync();
        }
    }
}