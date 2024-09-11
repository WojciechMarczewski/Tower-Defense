public class TargetingTower : Tower
{
    public Targeter targeter;
    public int range = 45;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        targeter.SetRange(range);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
