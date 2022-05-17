using System.IO;

namespace VoiceVoxCore.Sharp
{
    /// <summary>
    /// VoiceVoxを操作するインターフェース
    /// </summary>
    public interface IVoiceVoxSharp
    {
        /// <summary>
        /// コアのバージョン    
        /// </summary>
        VoiceVoxVersion CoreVersion { get; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="use_gpu">GPUを使用するかどうか</param>
        /// <param name="cpu_num_threads">推論に用いるスレッド数を設定する。0の場合論理コア数の半分か、物理コア数が設定される</param>
        /// <param name="load_all_models">trueなら全てのモデルをロードする</param>
        void Initialize(bool use_gpu, int cpu_num_threads = 0, bool load_all_models = true);

        /// <summary>
        /// TTSを実行する
        /// </summary>
        /// <param name="text">読み上げ対象文字列</param>
        /// <param name="speaker">Spekaer</param>
        /// <param name="dstStream">書き込み対象Stream</param>
        bool GenerateTTS(string text, Speaker speaker, Stream dstStream);

        /// <summary>
        /// TTSを実行する
        /// </summary>
        /// <param name="text">読み上げ対象文字列(UTF-8)</param>
        /// <param name="speaker">Spekaer</param>
        /// <param name="dstStream">書き込み対象Stream</param>
        bool GenerateTTS(byte[] text, Speaker speaker, Stream dstStream);
    }
}
