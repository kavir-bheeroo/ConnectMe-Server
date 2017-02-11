using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ConnectMe.Api.Data;

namespace ConnectMe.Api.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170128202810_AddedNewDbContext")]
    partial class AddedNewDbContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConnectMe.Api.Models.UserInfo", b =>
                {
                    b.Property<string>("Id");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });
        }
    }
}
