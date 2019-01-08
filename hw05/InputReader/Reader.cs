using System;
using System.Collections.Generic;
using System.IO;
using paa_hw5.Entity;

namespace paa_hw5.InputReader
{
    public static class Reader
    {
        public static Formula LoadInstance(string path)
        {
            var form = new Formula();

            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // check if line is a comment
                    if (IsComment(line)) continue;

                    switch (line[0])
                    {
                        case 'p':
                            // configuration info
                            ParseConfiguration(line, ref form);
                            break;
                        case 'w':
                            // weight info
                            ParseWeight(line, ref form);
                            break;
                        default:
                            // clause info
                            ParseClause(line, ref form);
                            break;
                    }
                }
            }

            return form;
        }

        private static void ParseClause(string line, ref Formula form)
        {
            // if its not ending with 0, there is mistake in file
            var tmp = line.Split(" ");
            
            // check last 0
            if (int.Parse(tmp[tmp.Length - 1]) != 0) return;
            
            var literals = new List<Tuple<int, bool>>();
            // add clause
            for (var i = 0; i < tmp.Length - 1; i++)
            {
                var num = int.Parse(tmp[i]);
                var neg = num < 0;
                var index = Math.Abs(num);
                
                literals.Add(Tuple.Create(index, neg));
            }
            
            form.AddClause(literals);
        }
        
        private static void ParseWeight(string line, ref Formula form)
        {
            var tmp = line.Split(" ");

            for (var i = 1; i < tmp.Length; i++)
            {
                form.AddVariable(i, int.Parse(tmp[i]));
            }
        }
        
        private static void ParseConfiguration(string line, ref Formula form)
        {
            var tmp = line.Split(" ");
            
            form.SetVariablesNum(int.Parse(tmp[2]));
            form.SetClausesNum(int.Parse(tmp[3]));
        }
        
        private static bool IsComment(string line)
        {
            return line.StartsWith("c");
        }
    }
}