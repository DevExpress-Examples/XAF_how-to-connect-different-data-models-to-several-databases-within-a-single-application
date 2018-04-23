using DevExpress.Xpo;
using DevExpress.ExpressApp.Updating;

namespace ClassLibrary2 {
    [MemberDesignTimeVisibility(false)]
    public class ModuleInfo2 : XPBaseObject, IModuleInfo {
        public ModuleInfo2(Session session) : base(session) { }
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
