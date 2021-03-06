// <auto-generated />
using System;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211128204214_mig_table_news_add")]
    partial class mig_table_news_add
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityLayer.Concrete.News", b =>
                {
                    b.Property<int>("NewsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NewsContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NewsCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewsImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NewsStatus")
                        .HasColumnType("bit");

                    b.Property<string>("NewsTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NewsUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewsUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewsID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Page", b =>
                {
                    b.Property<int>("PageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PageCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PageStatus")
                        .HasColumnType("bit");

                    b.Property<string>("PageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PageUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PageID");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Service", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ServiceCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("ServiceContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ServiceCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ServiceStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ServiceTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ServiceUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("EntityLayer.Concrete.ServiceCategory", b =>
                {
                    b.Property<int>("ServiceCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ServiceCategoryContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ServiceCategoryCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ServiceCategoryStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ServiceCategoryTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ServiceCategoryUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceCategoryUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceCategoryID");

                    b.ToTable("ServiceCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
