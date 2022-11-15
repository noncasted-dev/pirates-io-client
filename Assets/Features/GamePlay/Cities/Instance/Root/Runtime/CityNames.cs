using System;

namespace GamePlay.Cities.Instance.Root.Runtime
{
    public static class CityNames
    {
        private const string _prem = "Perm";
        private const string _washington = "Washington";

        public static string AsString(this CityType city)
        {
            return city switch
            {
                CityType.Perm => _prem,
                CityType.Washington => _washington,
                _ => "Some city"
            };
        }
    }
}