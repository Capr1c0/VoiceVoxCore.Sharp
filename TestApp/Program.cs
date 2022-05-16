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
                    if (voicevox.TTSUnsafe("てすと", 0, out VoiceVoxWavData wav) == false)
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

                    voicevox.FreeWavData(wav);
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
    }
}
