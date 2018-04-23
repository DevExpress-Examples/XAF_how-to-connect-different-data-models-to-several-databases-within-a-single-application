using System;
using System.Linq;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;

namespace ClassLibrary1 {
    [DefaultClassOptions]
    public class PersistentClass1 : XPObject {
        public PersistentClass1(Session s) : base(s) { }
        public string PersistentProperty1 { get; set; }
    }
}
