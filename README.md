# VoiceVoxCore.Sharp
VoiceVoxのCoreをC#で動かすためのラッパー

対応Coreバージョン
* 0.12.0-preview.3

## 使い方
1. x64でビルドする
2. x64/(Debug/Release)からdllをVoiceVoxのフォルダに入れる
3. 使用したいコアのdllをVoiceVoxのフォルダに入れる
4. 実行方法
```cs
    class Program
    {
        static void Main(string[] args)
        {
            IVoiceVoxSharp voiceVox = VoiceVox.CreateInstance();
            Console.WriteLine($"使用しているコアのバージョン {voiceVox.CoreVersion}");

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
```