using DddAndEfCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.School
{
    public class Suffix : Entity
    {
        public static readonly Suffix Jr = new Suffix(Guid.Parse("314687c5-576c-48c2-bd7b-db56f7b6a552"), "Jr");
        public static readonly Suffix Sr = new Suffix(Guid.Parse("bab15095-f4a3-4f36-a2b1-8e552e09407b"), "Sr");
        public static readonly Suffix[] AllSuffixes = { Jr, Sr };

        public string Name { get; }

        protected Suffix()
        {

        }

        public Suffix(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public static Suffix FromId(Guid id)
        {
            return AllSuffixes.SingleOrDefault(w => w.Id == id);
        }
    }
}
