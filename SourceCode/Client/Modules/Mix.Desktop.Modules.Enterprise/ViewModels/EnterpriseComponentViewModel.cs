using Prism.Ioc;

namespace Mix.Desktop.Modules.Enterprise.ViewModels
{
    public class EnterpriseComponentViewModel : EnterpriseViewModel
    {
        #region Properties
    
        #endregion

        #region Fields
        #endregion
        #region Commands

        #endregion Commands
        #region Ctor

        public EnterpriseComponentViewModel(IContainerExtension container) : base(container)
        {
            
        }
        #endregion

        #region Methods

        protected override void RegisterCommands()
        {
        }

        #endregion
    }
}
