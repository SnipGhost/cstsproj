namespace tsproj.test_logic
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class test_question
    {
        public List<test_answer> answers = new List<test_answer>();
        public string details;
        public string image_filename;
        public int max_answers = 1;
        public string name;
        public string question;
        public int question_price = -1;
        public string sound_filename;

        public void Precache(format_tsf stream)
        {
            stream.PrecacheString(this.name);
            stream.PrecacheString(this.question);
            stream.PrecacheString(this.image_filename);
            for (int i = 0; i < this.answers.Count; i++)
            {
                stream.PrecacheString(this.answers[i].answer);
            }
        }

        public void Read(format_tsf stream)
        {
            this.name = stream.ReadString();
            this.question = stream.ReadString();
            this.image_filename = stream.ReadString();
            this.max_answers = stream.ReadInt();
            int num = stream.ReadInt();
            this.answers.Clear();
            for (int i = 0; i < num; i++)
            {
                test_answer item = new test_answer {
                    answer = stream.ReadString(),
                    is_right = stream.ReadBool()
                };
                this.answers.Add(item);
            }
        }

        public void Save(format_tsf stream)
        {
            stream.Write(this.name);
            stream.Write(this.question);
            stream.Write(this.image_filename);
            stream.Write(this.max_answers);
            stream.Write(this.answers.Count);
            for (int i = 0; i < this.answers.Count; i++)
            {
                stream.Write(this.answers[i].answer);
                stream.Write(this.answers[i].is_right);
            }
        }
    }
}

