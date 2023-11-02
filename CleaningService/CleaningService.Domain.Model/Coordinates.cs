namespace CleaningService.Domain.Model;

public class Coordinates
{
    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString()
    {
        return $"<{X},{Y}>";
    }

    public override int GetHashCode()
    {
        return (X, Y).GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Coordinates other = (Coordinates)obj;
        return X == other.X && Y == other.Y;
    }
}