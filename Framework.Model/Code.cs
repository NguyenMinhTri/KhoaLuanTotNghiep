using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Framework.Model
{

    [Table("Code")]
    public class Code : Auditable
    {
		public int Id { get; set; }
		public String Name { get; set; }
		public String Value { get; set; }
	}
}
