using school.Data.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.core.features.student.query.Results
{
    public class GetStudentResult
    {

        public int StudID { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }


        public string? nameOfdept { get; set; }



    }
}
