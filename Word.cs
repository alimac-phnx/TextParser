using System.Collections.Generic;

namespace CSharp_eng3._1
{
    public class Word
    {
        private static List<Word> wordList = new List<Word>();
        private string content;

        public Word(string line)
        {
            content = line;
            if (this.Content != null) { wordList.Add(this); }
            
        }
        public Word()
        {
        }
        public string Content { get { return content; } set { content = value; } }
        public List<Word> Words { get { return wordList; } }
    }
}