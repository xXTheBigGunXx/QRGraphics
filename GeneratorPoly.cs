using System;
using System.Collections.Generic;

class GeneratorPoly
{
    private static Dictionary<int, int> map2;
    private List<int> GeneratorPolynomial(int n)
    {
        map2 = GeneratorPoly.NumberWithN2();

        List<Terms> terms = new List<Terms>();
        Terms[] twoNumbersToMultiplie = new Terms[] { new Terms(0, 1), new Terms(0, 0) };
        List<int> poly = new List<int>();

        for(int i = 0; i < n; i++)
        {
            if (i == 0)
            {
                foreach (Terms item in twoNumbersToMultiplie)
                {
                    terms.Add(item);
                }
            }
            else
            {
                terms = MultiplieForGalois(terms, twoNumbersToMultiplie);
            }

            twoNumbersToMultiplie[1] = new Terms(i+1, 0);
            poly = CombineWithN(terms, i + 2);
        }

        /*Console.WriteLine(new string('!', 50));
        poly.ForEach(x => Console.WriteLine(x));*/

        return poly;
    }
    private static int Get(int index)
    {
        if(index == 255)
        {
            return 1;
        }
        index %= 255;
        return map2[index];
    }
    private static List<int> CombineWithN(List<Terms> terms, int n)
    {
        List<int> powers = new List<int>(n);
        
        for(int i = 0; i < n; i++)
        {
            powers.Add(0);

            for(int j = 0; j < terms.Count; j++)
            {
                if (n - i - 1 == terms[j].xPower)
                {
                    Terms b = terms[j];
                    powers[i] ^= Get(b.aPower);
                }
            }
        }
        return powers;
    }
    private static List<Terms> MultiplieForGalois(List<Terms> terms, Terms[] twoNumbersToMultiplie)
    {
        List<Terms> tempList = new List<Terms>();

        for(int i = 0; i < terms.Count; i++)
        {
            for(int j = 0; j < twoNumbersToMultiplie.Length; j++)
            {
                Terms temp = terms[i].Multiply(twoNumbersToMultiplie[j]);
                tempList.Add(temp);    
            }
        }

        return tempList;
    }
    private static Dictionary<int, int> NumberWithN2()
    {
        Dictionary<int, int> hashMap = new Dictionary<int, int>() { { 0, 1 } };
        int curr = 1;

        for (int i = 1; i < 255; i++)
        {
            curr *= 2;
            if (curr > 255)
            {
                curr = curr ^ 285;
            }
            hashMap.Add(i, curr);
        }
        return hashMap;
    }
}




