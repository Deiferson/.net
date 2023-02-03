
/*
CREATE DATABASE [dbUser4]
GO

USE [dbUser4]
GO

CREATE TABLE [User] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
    [Nome] NVARCHAR(80) NOT NULL
)
*/


using Dapper;
using Microsoft.Data.SqlClient;

const string connectionString ="Server=localhost\\sqlexpress;Database=dbTeste; User ID=sa;Password=sa; TrustServerCertificate=True";

await using (var connection = new SqlConnection(connectionString))
{
    var id = await connection.ExecuteScalarAsync<int>("INSERT INTO [User] VALUES(@nome);SELECT CAST(scope_identity() AS INT)",
        new
        {
            nome = "teste"
        });
    
    Console.WriteLine(id);

    var users = await connection.QueryAsync("SELECT [Id], [Nome] FROM [User]");

    foreach (var user in users) 
        Console.WriteLine(user.Nome);

    await connection.ExecuteAsync(
    "UPDATE [User] SET [Nome]=@nome WHERE [Id]=@id",
    new { id = 1, nome = "teste2" });

    await connection.ExecuteAsync(
    "DELETE FROM [User] WHERE [Id]=@id",
    new { id = 1 });    
}






