#pragma once
#include "Renderer.h"
#include "RayCastRender.h"
#include "Player.h"
#include "Input.h"

class Game
{
public:
	Game();
	~Game();

	void start();	

private:
	bool m_running = true;
	Renderer* m_renderer;
	RayCastRenderer* m_rayCastRenderer;
	Player m_player;
	Input m_input;
	
	void update(float deltaTime);
};
