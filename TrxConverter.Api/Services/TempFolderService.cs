namespace TrxConverter.Api.Services
{
    /// <summary>
    /// 一時フォルダのインターフェースです。
    /// </summary>
    public interface ITempFolderService
    {
        /// <summary>
        /// 一時フォルダを作成する。
        /// </summary>
        /// <returns>一時フォルダのパス</returns>
        string Create();
    }

    /// <summary>
    /// 一時フォルダのサービスです。
    /// </summary>
    public class TempFolderService : ITempFolderService
    {
        /// <summary>
        /// 一時フォルダを作成する。
        /// </summary>
        /// <returns>一時フォルダのパス</returns>
        public string Create()
        {
            var path = Path.Combine(AppContext.BaseDirectory, Guid.NewGuid().ToString());
            Directory.CreateDirectory(path);
            return path;
        }
    }
}
