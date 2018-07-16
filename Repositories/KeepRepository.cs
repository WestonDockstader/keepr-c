using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_c.Models;

namespace keepr_c.Repositories
{
  public class KeepsRepository:DbContext
  {
    public KeepsRepository(IDbConnection db):base(db)
    {}
    public Keep CreateKeep(Keep newKeep)
    {
      int id=_db.ExecuteScalar<int>("INSERT INTO keeps (name,description,address,userId,privatepublic,views,keeps) VALUES(@Name,@Description,@Address,@UserId,@Private,@Views,@Keeps); SELECT LAST_INSERT_ID();", newKeep);
      newKeep.Id=id;
      return newKeep;
    }
    public void EditKeep(Keep eKeep)
    {
      _db.Execute("UPDATE keeps SET description=@Description, privatepublic=@Private, name=@Name, views=@Views, keeps=@Keeps WHERE id=@id",eKeep);
    }
    public void DeleteKeep(int id)
    {
      _db.Execute("DELETE FROM keeps WHERE id=@id",new{id});
    }
    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM keeps;");
    }
    public IEnumerable<Keep> GetKeepsByUserId(string userId)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE userId=@userId", new{userId});
    }
    public void AddtoVault(int keepId, int vaultId, string userId)
    {
      _db.Execute("INSERT INTO vaultkeeps (vaultId,keepId,userId) VALUES (@vaultId,@keepId,@userId", new{vaultId,keepId,userId});
    }
    public void RemoveFromVault(int id)
    {
      _db.Execute("DELETE FROM vaultkeeps WHERE id=@id",new{id});
    }
  }
}