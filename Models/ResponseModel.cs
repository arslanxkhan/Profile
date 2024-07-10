namespace Models
{
    public class ResponseModel
    {


        public ResponseModel(bool isSuccess, string title)
        {
            Title = title ?? string.Empty;
            IsSuccess = isSuccess;
        }


        public ResponseModel(string title, string content)
        {
            Content = content ?? string.Empty;
            Title = title ?? string.Empty;
        }

        public ResponseModel(Dictionary<string, string> errors)
        {
            Errors = errors;
        }

        public ResponseModel(dynamic data)
        {
            Data = data ?? string.Empty;
        }

        public ResponseModel(bool isSuccess, string title, dynamic data)
        {
            Title = title ?? string.Empty;
            Data = data ?? string.Empty;
            IsSuccess = isSuccess;
        }


        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

        public dynamic? Data { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public bool IsSuccess { get; set; }

    }
}
