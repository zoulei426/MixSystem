using System;

namespace Mix.Data
{
    public interface ICurrentUser
    {
        public Guid? ID { get; set; }
    }
}