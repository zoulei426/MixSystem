using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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