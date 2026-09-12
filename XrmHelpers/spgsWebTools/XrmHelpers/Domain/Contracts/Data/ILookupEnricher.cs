using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupEnricher<TDto>
    {
        TDto Execute(TDto dto);
    }
}