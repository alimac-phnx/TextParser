using System;
using System.IO;
using System.Linq;

namespace CSharp_eng3._1
{
    public class TextParser
    {
        public string pathToRead;
        public string pathToWrite;

        public TextParser(string path1, string path2)
        {
            pathToRead = path1;
            pathToWrite = path2;
        }

        public bool IsPunct(char sign)
        {
            char[] punctSigns = { '.', ',', ';', ':', '!', '?', '(', ')' };

            if (punctSigns.Any(p => p == sign)) { return true; }
            else { return false; }
        }

        public void Parse()
        {
            //StreamWriter fileOut = new StreamWriter(pathToWrite);
            Text text = new Text();
            Sentence sentence = new Sentence();

            foreach (string line in File.ReadLines(pathToRead))
            {
                string lineWord = null;
                Word word;

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != ' ')
                    {
                        if (IsPunct(line[i]))
                        {
                            Punctuation punctuation = new Punctuation(line[i]);
                            word = new Word(lineWord.Substring(0, lineWord.Length));
                            lineWord = null;
                            sentence = new Sentence(word.Words, punctuation);
                        }
                        else 
                        { 
                            lineWord += line[i]; 
                        }
                    }
                    else
                    {
                        word = new Word(lineWord);
                        lineWord = null;
                    }
                }
                sentence.Content += '\n';
            }
            text = new Text(sentence);
            text.FileOut(/*fileOut*/);
           // text.SortByNumberOfWords(sentence.Sentences);
            //text.SortBySentenceLength(sentence.Sentences);
            //text.QuestionMarkWords(sentence.Sentences);
            //text.DeleteConsonantLetterWord(sentence.Sentences);
            //text.WordsReplace(sentence.Sentences);
            //text.DeleteStopWords(sentence.Sentences);

            text.DictionaryWork(sentence.Sentences);

           //text.SerialiseToXml();
            //text = new Text(sentence);
            //Console.WriteLine();
            //text.FileOut(/*fileOut*/);
        }
    }
}