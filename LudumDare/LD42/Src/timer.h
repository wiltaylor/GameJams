/**
*  @file    timer.h
*  @author  Wil Taylor (wilfridtaylor@gmail.com)
*  @date    21/3/2018
*  @version 1.0
*
*  @brief Simple timer class
*
*  @section DESCRIPTION
*
*  This is a simple class that can be used for game timing.
*  
*  simply call reset to start the timer and the unit methods
*  will return how long since those methods have been called.
*
*/
#ifndef __TIMER_H_
#define __TIMER_H_
#include <chrono>

using namespace std;

class Timer
{
	typedef chrono::high_resolution_clock Clock;
public:
	Timer() {}
	~Timer() {}

	/**
	 * \brief Resets the epoch to current time.
	 */
	void reset() { _epoch = Clock::now(); }

	/**
	 * \brief How many seconds since reset was last called.
	 * \return Seconds since reset was called.
	 */
	double seconds() const;


	/**
	 * \brief How many milliseconds since reset was last called.
	 * \return milliseconds since reset was called.
	 */
	long milliseconds() const;


	/**
	 * \brief How many nanoseconds since reset was last called.
	 * \return nanoseconds since reset was called.
	 */
	long nanoseconds() const;


	/**
	 * \brief Updates FPS counter by 1 frame.
	 */
	void UpdateFPS();


	/**
	 *  \brief Retrives current FPS
	 *  \returns FPS counter.
	 */
	double getFPS() { return m_fps;	}

private:
	Clock::time_point _epoch;
	double m_fps = 0.0;
	Clock::time_point _lastFPSCheck;
};

#endif