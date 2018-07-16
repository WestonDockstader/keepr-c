using System.Data;
using keepr_c.Models;

namespace keepr_c.Repositories
{
  public class VaultKeepsRepository:DbContext
  {
    public VaultKeepsRepository(IDbConnection db):base(db)
    {}
    public VaultKeep CreateVaultKeep(VaultKeep newVaultKeep)
    {
      int id=_db.ExecuteScalar<int>("INSERT INTO vaultkeeps (vaultId,keepId,userId) VALUES (@VaultId,@KeepId,@UserId); SELECT LAST_INSERT_ID();",newVaultKeep);
      newVaultKeep.Id=id;
      return newVaultKeep;
    }
    public void EditVaultKeep(VaultKeep eVaultKeep)
    {

    }
    public void DeleteVaultKeep(int id)
    {
      _db.Execute("DELETE FROM vaultkeeps WHERE id=@id;",new{id});
    }
    public IEnumerable<VaultKeep> GetVaultKeepsByUserId(string userId)
    {
      return _db.Query<VaultKeep>("SELECT * FROM vaultkeeps WHERE userId=@userId",new{userId});
    }
  }
}