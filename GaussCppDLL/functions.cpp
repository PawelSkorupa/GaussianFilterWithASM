
#include "pch.h"
#include "functions.h"

int test()
{
	int a = 1;
	int b = 2;
	return a + b;
}


void gaussBlur(BYTE inputPixels[], int size, int imageWidth, int startHeight, int endHeight) {
	int matrix[] = {
	1, 2, 1,
	2, 4, 2,
	1, 2, 1
	};
	int width = imageWidth * 3;
	for (int i = width * startHeight; i < width * endHeight; i += 3) {
		if ((i - width - 3) >= 0 && (i - width + 3) >= 0 && (i - width + 3) < size && (i + width - 3) >= 0 && (i + width - 3) < size) {
			int newPixel1 = inputPixels[i - width - 3] * matrix[0] + inputPixels[i - width] * matrix[1] + inputPixels[i - width + 3] * matrix[2] +
				inputPixels[i - 3] * matrix[3] + inputPixels[i] * matrix[4] + inputPixels[i + 3] * matrix[5] +
				inputPixels[i + width - 3] * matrix[6] + inputPixels[i + width] * matrix[7] + inputPixels[i + width + 3] * matrix[8];
			int newPixel2 = inputPixels[i + 1 - width - 3] * matrix[0] + inputPixels[i + 1 - width] * matrix[1] + inputPixels[i + 1 - width + 3] * matrix[2] +
				inputPixels[i + 1 - 3] * matrix[3] + inputPixels[i + 1] * matrix[4] + inputPixels[i + 1 + 3] * matrix[5] +
				inputPixels[i + 1 + width - 3] * matrix[6] + inputPixels[i + 1 + width] * matrix[7] + inputPixels[i + 1 + width + 3] * matrix[8];
			int newPixel3 = inputPixels[i + 2 - width - 3] * matrix[0] + inputPixels[i + 2 - width] * matrix[1] + inputPixels[i + 2 - width + 3] * matrix[2] +
				inputPixels[i + 2 - 3] * matrix[3] + inputPixels[i + 2] * matrix[4] + inputPixels[i + 2 + 3] * matrix[5] +
				inputPixels[i + 2 + width - 3] * matrix[6] + inputPixels[i + 2 + width] * matrix[7] + inputPixels[i + 2 + width + 3] * matrix[8];
			inputPixels[i] = newPixel1 / 16;
			inputPixels[i + 1] = newPixel2 / 16;
			inputPixels[i + 2] = newPixel3 / 16;
		}
	}
}