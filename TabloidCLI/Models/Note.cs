using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.Models
{
    class Note
    {

        public int Id { get; set; } 

        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }

    }
}
