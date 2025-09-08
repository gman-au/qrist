using System;
using Qrist.Domain;

namespace Qrist.Interfaces
{
    public interface ISessionCache
    {
        void Store(Guid id, string state, string qrCodeData, string accessToken = null);

        SessionStateItem RetrieveByState(string state);

        SessionStateItem RetrieveById(Guid id);
    }
}