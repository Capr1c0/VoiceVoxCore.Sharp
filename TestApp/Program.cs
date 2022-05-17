using System;
using System.IO;
using VoiceVoxCore.Sharp;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IVoiceVoxSharp voiceVox = VoiceVox.CreateInstance();
            voiceVox.Initialize(true);

            using (FileStream fs = new FileStream("Sample.wav", FileMode.Create))
            {
                voiceVox.GenerateTTS("サンプル", Speaker.ずんだもん_ノーマル, fs);
                fs.Flush();
            }

            Console.WriteLine("生成終了");
            Console.ReadLine();
            return;
        }
    }
}
