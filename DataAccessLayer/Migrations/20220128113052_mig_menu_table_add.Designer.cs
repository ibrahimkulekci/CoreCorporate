﻿// <auto-generated />
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
    [Migration("20220128113052_mig_menu_table_add")]
    partial class mig_menu_table_add
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityLayer.Concrete.Catalog", b =>
                {
                    b.Property<int>("CatalogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CatalogContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CatalogCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CatalogImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CatalogPdfIframe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CatalogPdfUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CatalogStatus")
                        .HasColumnType("bit");

                    b.Property<string>("CatalogTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CatalogUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CatalogUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CatalogID");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Gallery", b =>
                {
                    b.Property<int>("GalleryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GalleryContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GalleryCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GalleryImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GalleryStatus")
                        .HasColumnType("bit");

                    b.Property<string>("GalleryTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GalleryUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GalleryUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GalleryID");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("EntityLayer.Concrete.GalleryImage", b =>
                {
                    b.Property<int>("GalleryImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<int>("GalleryId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("GalleryImageId");

                    b.ToTable("GalleryImages");
                });

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

            modelBuilder.Entity("EntityLayer.Concrete.Partner", b =>
                {
                    b.Property<int>("PartnerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PartnerContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PartnerCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PartnerImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PartnerStatus")
                        .HasColumnType("bit");

                    b.Property<string>("PartnerTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PartnerUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PartnerUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartnerID");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProductStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ProductTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EntityLayer.Concrete.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductCategoryContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductCategoryCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductCategoryImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProductCategoryStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ProductCategoryTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductCategoryUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductCategoryUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductCategoryID");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Reference", b =>
                {
                    b.Property<int>("ReferenceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ReferenceContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReferenceCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReferenceImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReferenceStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ReferenceTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReferenceUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReferenceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReferenceID");

                    b.ToTable("References");
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

            modelBuilder.Entity("EntityLayer.Concrete.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("TeamCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TeamDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamPosition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TeamStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TeamUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TeamUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
