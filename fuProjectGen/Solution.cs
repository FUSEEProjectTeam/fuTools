using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fuProjectGen
{
    partial class SolutionFile
    {
        private readonly string _guid;
        private readonly string _name;
        
        public SolutionFile(Guid guid, string name)
        {
            _guid = guid.ToString("B");
            _name = name;
        }
    }
}
