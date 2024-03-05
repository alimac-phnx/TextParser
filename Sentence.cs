using System.Collections.Generic;

namespace CSharp_eng3._1
{
    public class Sentence
    {
        private static List<Sentence> senList = new List<Sentence>();
        private string contentEnd;
        private static string content;
        public int counterEnd;
        public static int counter;

        public Sentence(List<Word> words, Punctuation punctuation)
        {
            if (punctuation.IsEnd())
            {
                contentEnd = SentenceMaking(words, punctuation, content);

                counterEnd += words.Count + counter;
                counter = 0;
            }
            else
            {
                content = SentenceMaking(words, punctuation, content);

                counter += words.Count;
            }
            if (this.Content != null) { senList.Add(this); }

            words.Clear();
        }

        public Sentence()
        {
        }
        public string Content { get { return contentEnd; } set { contentEnd = value; } }
        public string ListContent
        {
            get
            {
                string str = "";
                foreach (Sentence s in this.Sentences)
                { str += s.Content + "%"; }
                return str;
            }
        }

        public string SentenceMaking(List<Word> words, Punctuation punctuation, string cont)
        {
            content = null;
            foreach (Word w in words)
            {
                cont += " " + w.Content;
            }

            cont = cont.Trim();
            cont += punctuation.Content;
            return cont;
        }

        public List<Sentence> Sentences { get { return senList; } set { senList = value; } }

    }
}