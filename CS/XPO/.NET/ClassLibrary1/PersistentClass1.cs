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

namespace ClassLibrary1 {
    [DefaultClassOptions]
    public class PersistentClass1 : BaseObject { 
        public PersistentClass1(Session session)
            : base(session) {
        }

        private string _PersistentProperty1A;
        public string PersistentProperty1A {
            get { return _PersistentProperty1A; }
            set { SetPropertyValue(nameof(PersistentProperty1A), ref _PersistentProperty1A, value); }
        }

        private string _PersistentProperty1B;
        public string PersistentProperty1B {
            get { return _PersistentProperty1B; }
            set { SetPropertyValue(nameof(PersistentProperty1B), ref _PersistentProperty1B, value); }
        }
    }
}