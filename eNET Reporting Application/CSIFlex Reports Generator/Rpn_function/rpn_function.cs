using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpnParser;
using System.Diagnostics;

namespace Rpn_function
{
    public class Rpn
    {

        /*********************************************************************/
        /* Evaluate the expression                                           */
        /*********************************************************************/
        public RpnParser.RpnOperand RpnEval(List<RpnElement> tokens, List<string> current_values)
        {
            Stack<RpnOperand> oprndStack = new Stack<RpnOperand>();
            RpnOperand oprnd = null;

            for (int nextElem = 0; nextElem < tokens.Count; ++nextElem)
            {
                RpnElement token = tokens[nextElem];
                switch (token.ElementType)
                {
                    // create an operand from the token and push it onto the operand stack
                    case ElementType.Literal:
                    case ElementType.Constant:
                        oprnd = new RpnOperand((RpnToken)token);
                        break;
                    // Get the current value of the identifier and push it onto the operand stack
                    case ElementType.Identifier:
                        oprnd = EvalIdentifier((RpnToken)token,   current_values);
                        break;
                    // Pop the left and right sides of the operator from the operand stack
                    // Perform the operation and push the results onto the operand stack
                    case ElementType.Operator:

                      


                        RpnOperator oper = (RpnOperator)token;
                        RpnOperand lhs, rhs;
                        lhs = rhs = null;
                        if (oper.IsMonadic && oprndStack.Count >= 1)
                        {
                            lhs = new RpnOperand(0);
                            rhs = oprndStack.Pop();
                        }
                        else
                            if (oprndStack.Count >= 2)
                        {
                            rhs = oprndStack.Pop();
                            lhs = oprndStack.Pop();
                        }
                        if (lhs == null)
                            StackError("operator", token.StrValue);
                        else
                            oprnd = EvalOperator(oper, lhs, rhs);
                        break;
                    // Determine if True or False condition has be met
                    // Set element index to the next element to be processed
                    case ElementType.CondTrue:
                    case ElementType.CondFalse:
                        oper = (RpnOperator)token;
                        if (oprndStack.Count < 1)
                            StackError("CondFalse", token.StrValue);
                        if (oper.OpType == OperatorType.CondTrue)
                        {
                            lhs = oprndStack.Pop();
                            if (lhs.NumValue == 0)
                                nextElem = oper.CondGoto - 1;  // for loop will add one
                        }
                        else
                            nextElem = oper.CondGoto - 1; // for loop will add one
                        continue;
                    // Pop the function arguments from the operand stack and place into an array
                    // Evalualate the function and push the results onto the operand stack
                    case ElementType.Function:
                        RpnFunction func = (RpnFunction)token;
                        if (oprndStack.Count >= func.ArgCount)
                        {
                            RpnOperand[] args = new RpnOperand[func.ArgCount];
                            for (int i = func.ArgCount - 1; i >= 0; --i)
                                args[i] = oprndStack.Pop();
                           // if (evalCBox.Checked)
                                oprnd = EvalFunction(func, args);
                        }
                        else
                            StackError("function", token.StrValue);
                        break;
                    // Pop Identifier and value to be assigned fron the stack
                    // Push the resulting operand onto the operand stack
                    case RpnParser.ElementType.Assignment:
                        if (oprndStack.Count > 1)
                        {
                            rhs = oprndStack.Pop();   // value to be assigned
                            lhs = oprndStack.Pop();   // identifier to be assigned to
                            oprnd = AssignOperator((RpnOperator)token, lhs, rhs);
                        }
                        else
                            StackError("assignment", token.StrValue);
                        break;

                }
                oprndStack.Push(oprnd);
            }
            if (oprndStack.Count != 1)
                StackError("result", "");
            return oprndStack.Pop();
        }

        private void StackError(string token, string value)
        {
            string text = String.Format("Insufficiant operands on stack for {0}: {1}", token, value);
            throw new ApplicationException(text);
        }

