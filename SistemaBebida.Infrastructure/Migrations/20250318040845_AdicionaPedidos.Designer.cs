﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaBebida.Infrastructure.Persistence;

#nullable disable

namespace SistemaBebida.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250318040845_AdicionaPedidos")]
    partial class AdicionaPedidos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SistemaBebida.Domain.Entities.Contato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RevendaId")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RevendaId");

                    b.ToTable("Contatos");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.EnderecoEntrega", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RevendaId")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RevendaId");

                    b.ToTable("EnderecoEntrega");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.ItemPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PedidoClienteId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoClienteId");

                    b.ToTable("ItemPedido");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cliente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Observacao")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.PedidoCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CNPJCliente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RevendaId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("RevendaId");

                    b.ToTable("PedidosClientes");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.Revenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefones")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Revendas");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.Contato", b =>
                {
                    b.HasOne("SistemaBebida.Domain.Entities.Revenda", null)
                        .WithMany("Contatos")
                        .HasForeignKey("RevendaId");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.EnderecoEntrega", b =>
                {
                    b.HasOne("SistemaBebida.Domain.Entities.Revenda", null)
                        .WithMany("EnderecosEntrega")
                        .HasForeignKey("RevendaId");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.ItemPedido", b =>
                {
                    b.HasOne("SistemaBebida.Domain.Entities.PedidoCliente", null)
                        .WithMany("Itens")
                        .HasForeignKey("PedidoClienteId");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.PedidoCliente", b =>
                {
                    b.HasOne("SistemaBebida.Domain.Entities.Revenda", "Revenda")
                        .WithMany()
                        .HasForeignKey("RevendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Revenda");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.PedidoCliente", b =>
                {
                    b.Navigation("Itens");
                });

            modelBuilder.Entity("SistemaBebida.Domain.Entities.Revenda", b =>
                {
                    b.Navigation("Contatos");

                    b.Navigation("EnderecosEntrega");
                });
#pragma warning restore 612, 618
        }
    }
}
