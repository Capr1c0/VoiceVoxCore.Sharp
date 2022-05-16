#pragma once
#include "core.h"

#define VOICEVOX_WRAPPER extern "C" __declspec(dllexport)

VOICEVOX_WRAPPER void Initialize(bool use_gpu, int cpu_num_threads, bool load_all_models);
VOICEVOX_WRAPPER VoicevoxResultCode GenerateTTS(const char* text, int64_t speaker_id, int* output_binary_size, uint8_t** output_wav);
VOICEVOX_WRAPPER VoicevoxResultCode LoadDictionary(const char* dict_path);
VOICEVOX_WRAPPER void Exit();
VOICEVOX_WRAPPER void FreeWav(uint8_t** wav);
