Max Value of Items RPG game(Greedy & MaxHeap)


You're playing RPG. Found a room full of treasure:

You have n slots. Object of the same type could stack together, with the max size of stack depending of the type.( Coins max is 10, Diamonds max is 5, Armor max is 1)

Each stack(or partial stack) takes up 1 inventory slot. Each item hash a selling price. (A stack of Diamond worth 10, so 5 stacks of Diamonds worth 50). Find the maximize value.


/*
n: # of Slots
items: Array of items types, one for each item in the room
itemInfo: Array of structors, one for each unique item type
*/
public class itemInfo
{
     public string name;                       //Name of Item
     public int maximumStackSize;             //The maximum item that put into a slot
     public int value;                        //Single item's value  
     
     .........
}


#region Naive Greedy
Brutal Force approach: say we have N slots, M items, to achieve the maxValue, we need for each slot, it takes the max Value.
So we start at the slot 0, take the max, then renew the the number of item that we put on the slots, then we move the next slot

Since there are N slots, and we have M items, for each slot, we need to compare M times, so it's a O(N*M) algorithm, we implement 3 hashtable for fast retrieve time, 1 for tracking the maxStackSize, the 2nd is for tracking the value , 3rd one is for renew the number. so it takesO(3*M) space

public int maxValue(int n, string[] items, ItemInfo[] infos)
        {
            int maxVal = 0;
            Dictionary<string, int> maxDict = new Dictionary<string, int>();
            Dictionary<string, int> valDict = new Dictionary<string, int>();
            Dictionary<string, int> countDict = new Dictionary<string, int>();
            foreach (ItemInfo itemInfo in infos)
            {
                maxDict.Add(itemInfo.name, itemInfo.maximumStackSize);
                valDict.Add(itemInfo.name, itemInfo.value);
            }
            foreach (string item in items)
            {
                if (!countDict.ContainsKey(item))
                    countDict.Add(item, 1);
                else
                    countDict[item]++;
            }
            //Start searching, For each slots, we take the maxValue, and renew the hash table
            for (int i = 0; i < n && countDict.Count != 0; i++)
            {
                int max = 0;
                string select = "";   //Reset
                int num = 0;
                foreach (string item in countDict.Keys)
                {
                    int count = countDict[item];
                    if (count >= maxDict[item])   //Current number > maxStack, take the maxStack number
                    {
                        if (maxDict[item] * valDict[item] > max) //Greater then the current max, set the item
                        {
                            select = item;
                            num = maxDict[item];
                            max = maxDict[item] * valDict[item];
                        }
                    }
                    else                    //Take the current number
                    {
                        if (count * valDict[item] > max) //Greater then the current max, set the item
                        {
                            select = item;
                            num = count;
                            max = count * valDict[item];
                        }
                    }
                }
                //Update the total MaxValue
                maxVal += max;
                //Renew the hash table - the rest of the items
                if (!select.Equals(""))
                {
                    countDict[select] = countDict[select] - num;
                    if (countDict[select] == 0)//Use, Delete
                        countDict.Remove(select);
                }
            }
            return maxVal;
        }

#endregion



#region  Using Heap

We pack all the given items into a stack(If can pack a stack, make it a partial stack), then we calculate the value of the stack, push the value into a MaxHeap, then we take the first K slots to achieve the max Total Value.

Since we iterate all the given M items and insert them into the maxHeap, the time compleixty is O(M*logM).


      public int maxValue(int n, string[] items, ItemInfo[] infos)
        {
            int maxVal = 0;   //MaxValue
            //Check null
            if (items == null || infos == null || n <= 0 || items.Length == 0 || infos.Length == 0)
                return maxVal;
            Dictionary<string, int> maxDict = new Dictionary<string, int>(); //name - maxStackSize
            Dictionary<string, int> valDict = new Dictionary<string, int>(); //name - Value
            Dictionary<string, int> countDict = new Dictionary<string, int>(); //name - actual number
            //Push the info into maxDict and valDict
            foreach (ItemInfo itemInfo in infos)
            {
                maxDict.Add(itemInfo.name, itemInfo.maximumStackSize);
                valDict.Add(itemInfo.name, itemInfo.value);
            }
            //Count how many item is available
            foreach (string item in items)
            {
                if (!countDict.ContainsKey(item))
                    countDict.Add(item, 1);
                else
                    countDict[item]++;
            }
            //If using MaxHeap.
            SortedDictionary<int, string> maxList = new SortedDictionary<int, string>(new DupKeyCom());
            while (countDict.Count != 0)
            {
                foreach (var item in maxDict.Keys)
                {
                    if (countDict.ContainsKey(item) && valDict.ContainsKey(item))
                    {
                        if (countDict[item] >= maxDict[item])
                        {
                            maxList.Add(maxDict[item] * valDict[item], item);
                            countDict[item] = countDict[item] - maxDict[item];
                        }
                        else //Current number can cont fill a stack, we need to set 0
                        {
                            maxList.Add(countDict[item] * valDict[item], item);
                            countDict.Remove(item);
                        }
                    }
                }
            }
            var enmerator = maxList.GetEnumerator();
            enmerator.MoveNext();     //Move to the valid key-value pair
            for (int i = 0; i < n; i++)
            {
                maxVal += enmerator.Current.Key;
                enmerator.MoveNext();
            }
            enmerator.Dispose();
            return maxVal;
        }




#endregion
