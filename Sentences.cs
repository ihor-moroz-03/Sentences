using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindDeepestNestedBracketsSort
{
    class Sentences : IEnumerable<string>
    {
        List<string> _sentences;

        public Sentences(string[] lines)
        {
            _sentences = new();
            StringBuilder sentence = new();
            foreach (string line in lines)
            {
                if (line.Length == 0) continue;
                int from = 0, dot;
                do
                {
                    dot = line.IndexOf('.', from);
                    if (dot == -1)
                    {
                        sentence.Append(line[from..]);
                        break;
                    }
                    sentence.Append(line[from..dot]);
                    from = dot + 1;
                    _sentences.Add(sentence.ToString());
                    sentence.Clear();
                }
                while (from < line.Length);
            }
            //if no dot found in the end, remaining piece is assumed to be a sentence
            if (sentence.Length > 0) _sentences.Add(sentence.ToString());
        }

        public IEnumerator<string> GetEnumerator() => _sentences.GetEnumerator();

        public string GetMaxBrackets()
        {
            if (_sentences.Count == 0) return "";
            int max = 0, indexOfMax = 0;
            for (int i = 0; i < _sentences.Count; ++i)
            {
                int count = 0;
                foreach (char c in _sentences[i])
                {
                    switch (c)
                    {
                        case '(': count++; break;
                        case ')':
                            if (count > max) { max = count; indexOfMax = i; }
                            count = 0;
                            break;
                    }
                }
            }
            return _sentences[indexOfMax];
        }

        public void Sort(Comparison<string> cmp) => _sentences.Sort(cmp);

        IEnumerator IEnumerable.GetEnumerator() => _sentences.GetEnumerator();
    }
}
