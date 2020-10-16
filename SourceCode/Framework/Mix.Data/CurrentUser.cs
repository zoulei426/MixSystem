using System;

namespace Mix.Data
{
    public class CurrentUser : ICurrentUser
    {
        public Guid? ID { get => Guid.Empty; set => SetID(); }

        private void SetID()
        {
        }
    }
}