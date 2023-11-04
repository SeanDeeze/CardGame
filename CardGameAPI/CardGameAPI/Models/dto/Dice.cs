namespace CardGame.Models.dto
{
    public class Dice
    {
        public Dice(int value)
        {
            Value = value;
            Icon = $"fas fa-dice-{ConvertIntToWord(Value)} fa-2";
        }

        public int Value { get; set; }
        public string Icon { get; set; }

        public string ConvertIntToWord(int value)
        {
            switch (value)
            {
                case 0: return "zero";
                case 1: return "one";
                case 2: return "two";
                case 3: return "three";
                case 4: return "four";
                case 5: return "five";
                case 6: return "six";
            }

            return "zero";
        }
    }
}