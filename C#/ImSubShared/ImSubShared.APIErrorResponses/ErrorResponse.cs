namespace ImSubShared.APIErrorResponses
{
    
    public class ErrorResponse<T> where T : class
    {
        public SubCode SubCode { get; set; }
        public T ErrorDescription { get; set; }
    }
}
