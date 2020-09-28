using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class QuineMccluskeySolver
{
    public static int Solve(int[] truthtable, int variableCount)
    {
        List<MatchedPair> minterms = new List<MatchedPair>();
        List<MatchedPair>[] firstorderedlist = new List<MatchedPair>[variableCount+1];
        List<MatchedPair>[] secondorderedlist = new List<MatchedPair>[variableCount];
        List<MatchedPair>[] thirdorderedlist = new List<MatchedPair>[variableCount-1];
        List<MatchedPair> tempDelete = new List<MatchedPair>();
        List<MatchedPair> primeImplicants = new List<MatchedPair>();

        for (int i = 0; i < truthtable.Length; i++)
        {
            if (truthtable[i] == 1)
            {
                int[] temp = ConvertToTerm(i,variableCount);
                MatchedPair tempPair;
                tempPair.boolterm = temp;
                tempPair.minterms = new List<int>();
                tempPair.minterms.Add(i);
                minterms.Add(tempPair);
            }
        }
        foreach(MatchedPair minterm in minterms)
        {
            int countOnes = CountOnes(minterm.boolterm);
            if (firstorderedlist[countOnes] == null)
            {
                firstorderedlist[countOnes] = new List<MatchedPair>();
            }
            firstorderedlist[countOnes].Add(minterm);
        }

        
        for(int i = 0;i < variableCount;i++)
        {
            if (firstorderedlist[i] != null)
            {
                foreach (MatchedPair minTerm in firstorderedlist[i])
                {
                    bool isUsed = false;
                    if(firstorderedlist[i+1]!= null)
                    {
                        foreach (MatchedPair nextTerm in firstorderedlist[i + 1])
                        {
                            if (OffByOne(minTerm.boolterm, nextTerm.boolterm))
                            {
                                isUsed = true;
                                if (!tempDelete.Contains(nextTerm))
                                {
                                    tempDelete.Add(nextTerm);
                                }
                                int[] diff = GetDifference(minTerm.boolterm, nextTerm.boolterm);
                                if (secondorderedlist[CountOnes(diff)] == null)
                                {
                                    secondorderedlist[CountOnes(diff)] = new List<MatchedPair>();
                                }
                                MatchedPair matchedPair;
                                matchedPair.boolterm = diff;
                                matchedPair.minterms = new List<int>();
                                matchedPair.minterms.AddRange(minTerm.minterms);
                                matchedPair.minterms.AddRange(nextTerm.minterms);
                                secondorderedlist[CountOnes(diff)].Add(matchedPair);
                            }
                        }
                    }
                    if (isUsed)
                    {
                        if (!tempDelete.Contains(minTerm))
                        {
                            tempDelete.Add(minTerm);
                        }
                    }

                }
            }
            
            
        }
        for (int i = 0; i < variableCount+1; i++)
        {
            if (firstorderedlist[i] != null)
            {
                foreach (MatchedPair term in tempDelete)
                {
                    firstorderedlist[i].Remove(term);
                }
            }
        }
        tempDelete = new List<MatchedPair>();



        for (int i = 0; i < variableCount - 1; i++)
        {
            if (secondorderedlist[i] != null)
            {
                foreach (MatchedPair minTerm in secondorderedlist[i])
                {
                    bool isUsed = false;
                    if (secondorderedlist[i + 1] != null)
                    {
                        foreach (MatchedPair nextTerm in secondorderedlist[i + 1])
                        {
                            if (OffByOne(minTerm.boolterm, nextTerm.boolterm))
                            {
                                isUsed = true;
                                if (!tempDelete.Contains(nextTerm))
                                {
                                    tempDelete.Add(nextTerm);
                                }
                                int[] diff = GetDifference(minTerm.boolterm, nextTerm.boolterm);
                                if (thirdorderedlist[CountOnes(diff)] == null)
                                {
                                    thirdorderedlist[CountOnes(diff)] = new List<MatchedPair>();
                                }
                                MatchedPair matchedPair;
                                matchedPair.boolterm = diff;
                                matchedPair.minterms = new List<int>();
                                matchedPair.minterms.AddRange(minTerm.minterms);
                                matchedPair.minterms.AddRange(nextTerm.minterms);
                                thirdorderedlist[CountOnes(diff)].Add(matchedPair);
                            }
                        }
                    }
                    if (isUsed)
                    {
                        if (!tempDelete.Contains(minTerm))
                        {
                            tempDelete.Add(minTerm);
                        }
                    }
                }
            }

        }
        for (int i = 0; i < variableCount; i++)
        {
            if (secondorderedlist[i] != null)
            {
                foreach (MatchedPair term in tempDelete)
                {
                    secondorderedlist[i].Remove(term);
                }
            }
        }
        tempDelete = new List<MatchedPair>();

        for (int i = 0; i < variableCount - 2; i++)
        {
            if (thirdorderedlist[i] != null)
            {
                foreach (MatchedPair minTerm in thirdorderedlist[i])
                {
                    bool isUsed = false;
                    if (thirdorderedlist[i + 1] != null)
                    {
                        foreach (MatchedPair nextTerm in thirdorderedlist[i + 1])
                        {
                            if (OffByOne(minTerm.boolterm, nextTerm.boolterm))
                            {
                                isUsed = true;
                                if (!tempDelete.Contains(nextTerm))
                                {
                                    tempDelete.Add(nextTerm);
                                }
                                int[] diff = GetDifference(minTerm.boolterm, nextTerm.boolterm);

                                MatchedPair matchedPair;
                                matchedPair.boolterm = diff;
                                matchedPair.minterms = new List<int>();
                                matchedPair.minterms.AddRange(minTerm.minterms);
                                matchedPair.minterms.AddRange(nextTerm.minterms);
                               
                                if (!BoolIsPresent(primeImplicants, matchedPair))
                                {
                                    primeImplicants.Add(matchedPair);
                                }
                            }
                        }
                    }
                    if (isUsed)
                    {
                        if (!tempDelete.Contains(minTerm))
                        {
                            tempDelete.Add(minTerm);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < variableCount-1; i++)
        {
            if (thirdorderedlist[i] != null)
            {
                foreach (MatchedPair term in tempDelete)
                {
                    thirdorderedlist[i].Remove(term);
                }
            }
        }
        tempDelete = new List<MatchedPair>();

        for (int i = 0; i < variableCount + 1; i++)
        {
            if (firstorderedlist[i] != null && firstorderedlist[i].Count > 0)
            {
                
                foreach (MatchedPair minterm in firstorderedlist[i])
                {
                    if (!BoolIsPresent(primeImplicants, minterm))
                    {
                        primeImplicants.Add(minterm);
                    }

                }
            }
        }

        for (int i = 0; i < variableCount; i++)
        {
            if (secondorderedlist[i] != null && secondorderedlist[i].Count > 0)
            {
                foreach (MatchedPair minterm in secondorderedlist[i])
                {
                    if (!BoolIsPresent(primeImplicants, minterm))
                    {
                        primeImplicants.Add(minterm);
                    }
                }
            }
        }
        for (int i = 0; i < variableCount-1; i++)
        {
            if (thirdorderedlist[i] != null && thirdorderedlist[i].Count > 0)
            {
                
                foreach (MatchedPair minterm in thirdorderedlist[i])
                {
                    if (!BoolIsPresent(primeImplicants, minterm))
                    {
                        primeImplicants.Add(minterm);
                    }
                }
            }
        }


        List<MatchedPair> corePrimeImplicants = GetCorePrimes(minterms, primeImplicants);
        foreach(MatchedPair mp in corePrimeImplicants)
        {
            primeImplicants.Remove(mp);
        }
        
        List<int> minInts = new List<int>();
        foreach(MatchedPair mp in minterms)
        {
            minInts.Add(mp.minterms.First());
        }
        ReduceMinterms(minInts, corePrimeImplicants);
        int literals = 0;
        foreach (MatchedPair core in corePrimeImplicants)
        {

            literals += CountLiterals(core.boolterm);
        }
        if (minInts.Count < 1)
        {
            return literals;
        }
        ReduceMintermsEvenMore(minInts,primeImplicants);

        literals +=  AlternativePetricksMethod(primeImplicants,minInts);

        return literals;
    }
    static int AlternativePetricksMethod(List<MatchedPair> primes, List<int> minterms)
    {
        int res = 0;
        int i = 0;
        while (i < 100) {

                List<List<MatchedPair>> combinationsForiLiterals = GetCombinations(primes, i);
                if (combinationsForiLiterals != null)
                {
                    foreach (List<MatchedPair> combination in combinationsForiLiterals)
                    {
                    
                    List<int> compare = new List<int>();
                        foreach (MatchedPair temp in combination)
                        {
                            compare = compare.Union(temp.minterms).ToList();
                        }
                        if (minterms.All(j => compare.Contains(j)))
                        {

                        return i;
                        }
                    }
                }
            i++;
            }
        return res;
    }
    static List<List<MatchedPair>> GetCombinations(List<MatchedPair> primes, int literalCount)
    {
        List<List<MatchedPair>> result = new List<List<MatchedPair>>();
        foreach(MatchedPair term in primes)
        {
            int literals = CountLiterals(term.boolterm);
            if (literals == literalCount)
            {
                result.Add(new List<MatchedPair> { term });
            }
            if (literals > literalCount)
            {
                return null;
            }
            else if(literals < literalCount)
            {
                List<List<MatchedPair>> temp = GetCombinations(primes.Except(new List<MatchedPair> { term }).ToList(), literalCount - literals);
                if(temp != null)
                {
                    foreach (List<MatchedPair> tempList in temp)
                    {
                        tempList.Add(term);
                        result.Add(tempList);
                    }
                }
            }
        }
        return result;
    }

    static int CountLiterals(int[] minterm)
    {
        int res = 0;
        foreach(int i in minterm)
        {
            if(i!= -1)
            {
                res++;
            }
        }
        return res;
    }

    static void ReduceMintermsEvenMore(List<int> minterms, List<MatchedPair> primes)
    {
        Dictionary<int, List<MatchedPair>> tempList = new Dictionary<int, List<MatchedPair>>();
        foreach(int i in minterms)
        {
            List<MatchedPair> temp = new List<MatchedPair>();
            foreach(MatchedPair mp in primes)
            {
                if (mp.minterms.Contains(i))
                {
                    temp.Add(mp);
                }
            }
            tempList.Add(i,temp);
        }


        foreach(KeyValuePair<int,List<MatchedPair>> mplist in tempList)
        {
            foreach(KeyValuePair<int, List<MatchedPair>> mplistcandidate in tempList)
            {
                if (mplist.Key != mplistcandidate.Key)
                {
                    if (mplistcandidate.Value.All(j => mplist.Value.Contains(j)))
                    {
                        minterms.Remove(mplist.Key);
                    }
                }
            }
        }

    }

    static void ReduceMinterms(List<int> minterms, List<MatchedPair> corePrimes)
    {
        foreach(MatchedPair candidate in corePrimes)
        {
            foreach(int i in candidate.minterms)
            {
                minterms.Remove(i);
            }
        }
    }
    static List<MatchedPair> GetCorePrimes(List<MatchedPair> minterms,List<MatchedPair> primes)
    {
        List<MatchedPair> result = new List<MatchedPair>();
        foreach(MatchedPair minterm in minterms)
        {
            List<MatchedPair> temp = new List<MatchedPair>();
            foreach (MatchedPair candidatePair in primes)
            {
                if (candidatePair.minterms.Contains(minterm.minterms.First()))
                {
                    temp.Add(candidatePair);
                }
            }
            if(temp.Count == 1)
            {
                if (!BoolIsPresent(result, temp.First()))
                {
                   result.Add(temp.First());
                }
                
            }
        }
        return result;
    }
    static bool BoolIsPresent(List<MatchedPair> list, MatchedPair term)
    {
        foreach(MatchedPair minterm in list)
        {
            if (minterm.boolterm.SequenceEqual(term.boolterm)){
                return true;
            }
        }
        return false;
    }
    static bool OffByOne(int[] minterm, int[] nextTerm)
    {
        int count = 0;
        for (int i = 0; i < minterm.Length; i++)
        {
            if (minterm[i] != nextTerm[i])
            {
                count++;
            }
        }
        if (count <= 1)
        {
            return true;
        }
        return false;
    }

    static int[] GetDifference(int[] minterm, int[] nextTerm)
    {
        int[] res = new int[minterm.Length];
        for(int i = 0; i < minterm.Length; i++)
        {
            if(minterm[i] == nextTerm[i])
            {
                res[i] = minterm[i];
            }
            else
            {
                res[i] = -1;
            }
        }
        return res;
    }

    public static int CountOnes(int[] minterm)
    {
        int result = 0;
        foreach (int candidate in minterm)
        {
            if (candidate == 1)
            {
                result++;
            }
        }
        return result;
    }
    public static int[] ConvertToTerm(int n,int variableCount)
    {
        int[] result = new int[variableCount];
        for (int i = 0; i < variableCount; i++)
        {
            result[i] = 1 & (n >> (0 + i));
        }
        return result;
    }
    static void PrintArray(List<int> arr)
    {
        string res = "";
        foreach (int i in arr)
        {
            res += ",";
            res += i.ToString();
            
        }
        Debug.Log(res);
    }
    static void PrintArray(int[] arr)
    {
        string res = "";
        foreach(int i in arr)
        {
            res += i.ToString();
        }
        Debug.Log(res);
    }
    struct MatchedPair
    {
        public int[] boolterm;
        public List<int> minterms;

    }
}
