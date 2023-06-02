using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    public class Theorem
    {
        public string _tname { get; set; }
        public string _condition { get; set; }
        public string _conclusion { get; set; }
        public string _proof { get; set; }

        public Theorem(string name, string condition, string conclusion, string proof)
        {
            _tname = name;
            _condition = condition;
            _conclusion = conclusion;
            _proof = proof;
        }

        public static explicit operator Theorem(Formula v)
        {
            throw new NotImplementedException();
        }
    }
}
