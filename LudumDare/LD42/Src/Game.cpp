#include "Game.h"
#include "timer.h"
#include <chrono>

Game::Game()
{
	m_renderer = new Renderer(800, 600);
	m_rayCastRenderer = new RayCastRenderer(m_renderer);
}

Game::~Game()
{
	delete m_renderer;
}

void Game::start()
{
	Timer timer;
	timer.reset();

	std::string map;

	map += "################################";
	map += "#...............#..............#";
	map += "#.......#########.......########";
	map += "#..............##..............#";
	map += "#......##......##......##......#";
	map += "#......##..............##......#";
	map += "#..............##..............#";
	map += "###............####............#";
	map += "##.............###.............#";
	map += "#............####............###";
	map += "#..............................#";
	map += "#..............##..............#";
	map += "#..............##..............#";
	map += "#...........#####...........####";
	map += "#..............................#";
	map += "###..####....########....#######";
	map += "####.####.......######.........#";
	map += "#...............#..............#";
	map += "#.......#########.......##..####";
	map += "#..............##..............#";
	map += "#......##......##.......#......#";
	map += "#......##......##......##......#";
	map += "#..............##..............#";
	map += "###............####............#";
	map += "##.............###.............#";
	map += "#............####............###";
	map += "#..............................#";
	map += "#..............................#";
	map += "#..............##..............#";
	map += "#...........##..............####";
	map += "#..............##..............#";
	map += "################################";
	
	m_rayCastRenderer->setMapData(map, 32,32);
	
	m_player.setX(8.0f);
	m_player.setY(8.0f);

	long timeStep = 1000000;
	long currentStep = 0;

	auto tp1 = std::chrono::system_clock::now();
	auto tp2 = std::chrono::system_clock::now();	

	while(m_running)
	{
		// Handle Timing
		tp2 = std::chrono::system_clock::now();
		std::chrono::duration<float> elapsedTime = tp2 - tp1;
		tp1 = tp2;
		float ElapsedTime = elapsedTime.count();
		
		m_renderer->setWindowTitle("FPS: " + to_string(1.0f / ElapsedTime) + "Angle: " + to_string(m_player.getA()));

		update(ElapsedTime);
	}
}

void Game::update(float deltaTime)
{
	m_renderer->cleanPixelBuffer();

	m_input.update();

	if (m_input.keyDown(SDL_SCANCODE_ESCAPE) || m_input.hasQuit())
		m_running = false;

	if(m_input.keyDown(SDL_SCANCODE_A))
		m_player.setA(m_player.getA() - 0.8f * deltaTime);

	if (m_input.keyDown(SDL_SCANCODE_D))
		m_player.setA(m_player.getA() + 0.8f * deltaTime);

	if(m_input.keyDown(SDL_SCANCODE_W))
	{
		m_player.setX(m_player.getX() + sinf(m_player.getA()) * 5.0f * deltaTime);
		m_player.setY(m_player.getY() + cosf(m_player.getA()) * 5.0f * deltaTime);
	}

	if (m_input.keyDown(SDL_SCANCODE_S))
	{
		m_player.setX(m_player.getX() - sinf(m_player.getA()) * 5.0f * deltaTime);
		m_player.setY(m_player.getY() - cosf(m_player.getA()) * 5.0f * deltaTime);
	}

	if(m_input.keyDown(SDL_SCANCODE_Q))
	{
		m_player.setX(m_player.getX() - cosf(m_player.getA()) * 5.0f * deltaTime);
		m_player.setY(m_player.getY() + sinf(m_player.getA()) * 5.0f * deltaTime);
	}

	if (m_input.keyDown(SDL_SCANCODE_E))
	{
		m_player.setX(m_player.getX() + cosf(m_player.getA()) * 5.0f * deltaTime);
		m_player.setY(m_player.getY() - sinf(m_player.getA()) * 5.0f * deltaTime);
	}

	m_rayCastRenderer->setPlayerPosition(m_player.getX(), m_player.getY(), m_player.getA());
	m_rayCastRenderer->Draw();

	m_renderer->flip();
}


