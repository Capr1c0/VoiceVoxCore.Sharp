using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VoiceVoxCore.Sharp
{
    public partial class VoiceVox : IDisposable
    {
        /// <summary>
        /// 解放済みかどうか
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="use_gpu">GPUを使用するかどうか</param>
        /// <param name="cpu_num_threads">推論に用いるスレッド数を設定する。0の場合論理コア数の半分か、物理コア数が設定される</param>
        /// <param name="load_all_models">trueなら全てのモデルをロードする</param>
        public void Initialize(bool use_gpu, int cpu_num_threads = 0, bool load_all_models = true)
        {
            
            VoiceVoxNative.Initialize(use_gpu, cpu_num_threads , load_all_models);
            byte[] path = Encoding.UTF8.GetBytes($@"open_jtalk_dic_utf_8-1.11");
            GCHandle handle = GCHandle.Alloc(path, GCHandleType.Pinned);
            VoiceVoxNative.LoadDictionary(path);
            handle.Free();
        }

        /// <summary>
        /// TTSを実行します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="speaker_id"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool TTSUnsafe(string text, long speaker_id, out VoiceVoxWavData data)
        {
            byte[] binary = Encoding.UTF8.GetBytes(text);
            return this.TTSUnsafe(binary, speaker_id, out data);
        }
        /// <summary>
        /// TTSを実行します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="speaker_id"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool TTSUnsafe(byte[] text, long speaker_id, out VoiceVoxWavData data)
        {
            IntPtr ptr = IntPtr.Zero;
            int size = 0;
            var result = VoiceVoxNative.GenerateTTS(text, speaker_id, ref size, ref ptr);
            Console.WriteLine(result);
            Console.WriteLine($"Size = {size}");
            if (result != VoiceVoxNative.VoicevoxResultCode.VOICEVOX_RESULT_SUCCEED)
            {
                data = default;
                return false;
            }
            unsafe
            {
                data = new VoiceVoxWavData(
                    ptr,
                    new Span<byte>(ptr.ToPointer(), size)
                    );
            }
            return true;
        }

        public void FreeWavData(VoiceVoxWavData wavData)
        {
            VoiceVoxNative.FreeWav(ref wavData.ptr);
            wavData.ptr = IntPtr.Zero;
        }

        /// <summary>
        /// 解放処理
        /// </summary>
        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }
            VoiceVoxNative.Exit();
            this.isDisposed = true;
        }
    }


    public partial class VoiceVox : IAsyncDisposable
    {
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="use_gpu">GPUを使用するかどうか</param>
        /// <param name="cpu_num_threads">推論に用いるスレッド数を設定する。0の場合論理コア数の半分か、物理コア数が設定される</param>
        /// <param name="load_all_models">trueなら全てのモデルをロードする</param>
        public async Task InitializeAsync(bool use_gpu, int cpu_num_threads = 0, bool load_all_models = true)
        {
            await Task.Run(() =>
            {
                this.Initialize(use_gpu, cpu_num_threads, load_all_models);
            });
        }


        public async ValueTask DisposeAsync()
        {
            await Task.Run(this.Dispose);
        }
    }

    public ref struct VoiceVoxWavData
    {
        public IntPtr ptr;
        public Span<byte> Data;

        public VoiceVoxWavData(IntPtr ptr, Span<byte> data)
        {
            this.ptr = ptr;
            Data = data;
        }
    }
}
