using System.ComponentModel.DataAnnotations;

namespace DoctorTime.API.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        protected BaseEntity(long id)
        {
            Id = id;
        }

        protected BaseEntity()
        {
        }
    }
}
