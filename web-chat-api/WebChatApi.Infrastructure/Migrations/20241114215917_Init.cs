using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebChatApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    avatar_url = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    stack = table.Column<List<string>>(type: "text[]", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    last_online = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    current_status = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    git_hub_url = table.Column<string>(type: "text", nullable: true),
                    linked_in_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "channels",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    avatar_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channels", x => x.id);
                    table.ForeignKey(
                        name: "fk_channels_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "group_chats",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    avatar_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_group_chats", x => x.id);
                    table.ForeignKey(
                        name: "fk_group_chats_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personal_messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    attachment_url = table.Column<string>(type: "text", nullable: true),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false),
                    edited_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    recipient_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personal_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_personal_messages_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_personal_messages_users_recipient_id",
                        column: x => x.recipient_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "channel_messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    attachment_url = table.Column<string>(type: "text", nullable: true),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false),
                    edited_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    channel_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channel_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_channel_messages_channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_messages_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "channel_users",
                columns: table => new
                {
                    channel_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channel_users", x => new { x.channel_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_channel_users_channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_users_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_chat_messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    attachment_url = table.Column<string>(type: "text", nullable: true),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false),
                    edited_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    group_chat_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_group_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_group_chat_messages_group_chats_group_chat_id",
                        column: x => x.group_chat_id,
                        principalTable: "group_chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_group_chat_messages_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "group_chat_users",
                columns: table => new
                {
                    group_chat_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_group_chat_users", x => new { x.group_chat_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_group_chat_users_group_chats_group_chat_id",
                        column: x => x.group_chat_id,
                        principalTable: "group_chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_group_chat_users_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_channel_messages_author_id",
                table: "channel_messages",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_channel_messages_channel_id",
                table: "channel_messages",
                column: "channel_id");

            migrationBuilder.CreateIndex(
                name: "ix_channel_users_user_id",
                table: "channel_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_channels_creator_id",
                table: "channels",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_group_chat_messages_author_id",
                table: "group_chat_messages",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_group_chat_messages_group_chat_id",
                table: "group_chat_messages",
                column: "group_chat_id");

            migrationBuilder.CreateIndex(
                name: "ix_group_chat_users_user_id",
                table: "group_chat_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_group_chats_creator_id",
                table: "group_chats",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_personal_messages_author_id",
                table: "personal_messages",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_personal_messages_recipient_id",
                table: "personal_messages",
                column: "recipient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "channel_messages");

            migrationBuilder.DropTable(
                name: "channel_users");

            migrationBuilder.DropTable(
                name: "group_chat_messages");

            migrationBuilder.DropTable(
                name: "group_chat_users");

            migrationBuilder.DropTable(
                name: "personal_messages");

            migrationBuilder.DropTable(
                name: "channels");

            migrationBuilder.DropTable(
                name: "group_chats");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
