/**
*  @file    timer.cpp
*  @author  Wil Taylor (wilfridtaylor@gmail.com)
*  @date    21/3/2018
*  @version 1.0
*
*  @brief Simple timer class
*
*  @section DESCRIPTION
*
*  See header file for detailed comments on each method.
*
*/
#include "timer.h"

 
double Timer::seconds() const
{
	return chrono::duration_cast<chrono::milliseconds>(Clock::now() - _epoch).count() / 1000.0;
}

 
long Timer::milliseconds() const
{
	return static_cast<long>(chrono::duration_cast<chrono::milliseconds>(Clock::now() - _epoch).count());
}

 
long Timer::nanoseconds() const
{
	return static_cast<long>(chrono::duration_cast<chrono::nanoseconds>(Clock::now() - _epoch).count());

}

void Timer::UpdateFPS()
{
	const auto current = Clock::now();
	m_fps = 1000.0f / chrono::duration_cast<chrono::milliseconds>(current - _lastFPSCheck).count();
	_lastFPSCheck = current;
}

