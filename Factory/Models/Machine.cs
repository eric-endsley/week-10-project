using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Machine
    {
        public Machine()
        {
            this.Engineers = new HashSet<EngineerMachine>();
        }

        public int MachineId { get; set; }

        public string MachineName{ get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InstallDate { get; set; }


        public virtual ICollection<EngineerMachine> Engineers { get; set; }
    }
}