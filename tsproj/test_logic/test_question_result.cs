namespace tsproj.test_logic
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class test_question_result
    {
        public List<byte> answerId = new List<byte>();
        public short questionId;
        public short themeId;
        public int timeSpent;
    }
}

