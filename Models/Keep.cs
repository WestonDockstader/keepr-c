namespace keepr_c.Models
{
  public class Keep
  {
    public int Id { get;set; }
    public string Name { get;set; }
    public string Description { get;set; }
    public string Address { get;set; }
    public string UserId { get;set; }
    public string Shareable { get;set; } // changed from int
    public int Views { get;set; }
    public int Keeps { get;set; }
  }
}