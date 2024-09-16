public class TargetingTower : Tower
{
<<<<<<< HEAD
    public Targeter Targeter;
    public int Range = 45;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Targeter.SetRange(Range);
=======
    public Targeter targeter;
    public int range = 45;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        targeter.SetRange(range);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    }

    // Update is called once per frame
    void Update()
    {

    }
}
