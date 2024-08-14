using Refit;

namespace HMS.LogicProvider;

public interface BillsRefit
{
    [Get("/bills")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Get();

    [Get("/bills/bills-with-services")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> GetWithServices();

    [Get("/bills/bills-with-services-by-userid/{userId}")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> GetWithServicesByUserId(string userId);

    [Post("/bills")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<string>> Post();
}
