using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

enum Cipher {A=10,B,C,D,E,F,G,H,I,J,K,L,M,N};
public static class DecimalEncoder
{

    public static string encode(int decimalInt, int baseInt)
    {
        if(baseInt == 0)
        {
            return "";
        }
        string result = "";
        List<int> resultInts = new List<int>();
        while (decimalInt > 0)
        {
            resultInts.Add(decimalInt % baseInt);
            decimalInt = decimalInt / baseInt;
        }
        resultInts.Reverse();
        foreach (int i in resultInts)
        {
            string encodedInt = Encode(i);
            result += encodedInt;
        }
        return result;
    }
    private static string Encode(int toEncode)
    {
        if (toEncode < 10)
        {
            return toEncode.ToString();
        } else
        {
            return ((Cipher)toEncode).ToString();
        }

    }
    public static string Decode(string toDecode)
    {
        int.TryParse(toDecode, out int toDecodeInt);
        if (toDecodeInt < 10 && toDecodeInt > 0)
        {
            return toDecode;
        }
        Enum.TryParse(toDecode, out Cipher state);
        return ((int)state).ToString();
    }


}
