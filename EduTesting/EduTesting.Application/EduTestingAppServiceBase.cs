using Abp.Application.Services;

namespace EduTesting
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class EduTestingAppServiceBase : ApplicationService
    {
        protected EduTestingAppServiceBase()
        {
            LocalizationSourceName = EduTestingConsts.LocalizationSourceName;
        }
    }
}