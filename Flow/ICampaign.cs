using System;

namespace TextGame.Flow 
{
    public interface ICampaign 
    {
        Character FirstFrame(Character character);
        Character DefaultFrame(Character character);
    }
}