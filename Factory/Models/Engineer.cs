using System.Collections.Generic;

namespace Factory.Models
{
  public class Engineer
  {
    public string Name { get; set; }
    public int EngineerId { get; set; }
    public Machine Machine { get; set; }
    public int MachineId { get; set; }
  }
}