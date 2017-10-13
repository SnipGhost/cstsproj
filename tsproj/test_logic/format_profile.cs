namespace tsproj.test_logic
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class format_profile
    {
        public bool haspwd = false;
        public string profile_id;
        public static string ProfileDirectory = "profiles/";
        private int pwd;
        public List<test_result> test_results = new List<test_result>();

        public format_profile(string id)
        {
            this.profile_id = id;
            format_bd.curBd.profiles.Add(this);
        }

        public void copyResultsTo(format_profile p2)
        {
            for (int i = 0; i < this.test_results.Count; i++)
            {
                if (!p2.test_results.Contains(this.test_results[i]))
                {
                    p2.test_results.Add(this.test_results[i]);
                }
            }
        }

        public bool equalsto(format_profile p2) => 
            ((this.profile_id == p2.profile_id) && (this.pwd == p2.pwd));

        public bool isPwd(string pwd) => 
            (tools.text_en(pwd) == this.pwd);

        public static format_profile Load(string profile_id)
        {
            for (int i = 0; i < format_bd.curBd.profiles.Count; i++)
            {
                if (format_bd.curBd.profiles[i].profile_id == profile_id)
                {
                    return format_bd.curBd.profiles[i];
                }
            }
            return null;
        }

        public void Save()
        {
            for (int i = 0; i < format_bd.curBd.profiles.Count; i++)
            {
                if (format_bd.curBd.profiles[i].profile_id == this.profile_id)
                {
                    format_bd.curBd.profiles[i] = this;
                    format_bd.Save("data.obj", format_bd.curBd);
                    return;
                }
            }
            format_bd.curBd.profiles.Add(this);
            format_bd.Save("data.obj", format_bd.curBd);
        }

        public void setPwd(string pwd)
        {
            this.haspwd = !string.IsNullOrEmpty(pwd);
            this.pwd = tools.text_en(pwd);
        }
    }
}

