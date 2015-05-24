using System;
using TMD.Models.DomainModels;

namespace TMD.Interfaces.Repository
{
    public interface IConfigurationRepository: IBaseRepository<Configuration, int>
    {
        string GetEbayLoadStartTimeFrom();
        int UpsertEbayLoadStartTimeFromConfiguration(DateTime ebayLoadStartTimeFrom);
    }
}
