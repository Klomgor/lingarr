﻿using Lingarr.Core.Data;
using Lingarr.Core.Entities;
using Lingarr.Server.Models;
using Microsoft.EntityFrameworkCore;
using Lingarr.Server.Models.Api;
using Lingarr.Server.Interfaces.Services;

namespace Lingarr.Server.Services;

public class MediaService : IMediaService
{
    private readonly LingarrDbContext _dbContext;
    private readonly ISubtitleService _subtitleService;
    private readonly ILogger<MediaService> _logger;

    public MediaService(LingarrDbContext dbContext, 
        ISubtitleService subtitleService,
        ILogger<MediaService> logger)
    {
        _dbContext = dbContext;
        _subtitleService = subtitleService;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<PagedResult<MovieResponse>> GetMovies(
        string? searchQuery,
        string? orderBy,
        bool ascending,
        int pageNumber,
        int pageSize)
    {
        var query = _dbContext.Movies
            .Include(m => m.Images)
            .AsSplitQuery()
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            query = query.Where(m => m.Title.ToLower().Contains(searchQuery.ToLower()));
        }

        query = orderBy switch
        {
            "Id" => ascending ? query.OrderBy(m => m.Id) : query.OrderByDescending(m => m.Id),
            "Title" => ascending ? query.OrderBy(m => m.Title) : query.OrderByDescending(m => m.Title),
            "DateAdded" => ascending ? query.OrderBy(m => m.DateAdded) : query.OrderByDescending(m => m.DateAdded),
            _ => ascending ? query.OrderBy(m => m.Title) : query.OrderByDescending(m => m.Title)
        };

        var totalCount = await query.CountAsync();
        var movies = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var enrichedMovies = new List<MovieResponse>();
        foreach (var movie in movies)
        {
            _logger.LogInformation("Processing movie: {RadarrId} - {Title} with Path: |Green|{Path}|/Green|", movie.RadarrId, movie.Title, movie.Path);
            var subtitles = await _subtitleService.GetAllSubtitles(movie.Path);
            var enrichedMovie = new MovieResponse
            {
                Id = movie.Id,
                RadarrId = movie.RadarrId,
                Title = movie.Title,
                FileName = movie.FileName,
                Path = movie.Path,
                DateAdded = movie.DateAdded,
                Images = movie.Images,
                Subtitles = subtitles
            };
            enrichedMovies.Add(enrichedMovie);
        }

        return new PagedResult<MovieResponse>
        {
            Items = enrichedMovies,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    
    /// <inheritdoc />
    public async Task<PagedResult<Show>> GetShows(
        string? searchQuery,
        string? orderBy,
        bool ascending,
        int pageNumber,
        int pageSize)
    {
        var query = _dbContext.Shows
            .Include(s => s.Images)
            .Include(s => s.Seasons)
            .ThenInclude(season => season.Episodes)
            .AsSplitQuery()
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            query = query.Where(s => s.Title.ToLower().Contains(searchQuery.ToLower()));
        }

        query = orderBy switch
        {
            "Id" => ascending ? query.OrderBy(s => s.Id) : query.OrderByDescending(s => s.Id),
            "Title" => ascending ? query.OrderBy(s => s.Title) : query.OrderByDescending(s => s.Title),
            "DateAdded" => ascending ? query.OrderBy(s => s.DateAdded) : query.OrderByDescending(s => s.DateAdded),
            _ => ascending ? query.OrderBy(s => s.Title) : query.OrderByDescending(s => s.Title)
        };

        var totalCount = await query.CountAsync();
        var shows = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Show>
        {
            Items = shows,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}