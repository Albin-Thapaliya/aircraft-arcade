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

    public void DisbandGuild(string guildName)
    {
        Guild guild = guilds.Find(x => x.guildName == guildName);
        if (guild != null)
        {
            guilds.Remove(guild);
            OnGuildNotification?.Invoke($"Guild '{guildName}' has been disbanded.");
        }
    }

    public void DisplayGuildMembers(string guildName)
    {
        Guild guild = guilds.Find(x => x.guildName == guildName);
        if (guild != null)
        {
            foreach (var member in guild.members)
            {
                Debug.Log($"{member.playerName} - {member.role}");
            }
        }
    }

    public void DisplayGuilds()
    {
        foreach (var guild in guilds)
        {
            Debug.Log(guild.guildName);
        }
    }

    public Guild GetGuildByName(string name)
    {
        return guilds.Find(x => x.guildName == name);
    }

    public GuildMember GetGuildMemberById(string guildName, string playerId)
    {
        Guild guild = guilds.Find(x => x.guildName == guildName);
        if (guild != null)
        {
            return guild.members.Find(x => x.playerId == playerId);
        }
        return null;
    }

    public void AddExperience(string guildName, int amount)
    {
        Guild guild = guilds.Find(x => x.guildName == guildName);
        if (guild != null)
        {
            guild.AddExperience(amount);
        }
    }

    public void UpdateGuildMemberRole(string guildName, string playerId, string newRole)
    {
        Guild guild = guilds.Find(x => x.guildName == guildName);
        if (guild != null)
        {
            GuildMember member = guild.members.Find(x => x.playerId == playerId);
            if (member != null)
            {
                member.role = newRole;
            }
        }
    }

    public void PromoteGuildMember(string guildName, string playerId)
    {
        UpdateGuildMemberRole(guildName, playerId, "Officer");
    }

    public void DemoteGuildMember(string guildName, string playerId)
    {
        UpdateGuildMemberRole(guildName, playerId, "Member");
    }

    public void KickGuildMember(string guildName, string playerId)
    {
        Guild guild = guilds.Find(x => x.guildName == guildName);
        if (guild != null)
        {
            guild.RemoveMember(playerId);
            OnGuildNotification?.Invoke($"Member {playerId} has been kicked from {guildName}.");
        }
    }
}