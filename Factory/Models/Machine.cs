using System.Collections.Generic;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    public string Name { get; set; }
    public List<Assignment> JoinEntities { get; set; }
  }
}