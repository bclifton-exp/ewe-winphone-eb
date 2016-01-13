using System.Threading;
using System.Threading.Tasks;
using Expedia.Entities.Suggestions;

namespace Expedia.Services.Interfaces
{
    public interface ISuggestionService
    {
        Task<SuggestionsResponse> Suggest(CancellationToken ct, string query, SuggestionLob lob);
        Task<SuggestionsResponse> Suggest(CancellationToken ct, double latitude, double longitude, SuggestionLob lob);
    }
}
