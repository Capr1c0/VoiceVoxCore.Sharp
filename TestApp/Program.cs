using System;
using System.IO;
using VoiceVoxCore.Sharp;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (VoiceVox voicevox = new VoiceVox())
                {
                    voicevox.Initialize(true);

                    GenerateTTS(voicevox, "サンプル1");
                    GenerateTTS(voicevox, "サンプル2");
                    GenerateTTS(voicevox, "サンプル3");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Catch Exception");
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("生成終了");
            Console.ReadLine();
            return;
        }

        static void GenerateTTS(VoiceVox voiceVox, string text)
        {
            Console.WriteLine($"生成開始 {text}");
            if (voiceVox.TTSUnsafe(text, 0, out VoiceVoxWavData wav) == false)
            {
                Console.WriteLine("生成失敗");
                Console.ReadLine();
                return;
            }
            using (FileStream fs = new FileStream("Sample.wav", FileMode.Create))
            {
                fs.Write(wav.Data);
                fs.Flush();
            }

            voiceVox.FreeWavData(wav);
            Console.WriteLine("生成終了");
            return;
        }
    }
}
