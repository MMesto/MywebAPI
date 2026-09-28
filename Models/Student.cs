namespace MywebAPI.Models;   // "dit bestand hoort bij de map Models"

public class Student
{
    public int Id { get; set; }
    public string Studentnummer { get; set; }
    public string Naam { get; set; }

    // Constructor: hiermee maak je snel een nieuwe student aan
    public Student(int id, string studentnummer, string naam)
    {
        Id = id;
        Studentnummer = studentnummer;
        Naam = naam;
    }

    // Tijdelijke "nep-database": een lijst met studenten
    // (later vervang je dit door een echte database)
    public static List<Student> Students = new List<Student>
    {
        new Student(1, "25012345", "Jelle"),
        new Student(2, "25067890", "Mohamad"),
        new Student(3, "25011111", "Sara")
    };
}