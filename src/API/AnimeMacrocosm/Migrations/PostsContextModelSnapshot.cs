﻿// <auto-generated />
using System;
using AnimeMacrocosm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnimeMacrocosm.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class PostsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnimeMacrocosm.Models.CreatorAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("SeriesItemId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesItemId");

                    b.ToTable("CreatorAuthors");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Distributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("DistributorName");

                    b.HasKey("Id");

                    b.ToTable("Distributors");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Format", b =>
                {
                    b.Property<int>("FormatId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FormatName");

                    b.HasKey("FormatId");

                    b.ToTable("Formats");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreType");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageCaption");

                    b.Property<string>("ImageDimensions");

                    b.Property<string>("ImageFormat");

                    b.Property<string>("ImagePath");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PostContent");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("PostTitle")
                        .HasMaxLength(100);

                    b.Property<int?>("UserId");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.ProductionStudio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("ProductionStudioName");

                    b.HasKey("Id");

                    b.ToTable("ProductionStudios");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Series", b =>
                {
                    b.Property<int>("SeriesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("SeriesId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.SeriesCreator", b =>
                {
                    b.Property<int>("CreatorId");

                    b.Property<int>("SeriesId");

                    b.HasKey("CreatorId", "SeriesId");

                    b.HasIndex("SeriesId");

                    b.ToTable("SeriesCreators");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.SeriesImage", b =>
                {
                    b.Property<int>("SeriesId");

                    b.Property<int>("ImageId");

                    b.HasKey("SeriesId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("SeriesImages");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.SeriesItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatorAuthorId");

                    b.Property<string>("Description");

                    b.Property<int>("DistributorId");

                    b.Property<int>("FormatId");

                    b.Property<string>("Length");

                    b.Property<int>("ProductionId");

                    b.Property<DateTime?>("ReleaseDate");

                    b.Property<int>("SeriesId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId");

                    b.HasIndex("FormatId");

                    b.HasIndex("ProductionId");

                    b.HasIndex("SeriesId");

                    b.ToTable("SeriesItems");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.SeriesItemImage", b =>
                {
                    b.Property<int>("SeriesItemId");

                    b.Property<int>("ImageId");

                    b.HasKey("SeriesItemId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("SeriesItemImages");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserEmailAddress");

                    b.Property<string>("UserScreenName")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.CreatorAuthor", b =>
                {
                    b.HasOne("AnimeMacrocosm.Models.SeriesItem")
                        .WithMany("CreatorAuthors")
                        .HasForeignKey("SeriesItemId");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Post", b =>
                {
                    b.HasOne("AnimeMacrocosm.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.SeriesCreator", b =>
                {
                    b.HasOne("AnimeMacrocosm.Models.CreatorAuthor", "CreatorAuthor")
                        .WithMany("SeriesCreators")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnimeMacrocosm.Models.Series", "Series")
                        .WithMany("SeriesCreators")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.SeriesImage", b =>
                {
                    b.HasOne("AnimeMacrocosm.Models.Image", "Image")
                        .WithMany("SeriesImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnimeMacrocosm.Models.Series", "Series")
                        .WithMany("SeriesImages")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.SeriesItem", b =>
                {
                    b.HasOne("AnimeMacrocosm.Models.Distributor", "Distributor")
                        .WithMany("SeriesItems")
                        .HasForeignKey("DistributorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnimeMacrocosm.Models.Format", "Format")
                        .WithMany()
                        .HasForeignKey("FormatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnimeMacrocosm.Models.ProductionStudio", "ProductionStudio")
                        .WithMany("SeriesItems")
                        .HasForeignKey("ProductionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnimeMacrocosm.Models.Series", "Series")
                        .WithMany("SeriesItems")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.SeriesItemImage", b =>
                {
                    b.HasOne("AnimeMacrocosm.Models.Image", "Image")
                        .WithMany("SeriesItemImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnimeMacrocosm.Models.SeriesItem", "SeriesItem")
                        .WithMany("SeriesItemImages")
                        .HasForeignKey("SeriesItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
