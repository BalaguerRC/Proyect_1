namespace Api_User.Wrappers
{
    public class Pagination<T>
    {
        public Pagination() { }
        public Pagination(T data)
        {
            Succedd = true;
            Message = "";
            Errors = null;
            Data = data;

        }
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Succedd { get; set; }
        public string[] Errors { get; set; }
    }
}
