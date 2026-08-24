using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinafaraECF
{
    public abstract class Component
    {
        public virtual void OnBegin() { }
        public virtual void Process(double DeltaTime) { }
    }
}
