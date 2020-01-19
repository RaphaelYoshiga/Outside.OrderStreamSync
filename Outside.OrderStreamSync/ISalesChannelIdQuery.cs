using System.Threading.Tasks;

namespace Outside.OrderStreamSync
{
    public interface ISalesChannelIdQuery
    {
        Task<int> GetIdBy(string salesChannelDescription);
    }
}