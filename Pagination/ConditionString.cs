using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination
{
    public class ConditionString
    {
        public string EXEC { get; set; }
        public string EXEC_KEY { get; set; }
        public ConditionString() {
            EXEC = "";
            EXEC_KEY = "";
        }
        public ConditionString(string EXEC_ = "", string EXEC_KEY_ = "")
        {
            EXEC = EXEC_;
            EXEC_KEY = EXEC_KEY_;
        }
        public ConditionString Where(string condition_string)
        {
            EXEC = condition_string.ToString();
            EXEC_KEY = Convert(condition_string);
            return new ConditionString(EXEC, EXEC_KEY);
        }
        public ConditionString And(string condition_string)
        {
            EXEC = EXEC +" AND "+ condition_string.ToString();
            string KEY = Convert(condition_string);
            EXEC_KEY = EXEC_KEY + (KEY != "" ? "," : "") + KEY;
            return new ConditionString(EXEC, EXEC_KEY);
        }
        public ConditionString Or(string condition_string)
        {
            EXEC = EXEC + " OR " + condition_string.ToString();
            string KEY = Convert(condition_string);
            EXEC_KEY = EXEC_KEY + (KEY != "" ? "," : "") + KEY;
            return new ConditionString(EXEC,EXEC_KEY);
        }
        private string Convert(string condition_string) {
            var con = condition_string.Split(' ');
            if (EXEC_KEY.Contains(con[0])) {
                return "";
            }
            return condition_string.Split(' ')[0].Replace("[", "").Replace("]", "");
        }
    }
}
