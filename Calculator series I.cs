
#region Case 1 Only +, - ,(, ), empty
//Case 1: Only +, - ,(, ), empty

/*
Iterative solution by identifying character one by one. We could introduce a sign to handle the "+" and "-", and a stack the handle the brackets  Once meet the left bracket, we push the current result and sign and reset them, if meet the right bracket, we pop the previous result and sign and do add action. O(N) time, O(N) space(=>for "(....(1)....)" case)
*/

 public int Calculate(string s)
     {
          int sign = -1, result = 0;
          Stack<int> stack = new Stack<int>();

          for(int i = 0; i < s.Length; i++)
          {
               if(char.IsDigit(s[i] - '0')
               {
                    int sum = s[i] - '0';
                    while(i + 1 < s.Length && char.IsDigit(s[i + 1])
                    {
                         sum = sum * 10 + (s[i + 1] - '0');
                         i++;
                    }
                    result = result + sum * sign;
               }
               else if(s[i] == '+')
               {
                    sign = 1;
               }
               else if(s[i] == '-')
               {
                    sign = -1;
               }
               else if(s[i] == '(')
               {
                    stack.Push(result);
                    stack.Push(sign);
                    result = 0;
                    sign = 1;
               }
               else if(s[i] == ')')
               {
                    result = result * stack.Pop() + stack.Pop();  //1st Pop is sign, 2nd Pop is previous result
               }
          }
          return result;
     }
#endregion 


#region Case 2 Only +, - , *, /, empty
//Case 2: Only +, - , *, /, empty

/*
Iterative solution by identifying character one by one. We use initialize char sign == '+' to tracking the operator before the current number(Which mean for the first number is negative, there are always a 0 in front of it). Then use a stack, every time we meet a new operator  we push the previous number and reset the number variable. Then we just Sum all the elements in the stack. O(2N) time, O(N) space
*/
     public int Calculate(string s)
     {
          if(string.IsNullOrEmpty(s))
               return 0;
          Stack<int> stack = new Stack<int>();
          int num = 0;
          char sign = '+';

          for(int i = 0; i < s.Length; i++)
          {
               if(char.IsDigit(s[i])
               {
                    num = (num * 10 + s[i] - '0') > int.MaxValue ? int.MaxValue : (num * 10 + s[i] - '0');
               }
               if(!char.IsDigit(s[i]) && s[i] != ' ' || i == s.Length - 1)
               {
                    if(sign == '-')
                    {
                         stack.Push(-num);
                    }
                    if(sign == '+')
                    {
                         stack.Push(num);
                    }
                    if(sign == '*')
                    {
                         stack.Push(stack.Pop() * num);
                    }
                    if(sign =='/')
                    {
                         stack.Push(stack.Pop() / num);
                    }
                    //Rest sign and num
                    sign = s[i];
                    num = 0;
               }
          }
          //Sum them up
          int res = 0;
          foreach(var i in stack)
               res += i;
          return res;
     }

//Improve to one-pass algorithm //慎用，与其他cases做法迥异
     public int Calculate(string s)
     {
          if(string.IsNullOrEmpty(s))
               return 0;
          Stack<int> stack = new Stack<int>();
          int num = 0;
          char sign = '+';
          int res = 0;

          for(int i = 0; i < s.Length; i++)
          {
               char curr = s[i];
               if(char.IsDigit(curr))
               {
                    num = (num * 10 + curr - '0') > int.MaxValue ? int.MaxValue : (num * 10 + curr - '0');
               }
               if(curr == '+' || curr == '-' || curr == '*' || curr == '/' || i == s.Length - 1)
               {
                    if(sign == '+' || sign == '-')
                    {
                         int temp = sign == '+' ? num : -num;
                         stack.Push(temp);
                         res = res + temp;
                    }
                    else
                    {
                         res = res - stack.Peek();    //Hanle the */ is before the +-
                         int temp = sign == '*' ? stack.Pop() * num : stack.Pop() / num;
                         stack.Push(temp);
                         res = res + temp;
                    }
                    sign = curr;
                    num = 0;
               }
          }
          return res;
     }
	 

#endregion



#region Case 3 +, -, *, /, allowed negative after sign, like 1+-1 => 1+ (-1)

//+, -, *, /, allowed negative after sign
       public int? Calculate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            input = input.Replace(" ", "");   //Delete all empty case

            Stack<int> stack = new Stack<int>();
            int num = 0;
            char sign = '+';
            int negative = 1;

            for (int i = 0; i < input.Length; i++)
            {
                char curr = input[i];
                if (char.IsDigit(curr))
                {
                    num = (num * 10 + curr - '0') > int.MaxValue ? int.MaxValue : (num * 10 + curr - '0');
                }
                if (i == input.Length - 1 || !char.IsDigit(curr))
                {
                    //Handle 3+-10 => 3+(-10) case
                    if (curr == '-' && (i == 0 || !char.IsDigit(input[i - 1])))
                    {
                        negative = -1;
                        continue;
                    }
                    if(sign == '+')
                        stack.Push(negative * num);
                    else if(sign == '-')
                        stack.Push(negative * -1 * num);
                    else if (sign == '*')
                        stack.Push(stack.Pop() * negative * num);
                    else if(sign == '/')
                        stack.Push(stack.Pop() / (negative * num));

                    //Reset num, sign and negative
                    num = 0;
                    negative = 1;
                    sign = curr;
                }
            }
            //Sum the stack up
            int res = 0;
            while (stack.Any())
                res += stack.Pop();
            return res;
        }


#endregion


#region Case 4 +，-， *， /， (, )

//Case 4 +，-， *， /， (, ) Case

/*Basically same as above. For the bracket case, we need to calculate all the bracket cases: Since the calculation inside the bracket always come first.

So when we meet the first left bracket, we introduce a counter variable which is make sure the bracket comes with a pair, a start variable set to the current index, and a length variable to tracking the length of content inside the brackets, the whole process sounds like DFS. */

       public int Calculate(string input)
        {
            input = input.Replace(" ", "");   //Delete all empty case
            if (string.IsNullOrEmpty(input))
                return 0;

            Stack<int> stack = new Stack<int>();
            int num = 0;
            char sign = '+';
            int negative = 1;

            for (int i = 0; i < input.Length; i++)
            {
                char curr = input[i];
                if (char.IsDigit(curr))
                {
                    num = (num * 10 + curr - '0') > int.MaxValue ? int.MaxValue : (num * 10 + curr - '0');
                }
                if (i == input.Length - 1 || !char.IsDigit(curr))
                {
                    //Handle 3+-10 => 3+(-10) case
                    if (curr == '-' && (i == 0 || !char.IsDigit(input[i - 1])))
                    {
                        negative = -1;
                        continue;
                    }
                    //Consider brackets cases
                    if (curr == '(')
                    {
                        int count = 1;
                        int start = i;
                        int len = 0;
                        i++;
                        while (i < input.Length && count > 0)
                        {
                            curr = input[i];
                            if (curr == '(')
                                count++;
                            else if (curr == ')')
                                count--;

                            i++;
                            len++;
                        }
                        //Exclusce the last ')'
                        i = i - 1;
                        len--;
                        string subInput = input.Substring(start + 1, len);
                        num = Calculate(subInput);

                        if (i == input.Length - 1)
                        {
                            i = i - 1;
                        }
                        continue;
                    }

                    //Operator cases
                    if (sign == '+')
                        stack.Push(negative * num);
                    else if(sign == '-')
                        stack.Push(negative * -1 * num);
                    else if (sign == '*')
                        stack.Push(stack.Pop() * negative * num);
                    else if(sign == '/')
                        stack.Push(stack.Pop() / (negative * num));

                    //Reset num, sign and negative
                    num = 0;
                    negative = 1;
                    sign = curr;
                }
            }
            //Sum the stack up
            int res = 0;
            while (stack.Any())
                res += stack.Pop();
            return res;
        }


#endregion



#region Case 5  Only +, -, *, / and ^

/*
Since there are 3 level operator, we use a hash table to set the rule of operator:  <char, int> each operator has its own level, And we use two stack to tracking the number and operators: one is stack of integer, another is stack of char 

we iterative loop through the string one by one, if meet numeric char, we use a num variable to convert into integer; if meet non-numeric char, we push the previous the num variable into stack and reset to 0: Then if the opStack is empty or the current operator's level higher than the top operator in the opStack, we push current operator into opStack ; If not, which mean the operator in the opStack comes first then current operator - Then we first need to calculate the previous num and operator, after than we push the current operator into opStack.

After we loop through the whole string, if opStack is not empty, we need to do the calculate again. Then Pop the top element of numSack. That's the result. O(N) time, O(N+1) space

*/

public int Calculate(string input)
{
     input = input.Replace(" ","");  //Delete all empty chars
     
     if(string.IsNullOrEmpty(input)) //Check null or empty, since int is non-null able, so return 0
          return 0;
     int num = 0;
     Stack<int> numStack = new Stack<int>();
     Stack<char> opStack = new Stack<char>();
     
     Dictionary<char, int> opDict = new Dictionary<char, int>();
     opDict.Add('+', 1);   opDict.Add('-', 1);
     opDict.Add('*', 2);   opDict.Add('/', 2);
     opDict.Add('^', 3);

     for(int i = 0; i < input.Length; i++)
     {
          char curr = input[i];
          if(char.IsDigit(curr))
          {
               num = num * 10 + curr - '0';   //In case of date type overflow, could check if (num*10+curr-'0') > int.MaxValue
          }
          else   //Meet operators
          {
               //Push the num into numStack and Reset
               numStack.Push(num);
               num = 0;
               //Empty case or Curr operator's level > top of opStack
               if(opStack.Count == 0 || opDict[curr] > opDict[opStack.Peek()])
                    opStack.Push(curr); 
               else                     //If not, we need to calculate the previous first, then Push the curr
               {
                    while(opStack.Any() && dict[curr] <= dict[opStack.Peek()])
                         helper(numStack, opStack);
                    opStack.Push(curr);
               }
          }
     }
     numStack.Push(num); //Push last number
     while(opStack.Any())          //Calculate the rest
          helper(numStack, opStack);

     return numStack.Pop();
}

//Helper for Calculate
private void helper(Stack<int> numStack, Stack<char> opStack)
{
     int b = numStack.Pop();
     int a = numStack.Pop();
     char op = opStack.Pop();

     //Calculate based on the operator
     if(op == '+')
          numStack.Push(a + b);
     else if(op == '-')
          numStack.Push(a - b);
     else if(op == '*')
          numStack.Push(a * b);
     else if(op == '/')
          numStack.Push(a / b);
     else if(op == '^')          //For a^b
          numStack.Push((int)Math.Pow(a, b));
     return;
}




#endregion
