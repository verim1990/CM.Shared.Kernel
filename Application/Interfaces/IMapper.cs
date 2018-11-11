using CM.Shared.Kernel.Application.Base;

namespace CM.Shared.Kernel.Application.Interfaces
{
    public interface IMapper<in TModel, out TDto>
        where TModel : IBaseEntity 
        where TDto : IDto
    {
        TDto ToDto(TModel model, bool throwIfNull = true);
    }
}