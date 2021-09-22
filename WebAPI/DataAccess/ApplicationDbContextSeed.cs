using ChallengeBackend.WebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeBackend.WebAPI.DataAccess
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser
            {
                UserName = "user@localhost",
                Email = "user@localhost"
            };

            await userManager.CreateAsync(user, password: "User123;");
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext dbContext)
        {
            if(!dbContext.Movies.Any())
            {
                Genre warlike = new() { Name = "Bélico" };
                Genre action = new() { Name = "Acción" };
                Genre drama = new() { Name = "Drama" };
                Genre blackHumor = new() { Name = "Humor negro" };
                Genre comedy = new() { Name = "Comedia" };
                Genre biographical = new() { Name = "Biográfico" };


                #region Movie: Inglourious Basterds
                Movie inglouriousBasterds = new()
                {
                    Title = "Bastardos sin gloria",
                    Release = new DateTime(2009, 08, 31),
                    Rating = 4.5,
                    Image = "https://localhost:44363/Movies/c50548fc-1805-497d-8c23-ff3dc630e182.jpg"
                };

                dbContext.MovieGenres.AddRange(
                        new MovieGenre
                        {
                            Genre = warlike,
                            Movie = inglouriousBasterds
                        },
                        new MovieGenre
                        {
                            Genre = action,
                            Movie = inglouriousBasterds
                        },
                        new MovieGenre
                        {
                            Genre = drama,
                            Movie = inglouriousBasterds
                        },
                        new MovieGenre
                        {
                            Genre = blackHumor,
                            Movie = inglouriousBasterds
                        }
                    );

                dbContext.MovieCharacters.AddRange(
                        new MovieCharacter
                        {
                            Movie = inglouriousBasterds,
                            Character = new Character
                            {
                                Name = "Brad Pitt",
                                Age = new DateTime(DateTime.Now.Subtract(new DateTime(1963, 12, 18)).Ticks).Year - 1,
                                Story = "",
                                Image = "https://localhost:44363/Characters/e3540775-5f25-4241-a8a5-021733a5e37d.jpg"
                            }
                        },
                        new MovieCharacter
                        {
                            Movie = inglouriousBasterds,
                            Character = new Character
                            {
                                Name = "Christoph Waltz",
                                Age = new DateTime(DateTime.Now.Subtract(new DateTime(1956, 10, 04)).Ticks).Year - 1,
                                Story = "",
                                Image = "https://localhost:44363/Characters/442aeeb7-cbbd-4b51-91cf-9cf24b80c92f.jpg"
                            }
                        },
                        new MovieCharacter
                        {
                            Movie = inglouriousBasterds,
                            Character = new Character
                            {
                                Name = "Michael Fassbender",
                                Age = new DateTime(DateTime.Now.Subtract(new DateTime(1977, 04, 02)).Ticks).Year - 1,
                                Story = "",
                                Image = "https://localhost:44363/Characters/f618d591-767a-4df2-b7dd-2ee60ba2630d.jpg"
                            },
                        },
                        new MovieCharacter
                        {
                            Movie = inglouriousBasterds,
                            Character = new Character
                            {
                                Name = "Eli Roth",
                                Age = new DateTime(DateTime.Now.Subtract(new DateTime(1972, 04, 18)).Ticks).Year - 1,
                                Story = "",
                                Image = "https://localhost:44363/Characters/5c36126b-d4f7-4329-8a98-edb9940b4fdf.jpg"
                            }
                        },
                        new MovieCharacter
                        {
                            Movie = inglouriousBasterds,
                            Character = new Character
                            {
                                Name = "Diane Kruger",
                                Age = new DateTime(DateTime.Now.Subtract(new DateTime(1976, 07, 15)).Ticks).Year - 1,
                                Story = "",
                                Image = "https://localhost:44363/Characters/f7d300e4-9cb4-4d49-a289-ce7186723968.jpg"
                            }
                        },
                        new MovieCharacter
                        {
                            Movie = inglouriousBasterds,
                            Character = new Character
                            {
                                Name = "Daniel Brühl",
                                Age = new DateTime(DateTime.Now.Subtract(new DateTime(1978, 06, 16)).Ticks).Year - 1,
                                Story = "",
                                Image = "https://localhost:44363/Characters/3d0c6d91-8a02-47c3-80fd-4a461c0b43c1.jpg"
                            }
                        },
                        new MovieCharacter
                        {
                            Movie = inglouriousBasterds,
                            Character = new Character
                            {
                                Name = "Til Schweiger",
                                Age = new DateTime(DateTime.Now.Subtract(new DateTime(1963, 12, 19)).Ticks).Year - 1,
                                Story = "",
                                Image = "https://localhost:44363/Characters/bc7e5c19-eac1-462c-a06f-d6fe5f2b47bb.jpg"
                            }
                        },
                        new MovieCharacter
                        {
                            Movie = inglouriousBasterds,
                            Character = new Character
                            {
                                Name = "Mélanie Laurent",
                                Age = new DateTime(DateTime.Now.Subtract(new DateTime(1983, 02, 21)).Ticks).Year - 1,
                                Story = "",
                                Image = "https://localhost:44363/Characters/b597c9f1-1b51-452e-ae7b-f60d4aa32144.jpg"
                            }
                        }
                    );
                #endregion


                #region Movie: The Wolf of Wall Street
                Movie TheWolfOfWallStreet = new()
                {
                    Title = "El lobo de Wall Street",
                    Release = new DateTime(2014, 01, 02),
                    Rating = 4.6,
                    Image = "https://localhost:44363/Movies/ec622fcc-fe1b-45d6-946d-67be63bad362.jpg"
                };

                dbContext.MovieGenres.AddRange(
                    new MovieGenre
                    {
                        Genre = comedy,
                        Movie = TheWolfOfWallStreet
                    },
                    new MovieGenre
                    {
                        Genre = drama,
                        Movie = TheWolfOfWallStreet
                    },
                    new MovieGenre
                    {
                        Genre = blackHumor,
                        Movie = TheWolfOfWallStreet
                    },
                    new MovieGenre
                    {
                        Genre = biographical,
                        Movie = TheWolfOfWallStreet
                    }
                );

                dbContext.MovieCharacters.AddRange(
                    new MovieCharacter
                    {
                        Movie = TheWolfOfWallStreet,
                        Character = new Character
                        {
                            Name = "Leonardo DiCaprio",
                            Age = new DateTime(DateTime.Now.Subtract(new DateTime(1974, 11, 11)).Ticks).Year - 1,
                            Story = "",
                            Image = "https://localhost:44363/Characters/f5c00817-4654-485e-8533-cc27721ac39c.jpg"
                        }
                    },
                    new MovieCharacter
                    {
                        Movie = TheWolfOfWallStreet,
                        Character = new Character
                        {
                            Name = "Jonah Hill",
                            Age = new DateTime(DateTime.Now.Subtract(new DateTime(1983, 12, 20)).Ticks).Year - 1,
                            Story = "",
                            Image = "https://localhost:44363/Characters/31942028-de31-4004-8eff-6034a7df2ab1.jpg"
                        }
                    },
                    new MovieCharacter
                    {
                        Movie = TheWolfOfWallStreet,
                        Character = new Character
                        {
                            Name = "Margot Robbie",
                            Age = new DateTime(DateTime.Now.Subtract(new DateTime(1990, 07, 02)).Ticks).Year - 1,
                            Story = "",
                            Image = "https://localhost:44363/Characters/7b63fdb9-d10e-4c62-b6b9-13d8aaa56322.jpg"
                        }
                    },
                    new MovieCharacter
                    {
                        Movie = TheWolfOfWallStreet,
                        Character = new Character
                        {
                            Name = "Kyle Chandler",
                            Age = new DateTime(DateTime.Now.Subtract(new DateTime(1965, 09, 17)).Ticks).Year - 1,
                            Story = "",
                            Image = "https://localhost:44363/Characters/b5605ca4-389a-47a3-8227-3e65dd6a9853.jpg"
                        }
                    },
                    new MovieCharacter
                    {
                        Movie = TheWolfOfWallStreet,
                        Character = new Character
                        {
                            Name = "Rob Reiner",
                            Age = new DateTime(DateTime.Now.Subtract(new DateTime(1947, 03, 06)).Ticks).Year - 1,
                            Story = "",
                            Image = "https://localhost:44363/Characters/ea99a7f3-8b82-489c-8851-34666186b01d.jpg"
                        }
                    },
                    new MovieCharacter
                    {
                        Movie = TheWolfOfWallStreet,
                        Character = new Character
                        {
                            Name = "Jon Bernthal",
                            Age = new DateTime(DateTime.Now.Subtract(new DateTime(1976, 09, 20)).Ticks).Year - 1,
                            Story = "",
                            Image = "https://localhost:44363/Characters/909af834-acc5-484f-a8db-f34e12893090.jpg"
                        }
                    },
                    new MovieCharacter
                    {
                        Movie = TheWolfOfWallStreet,
                        Character = new Character
                        {
                            Name = "Jean Dujardin",
                            Age = new DateTime(DateTime.Now.Subtract(new DateTime(1972, 06, 19)).Ticks).Year - 1,
                            Story = "",
                            Image = "https://localhost:44363/Characters/9bf1be12-e3e3-4e88-9d8e-b6ab559b26d9.jpg"
                        }
                    }
                );
                #endregion

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
