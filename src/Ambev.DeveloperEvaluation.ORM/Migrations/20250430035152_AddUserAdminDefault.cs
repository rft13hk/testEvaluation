using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAdminDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (@"
                INSERT INTO public.""Users""
                (""Id"", ""Username"", ""Email"", ""Phone"", ""Password"", ""Role"", ""Status"", ""CreatedAt"", ""UpdatedAt"")
                VALUES
                (
                    '07b1a7fc-ebe1-4ef4-b014-ca4605e75af6', 
                    'admin', 
                    'admin@ambev.com', 
                    '11998876655', 
                    '$2a$11$YyaTpVm0PqGe1hOU8gdjeOkcTeLwSiPbNGyLjH1NB.Ybvvq8Ca9/.', 
                    3, 
                    1, 
                    now(), 
                    now()
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DELETE FROM ""Users""
            WHERE Email = 'admin@ambev.com';
        ");
        }
    }
}
