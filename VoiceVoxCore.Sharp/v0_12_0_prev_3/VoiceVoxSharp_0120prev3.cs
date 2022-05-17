using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using VoiceVoxCore.Sharp.Exceptions;
using VoiceVoxCore.Sharp.v0_12_0_prev_3.Internal;

namespace VoiceVoxCore.Sharp.v0_12_0_prev_3
{
    internal class VoiceVoxSharp_0120prev3 : IVoiceVoxSharp_0120prev3
    {
        public VoiceVoxVersion Version => VoiceVoxVersion.v0_12_0_prev_3;

        public void Initialize(bool use_gpu, int cpu_num_threads = 0, bool load_all_models = true)
        {
            VoiceVoxNative_0120prev3.Initialize(use_gpu, cpu_num_threads, load_all_models);
            byte[] path = Encoding.UTF8.GetBytes($@"open_jtalk_dic_utf_8-1.11");
            GCHandle handle = GCHandle.Alloc(path, GCHandleType.Pinned);
            VoiceVoxNative_0120prev3.LoadDictionary(path);
            handle.Free();
        }
        public bool GenerateTTS(string text, Speaker speaker, Stream dstStream)
        {
            byte[] binary = Encoding.UTF8.GetBytes(text);
            return this.GenerateTTS(binary, speaker, dstStream);
        }

        public bool GenerateTTS(byte[] text, Speaker speaker, Stream dstStream)
        {
            IntPtr ptr = IntPtr.Zero;
            int size = 0;
            GCHandle handle = GCHandle.Alloc(text, GCHandleType.Pinned);
            var result = VoiceVoxNative_0120prev3.GenerateTTS(text, (long)speaker, ref size, ref ptr);
            handle.Free();
            if (result != VoicevoxResultCode.VOICEVOX_RESULT_SUCCEED)
            {
                switch (result)
                {
                    case VoicevoxResultCode.VOICEVOX_RESULT_NOT_LOADED_OPENJTALK_DICT:
                        {
                            throw new VoiceVoxDictionaryNotFoundException();
                        }
                }
                return false;
            }
            unsafe
            {
                var data = new ReadOnlySpan<byte>(ptr.ToPointer(), size);
                dstStream.Write(data);
            }
            VoiceVoxNative_0120prev3.FreeWav(ref ptr);
            return true;
        }
    }
}
