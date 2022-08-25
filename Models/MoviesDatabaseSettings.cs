namespace DeweyHomeMovieApi.Models;

public class MoviesDatabaseSettings
{
  public string ConnectionString { get; set; } = null!;

  public string DatabaseName { get; set; } = null!;

  public string TestCollectionName { get; set; } = null!;
  public string MovieCollectionName { get; set; } = null!;
}