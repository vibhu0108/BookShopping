using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Ecomm_Offical.DataAccess.Migrations
{
    public partial class AddSPCoverTypeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE Create_CoverType @name varchar(50) As insert CoverType values(@name)");
            migrationBuilder.Sql(@"CREATE PROCEDURE Update_CoverType @id int, @name varchar(50) As Update CoverType Set name = @name Where id =@id");
            migrationBuilder.Sql(@"CREATE PROCEDURE Delete_CoverType @id int As delete from CoverType Where id = @id");
            migrationBuilder.Sql(@"CREATE PROCEDURE Get_CoverTypes As Select* from CoverType");
            migrationBuilder.Sql(@"CREATE PROCEDURE Get_CoverType @id int As Select* from CoverType Where id = @id ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
