using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class BoardGamesModel : PageModel
{
    private readonly string filePath = "wwwroot/boardgame.csv";

    public List<BoardGame> BoardGames { get; set; } = new();

    [BindProperty]
    public BoardGame NewGame { get; set; } = new();

    public void OnGet()
    {
        BoardGames = ReadGamesFromFile();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            BoardGames = ReadGamesFromFile();
            return Page();
        }

        var newId = BoardGames.Count > 0 ? BoardGames.Max(b => b.Id) + 1 : 1;
        var newLine = $"{newId},{NewGame.Name},{NewGame.Players},{NewGame.PlayTime},{NewGame.RecommendedAge}";
        System.IO.File.AppendAllText(filePath, "\n" + newLine);

        return RedirectToPage();
    }

    private List<BoardGame> ReadGamesFromFile()
    {
        if (!System.IO.File.Exists(filePath)) return new List<BoardGame>();

        return System.IO.File.ReadAllLines(filePath)
            .Skip(1) 
            .Select(line => line.Split(','))
            .Select(parts => new BoardGame
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Players = int.Parse(parts[2]),
                PlayTime = int.Parse(parts[3]),
                RecommendedAge = int.Parse(parts[4])
            })
            .ToList();
    }
}
