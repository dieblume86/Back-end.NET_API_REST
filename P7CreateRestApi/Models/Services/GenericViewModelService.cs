using AutoMapper;
using Dot.Net.WebApi.Models.Services.Interfaces;
using Dot.Net.WebApi.Repositories.Interfaces;

namespace Dot.Net.WebApi.Models.Services
{
    public class GenericViewModelService<Entity, ViewModel> : IGenericViewModelService<Entity, ViewModel>
        where Entity : class
        where ViewModel : class, new()
    {
        protected readonly IGenericRepository<Entity> _repository;
        protected readonly IMapper _mapper;

        public GenericViewModelService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public int Add(ViewModel viewModel)
        {
            return _repository.Add(AutoMapToEntity(viewModel));
        }
        public Entity Get(int id)
        {
          return _repository.Get(id);
        }
        public void Update(ViewModel viewModel)
        {
            // Try to retrieve an Id from the ViewModel (convention "Id")
            var idProp = viewModel?.GetType().GetProperty("Id");
            if (idProp == null)
            {
                // No Id on the ViewModel: legacy behavior (map to a new entity)
                _repository.Update(AutoMapToEntity(viewModel));
                return;
            }

            var idObj = idProp.GetValue(viewModel);
            int id = Convert.ToInt32(idObj ?? 0);

            if (id == 0)
            {
                // Id not provided: fallback to Add to avoid incorrect insertion
                _repository.Add(AutoMapToEntity(viewModel));
                return;
            }

            // Retrieve the existing entity and map the VM values onto it
            var existing = _repository.Get(id);
            if (existing == null)
            {
                // If not found, create anyway (or throw an exception according to policy)
                _repository.Add(AutoMapToEntity(viewModel));
                return;
            }

            // Map the properties from the ViewModel onto the existing entity — preserve the Id
            _mapper.Map(viewModel, existing);

            _repository.Update(existing);
        }
        public void Delete(int id)
        {
         _repository.Delete(id);
        }

        public Entity AutoMapToEntity(ViewModel viewModel)
        {
            return _mapper.Map<Entity>(viewModel);
        }
        public ViewModel AutoMapToViewModel(Entity entity)
        {
            return _mapper.Map<ViewModel>(entity);
        }
    }
}
