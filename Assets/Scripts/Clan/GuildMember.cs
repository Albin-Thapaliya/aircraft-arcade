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

}