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
            if (!validPersonA) return;

            bool validPersonB = ValidInput(textPerson_B_AOD) && ValidInput(textPerson_B_YOD);       //data validation for Person B
            if (!validPersonB) return;


            //Declaring variables
            int person_A_AgeOfDeath = Convert.ToInt32(textPerson_A_AOD.Text);
            int person_A_YearOfDeath = Convert.ToInt32(textPerson_A_YOD.Text);

            int person_B_AgeOfDeath = Convert.ToInt32(textPerson_B_AOD.Text);
            int person_B_YearOfDeath = Convert.ToInt32(textPerson_B_YOD.Text);

            //Creating person object
            Person person_A = new Person(person_A_AgeOfDeath, person_A_YearOfDeath);
            Person person_B = new Person(person_B_AgeOfDeath, person_B_YearOfDeath);


            //Creating output to result textbox
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("Person A born on Year = {0} – {1} = {2}", person_A.YearOfDeath, person_A.AgeOfDeath, person_A.YearSeries);

            if (person_A.NumberOfPersonKilled < 0)
                builder.Append(", invalid data given.\r\n");
            else
                builder.AppendFormat(", number of people killed on year {0} is {1}.\r\n", person_A.YearSeries, person_A.NumberOfPersonKilled);


            builder.AppendFormat("Person B born on Year = {0} – {1} = {2}", person_B.YearOfDeath, person_B.AgeOfDeath, person_B.YearSeries);

            if (person_B.NumberOfPersonKilled < 0)
                builder.Append(", invalid data given.\r\n");
            else
                builder.AppendFormat(", number of people killed on year {0} is {1}.\r\n", person_B.YearSeries, person_B.NumberOfPersonKilled);


            //Check if you give invalid data
            if (person_A.NumberOfPersonKilled < 0 || person_B.NumberOfPersonKilled < 0)
            {
                builder.AppendFormat("\r\nReturn -1. Invalid data.");
                textResult.Text = builder.ToString();

                MessageBox.Show("Return -1. Invalid data", "Invalid data");
            }
            else
            {
                //Find the average of people killed
                int average = ((person_A.NumberOfPersonKilled + person_B.NumberOfPersonKilled) / 2);

                builder.AppendFormat("\r\nSo the average is ( {0} + {1} )/2 = {2}",
                    person_A.NumberOfPersonKilled, person_B.NumberOfPersonKilled, average);

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
