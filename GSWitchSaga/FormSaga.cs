using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSWitchSaga
{
    public partial class FormSaga : Form
    {
        public FormSaga()
        {
            InitializeComponent();
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            //Clearing result textbox
            textResult.Text = "";

            //Make sure all parameters for Person A are valid.
            bool validPersonA = ValidInput(textPerson_A_AOD) && ValidInput(textPerson_A_YOD);       //data validation for Person A
            bool validPersonB = ValidInput(textPerson_B_AOD) && ValidInput(textPerson_B_YOD);       //data validation for Person B

            if (!validPersonA || !validPersonB)                                                     //make sure all inputs are valid
                return;

            //Declaring variables
            int person_A_AgeOfDeath = Convert.ToInt32(textPerson_A_AOD.Text);
            int person_A_YearOfDeath = Convert.ToInt32(textPerson_A_YOD.Text);

            int person_B_AgeOfDeath = Convert.ToInt32(textPerson_B_AOD.Text);
            int person_B_YearOfDeath = Convert.ToInt32(textPerson_B_YOD.Text);

            //Find year series based on given age of death and year of death
            int person_A_Year_Series = person_A_YearOfDeath - person_A_AgeOfDeath;
            int person_B_Year_Series = person_B_YearOfDeath - person_B_AgeOfDeath;

            //Find the number of people killed by specific year series
            int person_A_NumberOfKilled = NumberOfPeopleKilled(person_A_Year_Series);
            int person_B_NumberOfKilled = NumberOfPeopleKilled(person_B_Year_Series);


            //Creating output to result textbox
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("Person A born on Year = {0} – {1} = {2}", person_A_YearOfDeath, person_A_AgeOfDeath, person_A_Year_Series);

            if (person_A_NumberOfKilled < 0)
                builder.Append(", invalid data given.\r\n");
            else
                builder.AppendFormat(", number of people killed on year {0} is {1}.\r\n", person_A_Year_Series, person_A_NumberOfKilled);


            builder.AppendFormat("Person B born on Year = {0} – {1} = {2}", person_B_YearOfDeath, person_B_AgeOfDeath, person_B_Year_Series);

            if (person_B_NumberOfKilled < 0)
                builder.Append(", invalid data given.\r\n");
            else
                builder.AppendFormat(", number of people killed on year {0} is {1}.\r\n", person_B_Year_Series, person_B_NumberOfKilled);


            //Check if you give invalid data
            if (person_A_NumberOfKilled < 0 || person_B_NumberOfKilled < 0)
            {
                builder.AppendFormat("\r\nReturn -1. Invalid data.");
                textResult.Text = builder.ToString();

                MessageBox.Show("Return -1. Invalid data", "Invalid data");
            }
            else
            {
                //Find the average of people killed
                decimal average = ((decimal)(person_A_NumberOfKilled + person_B_NumberOfKilled) / 2);

                builder.AppendFormat("\r\nSo the average is ( {0} + {1} )/2 = {2}",
                    person_A_NumberOfKilled, person_B_NumberOfKilled, average);

                textResult.Text = builder.ToString();
            }
        }


        /*
         * Validation for TextBox's text.
         * Check for empty text and then validating the text is number
         * 
         * @param textBox (TextBox), the TextBox Control to be validate.
         * return type boolean.
        */

        private bool ValidInput(TextBox textBox)
        {
            //Clean the input from whitespace
            string text = textBox.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                //if the value is empty then return not valid
                textBox.Focus();
                return false;
            }
            else
            {
                bool isNumber = text.All(char.IsNumber);    //check if input is in numeric format
                if (!isNumber)
                {
                    textBox.Focus();
                    textBox.SelectAll();
                    return false;
                }
            }

            return true;
        }


        /*
         * Find the number of villager(s) the witch has to kill on specified year.
         * The pattern is Fibonacci series, using Iterative approach.
         * 
         * @param year (int), year series.
         * return type int.
        */

        private int NumberOfPeopleKilled(int year)
        {
            //Skip operation and return the parameter's value
            if (year <= 0) return year;

            List<int> values = new List<int>() { 0, 1 };

            int firstNumber = 0, secondNumber = 1, nextNumber = 0;

            //return zero if year series less than or equal to zero
            if (year <= 0)
                return firstNumber;

            for (int i = 2; i <= year; i++)
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


        /*
         * Some people loves to press Enter (replacing Tab) to switch between controls.
        */
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Type type = this.ActiveControl.GetType();
            bool allowTab = !type.Equals(typeof(Button));

            if (keyData == (Keys.Enter) && allowTab)
            {
                SendKeys.Send("{TAB}");
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
