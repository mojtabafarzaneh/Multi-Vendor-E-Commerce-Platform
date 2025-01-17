using Hangfire;

namespace Multi_VendorE_CommercePlatform;

public class CustomJobActivator : JobActivator
{
    private readonly IServiceProvider _serviceProvider;

    public CustomJobActivator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override object ActivateJob(Type jobType)
    {
        return _serviceProvider.GetRequiredService(jobType);
    }
}