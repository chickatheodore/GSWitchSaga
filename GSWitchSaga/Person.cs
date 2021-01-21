using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSWitchSaga
{
    public class Person
    {

        #region Constructor
        public Person()
        {

        }

        public Person(int ageOfDeath, int yearOfDeath)
        {
            AgeOfDeath = ageOfDeath;
            YearOfDeath = yearOfDeath;
        }
        #endregion



        #region Read & write properties
        public int AgeOfDeath { get; set; }
        public int YearOfDeath { get; set; }
        #endregion

        
        
        #region Read only properties

        /*
         * Find different between Year of Death and Age of Death
         * return int
        */
        public int YearSeries
        {
            get
            {
                return YearOfDeath - AgeOfDeath;
            }
        }


        /*
         * Find the number of villager(s) the witch has to kill on specified year.
         * The pattern is Fibonacci series, using Iterative approach.
         * 
         * return int.
        */

        public int NumberOfPersonKilled
        {
            get
            {
                //Skip operation and return the parameter's value
                if (YearSeries <= 0) return YearSeries;

                List<int> values = new List<int>() { 0, 1 };

                int firstNumber = 0, secondNumber = 1, nextNumber = 0;

                //return zero if year series less than or equal to zero
                if (YearSeries <= 0)
                    return firstNumber;

                for (int i = 2; i <= YearSeries; i++)
                {
                    nextNumber = firstNumber + secondNumber;
                    firstNumber = secondNumber;
                    secondNumber = nextNumber;
                    values.Add(secondNumber);
                }

                int result = 0;
                foreach (int value in values)
                    result += value;

                return result;
            }
        }
        #endregion

    }
}
