using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecondSplitWise.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "group_payer",
                columns: table => new
                {
                    grouppayerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    expenseID = table.Column<int>(nullable: false),
                    user_expense_id = table.Column<int>(nullable: false),
                    paid_share = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_payer", x => x.grouppayerID);
                });

            migrationBuilder.CreateTable(
                name: "single_payer",
                columns: table => new
                {
                    singlepayerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    expenseID = table.Column<int>(nullable: false),
                    user_expense_id = table.Column<int>(nullable: false),
                    paid_share = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_single_payer", x => x.singlepayerID);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    userID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "friend",
                columns: table => new
                {
                    friendID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userID = table.Column<int>(nullable: false),
                    fID = table.Column<int>(nullable: false),
                    FrienduserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friend", x => x.friendID);
                    table.ForeignKey(
                        name: "FK_friend_user_FrienduserID",
                        column: x => x.FrienduserID,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_friend_user_userID",
                        column: x => x.userID,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    groupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    group_name = table.Column<string>(nullable: true),
                    created_by = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.groupID);
                    table.ForeignKey(
                        name: "FK_group_user_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_transaction",
                columns: table => new
                {
                    grouptransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    grouptransPayerID = table.Column<int>(nullable: true),
                    grouptransReceiverID = table.Column<int>(nullable: true),
                    groupID = table.Column<int>(nullable: true),
                    paid_share = table.Column<decimal>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_transaction", x => x.grouptransactionID);
                    table.ForeignKey(
                        name: "FK_group_transaction_group_groupID",
                        column: x => x.groupID,
                        principalTable: "group",
                        principalColumn: "groupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_group_transaction_user_grouptransPayerID",
                        column: x => x.grouptransPayerID,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_group_transaction_user_grouptransReceiverID",
                        column: x => x.grouptransReceiverID,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "member",
                columns: table => new
                {
                    membersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userID = table.Column<int>(nullable: true),
                    groupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member", x => x.membersID);
                    table.ForeignKey(
                        name: "FK_member_group_groupID",
                        column: x => x.groupID,
                        principalTable: "group",
                        principalColumn: "groupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_member_user_userID",
                        column: x => x.userID,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    paymentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    payerID = table.Column<int>(nullable: true),
                    commonmemberID = table.Column<int>(nullable: true),
                    groupID = table.Column<int>(nullable: true),
                    payment_amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.paymentID);
                    table.ForeignKey(
                        name: "FK_payment_user_commonmemberID",
                        column: x => x.commonmemberID,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payment_group_groupID",
                        column: x => x.groupID,
                        principalTable: "group",
                        principalColumn: "groupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payment_user_payerID",
                        column: x => x.payerID,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "expense",
                columns: table => new
                {
                    expenseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    expenseName = table.Column<string>(nullable: true),
                    created_by = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    groupID = table.Column<int>(nullable: true),
                    paymentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense", x => x.expenseID);
                    table.ForeignKey(
                        name: "FK_expense_user_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expense_group_groupID",
                        column: x => x.groupID,
                        principalTable: "group",
                        principalColumn: "groupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_expense_payment_paymentID",
                        column: x => x.paymentID,
                        principalTable: "payment",
                        principalColumn: "paymentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expense_member",
                columns: table => new
                {
                    expenseMemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    expenseID = table.Column<int>(nullable: true),
                    commonmemberID = table.Column<int>(nullable: true),
                    payableAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense_member", x => x.expenseMemberID);
                    table.ForeignKey(
                        name: "FK_expense_member_user_commonmemberID",
                        column: x => x.commonmemberID,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_expense_member_expense_expenseID",
                        column: x => x.expenseID,
                        principalTable: "expense",
                        principalColumn: "expenseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_expense",
                columns: table => new
                {
                    userexpenseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    expenseID = table.Column<int>(nullable: true),
                    user_expense_id = table.Column<int>(nullable: true),
                    paid_share = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_expense", x => x.userexpenseID);
                    table.ForeignKey(
                        name: "FK_user_expense_expense_expenseID",
                        column: x => x.expenseID,
                        principalTable: "expense",
                        principalColumn: "expenseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_expense_user_user_expense_id",
                        column: x => x.user_expense_id,
                        principalTable: "user",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expense_created_by",
                table: "expense",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_expense_groupID",
                table: "expense",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_expense_paymentID",
                table: "expense",
                column: "paymentID");

            migrationBuilder.CreateIndex(
                name: "IX_expense_member_commonmemberID",
                table: "expense_member",
                column: "commonmemberID");

            migrationBuilder.CreateIndex(
                name: "IX_expense_member_expenseID",
                table: "expense_member",
                column: "expenseID");

            migrationBuilder.CreateIndex(
                name: "IX_friend_FrienduserID",
                table: "friend",
                column: "FrienduserID");

            migrationBuilder.CreateIndex(
                name: "IX_friend_userID",
                table: "friend",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_group_created_by",
                table: "group",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_group_transaction_groupID",
                table: "group_transaction",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_group_transaction_grouptransPayerID",
                table: "group_transaction",
                column: "grouptransPayerID");

            migrationBuilder.CreateIndex(
                name: "IX_group_transaction_grouptransReceiverID",
                table: "group_transaction",
                column: "grouptransReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_member_groupID",
                table: "member",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_member_userID",
                table: "member",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_payment_commonmemberID",
                table: "payment",
                column: "commonmemberID");

            migrationBuilder.CreateIndex(
                name: "IX_payment_groupID",
                table: "payment",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_payment_payerID",
                table: "payment",
                column: "payerID");

            migrationBuilder.CreateIndex(
                name: "IX_user_expense_expenseID",
                table: "user_expense",
                column: "expenseID");

            migrationBuilder.CreateIndex(
                name: "IX_user_expense_user_expense_id",
                table: "user_expense",
                column: "user_expense_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expense_member");

            migrationBuilder.DropTable(
                name: "friend");

            migrationBuilder.DropTable(
                name: "group_payer");

            migrationBuilder.DropTable(
                name: "group_transaction");

            migrationBuilder.DropTable(
                name: "member");

            migrationBuilder.DropTable(
                name: "single_payer");

            migrationBuilder.DropTable(
                name: "user_expense");

            migrationBuilder.DropTable(
                name: "expense");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
