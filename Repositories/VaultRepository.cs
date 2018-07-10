using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_c.Models;


namespace keepr_c.Repositories
{
  public class VaultRepository:DbContext
  {
    public VaultRepository(IDbConnection db):base(db)
    {
    }
    public Vault CreateVault(Vault newVault)
    {
      int id=_db.ExecuteScalar<int>("INSERT INTO vaults (name,description,userId) VALUES(@Name,@Description,@UserId); SELECT LAST_INSERT_ID();", newVault);
      newVault.Id=id;
      return newVault;
    }
    // public void EditVault()
    // {
    //   _db.Execute("UPDATE vaults SET WHERE id=;");
    // }
    public void DeleteVault(int id)
    {
      _db.Execute("DELETE FROM vaults WHERE id=@id;",new{id});
    }
    public IEnumerable<Vault> GetAll()
    {
      return _db.Query<Vault>("SELECT * FROM vaults;");
    }
    public IEnumerable<Vault> GetVaultsByUserId(string userId)
    {
      return _db.Query<Vault>("SELECT * FROM vaults WHERE userId=@userId", new{userId});
    }
  }
}