using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;

namespace ClassLibrary2 {
    [DefaultClassOptions]
    public class PersistentClass2 : BaseObject {
        public virtual string PersistentPropertyX { get; set; }
    }
}
