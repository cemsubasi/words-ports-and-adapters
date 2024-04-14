namespace Infra.Common;

public interface IParser {
    T Parse<T>(string value);
}