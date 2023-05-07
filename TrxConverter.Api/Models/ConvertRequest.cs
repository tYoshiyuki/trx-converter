using System.ComponentModel.DataAnnotations;

namespace TrxConverter.Api.Models
{
    /// <summary>
    /// 変換リクエスト
    /// </summary>
    public class ConvertRequest
    {
        /// <summary>
        /// 変換対象となるファイル
        /// </summary>
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
