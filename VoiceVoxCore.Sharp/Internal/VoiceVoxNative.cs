using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VoiceVoxCore.Sharp
{
    internal static class VoiceVoxNative
    {
        public enum VoicevoxResultCode
        {
            // 成功
            VOICEVOX_RESULT_SUCCEED = 0,
            // OpenJTalk辞書がロードされていない
            VOICEVOX_RESULT_NOT_LOADED_OPENJTALK_DICT = 1,
        }

        /**
         * @fn
         * 初期化する
         * @brief 音声合成するための初期化を行う。他の関数を正しく実行するには先に初期化が必要
         * @param use_gpu trueならGPU用、falseならCPU用の初期化を行う
         * @param cpu_num_th推論に用いるスレッド数を設定する。0の場合論理コア数の半分か、物理コア数が設定されるreads 
         * @param load_all_models trueなら全てのモデルをロードする
         * @return 成功したらtrue、失敗したらfalse
         * @detail
         * 何度も実行可能。use_gpuを変更して実行しなおすことも可能。
         * 最後に実行したuse_gpuに従って他の関数が実行される。
         */
        [DllImport("VoiceVox.Core.Wrapper")]
        public static extern bool Initialize(bool use_gpu, int cpu_num_threads = 0, bool load_all_models = true);

        ///**
        // * モデルをロードする
        // * @param speaker_id 話者番号
        // * @return 成功したらtrue、失敗したらfalse
        // * @detail
        // * 必ずしも話者とモデルが1:1対応しているわけではない。
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern bool load_model(long speaker_id);

        ///**
        // * @fn
        // * モデルがロード済みかどうか
        // * @param speaker_id 話者番号
        // * @return ロード済みならtrue、そうでないならfalse
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern bool is_model_loaded(long speaker_id);

        /**
         * @fn
         * 終了処理を行う
         * @brief 終了処理を行う。以降関数を利用するためには再度初期化を行う必要がある。
         * @detail
         * 何度も実行可能。実行せずにexitしても大抵の場合問題ないが、
         * CUDAを利用している場合これを実行しておかないと例外が起こることがある。
         */
        [DllImport("VoiceVox.Core.Wrapper")]
        public static extern void Finalize();

        ///**
        // * @fn
        // * メタ情報を取得する
        // * @brief 話者名や話者IDのリストを取得する
        // * @return メタ情報が格納されたjson形式の文字列
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern byte[] metas();

        ///**
        // * @fn
        // * 対応デバイス情報を取得する
        // * @brief cpu, cudaのうち、使用可能なデバイス情報を取得する
        // * @return 各デバイスが使用可能かどうかをboolで格納したjson形式の文字列
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern byte[] supported_devices();

        ///**
        // * @fn
        // * 音素ごとの長さを求める
        // * @brief 音素列から、音素ごとの長さを求める
        // * @param length 音素列の長さ
        // * @param phoneme_list 音素列
        // * @param speaker_id 話者番号
        // * @return 音素ごとの長さ
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern bool yukarin_s_forward(long length, ref long phoneme_list, ref long speaker_id, ref float output);

        ///**
        // * @fn
        // * モーラごとの音高を求める
        // * @brief モーラごとの音素列とアクセント情報から、モーラごとの音高を求める
        // * @param length モーラ列の長さ
        // * @param vowel_phoneme_list 母音の音素列
        // * @param consonant_phoneme_list 子音の音素列
        // * @param start_accent_list アクセントの開始位置
        // * @param end_accent_list アクセントの終了位置
        // * @param start_accent_phrase_list アクセント句の開始位置
        // * @param end_accent_phrase_list アクセント句の終了位置
        // * @param speaker_id 話者番号
        // * @return モーラごとの音高
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern bool yukarin_sa_forward(long length, ref long vowel_phoneme_list, ref long consonant_phoneme_list,
        //                                          ref long start_accent_list, ref long end_accent_list,
        //                                          ref long start_accent_phrase_list, ref long end_accent_phrase_list,
        //                                          ref long speaker_id, ref float output);

        ///**
        // * @fn
        // * 波形を求める
        // * @brief フレームごとの音素と音高から、波形を求める
        // * @param length フレームの長さ
        // * @param phoneme_size 音素の種類数
        // * @param f0 フレームごとの音高
        // * @param phoneme フレームごとの音素
        // * @param speaker_id 話者番号
        // * @return 音声波形
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern bool decode_forward(long length, long phoneme_size, ref float f0, ref float phoneme,
        //                                      ref long speaker_id, ref float output);

        ///**
        // * @fn
        // * 最後に発生したエラーのメッセージを取得する
        // * @return エラーメッセージ
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern byte[] last_error_message();

        /**
         * @fn
         * open jtalkの辞書を読み込む
         * @return 結果コード
         */
        [DllImport("VoiceVox.Core.Wrapper")]
        public static extern VoicevoxResultCode LoadDictionary(byte[] dict_path);

        /**
         * @fn
         * text to spearchを実行する
         * @param text 音声データに変換するtextデータ
         * @param speaker_id 話者番号
         * @param output_binary_size 音声データのサイズを出力する先のポインタ
         * @param output_wav 音声データを出力する先のポインタ。使用が終わったらvoicevox_wav_freeで開放する必要がある
         * @return 結果コード
         */
        [DllImport("VoiceVox.Core.Wrapper")]
        public static extern VoicevoxResultCode GenerateTTS(byte[] text, long speaker_id, ref int output_binary_size,
                                                                 ref IntPtr output_wav);

        ///**
        // * @fn
        // * text to spearchをAquesTalkライクな記法で実行する
        // * @param text 音声データに変換するtextデータ
        // * @param speaker_id 話者番号
        // * @param output_binary_size 音声データのサイズを出力する先のポインタ
        // * @param output_wav 音声データを出力する先のポインタ。使用が終わったらvoicevox_wav_freeで開放する必要がある
        // * @return 結果コード
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern VoicevoxResultCode voicevox_tts_from_kana(byte[] text, long speaker_id,
        //                                                    ref int output_binary_size, ref IntPtr output_wav);

        /**
         * @fn
         * voicevox_ttsで生成した音声データを開放する
         * @param wav 開放する音声データのポインタ
         */
        [DllImport("VoiceVox.Core.Wrapper")]
        public static extern void FreeWav(IntPtr wav);

        ///**
        // * @fn
        // * エラーで返ってきた結果コードをメッセージに変換する
        // * @return エラーメッセージ文字列
        // */
        //[DllImport("VoiceVox.Core.Wrapper")]
        //public static extern byte[] voicevox_error_result_to_message(VoicevoxResultCode result_code);
    }
}
