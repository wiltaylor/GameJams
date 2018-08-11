cmake_minimum_required(VERSION 3.11 FATAL_ERROR)
set(CMAKE_CXX_STANDARD 17)
enable_language(CXX)
include(CheckSymbolExists)
check_symbol_exists("_M_AMD64" "" Win64)

project(LD42)

set(CMAKE_MODULE_PATH ${CMAKE_SOURCE_DIR}/cmake/Modules)

if(WIN32)
	set(SDL2_DIR "${CMAKE_SOURCE_DIR}/deps/Windows/sdl2")
endif()

include_directories(${SDL2_INCLUDE_DIR})

find_package(SDL2 REQUIRED)

add_executable(LD42 src/Main.cpp src/Game.cpp src/Player.cpp src/RayCastRender.cpp src/Renderer.cpp src/Timer.cpp src/Input.cpp)
target_link_libraries(LD42 ${SDL2_LIBRARIES})

if(WIN32)
	if( Win64)
		add_custom_command(TARGET LD42 POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy ${SDL2_DIR}/lib/x64/SDL2.dll ${CMAKE_CURRENT_BINARY_DIR})
	else()
		add_custom_command(TARGET LD42 POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy ${SDL2_DIR}/lib/x86/SDL2.dll ${CMAKE_CURRENT_BINARY_DIR})
	endif()
endif()
