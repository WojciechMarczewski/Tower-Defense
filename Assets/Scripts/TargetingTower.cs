public class TargetingTower : Tower
{
    public Targeter Targeter;
    public int Range = 45;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Targeter.SetRange(Range);
    }


}
