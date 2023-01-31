namespace First_For_Mvc_Project.Contracts.SliderImage
{
    public enum FileExtensions
    {
        JPG = 1,
        PNG = 2,
        JPEG = 4,
        MP4=8,
    }

    public static class FileExtensionExtensions
    {
        public static string GetExtension(this FileExtensions fileExtensions)
        {
            switch (fileExtensions)
            {
                case FileExtensions.JPG:
                    return $".{nameof(FileExtensions.JPG).ToLowerInvariant()}";
                case FileExtensions.PNG:
                    return $".png";
                case FileExtensions.JPEG:
                    return $".jpg";
                case FileExtensions.MP4:
                    return $".mp4";
                default:
                    throw new Exception("This extension not found");
            }
        }
    }
}
