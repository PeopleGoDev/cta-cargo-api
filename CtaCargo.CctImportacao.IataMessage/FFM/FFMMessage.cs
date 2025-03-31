namespace CtaCargo.CctImportacao.IataMessage.FFM;

public class FFMMessage
{
    public string FlightNumber { get; set; }
    public string FlightDate { get; set; }
    public string FlightDestination { get; set;}
    public string FlightPrefix { get; set; }
    public string FlightOrigin { get; set; }
}

public class FFMUld
{
    public string UldNumber { get; set; }
}

public class FFMUldMaster
{
    public string MasterNumber { get; set; }
    public int Pieces {  get; set; }
    public double Weight { get; set; }
    public string WeightUnit { get; set; }
    public string TotalPartial { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string DescriptionOfGoods { get; set; }
    public double Cubic {  get; set; }
    public string CubicUnit { get; set;}
}
