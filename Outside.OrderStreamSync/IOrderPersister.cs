using System.Threading.Tasks;

namespace Outside.OrderStreamSync
{
    public interface IOrderPersister
    {
        Task Persist(BackOfficeOrder backOfficeOrder);
    }
}