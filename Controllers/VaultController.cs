using System.Collections.Generic;
using keepr_c.Models;
using keepr_c.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr_c.Controllers
{
  [Route("[controller]")]
  public class VaultController : Controller
  {
    private readonly VaultRepository db;
    public VaultController(VaultRepository repo)
    { 
      db = repo; 
    }
    [HttpGet]
    public IEnumerable<Vault> GetAll()
    { 
      return db.GetAll(); 
    }
    [HttpGet("{userId}")]
    public IEnumerable<Vault> GetAllByUserId(string userId)
    { 
      return db.GetVaultsByUserId(userId); 
    }
    [HttpPost]
    public Vault Create([FromBody]Vault newVault)
    { 
      if(ModelState.IsValid)
      {
        return db.CreateVault(newVault);
      }
      return null;
    }
  }
}