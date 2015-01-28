using System.Collections.Generic;

namespace Timesheet.Micro.Models.Domain.Model
{
    public abstract class PersistentObject
    {
        public virtual int Id { get; set; }

        public virtual bool IsPersistent
        {
            get { return isPersistentObject(); }
        }

        public override bool Equals(object obj)
        {
            if (obj != null && GetType().Equals(obj.GetType()) && isPersistentObject())
            {
                var persistentObject = obj as PersistentObject;
                return (persistentObject != null) && (Id == persistentObject.Id);
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return isPersistentObject() ? Id.GetHashCode() : base.GetHashCode();
        }
        
        private bool isPersistentObject()
        {
            return (Id != 0);
        }

        public static bool operator ==(PersistentObject obj1, PersistentObject obj2)
        {
            return Equals(obj1, obj2);
        }

        public static bool operator !=(PersistentObject obj1, PersistentObject obj2)
        {
            return !(obj1 == obj2);
        }

        public abstract IEnumerable<string> FieldsToSave();
    }
}
