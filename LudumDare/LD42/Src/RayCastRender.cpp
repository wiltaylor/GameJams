#include "RayCastRender.h"

void RayCastRenderer::Draw()
{
	for (int x = 0; x < m_renderer->getScreenWidth(); x++)
	{
		float rayAngle = (m_playerA - m_fov / 2.0f) + (static_cast<float>(x) / static_cast<float>(m_renderer->getScreenWidth())) * m_fov;
		float distanceToWall = 0;
		bool hitWall = false;

		float eyeX = sinf(rayAngle);
		float eyeY = cosf(rayAngle);

		while (!hitWall && distanceToWall < m_maxDistance)
		{
			distanceToWall += m_stepSize;

			int testX = static_cast<int>(m_playerX + eyeX * distanceToWall);
			int testY = static_cast<int>(m_playerY + eyeY * distanceToWall);

			if (testX < 0 || testX >= m_mapWidth || testY < 0 || testY >= m_mapHeight)
			{
				hitWall = true;
				distanceToWall = m_maxDistance;
			}
			else
			{
				if (m_map->at(testY * m_mapWidth + testX) == '#')
				{
					hitWall = true;
				}
			}
		}

		int ceiling = static_cast<float>(m_renderer->getScreenHeight() / 2.0) - m_renderer->getScreenHeight() / static_cast<float>(distanceToWall);
		int floor = m_renderer->getScreenHeight() - ceiling;

		for (int y = 0; y < m_renderer->getScreenHeight(); y++)
		{
			if (y < ceiling)
			{
				m_renderer->setPixel(x, y, 0xFFFF0000);
			}
			else if (y > ceiling && y <= floor)
			{
				m_renderer->setPixel(x, y, 0xFF0000FF);
			}
			else
			{
				m_renderer->setPixel(x, y, 0xFF00FF00);
			}
		}

	}
}

RayCastRenderer::RayCastRenderer(Renderer* renderer): m_renderer{ renderer }
{

} 

RayCastRenderer::~RayCastRenderer()
{
	

}
