namespace Application.Core
{
    public class AppException
    {
        public AppException(int statusCode, string mesaage, string details = null)
        {
            StatusCode = statusCode;
            Mesaage = mesaage;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Mesaage { get; set; }
        public string Details { get; set; }
    }
}