using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Building
    {
        private int Id { get; set; }

        public Building(int id)
        {
            Id = id;
        }

        public int GetId() => Id;
    }
}
