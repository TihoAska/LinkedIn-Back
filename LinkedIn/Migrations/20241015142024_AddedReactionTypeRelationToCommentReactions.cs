using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedIn.Migrations
{
    /// <inheritdoc />
    public partial class AddedReactionTypeRelationToCommentReactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CommentReactions_ReactionTypeId",
                table: "CommentReactions",
                column: "ReactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReactions_ReactionTypes_ReactionTypeId",
                table: "CommentReactions",
                column: "ReactionTypeId",
                principalTable: "ReactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_ReactionTypes_ReactionTypeId",
                table: "CommentReactions");

            migrationBuilder.DropIndex(
                name: "IX_CommentReactions_ReactionTypeId",
                table: "CommentReactions");
        }
    }
}
