using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaApi.Models;
using CopaHAS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CopaHAS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Jogador> TB_JOGADORES { get; set; }
        public DbSet<Estadio> TB_ESTADIO { get; set; }
        public DbSet<Selecao> TB_SELECAO { get; set; }
        public DbSet<Tecnico> TB_TECNICO { get; set; }
        public DbSet<Jogo> TB_JOGO { get; set; }
        public DbSet<JogoSelecao> TB_JOGO_SELECAO { get; set; }
         
	      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogador>().ToTable("TB_JOGADORES");
            modelBuilder.Entity<Estadio>().ToTable("TB_ESTADIO");
            modelBuilder.Entity<Selecao>().ToTable("TB_SELECAO");
            modelBuilder.Entity<Tecnico>().ToTable("TB_TECNICO");
            modelBuilder.Entity<Jogo>().ToTable("TB_JOGO");
            modelBuilder.Entity<JogoSelecao>().ToTable("TB_JOGO_SELECAO");

            // SELECAO            
            modelBuilder.Entity<Selecao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Pais)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.Pais)
                      .IsRequired()
                      .HasMaxLength(100);
            });
            
            // JOGADOR (1:N com Selecao)            
            modelBuilder.Entity<Jogador>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.Posicao)
                      .HasMaxLength(50);
                entity.HasOne(d => d.SelecaoIdNavegacao)
                      .WithMany(p => p.Jogadores)
                      .HasForeignKey(d => d.SelecaoId)
                      .OnDelete(DeleteBehavior.Cascade);
                      
            });
            
            // TECNICO (1:1 com Selecao)            
            modelBuilder.Entity<Tecnico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasOne(d => d.SelecaoIdNavegacao)
                      .WithOne(p => p.Tecnico)
                      .HasForeignKey<Tecnico>(d => d.SelecaoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            
            // ESTADIO            
            modelBuilder.Entity<Estadio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(e => e.Cidade)
                      .HasMaxLength(100);
            });
            
            // JOGO (1:N com Estadio)            
            modelBuilder.Entity<Jogo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataHora)
                      .IsRequired();
                entity.HasOne(d => d.EstadioIdNavegacao)
                      .WithMany(p => p.Jogos)
                      .HasForeignKey(d => d.EstadioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            
            // JOGO-SELECÕES (N:N)            
            modelBuilder.Entity<JogoSelecao>(entity =>
            {
                entity.HasKey(e => new { e.JogoId, e.SelecaoId });
                entity.HasOne(d => d.JogoIdNavegacao)
                      .WithMany(p => p.JogoSelecoes)
                      .HasForeignKey(d => d.JogoId);

                entity.HasOne(d => d.SelecaoIdNavegacao)
                      .WithMany(p => p.JogoSelecoes)
                      .HasForeignKey(d => d.SelecaoId);
            });
            
                

            modelBuilder.Entity<Jogador>().HasData
            (
                new Jogador(){ Id=1, Nome="Hugo Souza",NumeroCamisa=1,Posicao="Goleiro",Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=2, Nome="Yuri Alberto",NumeroCamisa=9,Posicao="Atacante",Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=3, Nome="Danilo", NumeroCamisa=2, Posicao="Lateral Direito", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=4, Nome="Marquinhos", NumeroCamisa=4, Posicao="Zagueiro", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=5, Nome="Casemiro", NumeroCamisa=5, Posicao="Volante", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=6, Nome="Alex Sandro", NumeroCamisa=6, Posicao="Lateral Esquerdo", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=7, Nome="Lucas Paquetá", NumeroCamisa=7, Posicao="Meio Campo", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=8, Nome="Bruno Guimarães", NumeroCamisa=8, Posicao="Meio Campo", Status=Models.Enuns.StatusJogador.Reserva },
                new Jogador(){ Id=9, Nome="Richarlison", NumeroCamisa=10, Posicao="Atacante", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=10, Nome="Vinicius Jr", NumeroCamisa=11, Posicao="Atacante", Status=Models.Enuns.StatusJogador.Titular },
                new Jogador(){ Id=11, Nome="Rodrygo", NumeroCamisa=19, Posicao="Atacante", Status=Models.Enuns.StatusJogador.DepartamentoMedico },
                new Jogador(){ Id=12, Nome="Alisson", NumeroCamisa=23, Posicao="Goleiro", Status=Models.Enuns.StatusJogador.NaoRelacionado }
            );
            
            modelBuilder.Entity<Estadio>().HasData(

                new Estadio(){ Id=1, Nome="Santiago Bernabeu", Cidade="Madrid", Capacidade=75000},
                new Estadio(){ Id=2, Nome="Camp Nou", Cidade="Barcelona", Capacidade=80000},
                new Estadio(){ Id=3, Nome="Maracanã ", Cidade="Rio de Janeiro", Capacidade=72000},
                new Estadio(){ Id=4, Nome="Morumbi", Cidade="São Paulo", Capacidade=70000},
                new Estadio(){ Id=5, Nome="Mineirão", Cidade="Belo Horizonte", Capacidade=60000 },
                new Estadio(){ Id=6, Nome="Castelão", Cidade="Fortaleza", Capacidade=63000},
                new Estadio(){ Id=7, Nome="Estadio Asteca", Cidade="Cidade do México", Capacidade=80000 }
            );

            //Área para futuros inserts no banco de dados a partir de outras classes/objetos
        }

        
        
        

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .HaveColumnType("varchar").HaveMaxLength(200);

            base.ConfigureConventions(configurationBuilder);
        }

        //Inserir as linhas "new Jogador(){ Id = 1, ..." das lista de jogadores

    }
}

