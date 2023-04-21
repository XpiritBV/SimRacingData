namespace Classes
{
    public class RootObject
    {
        public string TrackName { get; set; }
        public string Session { get; set; }
        public string NumVehicles { get; set; }
        public string CurET { get; set; }
        public string EndET { get; set; }
        public string MaxLaps { get; set; }
        public string LapDist { get; set; }
        public object StartStream { get; set; }
        public object EndStream { get; set; }
        public string GamePhase { get; set; }
        public string YellowFlagState { get; set; }
        public string SectorFlags { get; set; }
        public string InRealtime { get; set; }
        public string StartLight { get; set; }
        public string NumRedLights { get; set; }
        public string PlayerName { get; set; }
        public string PlrFileName { get; set; }
        public string DarkCloud { get; set; }
        public string Raining { get; set; }
        public string AmbientTemp { get; set; }
        public string TrackTemp { get; set; }
        public string Wind { get; set; }
        public string MinPathWetness { get; set; }
        public string MaxPathWetness { get; set; }
        public List<Driver> Drivers { get; set; } = new List<Driver>();
    }
}
