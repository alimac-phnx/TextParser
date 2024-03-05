namespace CSharp_eng3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            TextParser textParser = new TextParser(@"c:\Users\alimac\Desktop\File1.txt", @"c:\Users\alimac\Desktop\File2.txt");
            
            textParser.Parse();
        }
    }
}
