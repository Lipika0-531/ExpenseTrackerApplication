// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Multithreading
{
    /// <summary>
    /// Driver class.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Driver method.
        /// </summary>
        public static void Main()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to MultiThreading concept !!!\nEnter a end value : ");
                Console.ForegroundColor = ConsoleColor.White;
                int.TryParse(Console.ReadLine(), out int endValue);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("The results >>>");
                Console.ForegroundColor = ConsoleColor.White;

                FizzBuzz fizzBuzz = new FizzBuzz(endValue);
                Action printFizz = () => { Console.WriteLine("Fizz"); };
                Action printBuzz = () => { Console.WriteLine("Buzz"); };
                Action printFizzBuzz = () => { Console.WriteLine("FizzBuzz"); };
                Action<int> printNumbers = (n) => { Console.WriteLine(n); };
                Thread threadA = new Thread(new ThreadStart(() => { fizzBuzz.Fizz(printFizz); }));
                Thread threadB = new Thread(new ThreadStart(() => { fizzBuzz.Buzz(printBuzz); }));
                Thread threadC = new Thread(new ThreadStart(() => { fizzBuzz.Fizzbuzz(printFizzBuzz); }));
                Thread threadD = new Thread(new ThreadStart(() => { fizzBuzz.Number(printNumbers); }));

                threadA.Start();
                threadB.Start();
                threadC.Start();
                threadD.Start();

                threadA.Join();
                threadB.Join();
                threadC.Join();
                threadD.Join();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Caught : {ex.Message}");
            }

            #region Using Lock Concept.
            /*FizzBuzzUsingLock fizzBuzzUsingLock = new FizzBuzzUsingLock(15);
            Action printFizzUsingLock = () => { Console.WriteLine("Fizz"); };
            Action printBuzzUsingLock = () => { Console.WriteLine("Buzz"); };
            Action printFizzBuzzUsingLock = () => { Console.WriteLine("FizzBuzz"); };
            Action<int> printNumbersUsingLock = (n) => { Console.WriteLine(n); };
            Thread thread1 = new Thread(new ThreadStart(() => { fizzBuzzUsingLock.Fizz(printFizz); }));
            Thread thread2 = new Thread(new ThreadStart(() => { fizzBuzzUsingLock.Buzz(printBuzz); }));
            Thread thread3 = new Thread(new ThreadStart(() => { fizzBuzzUsingLock.Fizzbuzz(printFizzBuzz); }));
            Thread thread4 = new Thread(new ThreadStart(() => { fizzBuzzUsingLock.Number(printNumbers); }));

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();*/
            #endregion
        }
    }
}