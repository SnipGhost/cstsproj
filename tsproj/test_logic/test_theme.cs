namespace tsproj.test_logic
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class test_theme
    {
        public List<string> levels = new List<string>();
        public string name;
        public List<test_question> questions = new List<test_question>();

        public void Precache(format_tsf stream)
        {
            int num;
            stream.PrecacheString(this.name);
            for (num = 0; num < this.levels.Count; num++)
            {
                stream.PrecacheString(this.levels[num]);
            }
            for (num = 0; num < this.questions.Count; num++)
            {
                this.questions[num].Precache(stream);
            }
        }

        public void Read(format_tsf stream)
        {
            int num2;
            this.name = stream.ReadString();
            int num = stream.ReadInt();
            this.levels.Clear();
            for (num2 = 0; num2 < num; num2++)
            {
                this.levels.Add(stream.ReadString());
            }
            int num3 = stream.ReadInt();
            this.questions.Clear();
            for (num2 = 0; num2 < num3; num2++)
            {
                test_question item = new test_question();
                item.Read(stream);
                this.questions.Add(item);
            }
        }

        public void Save(format_tsf stream)
        {
            int num;
            stream.Write(this.name);
            stream.Write(this.levels.Count);
            for (num = 0; num < this.levels.Count; num++)
            {
                stream.Write(this.levels[num]);
            }
            stream.Write(this.questions.Count);
            for (num = 0; num < this.questions.Count; num++)
            {
                this.questions[num].Save(stream);
            }
        }
    }
}

