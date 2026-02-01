#pragma once

#ifdef NATIVEMATHDLL_EXPORTS
#define NATIVEMATHDLL_API __declspec(dllexport)
#else
#define NATIVEMATHDLL_API __declspec(dllimport)
#endif

extern "C" {
    NATIVEMATHDLL_API int Multiply(int a, int b);
}
