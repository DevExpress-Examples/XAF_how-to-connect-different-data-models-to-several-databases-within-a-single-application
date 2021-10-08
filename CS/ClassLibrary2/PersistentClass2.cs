using System;
using System.Linq;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;

namespace ClassLibrary2 {
    [DefaultClassOptions]
    public class PersistentClass2 : XPObject {
        public PersistentClass2(Session session) : base(session) { }
        private string _PersistentPropertyX;
        public string PersistentPropertyX {
            get { return _PersistentPropertyX; }
            set { SetPropertyValue<string>(nameof(PersistentPropertyX), ref _PersistentPropertyX, value); }
        }
    }
}
