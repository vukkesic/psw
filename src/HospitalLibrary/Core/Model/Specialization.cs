using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Specialization
    {
        public int Id { get; set; }
        public string SpecName { get; set; }
        public Specialization() { }
        public Specialization(int id, string specName)
        {
            Id = id;
            SpecName = specName;
        }
    }
}
