namespace GamePlay.Cities.Instance.Root.Runtime
{
    public static class CityNames
    {
        public static string AsString(this CityType city)
        {
            return city switch
            {
                CityType.Spain_Cancun => "Cancun",
                CityType.Spain_SanThoma => "SanThoma",
                CityType.Spain_Belize => "Belize",
                CityType.Spain_PuertoCabezas => "PuertoCabezas",
                CityType.Spain_SantaMaria => "SantaMaria",
                CityType.Holland_Pensacola => "Pensacola",
                CityType.Holland_Savannah => "Savannah",
                CityType.Holland_GrandBahama => "GrandBahama",
                CityType.Holland_Carlos => "Carlos",
                CityType.Holland_Andross => "Andros",
                CityType.Holland_Gilhara => "Gilhara",
                CityType.Holland_Isabella => "Isabella",
                CityType.England_SanJuan => "SanJuan",
                CityType.England_Antigua => "Antigua",
                CityType.England_Barbados => "Barbados",
                CityType.England_Gergetown => "Georgetown",
                CityType.England_Barcelona => "Barcelona",
                CityType.England_RioHacha => "RioHacha",
                CityType.France_Sisal => "Sisal",
                CityType.France_VillaRica => "VillaRica",
                CityType.France_Tampico => "Tampico",
                CityType.France_RioGrande => "RioGrande",
                CityType.France_Magdelaine => "Magdelaine",
                CityType.Pirate_Havanna => "Havanna",
                CityType.Pirate_Tortuga => "Tortuga",
                CityType.Pirate_Trinidad => "Trinidad",
                _ => "SomeCity"
            };
        }
    }
}