using IdentityModel.Client;
using Mix.Windows.WPF;
using Prism.Ioc;
using Refit;
using System;
using System.Net.Http;

namespace Mix.Desktop.Modules.Enterprise.ViewModels
{
    public class EnterpriseComponentViewModel : ViewModelBase, IViewLoadedAndUnloadedAware
    {
        #region Ctor

        public EnterpriseComponentViewModel(IContainerExtension container) : base(container)
        {
        }

        public void OnLoaded()
        {
            var accessToken = System.Windows.Application.Current.Properties["AccessToken"].ToString();
            var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5002") };
            client.SetBearerToken(accessToken);
            Container.RegisterInstance(RestService.For<IEnterpriseApi>(client));
        }

        public void OnUnloaded()
        {
        }

        #endregion Ctor
    }
}