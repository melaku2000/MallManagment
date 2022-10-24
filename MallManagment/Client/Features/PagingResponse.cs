using MallManagment.Shared.Models.RequestFeatures;

namespace MallManagment.Client.Features
{
    public class PagingResponse<T> where T : class
    {
        public List<T>? Items { get; set; }
        public MetaData? MetaData { get; set; }
    }
}
