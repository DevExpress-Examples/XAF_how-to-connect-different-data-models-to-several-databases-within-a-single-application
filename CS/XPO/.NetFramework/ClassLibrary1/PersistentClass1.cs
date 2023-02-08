using System;
using System.Linq;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;

namespace ClassLibrary1 {
    [DefaultClassOptions]
    public class PersistentClass1 : XPObject {
        public PersistentClass1(Session session) : base(session) { }
        private string _PersistentProperty1A;
        public string PersistentProperty1A {
            get { return _PersistentProperty1A; }
            set { SetPropertyValue<string>(nameof(PersistentProperty1A), ref _PersistentProperty1A, value); }
        }
        private string _PersistentProperty1B;
        public string PersistentProperty1B {
            get { return _PersistentProperty1B; }
            set { SetPropertyValue<string>(nameof(PersistentProperty1B), ref _PersistentProperty1B, value); }
        }
    }
}
