using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    /// <summary>
    /// Класс для задания контрольной
    /// </summary>
    public class Task
    {
        public string Question { get; private set; }
        public string Answer { get; private set; }
        public string Hint { get; private set; }

        public Task(string question, string answer, string hint)
        {
            Question = question;
            Answer = answer;
            Hint = hint;
        }
    }
}
