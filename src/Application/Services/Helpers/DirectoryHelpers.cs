namespace codecrafters_http_server.src.Application.Services.Helpers
{
    public static class DirectoryHelpers
    {
        public  static string GetDirectoryPath(string[] args)
        {
            string directoryPath = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--directory" && i + 1 < args.Length)
                {
                    directoryPath = args[i + 1];
                    break;
                }
            }
            return directoryPath;

        }
    }
}
