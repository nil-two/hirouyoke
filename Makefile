.PHONY: all windows mac linux

all: windows mac linux

windows:
	Unity -batchmode -nographics -quit -projectPath . -buildWindows64Player ./Builds/hirouyoke_windows/hirouyoke.exe
	cp README.md ./Builds/hirouyoke_windows/README.txt
	cd Builds && zip -r  hirouyoke_windows.zip ./hirouyoke_windows
	rm -rf ./Builds/hirouyoke_windows

mac:
	Unity -batchmode -nographics -quit -projectPath . -buildOSXUniversalPlayer ./Builds/hirouyoke_mac/hirouyoke
	cp README.md ./Builds/hirouyoke_mac/README.txt
	cd Builds && zip -r  hirouyoke_mac.zip ./hirouyoke_mac
	rm -rf ./Builds/hirouyoke_mac

linux:
	Unity -batchmode -nographics -quit -projectPath . -buildLinux64Player ./Builds/hirouyoke_linux/hirouyoke
	cp README.md ./Builds/hirouyoke_linux/README.txt
	cd Builds && zip -r  hirouyoke_linux.zip ./hirouyoke_linux
	rm -rf ./Builds/hirouyoke_linux
