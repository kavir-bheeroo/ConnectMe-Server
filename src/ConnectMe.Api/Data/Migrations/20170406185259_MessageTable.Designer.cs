using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ConnectMe.Api.Data;

namespace ConnectMe.Api.Migrations.Database
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170406185259_MessageTable")]
    partial class MessageTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConnectMe.Api.Models.Message", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<string>("FromUserId");

                    b.Property<bool>("IsRead");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("ConnectMe.Api.Models.UserInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image")
                        .HasColumnType("varchar(MAX)");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("MessagingToken");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("ConnectMe.Api.Models.Worker", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId");

                    b.Property<int>("WorkerTypeId");

                    b.HasKey("Id");

                    b.ToTable("Worker");
                });

            modelBuilder.Entity("ConnectMe.Api.Models.WorkerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("WorkerType");
                });
        }
    }
}
