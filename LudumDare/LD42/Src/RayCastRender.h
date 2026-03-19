#include "Renderer.h"

class RayCastRenderer
{
public:
	RayCastRenderer(Renderer* renderer);
	~RayCastRenderer();

	void setPlayerPosition(float x, float y, float angle)
	{
		m_playerX = x;
		m_playerY = y;
		m_playerA = angle;
	}

	void setMapData(std::string& map, int width, int height)
	{
		m_map = &map;
		m_mapWidth = width;
		m_mapHeight = height;
	}

	void Draw();

private:
	Renderer* m_renderer;
	float m_playerX = 0;
	float m_playerY = 0;
	float m_playerA = 0;
	std::string* m_map;
	int m_mapWidth = 0;
	int m_mapHeight = 0;

	const float m_fov = 3.14159f / 4.0f;
	const float m_maxDistance = 16.0f;
	const float m_stepSize = 0.01f;

};