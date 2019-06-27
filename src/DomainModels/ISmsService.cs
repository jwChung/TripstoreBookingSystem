using Tripstore.DomainModels;

namespace Tripstore
{
    public interface ISmsService
    {
        void Send(MobileNumber receiver, string message);
    }
}