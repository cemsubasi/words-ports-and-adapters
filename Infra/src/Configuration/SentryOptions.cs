namespace Infra.Configurations;

public class SentryOptions {
  public string Dsn { get; init; }
  public double TracesSampleRate { get; init; }
  public bool Debug { get; init; }

}
