﻿// <auto-generated />
using System;
using AnimeMacrocosm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnimeMacrocosm.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20181231025345_AddedUser")]
    partial class AddedUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnimeMacrocosm.Models.ApplicationUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserEmailAddress");

                    b.Property<string>("UserScreenName")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserRefId");

                    b.Property<string>("PostContent");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("PostTitle")
                        .HasMaxLength(100);

                    b.HasKey("PostId");

                    b.HasIndex("ApplicationUserRefId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("AnimeMacrocosm.Models.Post", b =>
                {
                    b.HasOne("AnimeMacrocosm.Models.ApplicationUser", "Users")
                        .WithMany()
                        .HasForeignKey("ApplicationUserRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
