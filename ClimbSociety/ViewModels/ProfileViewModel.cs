using ClimbSociety.Models;

namespace ClimbSociety.ViewModels
{
    public class ProfileViewModel
    {
        private Developer dev;
        public ProfileViewModel(Developer developer)
        {
            dev = developer;
        }

        public Developer developer => dev;

        public int Id => dev.Id;
        public string Name => dev.Name;
        public string Description => dev.Description;
        public Dictionary<string, int> Skills => dev.Skills;

    }
}
