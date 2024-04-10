using System;

[Serializable]
public class PlayerData
{
    public float Health;
    public int JumpsAmount;

    public PlayerData ShallowCopy()
    {
        return (PlayerData)this.MemberwiseClone();
    }
}
