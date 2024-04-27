namespace Infra.Common;

public abstract class Writer {
  public enum Platform {
    File
  }

  public abstract void Write(object obj);
}