namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupResolverProvider<TKey>
    {
        bool CanResolve(ILookupMetadata<TKey> metadata);
        ILookupResult<TKey> Resolve(ILookupMetadata<TKey> metadata);
    }
}

/*
resolver registration
services.AddTransient<
    ILookupResolver<string>,
    SystemUserByEmployeeNumberResolver>();

services.AddTransient<
    ILookupResolver<Guid>,
    ContactByIdResolver>();

services.AddTransient<SystemUserQuery>();
services.AddTransient<ContactQuery>();


}
*/