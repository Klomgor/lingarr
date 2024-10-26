﻿// <auto-generated />
using System;
using Lingarr.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lingarr.Migrations.SQLite.Migrations
{
    [DbContext(typeof(LingarrDbContext))]
    [Migration("20241021105457_MappingRework")]
    partial class MappingRework
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("Lingarr.Core.Entities.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("INTEGER")
                        .HasColumnName("episode_number");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT")
                        .HasColumnName("file_name");

                    b.Property<string>("MediaHash")
                        .HasColumnType("TEXT")
                        .HasColumnName("media_hash");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT")
                        .HasColumnName("path");

                    b.Property<int>("SeasonId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("season_id");

                    b.Property<int>("SonarrId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("sonarr_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_episodes");

                    b.HasIndex("SeasonId")
                        .HasDatabaseName("ix_episodes_season_id");

                    b.ToTable("episodes", (string)null);
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int?>("MovieId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("movie_id");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("path");

                    b.Property<int?>("ShowId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("show_id");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_images");

                    b.HasIndex("MovieId")
                        .HasDatabaseName("ix_images_movie_id");

                    b.HasIndex("ShowId")
                        .HasDatabaseName("ix_images_show_id");

                    b.ToTable("images", (string)null);
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_added");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("file_name");

                    b.Property<string>("MediaHash")
                        .HasColumnType("TEXT")
                        .HasColumnName("media_hash");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("path");

                    b.Property<int>("RadarrId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("radarr_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_movies");

                    b.ToTable("movies", (string)null);
                });

            modelBuilder.Entity("Lingarr.Core.Entities.PathMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<string>("DestinationPath")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("destination_path");

                    b.Property<int>("MediaType")
                        .HasColumnType("INTEGER")
                        .HasColumnName("media_type");

                    b.Property<string>("SourcePath")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("source_path");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_path_mappings");

                    b.ToTable("path_mappings", (string)null);
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT")
                        .HasColumnName("path");

                    b.Property<int>("SeasonNumber")
                        .HasColumnType("INTEGER")
                        .HasColumnName("season_number");

                    b.Property<int>("ShowId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("show_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_seasons");

                    b.HasIndex("ShowId")
                        .HasDatabaseName("ix_seasons_show_id");

                    b.ToTable("seasons", (string)null);
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Setting", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("key");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("value");

                    b.HasKey("Key")
                        .HasName("pk_settings");

                    b.ToTable("settings", (string)null);
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_added");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("path");

                    b.Property<int>("SonarrId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("sonarr_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_shows");

                    b.ToTable("shows", (string)null);
                });

            modelBuilder.Entity("Lingarr.Core.Entities.TranslationRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("completed_at");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<string>("JobId")
                        .HasColumnType("TEXT")
                        .HasColumnName("job_id");

                    b.Property<int?>("MovieId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("movie_id");

                    b.Property<int?>("ShowId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("show_id");

                    b.Property<string>("SourceLanguage")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("source_language");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER")
                        .HasColumnName("status");

                    b.Property<string>("SubtitleToTranslate")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("subtitle_to_translate");

                    b.Property<string>("TargetLanguage")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("target_language");

                    b.Property<string>("TranslatedSubtitle")
                        .HasColumnType("TEXT")
                        .HasColumnName("translated_subtitle");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_translation_requests");

                    b.HasIndex("MovieId")
                        .HasDatabaseName("ix_translation_requests_movie_id");

                    b.HasIndex("ShowId")
                        .HasDatabaseName("ix_translation_requests_show_id");

                    b.ToTable("translation_requests", (string)null);
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Episode", b =>
                {
                    b.HasOne("Lingarr.Core.Entities.Season", "Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_episodes_seasons_season_id");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Image", b =>
                {
                    b.HasOne("Lingarr.Core.Entities.Movie", "Movie")
                        .WithMany("Images")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_images_movies_movie_id");

                    b.HasOne("Lingarr.Core.Entities.Show", "Show")
                        .WithMany("Images")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_images_shows_show_id");

                    b.Navigation("Movie");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Season", b =>
                {
                    b.HasOne("Lingarr.Core.Entities.Show", "Show")
                        .WithMany("Seasons")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_seasons_shows_show_id");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Lingarr.Core.Entities.TranslationRequest", b =>
                {
                    b.HasOne("Lingarr.Core.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("fk_translation_requests_movies_movie_id");

                    b.HasOne("Lingarr.Core.Entities.Show", "Show")
                        .WithMany()
                        .HasForeignKey("ShowId")
                        .HasConstraintName("fk_translation_requests_shows_show_id");

                    b.Navigation("Movie");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Movie", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Season", b =>
                {
                    b.Navigation("Episodes");
                });

            modelBuilder.Entity("Lingarr.Core.Entities.Show", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Seasons");
                });
#pragma warning restore 612, 618
        }
    }
}