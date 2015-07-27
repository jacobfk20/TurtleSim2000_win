// SDLtest1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <SDL.h>

int _tmain(int argc, _TCHAR* argv[])
{
	if (SDL_Init(SDL_INIT_VIDEO) != 0) {
		printf("SDL Init Error");
		return 1;
	}

	SDL_Window *win = SDL_CreateWindow("Hello World!", 100, 100, 640, 480, SDL_WINDOW_SHOWN);
	if (win == nullptr) {
		printf("Could not create window.");
		SDL_Quit();
		return 1;
	}

	return 0;
	
}

