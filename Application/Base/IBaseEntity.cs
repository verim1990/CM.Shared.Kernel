using System;

namespace CM.Shared.Kernel.Application.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}