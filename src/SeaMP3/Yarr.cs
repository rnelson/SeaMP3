using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace SeaMP3;

internal static class Yarr
{
    public static async Task Setup()
    {
        await Utils.DownloadYtDlp();
        await Utils.DownloadFFmpeg();
    }

    public static async Task<RunResult<string>> Download(string url)
    {
        var ytdl = new YoutubeDL();
        var result = await ytdl.RunAudioDownload(
            url,
            AudioConversionFormat.Mp3
        );

        if (result.Success)
        {
            var destination = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads",
                Path.GetFileName(result.Data)
            );
            File.Move(result.Data, destination);
        }

        return result;
    }
}
