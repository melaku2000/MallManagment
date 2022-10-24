using MallManagment.Client.Features;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Models.RequestFeatures;

namespace MallManagment.Client.Services
{
    public interface IFanaHttpService<T> where T : class
    {
        Task<List<T>> GetLists(string url);
        Task<T> GetById(string url, string id);
        Task<PagingResponse<T>> GetPagedList(string url, PageParameter pageParameters);
        Task<ResponseDto<T>> Create(string url, T item);
        Task<ResponseDto<T>> Update(string url, T item);
        Task<ResponseDto<T>> Delete(string url, string id);
    }
}
