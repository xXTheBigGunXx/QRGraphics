using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
public class Terms
{
     public int aPower {  get; set; }
     public int xPower { get; set; }

    public bool negativeA {  get; set; }

     public Terms(int aPower, int xPower)
     {
        this.aPower = aPower;
        this.xPower = xPower;
     }

    public override string ToString()
    {
        return String.Format($"a^{aPower} x^{xPower}");
    }

    public Terms Multiply(Terms a)
    {
        return new Terms(a.aPower + this.aPower, a.xPower + this.xPower);
    }  
    
    public static Terms operator +(Terms a, Terms b)
    {
        return new Terms(a.aPower + b.aPower, a.xPower);
    }

    public static Terms operator -(Terms a, Terms b)
    {
        return new Terms(a.aPower - b.aPower, a.xPower);
    }
}

