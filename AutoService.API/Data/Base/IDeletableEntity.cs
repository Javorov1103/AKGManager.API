﻿namespace AutoService.API.Data
{
    using System;

    public interface IDeletableEntity : IEntity
    {
        DateTime? DeletedOn { get; set; }

        string DeletedBy { get; set; }

        bool IsDeleted { get; set; }
    }
}