        private RpnParser.RpnOperand EvalIdentifier(RpnToken token, List<string> current_values )
        {
            int pos;
            string id = token.StrValue;
            if (token.IsQualified)
                id = token.Qualifier + '.' + id;
            RpnOperand oprnd = null;
            if (id.ToLower() == "pi")
                oprnd = new RpnOperand(Math.PI);
            else {
                foreach (string line in current_values)
                {
                    if ((pos = line.IndexOf('=')) > 0)
                    {
                        string tboxId = line.Substring(0, pos).Trim();
                        string tboxValue = line.Substring(pos + 1).Trim();
                        if (id == tboxId)
                        {
                            oprnd = new RpnOperand(tboxValue);
                            break;
                        }
                    }
                }
                if (oprnd == null)
                {
                    double numValue = 0;
                    if (id.Length == 1 && Char.IsLetter(id[0]))
                    {
                        char ndxChar = id.ToLower()[0];
                        int index = ndxChar - 'a';
                        numValue = assignTo[index];
                    }
                    oprnd = new RpnOperand(numValue);
                }
                oprnd.FieldName = id;
            }
            return oprnd;
        }

        private double[] assignTo = new double[26];
        private RpnOperand AssignOperator(RpnOperator oper, RpnOperand lhs, RpnOperand rhs)
        {
            char ndxChar = lhs.FieldName.ToLower()[0];
            int index = ndxChar - 'a';
            double result = rhs.NumValue;
            double prevValue = assignTo[index];

            switch (oper.OpType)
            {
                case OperatorType.AssignMinus:
                    result = prevValue - result;
                    break;
                case OperatorType.AssignPlus:
                    result = prevValue + result;
                    break;
                case OperatorType.AssignMultiply:
                    result = prevValue * result;
                    break;
                case OperatorType.AssignDivide:
                    result = prevValue / result;
                    break;
                case OperatorType.AssignRemainder:
                    result = prevValue % result;
                    break;
                case OperatorType.AssignIntegerDivide:
                    result = (double)(prevValue / (int)result);
                    break;
            }

            assignTo[index] = result;

            return new RpnOperand(result);
        }

        private RpnParser.RpnOperand EvalOperator(RpnOperator op, RpnOperand lhs, RpnOperand rhs)
        {
            RpnOperand oprnd = null;
            switch (op.OpGroup)
            {
                case OperatorGroup.Arithmetic:
                    oprnd = MathOperator(op, lhs, rhs);
                    break;
                case OperatorGroup.Bitwise:
                    oprnd = BitWiseOperator(op, lhs, rhs);
                    break;
                case OperatorGroup.Comparison:
                    oprnd = CompOperator(op, lhs, rhs);
                    break;
                case OperatorGroup.Logical:
                    oprnd = LogicalOperator(op, lhs, rhs);
                    break;
                default:
                    throw new ApplicationException("Invalid operator: " + op.ToString());
            }
            return oprnd;
        }

        private RpnOperand MathOperator(RpnOperator op, RpnOperand lhs, RpnOperand rhs)
        {
            bool isString = false;
            string retStr = "";
            double result = 0;
            double left = lhs.NumValue;
            double right = rhs.NumValue;
            if (Double.IsNaN(left) || Double.IsNaN(right))
            {
                if (op.OpType == OperatorType.Plus)
                    isString = true;
                else
                    throw new ApplicationException("Invalid string operation: " + op.StrValue);
            }
            switch (op.OpType)
            {
                case OperatorType.Plus:
                    if (isString)
                        retStr = lhs.StrValue + rhs.StrValue;
                    else
                        //if (op.IsMonadic)
                        //result = lhs.NumValue;
                        //else
                        result = lhs.NumValue + rhs.NumValue;
                    break;
                case OperatorType.Minus:
                    //if (op.IsMonadic)
                    //result = 0 - lhs.NumValue;
                    //else
                    result = lhs.NumValue - rhs.NumValue;
                    break;
                case OperatorType.Multiply:
                    result = lhs.NumValue * rhs.NumValue;
                    break;
                case OperatorType.Remainder:
                    result = lhs.NumValue % rhs.NumValue;
                    break;
                case OperatorType.Divide:
                case OperatorType.IntegerDivide:
                    result = lhs.NumValue / rhs.NumValue;
                    if (op.OpType == OperatorType.IntegerDivide)
                        result = Math.Truncate(result);
                    break;
            }
            RpnOperand oprnd;
            if (isString)
                oprnd = new RpnOperand(retStr);
            else
                oprnd = new RpnOperand(result);
            return oprnd;
        }

