// <copyright file="FizzBuzz.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Multithreading
{
    using System.Threading;

    /// <summary>
    /// Contains methods to print values based on a specific logic.
    /// </summary>
    public class FizzBuzz
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
        /// Declaration of Semaphore.
        /// </summary>
#pragma warning disable SX1309 // Field names should begin with underscore
        private Semaphore semaphore = new Semaphore(1, 1);
#pragma warning restore SX1309 // Field names should begin with underscore

        /// <summary>
        /// Initializes a new instance of the <see cref="FizzBuzz"/> class.
        /// </summary>
        /// <param name="number">Total Count</param>
        public FizzBuzz(int number)
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
                this.semaphore.WaitOne();

                if (this.startValue > this.totalCountOfNumbers)
                {
                    this.semaphore.Release();
                    break;
                }

                if (this.startValue % 3 == 0 && this.startValue % 5 != 0)
                {
                    printFizz();
                    this.startValue++;
                }

                this.semaphore.Release();
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
                this.semaphore.WaitOne();
                if (this.startValue > this.totalCountOfNumbers)
                {
                    this.semaphore.Release();
                    break;
                }

                if (this.startValue % 5 == 0 && this.startValue % 3 != 0)
                {
                    printBuzz();
                    this.startValue++;
                }

                this.semaphore.Release();
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
                this.semaphore.WaitOne();
                if (this.startValue > this.totalCountOfNumbers)
                {
                    this.semaphore.Release();
                    break;
                }

                if (this.startValue % 5 == 0 && this.startValue % 3 == 0)
                {
                    printFizzBuzz();
                    this.startValue++;
                }

                this.semaphore.Release();
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
                this.semaphore.WaitOne();
                if (this.startValue > this.totalCountOfNumbers)
                {
                    this.semaphore.Release();
                    break;
                }

                if (this.startValue % 5 != 0 && this.startValue % 3 != 0)
                {
                    printNumber(this.startValue);
                    this.startValue++;
                }

                this.semaphore.Release();
            }
        }
    }
}
