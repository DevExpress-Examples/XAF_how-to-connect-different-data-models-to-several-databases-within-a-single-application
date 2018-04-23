using System;
using System.Linq;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;

namespace ClassLibrary2 {
    [DefaultClassOptions]
    public class PersistentClass2 : XPObject {
        public PersistentClass2(Session s) : base(s) { }
        public string PersistentProperty2 { get; set; }
    }
}
