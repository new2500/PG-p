Most General Case: Can handle +, -, *, /, ^, (, ) and Negative sign

/*
Since there are 3 level operator, we use a hash table to set the rule of operator:  <char, int> each operator has its own level, And we use two stack to tracking the number and operators: one is stack of integer, another is stack of char 

For the bracket case, we need to calculate all the bracket cases: Since the calculation inside the bracket always come first.
So when we meet the first left bracket( , we introduce a counter variable which is make sure the bracket comes with a pair, a start variable set to the current index, and a length variable to tracking the length of content inside the brackets, the whole process sounds like DFS. 

we iterative loop through the string one by one, if meet numeric char, we use a num variable to convert into integer; if meet non-numeric char, we push the previous the num variable into stack and reset to 0: Then if the opStack is empty or the current operator's level higher than the top operator in the opStack, we push current operator into opStack ; If not, which mean the operator in the opStack comes first then current operator - Then we first need to calculate the previous num and operator, after than we push the current operator into opStack.

After we loop through the whole string, if opStack is not empty, we need to do the calculate again. Then Pop the top element of numSack. That's the result. O(N) time, O(N+1) space
*/


public int Calculate(string input)
{
     input = input.Replace(" ","");
     if(string.IsNullOrEmpty(input))
          return 0;

     int num = 0;
     int negativeSign = 1;

     Stack<int> numStack = new Stack<int>();
     Stack<char> opStack = new Stack<char>();

     //Hash table to define the priority of operators
     Dictionary<char, int> opDict = new Dictionary<char, int>();
     opDict.Add('+', 1);     opDict.Add('-', 1);
     opDict.Add('*', 2);     opDict.Add('/', 1);
     opDict.Add('^', 3);

     for(int i = 0; i < input.Length; i++)
     {
          char curr = input[i];
          if(char.IsDigit(curr))
          {
               num = (num * 10 + curr - '0');  //Could check if (num*10+curr-'0') > int.MaxValue
          }
          else
          {
               //Bracket cases
               if(curr == '(')
               {
                    int count = 1;
                    int j = i + 1;
                    int len = 0;
                    while(j < input.Length)
                    {
                         char tempChar = input[j];
                         if(tempChar == '(')
                              count++;
                         else if(tempChar == ')')
                              count--;
                         
                         if(count == 0)  break;   //Find all pairs, out of current loop
                         j++; len++;
                    }
                    int temp = Calculate(input.Substring(i + 1, len)); //Exclude brackets
                    num = temp;  i = j;
                    continue;
               }

               //Negative Sign case
               if(curr == '-')
               {    //We just case about the '-' after a operator and before a number
                    if(i == 0 || !char.IsDigit(input [i - 1]) && input[i - 1] != ')') 
                    {
                         negativeSign = -1; continue;
                    }
               }

               //Push the num into numStack and Reset the num
               numStack.Push(negativeSign * num);
               num = 0; negativeSign = 1;

               //Operators Cases:
               if(opStack.Count == 0 || opDict[curr] > opDict[opStack.Peek()])
                    opStack.Push(curr);
               else
               {
                    while(opStack.Any() && opDict[curr] <= opDict[opStack.Peek()])
                         helper(numStack, opStack);
                    opStack.Push(curr);
               }
          }
     }
     //Push last number, if opStack is not empty, do calculation
     numStack.Push(num);
     while(opStack.Any())
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
