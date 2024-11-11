namespace CareGuide.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public readonly int StatusCode = 404;
        public NotFoundException() : base("Not found") { }

    }
}
