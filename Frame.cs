using System;

namespace TextGame 
{
    public class Frame
    {
         public static T Do<T>(Func<T> action) { 
            BeginFrame();
            var result = action();
            return result;
        }

        static void BeginFrame() 
        {
            Console.WriteLine("-------------------------------------------------");
        }
    }
}