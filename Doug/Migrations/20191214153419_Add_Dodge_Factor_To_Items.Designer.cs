﻿// <auto-generated />
using System;
using Doug;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doug.Migrations
{
    [DbContext(typeof(DougContext))]
    [Migration("20191214153419_Add_Dodge_Factor_To_Items")]
    partial class Add_Dodge_Factor_To_Items
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Doug.Items.Item", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSellable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTradable")
                        .HasColumnType("bit");

                    b.Property<int>("MaxStack")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<string>("TargetActionId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");
                });

            modelBuilder.Entity("Doug.Models.Channel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("Doug.Models.Coffee.CoffeeBreak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BotToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoffeeRemindJobId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FatCounter")
                        .HasColumnType("int");

                    b.Property<bool>("IsCoffeeBreak")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastCoffee")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CoffeeBreak");
                });

            modelBuilder.Entity("Doug.Models.Coffee.Roster", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsReady")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSkipping")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Roster");
                });

            modelBuilder.Entity("Doug.Models.DropTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Droptables");
                });

            modelBuilder.Entity("Doug.Models.GambleChallenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("RequesterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GambleChallenges");
                });

            modelBuilder.Entity("Doug.Models.Government", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RevolutionCooldown")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevolutionLeader")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RevolutionTimestamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ruler")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TaxRate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Government");
                });

            modelBuilder.Entity("Doug.Models.LootItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DropTableId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Probability")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id", "DropTableId");

                    b.HasIndex("DropTableId");

                    b.ToTable("LootItem");
                });

            modelBuilder.Entity("Doug.Models.Monsters.Monster", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AttackCooldown")
                        .HasColumnType("int");

                    b.Property<int>("CriticalHitChance")
                        .HasColumnType("int");

                    b.Property<int>("DamageType")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dodge")
                        .HasColumnType("int");

                    b.Property<string>("DropTableId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ExperienceValue")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("Hitrate")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxAttack")
                        .HasColumnType("int");

                    b.Property<int>("MinAttack")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Resistance")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DropTableId");

                    b.ToTable("Monsters");
                });

            modelBuilder.Entity("Doug.Models.Monsters.MonsterAttacker", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SpawnedMonsterId")
                        .HasColumnType("int");

                    b.Property<int>("DamageDealt")
                        .HasColumnType("int");

                    b.HasKey("UserId", "SpawnedMonsterId");

                    b.HasIndex("SpawnedMonsterId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("MonsterAttacker");
                });

            modelBuilder.Entity("Doug.Models.Monsters.RegionMonster", b =>
                {
                    b.Property<string>("ChannelId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MonsterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ChannelId", "MonsterId");

                    b.ToTable("RegionMonster");
                });

            modelBuilder.Entity("Doug.Models.Monsters.SpawnedMonster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AttackCooldown")
                        .HasColumnType("datetime2");

                    b.Property<string>("Channel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<string>("MonsterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MonsterId");

                    b.ToTable("SpawnedMonsters");
                });

            modelBuilder.Entity("Doug.Models.Party", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("Doug.Models.Recipe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Doug.Models.Shop", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceMultiplier")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Doug.Models.ShopItem", b =>
                {
                    b.Property<string>("ShopId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ItemId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ShopId", "ItemId");

                    b.ToTable("ShopItem");
                });

            modelBuilder.Entity("Doug.Models.Slurs.RecentFlame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SlurId")
                        .HasColumnType("int");

                    b.Property<string>("TimeStamp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RecentSlurs");
                });

            modelBuilder.Entity("Doug.Models.Slurs.Slur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Slurs");
                });

            modelBuilder.Entity("Doug.Models.User.InventoryItem", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("InventoryPosition")
                        .HasColumnType("int");

                    b.Property<string>("ItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("UserId", "InventoryPosition");

                    b.HasIndex("ItemId");

                    b.ToTable("InventoryItem");
                });

            modelBuilder.Entity("Doug.Models.User.Loadout", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BodyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BootsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GlovesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HeadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeftHandId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeftRingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NeckId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RightHandId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RightRingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SkillbookId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BodyId");

                    b.HasIndex("BootsId");

                    b.HasIndex("GlovesId");

                    b.HasIndex("HeadId");

                    b.HasIndex("LeftHandId");

                    b.HasIndex("LeftRingId");

                    b.HasIndex("NeckId");

                    b.HasIndex("RightHandId");

                    b.HasIndex("RightRingId");

                    b.HasIndex("SkillbookId");

                    b.ToTable("Loadout");
                });

            modelBuilder.Entity("Doug.Models.User.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<DateTime>("AttackCooldown")
                        .HasColumnType("datetime2");

                    b.Property<int>("Constitution")
                        .HasColumnType("int");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<int>("Energy")
                        .HasColumnType("int");

                    b.Property<long>("Experience")
                        .HasColumnType("bigint");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("LoadoutId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Luck")
                        .HasColumnType("int");

                    b.Property<int?>("PartyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SkillCooldown")
                        .HasColumnType("datetime2");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LoadoutId");

                    b.HasIndex("PartyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Doug.Models.User.UserEffect", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EffectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "EffectId");

                    b.ToTable("UserEffect");
                });

            modelBuilder.Entity("Doug.Items.Consumable", b =>
                {
                    b.HasBaseType("Doug.Items.Item");

                    b.HasDiscriminator().HasValue("Consumable");
                });

            modelBuilder.Entity("Doug.Items.EquipmentItem", b =>
                {
                    b.HasBaseType("Doug.Items.Item");

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("AgilityFactor")
                        .HasColumnType("int");

                    b.Property<int>("AgilityRequirement")
                        .HasColumnType("int");

                    b.Property<int>("AttackSpeed")
                        .HasColumnType("int");

                    b.Property<int>("AttackSpeedFactor")
                        .HasColumnType("int");

                    b.Property<int>("Constitution")
                        .HasColumnType("int");

                    b.Property<int>("ConstitutionFactor")
                        .HasColumnType("int");

                    b.Property<int>("ConstitutionRequirement")
                        .HasColumnType("int");

                    b.Property<int>("CooldownReduction")
                        .HasColumnType("int");

                    b.Property<int>("CriticalDamageFactor")
                        .HasColumnType("int");

                    b.Property<int>("CriticalHitChanceFactor")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("DefenseFactor")
                        .HasColumnType("int");

                    b.Property<int>("Dodge")
                        .HasColumnType("int");

                    b.Property<int>("DodgeFactor")
                        .HasColumnType("int");

                    b.Property<string>("EffectId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Energy")
                        .HasColumnType("int");

                    b.Property<int>("EnergyFactor")
                        .HasColumnType("int");

                    b.Property<int>("EnergyRegen")
                        .HasColumnType("int");

                    b.Property<int>("FlatEnergyRegen")
                        .HasColumnType("int");

                    b.Property<int>("FlatHealthRegen")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("HealthFactor")
                        .HasColumnType("int");

                    b.Property<int>("HealthRegen")
                        .HasColumnType("int");

                    b.Property<int>("HitRateFactor")
                        .HasColumnType("int");

                    b.Property<int>("Hitrate")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<int>("IntelligenceFactor")
                        .HasColumnType("int");

                    b.Property<int>("IntelligenceRequirement")
                        .HasColumnType("int");

                    b.Property<int>("LevelRequirement")
                        .HasColumnType("int");

                    b.Property<int>("Luck")
                        .HasColumnType("int");

                    b.Property<int>("LuckFactor")
                        .HasColumnType("int");

                    b.Property<int>("LuckRequirement")
                        .HasColumnType("int");

                    b.Property<int>("MaxAttack")
                        .HasColumnType("int");

                    b.Property<int>("MinAttack")
                        .HasColumnType("int");

                    b.Property<int>("Pierce")
                        .HasColumnType("int");

                    b.Property<int>("PierceFactor")
                        .HasColumnType("int");

                    b.Property<int>("Resistance")
                        .HasColumnType("int");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("StrengthFactor")
                        .HasColumnType("int");

                    b.Property<int>("StrengthRequirement")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("EquipmentItem");
                });

            modelBuilder.Entity("Doug.Items.Food", b =>
                {
                    b.HasBaseType("Doug.Items.Consumable");

                    b.Property<int>("EnergyAmount")
                        .HasColumnType("int");

                    b.Property<int>("HealthAmount")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Food");
                });

            modelBuilder.Entity("Doug.Items.Lootbox", b =>
                {
                    b.HasBaseType("Doug.Items.Consumable");

                    b.Property<string>("DropTableId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("DropTableId");

                    b.HasDiscriminator().HasValue("Lootbox");
                });

            modelBuilder.Entity("Doug.Items.Ticket", b =>
                {
                    b.HasBaseType("Doug.Items.Consumable");

                    b.Property<string>("Channel")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Ticket");
                });

            modelBuilder.Entity("Doug.Items.SkillBook", b =>
                {
                    b.HasBaseType("Doug.Items.EquipmentItem");

                    b.Property<string>("SkillId")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("SkillBook");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Weapon", b =>
                {
                    b.HasBaseType("Doug.Items.EquipmentItem");

                    b.Property<bool>("IsDualWield")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Weapon");
                });

            modelBuilder.Entity("Doug.Items.SpecialFood", b =>
                {
                    b.HasBaseType("Doug.Items.Food");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("EffectId")
                        .HasColumnName("SpecialFood_EffectId")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("SpecialFood");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Axe", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("Axe");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Bow", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("Bow");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Claws", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("Claws");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Dagger", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("Dagger");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.GreatSword", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("GreatSword");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Gun", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("Gun");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Shield", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("Shield");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Staff", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("Staff");
                });

            modelBuilder.Entity("Doug.Items.WeaponType.Sword", b =>
                {
                    b.HasBaseType("Doug.Items.WeaponType.Weapon");

                    b.HasDiscriminator().HasValue("Sword");
                });

            modelBuilder.Entity("Doug.Models.LootItem", b =>
                {
                    b.HasOne("Doug.Models.DropTable", null)
                        .WithMany("Items")
                        .HasForeignKey("DropTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Doug.Models.Monsters.Monster", b =>
                {
                    b.HasOne("Doug.Models.DropTable", "DropTable")
                        .WithMany()
                        .HasForeignKey("DropTableId");
                });

            modelBuilder.Entity("Doug.Models.Monsters.MonsterAttacker", b =>
                {
                    b.HasOne("Doug.Models.Monsters.SpawnedMonster", "Monster")
                        .WithMany("MonsterAttackers")
                        .HasForeignKey("SpawnedMonsterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doug.Models.User.User", "User")
                        .WithOne()
                        .HasForeignKey("Doug.Models.Monsters.MonsterAttacker", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Doug.Models.Monsters.RegionMonster", b =>
                {
                    b.HasOne("Doug.Models.Channel", "Channel")
                        .WithMany("Monsters")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Doug.Models.Monsters.SpawnedMonster", b =>
                {
                    b.HasOne("Doug.Models.Monsters.Monster", "Monster")
                        .WithMany()
                        .HasForeignKey("MonsterId");
                });

            modelBuilder.Entity("Doug.Models.Party", b =>
                {
                    b.HasOne("Doug.Models.User.User", "Leader")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Doug.Models.ShopItem", b =>
                {
                    b.HasOne("Doug.Models.Shop", "Shop")
                        .WithMany("ShopItems")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Doug.Models.User.InventoryItem", b =>
                {
                    b.HasOne("Doug.Items.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("Doug.Models.User.User", "User")
                        .WithMany("InventoryItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Doug.Models.User.Loadout", b =>
                {
                    b.HasOne("Doug.Items.EquipmentItem", "Body")
                        .WithMany()
                        .HasForeignKey("BodyId");

                    b.HasOne("Doug.Items.EquipmentItem", "Boots")
                        .WithMany()
                        .HasForeignKey("BootsId");

                    b.HasOne("Doug.Items.EquipmentItem", "Gloves")
                        .WithMany()
                        .HasForeignKey("GlovesId");

                    b.HasOne("Doug.Items.EquipmentItem", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId");

                    b.HasOne("Doug.Items.EquipmentItem", "LeftHand")
                        .WithMany()
                        .HasForeignKey("LeftHandId");

                    b.HasOne("Doug.Items.EquipmentItem", "LeftRing")
                        .WithMany()
                        .HasForeignKey("LeftRingId");

                    b.HasOne("Doug.Items.EquipmentItem", "Neck")
                        .WithMany()
                        .HasForeignKey("NeckId");

                    b.HasOne("Doug.Items.EquipmentItem", "RightHand")
                        .WithMany()
                        .HasForeignKey("RightHandId");

                    b.HasOne("Doug.Items.EquipmentItem", "RightRing")
                        .WithMany()
                        .HasForeignKey("RightRingId");

                    b.HasOne("Doug.Items.SkillBook", "Skillbook")
                        .WithMany()
                        .HasForeignKey("SkillbookId");
                });

            modelBuilder.Entity("Doug.Models.User.User", b =>
                {
                    b.HasOne("Doug.Models.User.Loadout", "Loadout")
                        .WithMany()
                        .HasForeignKey("LoadoutId");

                    b.HasOne("Doug.Models.Party", null)
                        .WithMany("Users")
                        .HasForeignKey("PartyId");
                });

            modelBuilder.Entity("Doug.Models.User.UserEffect", b =>
                {
                    b.HasOne("Doug.Models.User.User", "User")
                        .WithMany("Effects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Doug.Items.Lootbox", b =>
                {
                    b.HasOne("Doug.Models.DropTable", "DropTable")
                        .WithMany()
                        .HasForeignKey("DropTableId");
                });
#pragma warning restore 612, 618
        }
    }
}
