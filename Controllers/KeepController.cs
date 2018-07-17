using Microsoft.AspNetCore.Mvc;
using keepr_c.Repositories;
using System.Collections.Generic;
using keepr_c.Models;

namespace keepr_c.Controllers
{
  [Route("[controller]")]
  public class KeepsController:Controller
  {
    private readonly KeepsRepository db;
    public KeepsController(KeepsRepository repo)
    {
      db=repo;
    }
    [HttpGet]
    public IEnumerable<Keep> GetAll()
    {
      return db.GetAll();
    }
    [HttpGet("{userId}")]
    public IEnumerable<Keep> GetAllByUserId(string userId)
    { 
      return db.GetKeepsByUserId(userId); 
    }
    [HttpPost]
    public Keep Create([FromBody]Keep newKeep)
    { 
      // var user = HttpContext.User;
      // newKeep.UserId = user.Identity.Name;
      if(ModelState.IsValid)
      {
        return db.CreateKeep(newKeep);
      }
      return null;
    }
    [HttpPut("{id}")]
    public void EditKeep([FromBody]Keep aKeep)
    {
      db.EditKeep(aKeep);
    }
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      db.DeleteKeep(id);
    }
  }
}