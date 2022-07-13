using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Community_BackEnd.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HidePersonalInfo = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentForumId = table.Column<int>(type: "int", nullable: true),
                    RestrictedRoleId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forums_AspNetRoles_RestrictedRoleId",
                        column: x => x.RestrictedRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Forums_Forums_ParentForumId",
                        column: x => x.ParentForumId,
                        principalTable: "Forums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserForum",
                columns: table => new
                {
                    ModeratedForumsId = table.Column<int>(type: "int", nullable: false),
                    ModeratorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserForum", x => new { x.ModeratedForumsId, x.ModeratorsId });
                    table.ForeignKey(
                        name: "FK_AppUserForum_AspNetUsers_ModeratorsId",
                        column: x => x.ModeratorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserForum_Forums_ModeratedForumsId",
                        column: x => x.ModeratedForumsId,
                        principalTable: "Forums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ForumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Topics_Forums_ForumId",
                        column: x => x.ForumId,
                        principalTable: "Forums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: true),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_News_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContextPostId = table.Column<int>(type: "int", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_ContextPostId",
                        column: x => x.ContextPostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b6d8dcf-6f66-439f-a49c-260fcc7da5d0", "7854d353-6f83-4901-8e3e-5b773dba6ab4", "Moderator", null },
                    { "2c788606-1f92-43e1-a297-cfca3da640c1", "269fd70f-bf2f-466d-9dc3-64f6bb288c20", "Administrator", null },
                    { "e017ee79-023b-43c7-94ef-03f0f0deb7e1", "ed2fdadc-0f61-4c59-8431-a08f19827166", "Writer", null },
                    { "e7036370-7cc5-42f4-9891-7511f4377e98", "94c40293-adff-47b0-84c6-c70ff6ce1b58", "User", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AboutMe", "AccessFailedCount", "ConcurrencyStamp", "CreationDate", "DisplayName", "Email", "EmailConfirmed", "Firstname", "HidePersonalInfo", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "06da8d10-341a-4917-a1dd-b45bc1f91a36", null, 0, "00adb204-22fe-47b0-b598-cc74b64d29dd", new DateTime(2022, 7, 13, 12, 38, 50, 762, DateTimeKind.Local).AddTicks(2143), "Brian", null, false, "Brian", true, false, null, null, null, null, null, false, "3177de6e-ed16-49b2-baa8-8d65935e2022", "Veliz", false, "Libre255" },
                    { "1db9b1da-35ab-4838-8bbc-35a14fdb4c42", null, 0, "2e646c40-694d-449c-9dc5-f7b748b0ed48", new DateTime(2022, 7, 13, 12, 38, 50, 762, DateTimeKind.Local).AddTicks(2163), "Benjamin", "godiset@gmail.com", false, "Benjamin", true, false, null, null, null, null, null, false, "e5677bed-1811-4860-883c-aab482feb8f0", "Nordin", false, "UnboundKey" },
                    { "738e0132-1648-48cf-b252-d8eade1e71a5", null, 0, "cef5c956-e8aa-4bcf-b5f3-b8efb092d3cc", new DateTime(2022, 7, 13, 12, 38, 50, 758, DateTimeKind.Local).AddTicks(9608), "Jens", "jens.eresund@gmail.com", false, "Jens", true, false, null, null, null, null, null, false, "b4256dd9-b33b-48fd-8921-e7bd3ad83ede", "Eresund", false, "jeres89" },
                    { "d88c8165-6ec6-436d-aba6-16ad3a7a98f0", null, 0, "b551ae66-ec06-4e1f-ae31-d0638a7bc9b5", new DateTime(2022, 7, 13, 12, 38, 50, 762, DateTimeKind.Local).AddTicks(2084), "Abel", "kokiabel1986@gmail.com", false, "Abel", true, false, null, null, null, null, null, false, "d9e5c878-d00a-4aac-8853-88d439b36651", "Magicho", false, "kembAB" }
                });

            migrationBuilder.InsertData(
                table: "Forums",
                columns: new[] { "Id", "Description", "Name", "ParentForumId", "RestrictedRoleId" },
                values: new object[] { 1, "The forum of all forums!", "Main", null, null });

            migrationBuilder.InsertData(
                table: "AppUserForum",
                columns: new[] { "ModeratedForumsId", "ModeratorsId" },
                values: new object[,]
                {
                    { 1, "06da8d10-341a-4917-a1dd-b45bc1f91a36" },
                    { 1, "1db9b1da-35ab-4838-8bbc-35a14fdb4c42" },
                    { 1, "738e0132-1648-48cf-b252-d8eade1e71a5" },
                    { 1, "d88c8165-6ec6-436d-aba6-16ad3a7a98f0" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2c788606-1f92-43e1-a297-cfca3da640c1", "06da8d10-341a-4917-a1dd-b45bc1f91a36" },
                    { "2c788606-1f92-43e1-a297-cfca3da640c1", "1db9b1da-35ab-4838-8bbc-35a14fdb4c42" },
                    { "2c788606-1f92-43e1-a297-cfca3da640c1", "738e0132-1648-48cf-b252-d8eade1e71a5" },
                    { "2c788606-1f92-43e1-a297-cfca3da640c1", "d88c8165-6ec6-436d-aba6-16ad3a7a98f0" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorId", "Categories", "Content", "EditDate", "PublishDate", "Title", "TopicId" },
                values: new object[] { 1, "738e0132-1648-48cf-b252-d8eade1e71a5", "{ Categories: ['News','Site Development'] }", "There are now news in the news feed. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut felis ante, consequat ac urna nec, pretium consequat diam. Donec imperdiet bibendum est sed luctus. Sed pretium, eros imperdiet laoreet dapibus, neque ante molestie metus, nec sagittis metus est ut velit. Nullam ut egestas diam. Curabitur accumsan diam ac lorem commodo, vitae condimentum ipsum porta. Duis ullamcorper, enim non hendrerit egestas, nunc libero auctor turpis, consequat accumsan ligula lacus eget dolor. Etiam tortor arcu, laoreet in iaculis ac, facilisis non libero. Morbi egestas massa mi, ut dictum tellus sagittis in. Morbi nec mattis elit. Aenean sit amet nisi viverra, dignissim magna.", null, new DateTime(2022, 7, 13, 12, 38, 50, 762, DateTimeKind.Local).AddTicks(9601), "There are News!", null });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "AuthorId", "CreationDate", "ForumId", "Title" },
                values: new object[] { 1, "738e0132-1648-48cf-b252-d8eade1e71a5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Hello World!" });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorId", "Categories", "Content", "EditDate", "PublishDate", "Title", "TopicId" },
                values: new object[] { 2, "d88c8165-6ec6-436d-aba6-16ad3a7a98f0", "{ Categories: ['News','Site Development'] }", "Ipsum dolor sit amet, consectetur adipiscing elit. Ut felis ante, consequat ac urna nec, pretium consequat diam. Donec imperdiet bibendum est sed luctus. Sed pretium, eros imperdiet laoreet dapibus, neque ante molestie metus, nec sagittis metus est ut velit. Nullam ut egestas diam. Curabitur accumsan diam ac lorem commodo, vitae condimentum ipsum porta. Duis ullamcorper, enim non hendrerit egestas, nunc libero auctor turpis, consequat accumsan ligula lacus eget dolor. Etiam tortor arcu, laoreet in iaculis ac, facilisis non libero. Morbi egestas massa mi, ut dictum tellus sagittis in. Morbi nec mattis elit. Aenean sit amet nisi viverra, dignissim magna.", null, new DateTime(2022, 7, 13, 12, 38, 50, 763, DateTimeKind.Local).AddTicks(1325), "Lorem Ipsum", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "ContextPostId", "EditDate", "PostDate", "TopicId" },
                values: new object[] { 1, "738e0132-1648-48cf-b252-d8eade1e71a5", "Hello World!", null, null, new DateTime(2022, 7, 13, 12, 34, 50, 762, DateTimeKind.Local).AddTicks(3651), 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "ContextPostId", "EditDate", "PostDate", "TopicId" },
                values: new object[] { 2, "d88c8165-6ec6-436d-aba6-16ad3a7a98f0", "Well met World!", 1, null, new DateTime(2022, 7, 13, 12, 35, 50, 762, DateTimeKind.Local).AddTicks(5158), 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "ContextPostId", "EditDate", "PostDate", "TopicId" },
                values: new object[] { 3, "06da8d10-341a-4917-a1dd-b45bc1f91a36", "How is the World weather?", 2, null, new DateTime(2022, 7, 13, 12, 36, 50, 762, DateTimeKind.Local).AddTicks(5557), 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "ContextPostId", "EditDate", "PostDate", "TopicId" },
                values: new object[] { 4, "1db9b1da-35ab-4838-8bbc-35a14fdb4c42", "The sun is shining, World!", 3, null, new DateTime(2022, 7, 13, 12, 37, 50, 762, DateTimeKind.Local).AddTicks(5564), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserForum_ModeratorsId",
                table: "AppUserForum",
                column: "ModeratorsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Forums_ParentForumId",
                table: "Forums",
                column: "ParentForumId");

            migrationBuilder.CreateIndex(
                name: "IX_Forums_RestrictedRoleId",
                table: "Forums",
                column: "RestrictedRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorId",
                table: "News",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_News_TopicId",
                table: "News",
                column: "TopicId",
                unique: true,
                filter: "[TopicId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ContextPostId",
                table: "Posts",
                column: "ContextPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TopicId",
                table: "Posts",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_AuthorId",
                table: "Topics",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ForumId",
                table: "Topics",
                column: "ForumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserForum");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Forums");

            migrationBuilder.DropTable(
                name: "AspNetRoles");
        }
    }
}
