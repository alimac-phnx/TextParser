namespace CSharp_eng3._1
{
    public class Punctuation
    {
        private char sign;

        public Punctuation(char sign) { this.sign = sign; }
        public Punctuation() {}
        public char Content { get { return sign; } set { sign = value; } }

        public bool IsEnd()
        {
            if (sign == '.' || sign == '!' || sign == '?') { return true; }
            else { return false; }
        }
    }
}