

using System;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mysterynumber_daphnevanrooij
{
    public partial class Form1 : Form
    {
        //define variables
        Random GenerateRandomValue = new Random(); 
        int maxvalue = 0;
        int minvalue = 0;

        int mysteryNumber;
        int attemptsLeft;

        public Form1()
        {
            InitializeComponent();
            gbxPlayDroo.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // start up screen

            pbxMovingPictureDroo.Left = gbxSetupDroo.Left;
            pbxMovingPictureDroo.Left = gbxPlayDroo.Left;
            pbxMovingPictureDroo.Left = gbxInformationDroo.Left;
            btnMovingPictureDroo.Left = 110;

            // make the game invisible
            gbxSetupDroo.Visible = false; 
            gbxPlayDroo.Visible = false;
            gbxInformationDroo.Visible = false;

            this.Width = pbxMovingPictureDroo.Width + 35;
        }

        private void btnMovingPictureDroo_Click(object sender, EventArgs e)
        {
            // open the game, make the game visible
            gbxSetupDroo.Visible = true;
            gbxPlayDroo.Visible = true;
            gbxInformationDroo.Visible = true;
            pbxMovingPictureDroo.Visible = false;
            btnMovingPictureDroo .Visible = false;
            
        }//reveal the game

        private void btnGoDroo_Click(object sender, EventArgs e) //what happens when you press go 
        {
            try // try to run code, catch any errors & handle it
            {

                // get the min and max values from text boxes
                minvalue = int.Parse(tbxStartDroo.Text);
                maxvalue = int.Parse(tbxStopDroo.Text);

                if (minvalue >= maxvalue) // show message when the minimum value is more than the maximum value
                {
                    MessageBox.Show("The minimum value must be less than the maximum value.");
                    LogMessage($"Error: Min value can't be bigger than max value.");
                    return;
                }

                // generate the mystery number
                mysteryNumber = GenerateRandomValue.Next(minvalue, maxvalue);

                // get the number of attempts
                attemptsLeft = int.Parse(tbxNumberOfAttemptsDroo.Text);
                if (attemptsLeft <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for attempts.");
                    LogMessage($"Error: Enter number of attempts");
                    return;
                }

                // progressbar
                pgbAttemptsLeftDroo.Maximum = attemptsLeft;
                pgbAttemptsLeftDroo.Value = attemptsLeft;
                pgbAttemptsLeftDroo.Minimum = 0;

                // Inform the user
                rtbInformationDroo.AppendText($"A random number between {minvalue} and {maxvalue} has been generated.\n");
                MessageBox.Show($"You have {attemptsLeft} attempts to guess the number.");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
                LogMessage($"Error: {ex.Message}");
            }

            gbxPlayDroo.Enabled = true;
        }

        private void btnguessdroo_Click(object sender, EventArgs e) //what happens when you press guess
        {
            try
            {
                if (mysteryNumber == 0)
                {
                    MessageBox.Show("Please generate the mystery number first!");
                    LogMessage("Generate a mystery number.");
                    return;
                }

                // get the users guess
                int userGuess = int.Parse(tbxGuessDroo.Text);

                if (userGuess < minvalue || userGuess > maxvalue)
                {
                    MessageBox.Show($"Please guess a number between {minvalue} and {maxvalue}.");
                    LogMessage($"Invalid guess! {userGuess} is out of range.");
                    return;
                }

                // if the guess is correct, show message
                if (userGuess == mysteryNumber)
                {
                    MessageBox.Show("C  );
                    LogMessage($"Correct guess {userGuess}, player has won the game!");

                    //play a sound when you win
                    System.Media.SoundPlayer player =
                    new System.Media.SoundPlayer();
                    player.SoundLocation = @"C:\Users\Daphne school\Downloads\victorymale-version-230553.wav";
                    player.Load();
                    player.Play();

                    ResetGame(); //resets the game
                    return;
                }

                
                attemptsLeft--; //subtract an attempt if the answer is wrong

                MessageBox.Show($"Wrong guess! Attempts left: {attemptsLeft}"); // wrong guess
                LogMessage($"Wrong guess: {userGuess}, try again.");

                pgbAttemptsLeftDroo.Value = attemptsLeft;


                int difference = Math.Abs(mysteryNumber - userGuess); //calculate difference between user's guess and the mystery number for the hot/cold bar
                UpdateTemperatureMeter(difference);

                // end game when there are no attempts left
                if (attemptsLeft <= 0)
                {
                    MessageBox.Show($"Game over! The mystery number was {mysteryNumber}.");
                    LogMessage($"Game over: Ran out of guesses.");
                    ResetGame();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for your guess.");
                LogMessage("Guess failed: number entered invalid.");
            }
        }

        private void UpdateTemperatureMeter(int difference)
        {
            try
            {
                int maxDifference = Math.Max(maxvalue - minvalue, 1); // Avoid dividing by 0
                int trackBarValue = (int)((1.0 - (double)difference / maxDifference) * tbHotColdDroo.Maximum);

                tbHotColdDroo.Value = Math.Max(0, Math.Min(trackBarValue, tbHotColdDroo.Maximum));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the temperature meter: {ex.Message}");
                LogMessage($"Unable to update temperature meter: {ex.Message}");
            }
        }  // update the temperature meter (hot/cold)

        private void ResetGame() //reset the game to blank
        {
            mysteryNumber = 0;
            attemptsLeft = 0;

            tbxStartDroo.Clear();
            tbxStopDroo.Clear();
            tbxGuessDroo.Clear();
            tbxNumberOfAttemptsDroo.Clear();

            pgbAttemptsLeftDroo.Value = 0;
            rtbInformationDroo.Clear();
            gbxPlayDroo.Enabled = false;
        }

        private void btnaboutdroo_Click(object sender, EventArgs e) //show information about me
        {
            MessageBox.Show("Made by Daphne van Rooij! Contact: 94465@roc-teraa.nl");
            LogMessage("About: Daphne van Rooij, 94465@roc-teraa.nl");
        }

        private void btncleardroo_Click(object sender, EventArgs e) //clear the game
        {
            ResetGame();
        }

        private void btncheatdroo_Click(object sender, EventArgs e) //immediately get the answer
        {
            if (mysteryNumber == 0)
            {
                MessageBox.Show("Please generate a mystery number first!");
                LogMessage("Cheat failed: Generate mystery number.");
            }
            else
            {
                MessageBox.Show($"The mystery number was {mysteryNumber}!");
                LogMessage($"Cheat: {mysteryNumber}");
            }
        }

        private void btnlocatedroo_Click(object sender, EventArgs e) //locate the game
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            MessageBox.Show($"The game is located at:\n{appPath}", "Locate Application");
            LogMessage($"Location: \n{appPath}");
        }

        private void LogMessage(string message)
        {
            rtbInformationDroo.AppendText($"{DateTime.Now}: {message}");
            rtbInformationDroo.ScrollToCaret();
        }//log actions and errors
    }
}
