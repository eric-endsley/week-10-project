using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Factory.Models
{
    public class Engineer
    {
        public Engineer()
        {
            this.Machines = new HashSet<EngineerMachine>();
        }
        
        public int EngineerId { get ; set; }
        public string EngineerFirstName { get; set; }
        public string EngineerLastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime HireDate { get; set; }

        public virtual ICollection<EngineerMachine> Machines { get; set; }
    }
}