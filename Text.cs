using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CSharp_eng3._1
{
    public class Text
    {
        private string content;
        public string lang;
        public List<string> TextContent = new List<string>();

        public Text(Sentence sentence)
        {
            content = "";
            AddSentances(sentence);
            lang = LanguageDefine(this);
            TextContent = sentence.ListContent.Split("%").ToList();
        }

        public Text()
        {
        }

        public void AddSentances(Sentence sentence)
        {
            foreach (Sentence s in sentence.Sentences)
            {
                if (s.Content != null)
                {
                    content += s.Content;
                    if (!content.EndsWith('\n'))
                    {
                        content += ' ';
                    }
                    
                }
            }

            content = content.Trim() + '\n';
        }

        [XmlIgnore]
        public string Content { get { return content; } set { content = value; } }

        public void FileOut(/*StreamWriter fileOut*/)
        {
            Console.Write(content);
            //fileOut.WriteLine(content);
        }

        public bool IsConsonant(string letter)
        {
            char[] vowelLettersRU = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
            char[] vowelLettersEN = { 'a', 'e', 'i', 'o', 'u', 'y' };

            if (lang == "ru")
            {
                if (!vowelLettersRU.Any(p => p == Convert.ToChar(letter.ToLower()))) { return true; }
                else { return false; }
            }
            else
            {
                if (!vowelLettersEN.Any(p => p == Convert.ToChar(letter.ToLower()))) { return true; }
                else { return false; }
            }
        }

        public string LanguageDefine(Text text)
        {
            string lang = null;
            foreach (char ch in text.Content)
            {
                if ((ch > 'а' && ch < 'я') || (ch > 'А' && ch < 'Я'))
                    lang = "ru";
                else if ((ch > 'a' && ch < 'z') || (ch > 'A' && ch < 'Z'))
                    lang = "en";
            }
            return lang;
        }

        public void SortByNumberOfWords(List<Sentence> sentences)
        {
            //sentences.Sort(); 

            for (int i = 1; i < sentences.Count; i++)
            {
                Sentence element = sentences[i];
                int p = i - 1;
                while(p >= 0 && sentences[p].counterEnd > element.counterEnd)
                {
                    sentences[p + 1] = sentences[p];
                    p -= 1;
                }
                sentences[p + 1] = element;
            }
            Console.WriteLine("\n-----------------------------------> Текст отсортирован в порядке возрастания количества слов в предложениях\n");
            //foreach (Sentence s in sentences)
            //{
            //    Console.WriteLine(s.Content + "    " + s.counterEnd);
            //}
        }

        public void SortBySentenceLength(List<Sentence> sentences)
        {
            for (int i = 1; i < sentences.Count; i++)
            {
                Sentence element = sentences[i];
                int p = i - 1;
                while (p >= 0 && sentences[p].Content.Trim().Length > element.Content.Trim().Length)
                {
                    sentences[p + 1] = sentences[p];
                    p -= 1;
                }
                sentences[p + 1] = element;
            }
            Console.WriteLine("\n-----------------------------------> Текст отсортирован в порядке возрастания длины предложений\n");
        }

        public void QuestionMarkWords(List<Sentence> sentences)
        {
            Console.Write("\nВведите длину слова: ");
            int len = Convert.ToInt32(Console.ReadLine());
            List<string> same = new List<string>();
            int i = 0;
            foreach (Sentence s in sentences)
            {
                if (s.Content.EndsWith('\n')) { s.Content = s.Content.Remove(s.Content.Length - 1); }
                
                if (s.Content.EndsWith('?')) 
                {
                    string[] lst = s.Content.Split(' ');
                    
                    foreach (string w in lst)
                    {
                        string word = w;
                        if (!Char.IsLetter(word[word.Length - 1]))
                        {
                            word = word.Remove(word.Length - 1);
                        }
                        if (word.Length == len)
                        {
                            if (i == 0)
                            {
                                Console.WriteLine($"\n{word}");
                                same.Add(word);
                                i++;
                            }
                            else if (!same.Contains(word))
                            {
                                Console.WriteLine($"\n{word}");
                                same.Add(word);
                            }
                        }
                    }
                }
            }
        }

        public void DeleteConsonantLetterWord(List<Sentence> sentences)
        {
            Console.Write("\nВведите длину слова: ");
            int len = Convert.ToInt32(Console.ReadLine());

            foreach (Sentence s in sentences)
            {
                string[] lst = s.Content.Split(' ');

                foreach (string w in lst)
                {
                    string word = w;
                    if (!Char.IsLetter(word[word.Length - 1]))
                    {
                            word = word.Remove(word.Length - 1);
                    }
                    if (word.Length == len)
                    {
                        if (IsConsonant(Convert.ToString(word[0])))
                        {
                            s.Content = s.Content.Replace(word, "");
                        }
                    }
                }
            }
        }

        public void WordsReplace(List<Sentence> sentences)
        {
            Console.Write("\nВведите длину слова: ");
            int len = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nВведите другое слово: ");
            string repW = Console.ReadLine();

            Random random = new Random();
            int n = random.Next(0, sentences.Count);
            string[] lst = sentences[n].Content.Split(' ');

            foreach (string w in lst)
            {
                string word = w;
                if (!Char.IsLetter(word[word.Length - 1]))
                {
                    word = word.Remove(word.Length - 1);
                }
                if (word.Length == len)
                {
                    sentences[n].Content = sentences[n].Content.Replace(word, repW);
                }
            }
        }

        public void DeleteStopWords(List<Sentence> sentences)
        {
            string fileName;
            string sg = "";
            if (lang == "ru") { fileName = @"c:\Users\alimac\Desktop\stopwords_ru.txt"; }
            else { fileName = @"c:\Users\alimac\Desktop\stopwords_en.txt"; }

            List<string> stopList = File.ReadAllLines(fileName).ToList();

            foreach (Sentence s in sentences)
            {
                string[] lst = s.Content.Split(' ');

                for (int i = 0; i < lst.Length; i++)
                {
                    string word = lst[i];
                    if (!Char.IsLetter(lst[i][lst[i].Length - 1]))
                    {
                        sg = lst[i].Substring(lst[i].Length - 1);
                        lst[i] = lst[i].Remove(lst[i].Length - 1);
                    }
                    foreach (string stop in stopList)
                    {
                        if (lst[i].ToLower() == stop)
                        {
                            lst[i] = "" + sg;
                            sg = "";
                        }
                        else 
                        {
                            lst[i] += sg;
                            sg = "";
                        }
                    }
                }
                s.Content = string.Join(" ", lst);
            }
        }

        public void SerialiseToXml()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(GetType());
            StreamWriter write = new StreamWriter(@"..\..\..\TextData.xml");
            xmlSerializer.Serialize(write, this);
        }

        public void DictionaryWork(List<Sentence> sentences)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < sentences.Count; i++)
            {
                for (int j = 0; j < sentences[i].Content.Split(" ").Length; j++)
                {
                    if (sentences[i].Content.EndsWith('\n')) { sentences[i].Content = sentences[i].Content.Remove(sentences[i].Content.Length - 1); }
                    string word = sentences[i].Content.Split(" ")[j];
                    if (!Char.IsLetter(word[word.Length - 1]))
                    {
                            word = word.Remove(word.Length - 1);
                    }
                    if (!dictionary.ContainsKey(word))
                    {
                        dictionary.Add(word, 1);
                    }
                    else
                    {
                        dictionary[word] += 1;
                    }
                    dictionary = dictionary.OrderBy(k => k.Key).ToDictionary(process => process.Key, process => process.Value);

                }
            }

            foreach (var item in dictionary)
            {
                string inds = "";
                foreach (Sentence s in sentences)
                {
                    if (s.Content.EndsWith('\n')) { s.Content = s.Content.Remove(s.Content.Length - 1); }
                    char[] punctSigns = { '.', ',', ';', ':', '!', '?', '(', ')' };
                    string[] strArr = s.Content.Split(punctSigns);
                    string strLine = string.Join(" ", strArr);
                    strArr = strLine.Split(" ");
                    if (strArr.Contains(item.Key))
                    {
                        inds += " : " + (sentences.IndexOf(s) + 1) ;
                    }
                }
                Console.WriteLine(item + inds);
            }
        }
    }
}