// <copyright file="FizzBuzzUsingLock.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Multithreading
{
    using System.Threading;

    /// <summary>
    /// Contains methods to print values based on a specific logic.
    /// </summary>
    internal class FizzBuzzUsingLock
    {
        /// <summary>
        /// Total number of values.
        /// </summary>
#pragma warning disable SX1309 // Field names should begin with underscore
        private readonly int totalCountOfNumbers;
#pragma warning restore SX1309 // Field names should begin with underscore

        /// <summary>
        /// Temporary variable to be incremented.
        /// </summary>
#pragma warning disable SX1309 // Field names should begin with underscore
        private int startValue = 1;
#pragma warning restore SX1309 // Field names should begin with underscore

        /// <summary>
        /// Lock the thread.
        /// </summary>
#pragma warning disable SX1309 // Field names should begin with underscore
        private object lockThread = new object();
#pragma warning restore SX1309 // Field names should begin with underscore

        /// <summary>
        /// Initializes a new instance of the <see cref="FizzBuzzUsingLock"/> class.
        /// </summary>
        /// <param name="number">number</param>
        public FizzBuzzUsingLock(int number)
        {
            this.totalCountOfNumbers = number;
        }

        /// <summary>
        /// Prints Fizz when the number is divisible by 3 and not be 5.
        /// </summary>
        /// <param name="printFizz">ACtion type</param>
        public void Fizz(Action printFizz)
        {
            while (this.startValue <= this.totalCountOfNumbers)
            {
                lock (this.lockThread)
                {
                    if (this.startValue <= this.totalCountOfNumbers)
                    {
                        if (this.startValue % 3 == 0 && this.startValue % 5 != 0)
                        {
                            printFizz();
                            this.startValue++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prints Buzz when the number is divisible by 5 and not be 3.
        /// </summary>
        /// <param name="printBuzz">printBuzz</param>
        public void Buzz(Action printBuzz)
        {
            while (this.startValue <= this.totalCountOfNumbers)
            {
                lock (this.lockThread)
                {
                    if (this.startValue <= this.totalCountOfNumbers)
                    {
                        if (this.startValue % 5 == 0 && this.startValue % 3 != 0)
                        {
                            printBuzz();
                            this.startValue++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prints FizzBuzz when the number is divisible by 3 and 5.
        /// </summary>
        /// <param name="printFizzBuzz">Action type</param>
        public void Fizzbuzz(Action printFizzBuzz)
        {
            while (this.startValue <= this.totalCountOfNumbers)
            {
                lock (this.lockThread)
                {
                    if (this.startValue <= this.totalCountOfNumbers)
                    {
                        if (this.startValue % 5 == 0 && this.startValue % 3 == 0)
                        {
                            printFizzBuzz();
                            this.startValue++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prints the actual number when the number is not divisible by 3 and 5.
        /// </summary>
        /// <param name="printNumber">Action type</param>
        public void Number(Action<int> printNumber)
        {
            while (this.startValue <= this.totalCountOfNumbers)
            {
                lock (this.lockThread)
                {
                    if (this.startValue <= this.totalCountOfNumbers)
                    {
                        if (this.startValue % 5 != 0 && this.startValue % 3 != 0)
                        {
                            printNumber(this.startValue);
                            this.startValue++;
                        }
                    }
                }
            }
        }
    }
}
