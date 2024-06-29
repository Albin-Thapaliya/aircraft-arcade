# Aircraft Arcade Multiplayer Game Project

## Introduction
Welcome to our open-source multiplayer game project built with Unity. This game features a rich multiplayer environment with dynamic matchmaking, communication tools, guild management, and spectator functionalities tailored for an engaging user experience.

## Pictures
![image](https://github.com/Albin-Thapaliya/aircraft-arcade/assets/173128584/4dfa517c-1231-4d22-94d4-4d03f903434a)
![image](https://github.com/Albin-Thapaliya/aircraft-arcade/assets/173128584/30f99a54-bc9a-4df5-8741-453903264147)

## Features
### Advanced Matchmaking
- Skill-based and preference-based player matching.
- Customizable settings for game modes and maps.

### Real-Time Communication
- In-game chat system allowing players to communicate during matches.
- Voice chat integration (planned).

### Guild System
- Create and manage guilds with multiple member roles.
- Guild events and notifications.
- Experience and leveling system for guilds.

### Spectator Mode
- Toggle between different player views.
- Access to real-time game stats and events.

## Installation
To set up the game for development or personal use:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Albin-Thapaliya/multiplayer-game.git
   ```
2. **Open the project in Unity:**
   - Ensure you have Unity 2020.3 LTS or later installed.
   - Open Unity Hub, add the cloned project directory, and click on it to open.

3. **Run the project:**
   - Navigate to the main scene in the Unity Editor.
   - Press the play button to start the editor simulation.

## Usage
### Matchmaking
```csharp
var matchmakingController = FindObjectOfType<MatchmakingController>();
matchmakingController.StartMatchmaking();
```

### Joining a Guild
```csharp
var guildManager = FindObjectOfType<GuildManager>();
var member = new GuildMember { playerName = "PlayerName", playerLevel = 5, playerId = "PlayerID" };
guildManager.JoinGuild("GuildName", member);
```

### Spectating a Game
```csharp
var spectatorController = FindObjectOfType<SpectatorController>();
spectatorController.EnableSpectatorMode();
```

## Contributing
We encourage community contributions! Here’s how you can contribute:
1. **Fork** the repository to your GitHub account.
2. **Create a branch** for your feature or fix.
3. **Commit** your changes with clear, descriptive messages.
4. **Push** your branch and submit a **pull request** to our repository.
5. **Review** the changes with the team, make necessary adjustments, and merge your code if accepted.

Please refer to our [contribution guidelines](LINK_TO_CONTRIBUTING_GUIDELINES) for more detailed instructions.

## Support
For support, please open an issue in the GitHub issue tracker or contact us directly at support@example.com.

## License
This project is licensed under the MIT License - see the [LICENSE](LINK_TO_LICENSE) file for details.

## Additional Resources
- [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
- [Photon Networking Guide](https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro)