        private RpnOperand CompOperator(RpnOperator op, RpnOperand lhs, RpnOperand rhs)
        {
            int comp = 0;
            if (lhs.IsString && rhs.IsString)
                comp = lhs.StrValue.CompareTo(rhs.StrValue);
            else
                comp = lhs.NumValue.CompareTo(rhs.NumValue);

            double result = 0;
            switch (op.OpType)
            {
                case OperatorType.CompEqual:
                    if (comp == 0)
                        result = 1;
                    break;
                case OperatorType.CompNotEqual:
                    if (comp != 0)
                        result = 1;
                    break;
                case OperatorType.CompGreater:
                    if (comp > 0)
                        result = 1;
                    break;
                case OperatorType.CompGreaterEqual:
                    if (comp >= 0)
                        result = 1;
                    break;
                case OperatorType.CompLessThan:
                    if (comp < 0)
                        result = 1;
                    break;
                case OperatorType.CompLessThanEqual:
                    if (comp <= 0)
                        result = 1;
                    break;
            }
            return new RpnOperand(result);
        }

        private RpnOperand BitWiseOperator(RpnOperator op, RpnOperand lhs, RpnOperand rhs)
        {
            RpnOperand oprnd;
            double lNum = lhs.NumValue;
            double rNum = rhs.NumValue;
            if (Double.IsNaN(lNum) || Double.IsNaN(rNum))
            {
                throw new ApplicationException("Bitwise operation not defined for strings");
            }
            else {
                double temp = Math.Round(lNum);
                if (temp > lNum)
                    lNum = temp - 1;
                else
                    lNum = temp;
                temp = Math.Round(rNum);
                if (temp > rNum)
                    rNum = temp - 1;
                else
                    rNum = temp;
                long left = Convert.ToInt64(lNum);
                long right = Convert.ToInt64(rNum);
                long bwLong = 0;
                switch (op.OpType)
                {
                    case OperatorType.BitwiseAnd:
                        bwLong = left & right;
                        break;
                    case OperatorType.BitwiseOr:
                        bwLong = left | right;
                        break;
                    case OperatorType.BitwiseXor:
                        bwLong = left ^ right;
                        break;
                }
                oprnd = new RpnOperand(Convert.ToDouble(bwLong));

            }
            return oprnd;
        }

        private RpnOperand LogicalOperator(RpnOperator op, RpnOperand lhs, RpnOperand rhs)
        {
            bool rhBool, lhBool;
            double result = 0;
            lhBool = rhBool = false;
            if (rhs.NumValue == 1)
                rhBool = true;
            if (lhs.NumValue == 1)
                lhBool = true;
            switch (op.OpType)
            {
                case OperatorType.LogicalAnd:
                    if (lhBool && rhBool)
                        result = 1;
                    break;
                case OperatorType.LogicalOr:
                    if (lhBool || rhBool)
                        result = 1;
                    break;
            }
            return new RpnOperand(result);
        }

        private RpnParser.RpnOperand EvalFunction(RpnFunction func, RpnOperand[] args)
        {
            double result = 0;
            bool argError = false;
            switch (func.Name)
            {
                case "Min":
                    if (args.Length != 2)
                        argError = true;
                    else
                        result = Math.Min(args[0].NumValue, args[1].NumValue);
                    break;
                case "Max":
                    if (args.Length != 2)
                        argError = true;
                    else
                        result = Math.Max(args[0].NumValue, args[1].NumValue);
                    break;
                case "Sin":
                    if (args.Length != 1)
                        argError = true;
                    else {
                        result = Math.Sin(args[0].NumValue);
                        result = Math.Round(result, 10);
                    }
                    break;
                default:
                    throw new ApplicationException("Undefined function: " + func.Name);
            }
            if (argError)
                throw new ApplicationException("Incorrect number of arguments passed to function: " + func.Name + " (" + args.Length.ToString() + ")");
            return new RpnOperand(result);
        }


    }
}
