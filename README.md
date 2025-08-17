![Proyekt Banneri](musicplayer/banner1.png)

# Console Music Player

A simple **console-based music player** written in C# using [NAudio](https://github.com/naudio/NAudio).  
It allows you to create playlists, control volume, navigate tracks, and keeps a history of played music.

## Features

- Add music files to a playlist
- Play, pause, skip to next or previous track
- Adjust volume from 0% to 100%
- Automatic playback of the next track when current track finishes
- Maintains a history of played tracks

## Requirements

- [.NET 9.0+](https://dotnet.microsoft.com/)
- [NAudio library](https://www.nuget.org/packages/NAudio/)

## Usage

1. Clone this repository:
 git clone https://github.com/alisuleymanlii/ConsoleMusicPlayer.git

2. Install NAudio via NuGet:

   dotnet add package NAudio

3. Run the project:

   dotnet run

4. Add music files when prompted, then use the following commands:

   play      - Play the current track
   pause     - Pause playback
   next      - Play next track
   prev      - Play previous track
   volume X  - Set volume to X% (0-100)
   history   - Show list of previously played tracks

## Notes

* Music history is saved in `history.txt` in the project directory.
* Volume is set to 50% by default.

## License

This project is licensed under the MIT License.


## 👤 Author

**Ali Suleymanli**


