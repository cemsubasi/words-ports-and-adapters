namespace Infra.Controllers.Common;

public class ActionResponse<T> {
  /* public DataResponse<T> items { get; private set; } */
  public T data { get; private set; }
  public ErrorResponse errors { get; private set; }

  private ActionResponse(T data) {
    this.data = data;
  }

  /* private ActionResponse(DataResponse<T> items) { */
  /*   this.items = items; */
  /* } */

  private ActionResponse(ErrorResponse errors) {
    this.errors = errors;
  }


  public static ActionResponse<T> Build(T data) {
    return new ActionResponse<T>(data);
  }

  /* public static ActionResponse<T> Build(DataResponse<T> data) { */
  /*   return new ActionResponse<T>(data); */
  /* } */

  /* public static ActionResponse<DataResponse<T>> Build(List<T> items) { */
  /*   return new ActionResponse<DataResponse<T>>(DataResponse<T>.Build(items)); */
  /* } */

  /* public static ActionResponse<DataResponse<T>> Build(List<T> items, uint page, uint size, uint totalSize) { */
  /*   return new ActionResponse<DataResponse<T>>(DataResponse<T>.Build(items, page, size, totalSize)); */
  /* } */

  public static ActionResponse<ErrorResponse> Build(ErrorResponse errors) {
    return new ActionResponse<ErrorResponse>(errors);
  }
}
