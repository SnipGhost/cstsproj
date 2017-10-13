namespace tsproj.test_logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Drawing;

    [Serializable]
    public sealed class format_testfile
    {
        public string image;
        public Dictionary<string, Image> images = new Dictionary<string, Image>();
        public string intro;
        public bool isPartiallyLoaded = false;
        public int level_mincap;
        public int max_points;
        public string msg_end;
        public string msg_fail;
        public string msg_pass;
        public int question_by_theme_count;
        public int question_count;
        public byte question_selection_mode;
        public Dictionary<string, byte[]> sounds = new Dictionary<string, byte[]>();
        public string test_id;
        public string test_recourses_dir;
        public static string TestDirectory = "tests/";
        public byte theme_selection_mode;
        public bool theme_use_levels;
        public List<test_theme> themes = new List<test_theme>();
        public int time_limit;
        public string vid_end;
        public string vid_fail;
        public string vid_intro;
        public string vid_pass;
        public Dictionary<string, byte[]> videos = new Dictionary<string, byte[]>();

        public string addSound(string path)
        {
            try
            {
                string fileName = Path.GetFileName(path);
                byte[] buffer = File.ReadAllBytes(path);
                if (this.sounds.ContainsKey(fileName))
                {
                    this.sounds[fileName] = buffer;
                    return fileName;
                }
                this.sounds.Add(fileName, buffer);
                return fileName;
            }
            catch
            {
            }
            return null;
        }

        public string addVideo(string path)
        {
            try
            {
                string fileName = Path.GetFileName(path);
                byte[] buffer = File.ReadAllBytes(path);
                if (this.videos.ContainsKey(fileName))
                {
                    this.videos[fileName] = buffer;
                    return fileName;
                }
                this.videos.Add(fileName, buffer);
                return fileName;
            }
            catch
            {
            }
            return null;
        }

        public void Delete()
        {
        }

        public List<string> getLevels()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < this.themes.Count; i++)
            {
                for (int j = 0; j < this.themes[i].levels.Count; j++)
                {
                    if (!list.Contains(this.themes[i].levels[j]))
                    {
                        list.Add(this.themes[i].levels[j]);
                    }
                }
            }
            return list;
        }

        public string getSound(string name)
        {
            if (this.sounds.ContainsKey(name))
            {
                string path = Path.GetTempPath() + name;
                File.WriteAllBytes(path, this.sounds[name]);
                return path;
            }
            return null;
        }

        public string getVideo(string name)
        {
            if (this.videos.ContainsKey(name))
            {
                string path = Path.GetTempPath() + name;
                File.WriteAllBytes(path, this.videos[name]);
                return path;
            }
            return null;
        }

        public static format_testfile Load(string test_id)
        {
            for (int i = 0; i < format_bd.curBd.tests.Count; i++)
            {
                if (format_bd.curBd.tests[i].test_id == test_id)
                {
                    return format_bd.curBd.tests[i];
                }
            }
            return null;
        }

        public void Save()
        {
            for (int i = 0; i < format_bd.curBd.tests.Count; i++)
            {
                if (format_bd.curBd.tests[i].test_id == this.test_id)
                {
                    format_bd.curBd.tests[i] = this;
                    format_bd.Save("data.obj", format_bd.curBd);
                    return;
                }
            }
            format_bd.curBd.tests.Add(this);
            format_bd.Save("data.obj", format_bd.curBd);
        }
    }
}

