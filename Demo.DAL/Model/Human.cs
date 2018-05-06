using System.Collections.Generic;

namespace Demo.DAL.Model
{
    public class Human : BaseModel
    {
        // Key
        public override int Id { get; set; }

        // Property
        public string Name { get; set; }
        public string Description { get; set; }


        // Navigation
        public virtual Pupil Pupil { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
