namespace WriteCongress.Web.Models {
    public class JsonServiceResult<T> {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }


        public JsonServiceResult(bool success, string message = null) {
            Success = success;
        }

        public JsonServiceResult(T data, bool success, string message = null) {
            Data = data;
            Success = success;
        }
    }
}