﻿// <auto-generated />
using System;
using DddAndEfCore.School;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DddAndEfCore.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DddAndEfCore.School.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CourseID");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Course", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("1b302e6f-81d6-4add-b8ef-424a4f685dd1"),
                            Name = "Calculus"
                        },
                        new
                        {
                            Id = new Guid("5d77a202-5c7a-44a3-99f9-f3fe3d34a0d3"),
                            Name = "Chemistry"
                        });
                });

            modelBuilder.Entity("DddAndEfCore.School.Enrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EnrollmentID");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollment", (string)null);
                });

            modelBuilder.Entity("DddAndEfCore.School.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StudentID");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FavoriteCourseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FavoriteCourseId");

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("DddAndEfCore.School.Suffix", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SuffixID");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suffix", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("314687c5-576c-48c2-bd7b-db56f7b6a552"),
                            Name = "Jr"
                        },
                        new
                        {
                            Id = new Guid("bab15095-f4a3-4f36-a2b1-8e552e09407b"),
                            Name = "Sr"
                        });
                });

            modelBuilder.Entity("DddAndEfCore.School.Enrollment", b =>
                {
                    b.HasOne("DddAndEfCore.School.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("DddAndEfCore.School.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DddAndEfCore.School.Student", b =>
                {
                    b.HasOne("DddAndEfCore.School.Course", "FavoriteCourse")
                        .WithMany()
                        .HasForeignKey("FavoriteCourseId");

                    b.OwnsOne("DddAndEfCore.School.Student.Name#DddAndEfCore.School.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("First")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("Last")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("LastName");

                            b1.Property<Guid?>("NameSuffixID")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("NameSuffixID");

                            b1.HasKey("StudentId");

                            b1.HasIndex("NameSuffixID");

                            b1.ToTable("Student", (string)null);

                            b1.HasOne("DddAndEfCore.School.Suffix", "Suffix")
                                .WithMany()
                                .HasForeignKey("NameSuffixID");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");

                            b1.Navigation("Suffix");
                        });

                    b.Navigation("FavoriteCourse");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("DddAndEfCore.School.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
