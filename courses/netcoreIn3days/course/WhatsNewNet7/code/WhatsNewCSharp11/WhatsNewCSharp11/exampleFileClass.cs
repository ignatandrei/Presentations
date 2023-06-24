//new file
class Y
{

    public required int MyProperty { get; set; }
    public void Do()
    {
        var s = new X();
        s.MyProperty = this.MyProperty;
    }
}
file class X
{
    public int MyProperty { get; set; }
}
