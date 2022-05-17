using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using VoiceVoxCore.Sharp.Exceptions;
using VoiceVoxCore.Sharp.v0_12_0_prev_3.Internal;

namespace VoiceVoxCore.Sharp
{
    public static class VoiceVox
    {
        /// <summary>
        /// 最新バージョン
        /// </summary>
        private static readonly VoiceVoxVersion _latestVersionCore = VoiceVoxVersion.v0_12_0_prev_3;

        /// <summary>
        /// VoiceVoxSharpのインスタンスを作成する
        /// </summary>
        /// <returns></returns>
        public static IVoiceVoxSharp CreateInstance()
        {
            return CreateInstance(_latestVersionCore);
        }

        /// <summary>
        /// VoiceVoxSharpのインスタンスを作成する
        /// </summary>
        /// <param name="voiceVoxVersion"></param>
        /// <returns></returns>
        public static IVoiceVoxSharp CreateInstance(VoiceVoxVersion voiceVoxVersion)
        {
            switch (voiceVoxVersion)
            {
                case VoiceVoxVersion.v0_12_0_prev_3:
                    {
                        return new v0_12_0_prev_3.VoiceVoxSharp_0120prev3();
                    }
                default:
                    {
                        throw new VoiceVoxVersionNotFoundException();
                    }
            }
        }
    }
}
