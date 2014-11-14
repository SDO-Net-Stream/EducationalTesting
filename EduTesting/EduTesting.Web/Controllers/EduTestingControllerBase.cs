using Abp.Web.Mvc.Controllers;

namespace EduTesting.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class EduTestingControllerBase : AbpController
    {
        protected EduTestingControllerBase()
        {
            LocalizationSourceName = EduTestingConsts.LocalizationSourceName;
        }
    }
}