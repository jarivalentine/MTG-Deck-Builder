using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Models;

public partial class MtgV1Context : DbContext
{
    public MtgV1Context()
    {
    }

    public MtgV1Context(DbContextOptions<MtgV1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardColor> CardColors { get; set; }

    public virtual DbSet<CardType> CardTypes { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<Rarity> Rarities { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=mtg_v1;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__artists__3213E83FC867953C");

            entity.ToTable("artists");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cards__3213E83F03749037");

            entity.ToTable("cards");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.ConvertedManaCost)
                .HasMaxLength(255)
                .HasColumnName("converted_mana_cost");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Flavor).HasColumnName("flavor");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Layout)
                .HasMaxLength(255)
                .HasColumnName("layout");
            entity.Property(e => e.ManaCost)
                .HasMaxLength(255)
                .HasColumnName("mana_cost");
            entity.Property(e => e.MtgId)
                .HasMaxLength(255)
                .HasColumnName("mtg_id");
            entity.Property(e => e.MultiverseId).HasColumnName("multiverse_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(255)
                .HasColumnName("number");
            entity.Property(e => e.OriginalImageUrl)
                .HasMaxLength(255)
                .HasColumnName("original_image_url");
            entity.Property(e => e.OriginalText).HasColumnName("original_text");
            entity.Property(e => e.OriginalType)
                .HasMaxLength(255)
                .HasColumnName("original_type");
            entity.Property(e => e.Power)
                .HasMaxLength(255)
                .HasColumnName("power");
            entity.Property(e => e.RarityCode)
                .HasMaxLength(255)
                .HasColumnName("rarity_code");
            entity.Property(e => e.SetCode)
                .HasMaxLength(255)
                .HasColumnName("set_code");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Toughness)
                .HasMaxLength(255)
                .HasColumnName("toughness");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Variations).HasColumnName("variations");

            entity.HasOne(d => d.Artist).WithMany(p => p.Cards)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK_cards_artists");

            entity.HasOne(d => d.RarityCodeNavigation).WithMany(p => p.Cards)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.RarityCode)
                .HasConstraintName("FK_cards_rarities");

            entity.HasOne(d => d.SetCodeNavigation).WithMany(p => p.Cards)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.SetCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cards_sets");
        });

        modelBuilder.Entity<CardColor>(entity =>
        {
            entity.HasKey(e => new { e.CardId, e.ColorId }).HasName("card_colors_card_id_color_id_primary");

            entity.ToTable("card_colors");

            entity.HasIndex(e => new { e.CardId, e.ColorId }, "card_colors_card_id_color_id_unique").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Card).WithMany(p => p.CardColors)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_card_colors_cards");

            entity.HasOne(d => d.Color).WithMany(p => p.CardColors)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_card_colors_colors");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasKey(e => new { e.CardId, e.TypeId }).HasName("card_types_card_id_type_id_primary");

            entity.ToTable("card_types");

            entity.HasIndex(e => new { e.CardId, e.TypeId }, "card_types_card_id_type_id_unique").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Card).WithMany(p => p.CardTypes)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_card_types_cards");

            entity.HasOne(d => d.Type).WithMany(p => p.CardTypes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_card_types_types");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__colors__3213E83F2D3B99AB");

            entity.ToTable("colors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__migratio__3213E83FC12ED5C9");

            entity.ToTable("migrations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batch).HasColumnName("batch");
            entity.Property(e => e.Migration1)
                .HasMaxLength(255)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personal__3213E83FF1366E32");

            entity.ToTable("personal_access_tokens");

            entity.HasIndex(e => e.Token, "personal_access_tokens_token_unique").IsUnique();

            entity.HasIndex(e => new { e.TokenableType, e.TokenableId }, "personal_access_tokens_tokenable_type_tokenable_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abilities).HasColumnName("abilities");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.LastUsedAt)
                .HasColumnType("datetime")
                .HasColumnName("last_used_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Token)
                .HasMaxLength(64)
                .HasColumnName("token");
            entity.Property(e => e.TokenableId).HasColumnName("tokenable_id");
            entity.Property(e => e.TokenableType)
                .HasMaxLength(255)
                .HasColumnName("tokenable_type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Rarity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rarities__3213E83F710C9509");

            entity.ToTable("rarities");

            entity.HasIndex(e => e.Code, "rarities_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sets__3213E83F0F72A017");

            entity.ToTable("sets");

            entity.HasIndex(e => e.Code, "sets_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__types__3213E83F62126D10");

            entity.ToTable("types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Type1)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
