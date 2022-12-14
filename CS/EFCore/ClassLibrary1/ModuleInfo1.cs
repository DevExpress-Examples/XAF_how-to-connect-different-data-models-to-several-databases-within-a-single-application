using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl.EF;

namespace ClassLibrary1 {
    public class ModuleInfo1 : BaseObject, IModuleInfo {
        public virtual string Version { get; set; }
        public virtual string Name { get; set; }
        public virtual string AssemblyFileName { get; set; }
        public virtual bool IsMain { get; set; }
        public override string ToString() {
            return !string.IsNullOrEmpty(Name) ? Name : base.ToString();
        }
    }
}
