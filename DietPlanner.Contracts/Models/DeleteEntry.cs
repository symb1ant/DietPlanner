using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Contracts.Models;
public class DeleteEntry
{
    public long Id { get; set; }
    public string UserID { get; set; }
}
