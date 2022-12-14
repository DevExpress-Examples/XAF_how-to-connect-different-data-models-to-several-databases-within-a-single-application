using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;

namespace ClassLibrary1 {
    [DefaultClassOptions]
    public class PersistentClass1 : BaseObject {
        public virtual string PersistentProperty1A { get; set; }
        public virtual string PersistentProperty1B { get; set; }
    }
}
