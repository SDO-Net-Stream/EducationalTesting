using Abp.Web.Mvc.Views;

namespace EduTesting.Web.Views
{
    public abstract class EduTestingWebViewPageBase : EduTestingWebViewPageBase<dynamic>
    {

    }

    public abstract class EduTestingWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected EduTestingWebViewPageBase()
        {
            LocalizationSourceName = EduTestingConsts.LocalizationSourceName;
        }
    }
}