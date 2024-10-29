

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
        int maxvalueDroo = 0;
        int minvalueDroo = 0;

        int mysteryNumberDroo;
        int attemptsLeftDroo;

        public Form1()
        {
            InitializeComponent();
            gbxPlayDroo.Enabled = false; //disable the play box

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // start up screen

            pbxMovingPictureDroo.Left = gbxSetupDroo.Left;
            pbxMovingPictureDroo.Left = gbxPlayDroo.Left;
            pbxMovingPictureDroo.Left = gbxInformationDroo.Left;
            btnMovingPictureDroo.Left = 110;
            lblMadeByDroo.Left = 18;

            // make the game invisible
            gbxSetupDroo.Visible = false; 
            gbxPlayDroo.Visible = false;
            gbxInformationDroo.Visible = false;

            this.Width = pbxMovingPictureDroo.Width + 15;

            this.Text = "Daphne van Rooij"; //application caption (name) has my name

        }

        private void btnMovingPictureDroo_Click(object sender, EventArgs e)
        {
            // open the game, make the game visible
            gbxSetupDroo.Visible = true;
            gbxPlayDroo.Visible = true;
            gbxInformationDroo.Visible = true;
            pbxMovingPictureDroo.Visible = false;
            btnMovingPictureDroo .Visible = false;
            lblMadeByDroo.Visible = false;

            pbxMovingpicture2Droo.Left = gbxSetupDroo.Left;
            pbxMovingpicture2Droo.Left = gbxPlayDroo.Left;
            pbxMovingpicture2Droo.Left = gbxInformationDroo.Left;
            tbxEnterYourNameDroo.Left = 80;
            lblNameDroo.Left = 120;
            btnSubmitDroo.Left = 125;
            btnShowGameDroo.Left = 100;

        }//reveal the game

        private void btnGoDroo_Click(object sender, EventArgs e) //what happens when you press go 
        {
            try // try to run code, catch any errors & handle it
            {

                // get the min and max values from text boxes
                minvalueDroo = int.Parse(tbxStartDroo.Text);
                maxvalueDroo = int.Parse(tbxStopDroo.Text); 

                if (minvalueDroo >= maxvalueDroo) // show message when the minimum value is more than the maximum value
                {
                    MessageBox.Show("The minimum value must be less than the maximum value.");
                    LogMessage($"Error: Min value can't be bigger than max value.");
                    return;
                }

                if (maxvalueDroo < 10 || minvalueDroo >= maxvalueDroo || maxvalueDroo > 100) // show message when values are out of range
                {
                    MessageBox.Show("Maximum value cannot be greater than 100 or less than 10.");
                    LogMessage("Error: Invalid range values.");
                    return;
                }

                // generate the mystery number
                mysteryNumberDroo = GenerateRandomValue.Next(minvalueDroo, maxvalueDroo);

                if (string.IsNullOrWhiteSpace(tbxNumberOfAttemptsDroo.Text) ||
                    !int.TryParse(tbxNumberOfAttemptsDroo.Text, out attemptsLeftDroo) ||
                    attemptsLeftDroo <= 0) //checks if input is empty or contains whitespace, then attempts to parse string into integer.
                {
                    MessageBox.Show("Please enter a valid positive number for attempts.");
                    LogMessage($"Error: Enter number of attempts");
                    return;
                }

                // progressbar
                pgbAttemptsLeftDroo.Maximum = attemptsLeftDroo;
                pgbAttemptsLeftDroo.Value = attemptsLeftDroo;
                pgbAttemptsLeftDroo.Minimum = 0;

                pgbWrongGuessesDroo.Maximum = attemptsLeftDroo;
                pgbWrongGuessesDroo.Value = 0;

                // Inform the user
                rtbInformationDroo.AppendText($"A random number between {minvalueDroo} and {maxvalueDroo} has been generated.\n");
                MessageBox.Show($"You have {attemptsLeftDroo} attempts to guess the number.");

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
                if (mysteryNumberDroo == 0) //show message if there's no mystery number
                {
                    MessageBox.Show("Please generate the mystery number first!");
                    LogMessage("Generate a mystery number.");
                    return;
                }

                // get the users guess
                int userGuess = int.Parse(tbxGuessDroo.Text);

                if (userGuess < minvalueDroo || userGuess > maxvalueDroo) //show message if guess is out of range
                {
                    MessageBox.Show($"Please guess a number between {minvalueDroo} and {maxvalueDroo}.");
                    LogMessage($"Invalid guess! {userGuess} is out of range.");
                    return;
                }

                // if the guess is correct, show message
                if (userGuess == mysteryNumberDroo)
                {
                    MessageBox.Show($"Congrats, {tbxEnterYourNameDroo.Text}! You have correctly guessed the mystery number.");
                    LogMessage($"Correct guess {userGuess}, player has won the game!");
                    pbxYouWinDroo.Left = gbxPlayDroo.Left;
                    pbxYouWinDroo.Visible = true;

                    pbxMovingPicture3Droo.Left = gbxInformationDroo.Left;
                    pbxMovingPicture3Droo.Left = gbxPlayDroo.Left;
                    pbxMovingPicture3Droo.Left = gbxSetupDroo.Left;

                    DialogResult playAgain = MessageBox.Show("Do you want to play again ? ", "Play Again", MessageBoxButtons.YesNo); //play again yes or no
                    if (playAgain == DialogResult.Yes)
                    {
                        ResetGame();
                        pbxYouWinDroo.Visible = false;
                        pbxMovingPicture3Droo.Visible= false;
                    }
                    else
                    {
                        Application.Exit(); //end application when pressing no
                    }

                    pbxYouWinDroo.Visible = false;
                    pbxMovingPicture3Droo.Visible = false;

                    //play a sound when you win
                    System.Media.SoundPlayer player =
                    new System.Media.SoundPlayer();
                    player.SoundLocation = @"C:\Users\Daphne school\Downloads\victorymale-version-230553.wav";
                    player.Load();
                    player.Play();

                    ResetGame(); //resets the game
                    return;
                }

                pgbWrongGuessesDroo.Value++;
                

                
                attemptsLeftDroo--; //subtract an attempt if the answer is wrong

                MessageBox.Show($"Wrong guess! Attempts left: {attemptsLeftDroo}"); // wrong guess
                LogMessage($"Wrong guess: {userGuess}, try again.");

                pgbAttemptsLeftDroo.Value = attemptsLeftDroo;


                int difference = Math.Abs(mysteryNumberDroo - userGuess); //calculate difference between user's guess and the mystery number for the hot/cold bar
                UpdateTemperatureMeter(difference);

                // end game when there are no attempts left
                if (attemptsLeftDroo <= 0)
                {

                    pbxYouLoseDroo.Left = gbxPlayDroo.Left;
                    pbxYouLoseDroo.Visible = true;
                    
                    pbxMovingPicture4Droo.Left = gbxInformationDroo.Left;
                    pbxMovingPicture4Droo.Left = gbxPlayDroo.Left;
                    pbxMovingPicture4Droo.Left = gbxSetupDroo.Left;

                    MessageBox.Show($"Game over! The mystery number was {mysteryNumberDroo}.");
                    LogMessage($"Game over: Ran out of guesses.");


                    DialogResult playAgain = MessageBox.Show("Do you want to play again ? ", "Play Again", MessageBoxButtons.YesNo);
                    if (playAgain == DialogResult.Yes)
                    {
                        ResetGame();
                        pbxYouWinDroo.Visible = false;
                        pbxMovingPicture4Droo.Visible = false;
                    }
                    else
                    {
                        Application.Exit();
                    }

                    pbxYouLoseDroo.Visible = false;
                    pbxMovingPicture4Droo.Visible= false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for your guess.");
                LogMessage("Guess failed: number entered invalid.");
            }
        }

        private void UpdateTemperatureMeter(int difference) //update the temperature meter
        {
            try
            {
                int maxDifference = Math.Max(maxvalueDroo - minvalueDroo, 1); // Avoid dividing by 0
                int trackBarValue = (int)((1.0 - (double)difference / maxDifference) * tbHotColdDroo.Maximum);
                //difference = absolute difference between user guess and mystery number.
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
            mysteryNumberDroo = 0;
            attemptsLeftDroo = 0;

            tbxStartDroo.Clear();
            tbxStopDroo.Clear();
            tbxGuessDroo.Clear();
            tbxNumberOfAttemptsDroo.Clear();

            pgbAttemptsLeftDroo.Value = 0;
            pgbWrongGuessesDroo.Value = 0;
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
            tbxGuessDroo.Text = mysteryNumberDroo.ToString(); //puts answer in textbox
            MessageBox.Show($"The mystery number is filled in for you."); //show message if success
            LogMessage($"Cheat used: {mysteryNumberDroo}");
            if (mysteryNumberDroo == 0) //show message if there's no mystery number
            {
                MessageBox.Show("Please generate a mystery number first!");
                LogMessage("Cheat failed: Generate mystery number.");
            }
            else //show the mystery number in a messagebox
            {
                MessageBox.Show($"The mystery number was {mysteryNumberDroo}!");
                LogMessage($"Cheat: {mysteryNumberDroo}");
            }
        }

        private void btnlocatedroo_Click(object sender, EventArgs e) //locate the game
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            MessageBox.Show($"The game is located at:\n{appPath}", "Locate Application");
            LogMessage($"Location: \n{appPath}");
        }

        private void LogMessage(string message) //log actions and errors
        {
            rtbInformationDroo.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}"); //the text that will show up in the rich text box, each on a new line
            rtbInformationDroo.ScrollToCaret(); //make text viewable if it exceeds the viewable area
        }

        private void btnShowGameDroo_Click(object sender, EventArgs e) //press button to reveal the game
        {
            pbxMovingpicture2Droo.Visible = false;
            tbxEnterYourNameDroo.Visible = false;
            lblNameDroo.Visible = false;
            btnSubmitDroo.Visible = false;
            btnShowGameDroo.Visible = false;
        }

        private void btnSubmitDroo_Click(object sender, EventArgs e) //submit your name
        {
      
                string playerName = tbxEnterYourNameDroo.Text.Trim(); //retrieves trimmed text from textbox, no trim makes it look messy
            
                if (string.IsNullOrEmpty(playerName)) //show message if you didn't enter a name
                {
                    MessageBox.Show("Please enter a name before you start the game.");
                    LogMessage("Error: Player name required!");
                    return;
                }

            MessageBox.Show($"Hi, {playerName}, Welcome to the Mystery Number game!"); //show message if you successfully entered name
            LogMessage($"Player name entered: {playerName}");
           
        }

        private void btnDefaultDroo_Click(object sender, EventArgs e) //set default values for easy testing
        {
            tbxStartDroo.Text = "0";
            tbxStopDroo.Text = "100";
            tbxNumberOfAttemptsDroo.Text = "5";
        }

    }
}
