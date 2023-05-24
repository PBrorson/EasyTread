namespace EasyTread.Domain.Contracts.Response;

public class PropertySetResponse
{
    public bool DeepGrove { get; set; }

    public WearPatternResponse WearPatternResponse { get; set; }

    public ShoulderWearResponse ShoulderWearResponse { get; set; }
}