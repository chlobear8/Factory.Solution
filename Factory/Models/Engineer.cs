using System.Collections.Generic;

namespace Factory.Models
{
  public class Engineer
  {
    public string Name { get; set; }
    public int EngineerId { get; set; }
    public List<Assignment> JoinEntities { get; }
  }
}