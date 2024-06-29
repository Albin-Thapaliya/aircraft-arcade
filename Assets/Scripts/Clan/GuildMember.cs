[System.Serializable]
public enum GuildRole
{
    Member,
    Officer,
    Leader
}

[System.Serializable]
public class GuildMember
{
    public string playerName;
    public int playerLevel;
    public string playerId;
    public GuildRole role;
}

public class Guild
{
    public string guildName;
    public List<GuildMember> members = new List<GuildMember>();

    public void AddMember(GuildMember member)
    {
        member.role = GuildRole.Member;
        members.Add(member);
    }

    public void RemoveMember(string playerId)
    {
        members.RemoveAll(member => member.playerId == playerId);
    }

    public void PromoteMember(Guild guild, string playerId, GuildRole newRole)
    {
        if (guild.members.Exists(member => member.playerId == playerId && member.role != GuildRole.Leader))
        {
            guild.PromoteMember(playerId, newRole);
            OnGuildNotification?.Invoke($"{playerId} has been promoted to {newRole} in {guild.guildName}.");
        }
    }

    public void OrganizeEvent(Guild guild, string eventName)
    {
        OnGuildNotification?.Invoke($"Guild event '{eventName}' is happening in {guild.guildName}!");
    }

    public void DisbandGuild(Guild guild)
    {
        guilds.Remove(guild);
        OnGuildNotification?.Invoke($"Guild '{guild.guildName}' has been disbanded.");
    }

    public void LeaveGuild(Guild guild, string playerId)
    {
        guild.RemoveMember(playerId);
        OnGuildNotification?.Invoke($"{playerId} has left {guild.guildName}.");
    }

    public void CreateGuild(string name)
    {
        Guild newGuild = new Guild { guildName = name };
        guilds.Add(newGuild);
        OnGuildNotification?.Invoke($"Guild '{name}' has been created.");
    }

    public void JoinGuild(Guild guild, GuildMember member)
    {
        guild.AddMember(member);
        OnGuildNotification?.Invoke($"{member.playerName} has joined {guild.guildName}.");
    }

    public void DisplayGuildMembers(Guild guild)
    {
        foreach (var member in guild.members)
        {
            Debug.Log($"{member.playerName} - {member.role}");
        }
    }

    public void DisplayGuilds()
    {
        foreach (var guild in guilds)
        {
            Debug.Log(guild.guildName);
        }
    }

    public delegate void GuildEvent(string message);
    public event GuildEvent OnGuildNotification;

    private static List<Guild> guilds = new List<Guild>();

    public static Guild GetGuildByName(string name)
    {
        return guilds.Find(guild => guild.guildName == name);
    }

    public static GuildMember GetGuildMemberById(string guildName, string playerId)
    {
        Guild guild = GetGuildByName(guildName);
        return guild.members.Find(member => member.playerId == playerId);
    }

}