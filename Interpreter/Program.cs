using System;
using System.Collections.Generic;

namespace Interpreter
{

    class Context
    {

        private string input;
        private int output;

        public Context(string input)
        {
            this.input = input;
        }

        public string Input
        {
            get { return input; }
            set { input = value; }
        }

        public int Output
        {
            get { return output; }
            set { output = value; }
        }

    }


    abstract class Phrase
    {

        public void Interpreter(Context context)
        {

            if (context.Input.Length == 0)
                return;

            while (context.Input.StartsWith(Nine()))
            {
                context.Output += 9 * Multiplier();
                context.Input = context.Input.Substring(2);
            }

            while (context.Input.StartsWith(Four()))
            {
                context.Output += 4 * Multiplier();
                context.Input = context.Input.Substring(2);
            }

            while (context.Input.StartsWith(Five()))
            {
                context.Output += 5 * Multiplier();
                context.Input = context.Input.Substring(1);
            }

            while (context.Input.StartsWith(One()))
            {
                context.Output += 1 * Multiplier();
                context.Input = context.Input.Substring(1);
            }
        }

        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();
        public abstract int Multiplier();

    }


    class PhraseThousands : Phrase
    {
        public override string One() { return "M"; }
        public override string Four() { return " "; }
        public override string Five() { return " "; }
        public override string Nine() { return " "; }
        public override int Multiplier() { return 1000; }
    }

    class PhraseHundreds : Phrase
    {
        public override string One() { return "C"; }
        public override string Four() { return "CD"; }
        public override string Five() { return "D"; }
        public override string Nine() { return "CM"; }
        public override int Multiplier() { return 100; }
    }

    class PhraseTens : Phrase
    {
        public override string One() { return "X"; }
        public override string Four() { return "XL"; }
        public override string Five() { return "L"; }
        public override string Nine() { return "XC"; }
        public override int Multiplier() { return 10; }
    }

    class PhraseOnes : Phrase
    {
        public override string One() { return "I"; }
        public override string Four() { return "IV"; }
        public override string Five() { return "V"; }
        public override string Nine() { return "IX"; }
        public override int Multiplier() { return 1; }
    }


    class Program
    {
        static void Main()
        {

            string roman = "MDXLIII";
            Context context = new Context(roman);

            List<Phrase> tree = new List<Phrase>();
            tree.Add(new PhraseThousands());
            tree.Add(new PhraseHundreds());
            tree.Add(new PhraseTens());
            tree.Add(new PhraseOnes());

            foreach (Phrase item in tree)
            {
                item.Interpreter(context);
            }
            Console.WriteLine(roman + " = " + context.Output);

            roman = "CMXVII";
            context = new Context(roman);
            foreach (Phrase item in tree)
            {
                item.Interpreter(context);
            }
            Console.WriteLine(roman + " = " + context.Output);

        }
    }

}
