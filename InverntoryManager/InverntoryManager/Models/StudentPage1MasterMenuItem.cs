using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverntoryManager
{

    public class StudentPageMasterMenuItem
    {
        public StudentPageMasterMenuItem()
        {
            TargetType = typeof(StudentPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}