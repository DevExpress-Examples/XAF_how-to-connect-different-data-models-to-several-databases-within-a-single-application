using DevExpress.Xpo;
using System.Configuration;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Updating;

namespace ClassLibrary1 {
    [MemberDesignTimeVisibility(false)]
    public class ModuleInfo1 : XPBaseObject, IModuleInfo {
        public ModuleInfo1(Session session) : base(session) { }
        [Key(true)]
        public int ID { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public string AssemblyFileName { get; set; }
        public bool IsMain { get; set; }
        public override string ToString() {
            return !string.IsNullOrEmpty(Name) ? Name : base.ToString();
        }
    }
}
