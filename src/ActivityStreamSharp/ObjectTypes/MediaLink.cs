namespace ActivityStreamSharp.ObjectTypes
{
    public class MediaLink : ForgivingExpandoObject
    {
        public int Duration { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
    }
}