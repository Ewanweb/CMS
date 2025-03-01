using CMS.Domain.Admin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Common.Utils;

    public class SlugGenerator<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly Func<T, string> _slugSelector;

        public SlugGenerator(IRepository<T> repository, Func<T, string> slugSelector)
        {
            _repository = repository;
            _slugSelector = slugSelector;
        }

    public async Task<string> CheckAndGenerateSlug(string slug, int? entityId = null)
    {
        // پاک‌سازی اولیه Slug
        slug = SanitizeSlug(slug);

        int count = 1;
        string originalSlug = slug;

        while (await _repository.AnyAsync(p =>
                   EF.Property<string>(p, "Slug") == slug &&
                   (entityId == null || EF.Property<int>(p, "Id") != entityId)))
        {
            if (count >= 100)
                throw new InvalidOperationException("Unable to generate unique slug.");

            slug = $"{originalSlug}-{count}";
            count++;
        }

        return slug;
    }

    private string SanitizeSlug(string slug)
        {
            slug = Regex.Replace(slug, @"[^a-zA-Z0-9\s]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = slug.ToLower();
            return slug.Trim('-');
        }
    }

