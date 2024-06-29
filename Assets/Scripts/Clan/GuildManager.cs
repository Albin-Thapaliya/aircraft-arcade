public class GuildManager : MonoBehaviour
{
    public List<Guild> guilds = new List<Guild>();

    public delegate void GuildEvent(string message);
    public event GuildEvent OnGuildNotification;

    public void CreateGuild(string name)
    {
        Guild newGuild = new Guild { guildName = name };
        guilds.Add(newGuild);
        OnGuildNotification?.Invoke($"Guild '{name}' has been created.");
    }

    public void JoinGuild(string guildName, GuildMember member)
    {
        Guild guild = guilds.Find(x => x.guildName == guildName);
        if (guild != null)
        {
            guild.AddMember(member);
            OnGuildNotification?.Invoke($"{member.playerName} has joined {guildName}.");
        }
    }

    public void LeaveGuild(string guildName, string playerId)
    {
        Guild guild = guilds.Find(x => x.guildName == guildName);
        if (guild != null)
        {
            guild.RemoveMember(playerId);
            OnGuildNotification?.Invoke($"Member {playerId} has left {guildName}.");
        }
    }
}