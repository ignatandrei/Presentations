namespace CSharp8WhatsNew
{
    public class RGBColor
    {
        
        public readonly int r;
        public readonly int g;
        public readonly int b;
        
        public RGBColor(int R, int G, int B)
        {
            r = R;
            g = G;
            b = B;
            
        }
        public void Deconstruct(out int R, out int G, out int B) =>
        (R, G,B) = (r,g,b);
    }
}