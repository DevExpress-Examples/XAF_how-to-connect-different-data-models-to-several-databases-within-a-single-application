using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ClassLibrary2 {
    [DefaultClassOptions]
    public class PersistentClass2 : BaseObject { 
        public PersistentClass2(Session session)
            : base(session) {
        }

        private string _PersistentProperty2A;
        public string PersistentProperty2A {
            get { return _PersistentProperty2A; }
            set { SetPropertyValue(nameof(PersistentProperty2A), ref _PersistentProperty2A, value); }
        }

        private string _PersistentProperty2B;
        public string PersistentProperty2B {
            get { return _PersistentProperty2B; }
            set { SetPropertyValue(nameof(PersistentProperty2B), ref _PersistentProperty2B, value); }
        }
    }
}