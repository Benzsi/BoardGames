using System.ComponentModel.DataAnnotations;

public class BoardGame
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A játék neve kötelező")]
    public string Name { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "A játékosok száma 1 és 10 között kell legyen")]
    public int Players { get; set; }

    [Range(1, 240, ErrorMessage = "A játékidő 1 és 240 perc között kell legyen")]
    public int PlayTime { get; set; }

    [Range(3, 99, ErrorMessage = "A javasolt életkor 3 és 99 között kell legyen")]
    public int RecommendedAge { get; set; }
}
