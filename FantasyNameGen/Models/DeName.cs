namespace FantasyNameGen.Models
{
    public class DeName
    {
        public int Id { get; set; }
        public string RomanName { get; set; } // собственно имя
        public string CyrilName { get; set; } // транскрипция на русском
        public char Gender { get; set; } // пол (м/ж)
        public string Variants { get; set; } // вариации
        public string Description { get; set; } // описание
        public bool Common { get; set; } // распространённое
    }
}
