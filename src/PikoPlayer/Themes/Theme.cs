namespace PikoPlayer.Themes
{
    public class Theme
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }

        public string Play => System.IO.Path.Combine(Path, "play.png");
        public string Prev => System.IO.Path.Combine(Path, "prev.png");
        public string Next => System.IO.Path.Combine(Path, "next.png");
    }
}
