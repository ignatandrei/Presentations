using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemos
{
    public partial class Department : AddRetrievedDate
    {
        [NotMapped]
        public DateTime? RetrievedDate { get ; set ; }
    }
}
