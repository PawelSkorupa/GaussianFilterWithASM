#pragma once

#ifdef GaussCppDLL_API
	#undef GaussCppDLL_API
	#define GaussCppDLL_API __declspec(dllexport)
#else
	#define GaussCppDLL_API __declspec(dllimport)
#endif

extern "C" int GaussCppDLL_API test();

extern "C" void GaussCppDLL_API gaussBlur(BYTE inputPixels[], int size, int imageWidth, int startHeight, int endHeight);