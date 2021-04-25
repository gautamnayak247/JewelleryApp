namespace Jewellery.Application.Services
{
    using Jewellery.Application.Interfaces;
    using System;

    public class GuidService : IGuidService
    {
        public Guid NewGuid()
            => Guid.NewGuid();

    }
}
