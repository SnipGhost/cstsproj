namespace tsproj.test_logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;

    [Serializable]
    public sealed class format_bd
    {
        public format_profile admin_profile;
        public static format_bd curBd;
        public string p_msg;
        public List<format_profile> profiles = new List<format_profile>();
        private static List<Thread> save_thread = new List<Thread>();
        private static format_bd st_bd;
        private static string st_name;
        public List<format_testfile> tests = new List<format_testfile>();

        public format_profile GetProfile(string id)
        {
            for (int i = 0; i < this.profiles.Count; i++)
            {
                if (this.profiles[i].profile_id == id)
                {
                    return this.profiles[i];
                }
            }
            return null;
        }

        public format_testfile GetTest(string id)
        {
            for (int i = 0; i < this.tests.Count; i++)
            {
                if (this.tests[i].test_id == id)
                {
                    return this.tests[i];
                }
            }
            return null;
        }

        public static string BTS(bool value)
        {
            if (value)
                return "[+] ";
            return "[-] ";
        }

        public static void SaveTests(format_bd bd)
        {
            StreamWriter writer = new StreamWriter("tests.txt");
            foreach (format_testfile test in bd.tests)
            {
                writer.WriteLine("ТЕСТ " + test.test_id);
                writer.WriteLine("===========================================================");
                foreach (test_theme theme in test.themes)
                {
                    writer.WriteLine("Тема " + theme.name);
                    writer.WriteLine();
                    foreach (test_question question in theme.questions)
                    {
                        if (question.question[question.question.Length - 1] == '\n')
                            writer.Write("\t" + question.question);
                        else
                            writer.WriteLine("\t" + question.question);

                        foreach (test_answer answer in question.answers)
                        {
                            string s = answer.answer;
                            if ((s[0] == 'А' || s[0] == 'Б' || s[0] == 'В' || s[0] == 'Г') && (s[1] == ')'))
                            {
                                s = s.Substring(2);
                            }
                            writer.WriteLine("\t\t" + BTS(answer.is_right) + s);
                        }
                        writer.WriteLine("\n");
                    }
                    writer.WriteLine("-----------------------------------------------------------");
                }
            }
            writer.Close();
        }

        public static format_bd Load(string name)
        {
            if (File.Exists(name))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                StreamReader reader = new StreamReader(name);
                format_bd _bd = formatter.Deserialize(reader.BaseStream) as format_bd;
                reader.Close();
                SaveTests(_bd);
                return _bd;
            }
            return new format_bd();
        }

        public void mergeBd(format_bd bd2)
        {
        }

        public static void Save(string name, format_bd bd)
        {
            st_bd = bd;
            st_name = name;
            Thread item = new Thread(new ThreadStart(format_bd.save_threaded));
            save_thread.Add(item);
            item.Start();
        }

        private static void save_threaded()
        {
            string str = st_name;
            format_bd _bd = st_bd;
            if (!string.IsNullOrEmpty(str) && (_bd != null))
            {
                for (bool flag = false; !flag; flag = true)
                {
                    try
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        StreamWriter writer = new StreamWriter(st_name);
                        formatter.Serialize(writer.BaseStream, st_bd);
                        writer.Close();
                    }
                    catch
                    {
                        Thread.Sleep(0x3e8);
                    }
                }
            }
            save_thread.Remove(Thread.CurrentThread);
            _bd = null;
            str = null;
        }

        public static bool isSaving =>
            (save_thread.Count > 0);
    }
}