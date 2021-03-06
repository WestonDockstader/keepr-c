using System.Collections.Generic;
using keepr_c.Models;
using keepr_c.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr_c.Controllers
{
  [Route("[controller]")]
  public class VaultsController : Controller
  {
    private readonly VaultRepository db;
    public VaultsController(VaultRepository repo)
    { 
      db = repo; 
    }
    [HttpGet("{userId}")]
    public IEnumerable<Vault> GetAllByUserId(string userId)
    { 
      return db.GetVaultsByUserId(userId); 
    }
    [HttpPost]
    public Vault Create([FromBody]Vault newVault)
    { 
      // var user = HttpContext.User;
      // newVault.UserId = user.Identity.Name;
      if(ModelState.IsValid)
      {
        return db.CreateVault(newVault);
      }
      return null;
    }
    [HttpPut("{id}")]
    public void EditVault()
    {}
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      db.DeleteVault(id);
    }
  }
}