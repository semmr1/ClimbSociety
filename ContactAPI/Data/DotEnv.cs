using System;

namespace ContactAPI.Data
{
    public class DotEnv
    {
        public static void LoadEnv()
        {
            DotNetEnv.Env.Load();
            DotNetEnv.Env.TraversePath().Load();
        }
    }
}
