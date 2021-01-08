using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Factory.Models
{
    public class Engineers
    {
        public Engineer()
        {
            this.Machines = new HashSet<EngineerMachine>();
        }
        
        public int EngineerId { get ; set; }
        public string EngineerName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime HireDate { get; set; }

        public virtual ICollection<EngineerMachine> Machines { get; set; }
    }
}