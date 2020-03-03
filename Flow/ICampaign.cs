using System;
using System.Collections.Generic;

namespace TextGame.Flow 
{
    public interface ICampaign 
    {
        List<Func<Character, Character>> Acts { get; }
    }
}