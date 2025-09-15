
Console.WriteLine("Hello, World!");

Console.WriteLine(Rating.of(5) >= Rating.of(4)); // YES

// Vou deixar isso aqui para verem como se faz os operadores de comparação
record Rating(byte Stars)
{

    public static Rating of(byte stars)
    {
        return new(stars);
    }

    public static bool operator >(Rating a, Rating b) => a.Stars > b.Stars;
    public static bool operator <(Rating a, Rating b) => a.Stars < b.Stars;

    public static bool operator >=(Rating a, Rating b) => a.Stars >= b.Stars;
    public static bool operator <=(Rating a, Rating b) => a.Stars <= b.Stars;
}