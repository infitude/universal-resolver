using System.Threading.Tasks;

namespace UniResolver.Services
{
    public interface IDidResolverService
    {
        DecentralizedIdentifierDescriptionObject ResolveDid(string did);
        Task<DecentralizedIdentifierDescriptionObject> ResolveDidAsync(string did);
    }
}