namespace FantasyNameGen.Models
{
    public class Surname
    {
        public int Id { get; set; }
        public string RomanSurname { get; set; } // собственно имя
        public string CyrilSurname { get; set; } // транскрипция на русском
        public bool Common { get; set; } // распространённое
    }
}
