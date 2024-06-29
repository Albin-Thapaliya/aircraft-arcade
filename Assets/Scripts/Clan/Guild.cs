public class Guild
{
    public string guildName;
    public int guildExperience;
    public int guildLevel;
    public List<GuildMember> members;

    public void AddExperience(int amount)
    {
        guildExperience += amount;
        while (guildExperience >= ExperienceToNextLevel(guildLevel))
        {
            guildExperience -= ExperienceToNextLevel(guildLevel);
            guildLevel++;
        }
    }

    private int ExperienceToNextLevel(int level)
    {
        return 1000 * level;
    }
}