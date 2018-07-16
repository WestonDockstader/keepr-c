using System.Collections.Generic;
using keepr_c.Models;
using keepr_c.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr_c.Controllers
{
  [Route("[controller]")]
  public class VaultKeepsController:Controller
  {
    private readonly VaultKeepsRepository db;
    public VaultKeepsController(VaultKeepsRepository repo)
    {
      db=repo;
    }
    // [HttpGet("{userId}")]
    // public IEnumerable<VaultKeep> GetAllByUserId(string userId)
    // {
    //   return db.GetVaultKeepsByUserId(userId);
    // }
    [HttpPost]
    public VaultKeep Create([FromBody]VaultKeep newVaultKeep)
    {
      if(ModelState.IsValid)
      {
        return db.CreateVaultKeep(newVaultKeep);
      }
      return null;
    }
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      db.DeleteVaultKeep(id);
    }
    [HttpPut("{id}")]
    public void EditVaultKeep()
    {}
    [HttpGet("vk/{id}")]
    public IEnumerable<Keep> GetVaultKeeps(int id)
    {
      return db.GetVaultKeeps(id);
    }
  }
}