namespace tsproj.test_logic
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class test_result
    {
        public List<test_question_result> question_results = new List<test_question_result>();
        public int result;
        public string test_level;
        public string test_message;
        public string test_name;
        public DateTime test_start_time;
        public TimeSpan test_time_spent;

        public void Precache(format_tsf stream)
        {
            stream.PrecacheString(this.test_name);
            stream.PrecacheString(this.test_message);
            stream.PrecacheString(this.test_level);
        }

        public void Read(format_tsf stream)
        {
            this.test_name = stream.ReadString();
            this.test_message = stream.ReadString();
            this.test_level = stream.ReadString();
            this.test_start_time = new DateTime(stream.ReadLong());
            this.result = stream.ReadInt();
            int num = stream.ReadInt();
            this.question_results.Clear();
            for (int i = 0; i < num; i++)
            {
                test_question_result item = new test_question_result {
                    themeId = stream.ReadShort(),
                    questionId = stream.ReadShort()
                };
                item.answerId.Clear();
                byte num3 = stream.ReadByte();
                for (int j = 0; j < num3; j++)
                {
                    item.answerId.Add(stream.ReadByte());
                }
                item.timeSpent = stream.ReadInt();
                this.question_results.Add(item);
            }
        }

        public void Save(format_tsf stream)
        {
            stream.Write(this.test_name);
            stream.Write(this.test_message);
            stream.Write(this.test_level);
            stream.Write(this.test_start_time.Ticks);
            stream.Write(this.result);
            stream.Write(this.question_results.Count);
            for (int i = 0; i < this.question_results.Count; i++)
            {
                stream.Write(this.question_results[i].themeId);
                stream.Write(this.question_results[i].questionId);
                stream.Write((byte) this.question_results[i].answerId.Count);
                for (int j = 0; j < this.question_results[i].answerId.Count; j++)
                {
                    stream.Write(this.question_results[i].answerId[j]);
                }
                stream.Write(this.question_results[i].timeSpent);
            }
        }
    }
}

