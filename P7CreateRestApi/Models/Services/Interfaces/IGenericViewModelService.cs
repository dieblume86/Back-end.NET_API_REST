namespace Dot.Net.WebApi.Models.Services.Interfaces
{
    public interface IGenericViewModelService<Entity, ViewModel>
        where Entity : class
        where ViewModel : class
    {
        int Add(ViewModel viewModel);
        Entity Get(int id);

        void Update(ViewModel viewModel);

        void Delete(int id);
    }
}
