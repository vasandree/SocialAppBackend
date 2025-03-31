namespace Common.Models.Dtos;

public class Pagination(int size, int count, int current)
{
    public int Size { get; set; } = size;

    public int Count { get; set; } = count;

    public int Current { get; set; } = current;
}