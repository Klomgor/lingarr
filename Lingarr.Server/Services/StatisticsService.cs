﻿using Lingarr.Core.Data;
using Lingarr.Core.Entities;
using Lingarr.Server.Interfaces.Services;
using Lingarr.Server.Models.FileSystem;
using Microsoft.EntityFrameworkCore;

namespace Lingarr.Server.Services;

public class StatisticsService : IStatisticsService
{
    private readonly LingarrDbContext _dbContext;

    public StatisticsService(
        LingarrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Statistics> GetStatistics()
    {
        return await GetOrCreateStatistics();
    }

    public async Task<IEnumerable<DailyStatistics>> GetDailyStatistics(int days = 30)
    {
        var startDate = DateTime.UtcNow.Date.AddDays(-days + 1); // +1 to include today
        var stats = await _dbContext.DailyStatistics
            .Where(d => d.Date >= startDate)
            .OrderBy(d => d.Date)
            .ToListAsync();

        return stats;
    }

    private async Task<Statistics> GetOrCreateStatistics()
    {
        var stats = await _dbContext.Statistics.SingleOrDefaultAsync();
        if (stats == null)
        {
            stats = new Statistics();
            _dbContext.Statistics.Add(stats);
            await _dbContext.SaveChangesAsync();
        }

        return stats;
    }

    private async Task<DailyStatistics> GetOrCreateDailyStatistics(DateTime today)
    {
        var dailyStats = await _dbContext.DailyStatistics
            .Where(d => d.Date >= today)
            .FirstOrDefaultAsync();

        if (dailyStats == null)
        {
            dailyStats = new DailyStatistics { Date = today };
            _dbContext.DailyStatistics.Add(dailyStats);
        }

        return dailyStats;
    }
    
    public async Task<int> UpdateTranslationStatistics(
        TranslationRequest request, 
        string serviceType, 
        List<SubtitleItem> subtitles, 
        List<SubtitleItem> translatedSubtitles)
    {
        var stats = await GetOrCreateStatistics();
        var today = DateTime.UtcNow.Date;

        // Update total counts
        stats.TotalLinesTranslated += translatedSubtitles.Sum(s => s.Lines.Count);
        stats.TotalCharactersTranslated += translatedSubtitles.Sum(s => s.Lines.Sum(l => l.Length));
        stats.TotalFilesTranslated++;

        // Update media type statistics
        var mediaType = request.MediaType.ToString();
        var mediaTypeStats = stats.TranslationsByMediaType;
        mediaTypeStats[mediaType] = mediaTypeStats.GetValueOrDefault(mediaType) + 1;
        stats.TranslationsByMediaType = mediaTypeStats;

        // Update service type statistics
        var serviceStats = stats.TranslationsByService;
        serviceStats[serviceType] = serviceStats.GetValueOrDefault(serviceType) + 1;
        stats.TranslationsByService = serviceStats;

        // Update language statistics
        var languageStats = stats.SubtitlesByLanguage;
        languageStats[request.TargetLanguage] = languageStats.GetValueOrDefault(request.TargetLanguage) + 1;
        stats.SubtitlesByLanguage = languageStats;

        // Update daily statistics
        var dailyStats =  await GetOrCreateDailyStatistics(today);
        dailyStats.TranslationCount++;

        return await _dbContext.SaveChangesAsync();
    }
}