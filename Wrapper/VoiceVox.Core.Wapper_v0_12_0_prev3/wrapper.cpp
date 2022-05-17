#include "wrapper.h"

VOICEVOX_WRAPPER void Core_initialize(bool use_gpu, int cpu_num_threads, bool load_all_models)
{
	initialize(use_gpu, cpu_num_threads, load_all_models);
}

VOICEVOX_WRAPPER VoicevoxResultCode Core_voicevox_tts(const char* text, int64_t speaker_id, int* output_binary_size, uint8_t** output_wav)
{
	return voicevox_tts(text, speaker_id, output_binary_size, output_wav);
}

VOICEVOX_WRAPPER void Core_finalize()
{
	finalize();
}

VOICEVOX_WRAPPER void Core_voicevox_wav_free(uint8_t** wav)
{
	voicevox_wav_free(*wav);
}

VOICEVOX_WRAPPER VoicevoxResultCode Core_voicevox_load_openjtalk_dict(const char* dict_path )
{
	return voicevox_load_openjtalk_dict(dict_path);
}
