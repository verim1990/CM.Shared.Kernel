using System;

namespace CM.Shared.Kernel.Application.Base
{
    public class BaseEntity : IBaseEntity
    { 
        public Guid Id { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}