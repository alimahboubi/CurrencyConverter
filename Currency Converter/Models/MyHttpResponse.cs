namespace Currency_Converter.Models
{
    public class MyHttpResponse<T>
    {
        public T Result { get; set; }
        public string Error { get; set; }
        public MyHttpResponse(T result)
        {
            if (result == null)
            {
                this.Error = "there is no content to show";
            }
            this.Result = result;
        }

        public MyHttpResponse(string error)
        {
            Error = error;
        }

        public MyHttpResponse(string error, T result)
        {
            Error = error;
            Result = result;
        }
        public MyHttpResponse()
        {

        }

    }
}
