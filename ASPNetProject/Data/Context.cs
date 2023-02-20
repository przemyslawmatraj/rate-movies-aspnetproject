﻿using ASPNetProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
                .HasKey(am => new { am.ActorId, am.MovieId });

            modelBuilder.Entity<ActorMovie>().HasOne(m => m.Movie)
                .WithMany(am => am.ActorMovies)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<ActorMovie>().HasOne(a => a.Actor)
                .WithMany(am => am.ActorsMovies)
                .HasForeignKey(a => a.ActorId);



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        
    }
}
