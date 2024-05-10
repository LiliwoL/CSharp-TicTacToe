using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CSharp_TicTacToe
{
    public partial class TicTacToeForm : Form
    {
        #region "Déclaration des attributs"
        // ===================================
        // Attributes
        // ===================================

        // Dictionnaire qui contiendra la liste des 9 boutons
        Dictionary<int, Button> buttonsDictionary = new Dictionary<int, Button>();

        // ID du joueur en cours (1 ou 2)
        int currentGamer = 1;

        // Etats possibles d'une case
        enum state
        {
            VIDE    = 0,
            CROIX   = 1,
            ROND    = 2
        };

        // Tableau d'état des cases
        Dictionary<int, state> resultTable = new Dictionary<int, state>();
        #endregion

        public TicTacToeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Chargement de l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TicTacToe_Load(object sender, EventArgs e)
        {
            // Initialisation du dict des boutons et gestion de l'événement CLIC
            InitButtonsDictionary();

            // Lancement de la partie
            NewGame();
        }
        
        /// <summary>
        ///     Création de la liste des boutons de l'app
        /// </summary>
        private void InitButtonsDictionary()
        {
            // Création de la liste contenant les boutons
            buttonsDictionary.Add(1,button1);
            buttonsDictionary.Add(2,button2);
            buttonsDictionary.Add(3,button3);
            buttonsDictionary.Add(4,button4);
            buttonsDictionary.Add(5,button5);
            buttonsDictionary.Add(6,button6);
            buttonsDictionary.Add(7,button7);
            buttonsDictionary.Add(8,button8);
            buttonsDictionary.Add(9,button9);

            // Réinitialisation de la valeur des boutons et inscription au même événement
            foreach (KeyValuePair<int, Button> entry in buttonsDictionary)
            {
                Button currentButton = entry.Value as Button;

                // Texte
                currentButton.Text = "";

                // Event Handler Subscribing - Inscription au gestionnaire d'événement
                currentButton.Click += ButtonClickHandler;
            }
        }


        /// <summary>
        ///     Lancement de la partie
        /// </summary>
        private void NewGame()
        {
            // Réinitialisation de la valeur des boutons et inscription au même événement
            foreach (KeyValuePair<int, Button> entry in buttonsDictionary)
            {
                Button currentButton = entry.Value as Button;                

                // Texte
                currentButton.Text = "";
                // Couleur de fond
                currentButton.BackColor = Color.White;
            }

            // Réinitialisation tableau de contenu des cases
            for(int i = 1; i < 10; i++)
            {
                resultTable[i] = state.VIDE;
            }

            // Changement de joueur
            ChangeGamer(1);
        }


        /// <summary>
        /// Gestionnaire du click des boutons
        /// </summary>
        /// <param name="sender">Bouton cliqué</param>
        /// <param name="e"></param>
        private void ButtonClickHandler(object sender, EventArgs e)
        {
            // Change texte du bouton en fonction du joueur
            ((Button)sender).Text = currentGamer == 1 ? "X" : "O";
            // Change couleur du texte
            ((Button)sender).ForeColor = currentGamer == 1 ? Color.Green : Color.Red;

            // Case cochée
            var name = ((Button)sender).Name;
            /// @TODO Moche
            int checkedCase = int.Parse(name.Substring(name.Length - 1));                

            // Met à jour resultTable qui sera lue dans checkGameStatus
            resultTable[checkedCase] = currentGamer == 1 ? state.CROIX : state.ROND;

            // Vérifie si la partie est gagnée
            if (CheckGameStatus() == state.VIDE)
            {
                // Changement de joueur
                ChangeGamer();
            }
            else
            {
                EndGame();
            }            
        }

        private void EndGame()
        {            
            if (MessageBox.Show("Bravo! " + currentGamer + "\nDo you want to continue?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NewGame();
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Methode appelée à chaque clic de bouton
        /// Le gagnant est le joueur en cours
        /// </summary>
        /// <returns></returns>
        private state CheckGameStatus()
        {
            // Par défaut, pas de gagnant
            state winner = state.VIDE;

            #region "Horizontal checks"
            // 1 2 3
            if (
                resultTable[1].Equals(resultTable[2]) &&
                resultTable[1].Equals(resultTable[3]) &&
                !resultTable[1].Equals(state.VIDE)
                )
            {
                Console.WriteLine(resultTable[1] + " wins");
                // Colors
                buttonsDictionary[1].BackColor = Color.OrangeRed;
                buttonsDictionary[2].BackColor = Color.OrangeRed;
                buttonsDictionary[3].BackColor = Color.OrangeRed;

                winner = resultTable[1];
            }

            // 4 5 6
            if (
                resultTable[4].Equals(resultTable[5]) &&
                resultTable[4].Equals(resultTable[6]) &&
                !resultTable[4].Equals(state.VIDE)
                )
            {
                Console.WriteLine(resultTable[4] + " wins");
                // Colors
                buttonsDictionary[4].BackColor = Color.OrangeRed;
                buttonsDictionary[5].BackColor = Color.OrangeRed;
                buttonsDictionary[6].BackColor = Color.OrangeRed;

                winner = resultTable[4];
            }

            // 7 8 9
            if (
                resultTable[7].Equals(resultTable[8]) &&
                resultTable[7].Equals(resultTable[9]) &&
                !resultTable[7].Equals(state.VIDE)
                )
            {
                Console.WriteLine(resultTable[7] + " wins");
                // Colors
                buttonsDictionary[7].BackColor = Color.OrangeRed;
                buttonsDictionary[8].BackColor = Color.OrangeRed;
                buttonsDictionary[9].BackColor = Color.OrangeRed;

                winner = resultTable[7];
            }
            #endregion

            #region "Vertical checks"
            // 1 4 7
            if (
                resultTable[1].Equals(resultTable[4]) &&
                resultTable[1].Equals(resultTable[7]) &&
                !resultTable[1].Equals(state.VIDE)
                )
            {
                Console.WriteLine(resultTable[1] + " wins");
                // Colors
                buttonsDictionary[1].BackColor = Color.OrangeRed;
                buttonsDictionary[4].BackColor = Color.OrangeRed;
                buttonsDictionary[7].BackColor = Color.OrangeRed;

                winner = resultTable[1];
            }

            // 2 5 8
            if (
                resultTable[2].Equals(resultTable[5]) &&
                resultTable[2].Equals(resultTable[8]) &&
                !resultTable[2].Equals(state.VIDE)
                )
            {
                Console.WriteLine(resultTable[2] + " wins");
                // Colors
                buttonsDictionary[2].BackColor = Color.OrangeRed;
                buttonsDictionary[5].BackColor = Color.OrangeRed;
                buttonsDictionary[8].BackColor = Color.OrangeRed;

                winner = resultTable[2];
            }

            // 3 6 9
            if (
                resultTable[3].Equals(resultTable[6]) &&
                resultTable[3].Equals(resultTable[9]) &&
                !resultTable[3].Equals(state.VIDE)
                )
            {
                Console.WriteLine(resultTable[3] + " wins");
                // Colors
                buttonsDictionary[3].BackColor = Color.OrangeRed;
                buttonsDictionary[6].BackColor = Color.OrangeRed;
                buttonsDictionary[9].BackColor = Color.OrangeRed;

                winner = resultTable[3];
            }
            #endregion

            #region "Diagonal checks"            
            // 1 5 9
            if (
                resultTable[1].Equals(resultTable[5]) &&
                resultTable[5].Equals(resultTable[9]) &&
                !resultTable[9].Equals(state.VIDE)
                )
            {
                Console.WriteLine(resultTable[1] + " wins");
                // Colors
                buttonsDictionary[1].BackColor = Color.OrangeRed;
                buttonsDictionary[5].BackColor = Color.OrangeRed;
                buttonsDictionary[9].BackColor = Color.OrangeRed;

                winner = resultTable[1];
            }

            // 3 5 7
            if (
                resultTable[3].Equals(resultTable[5]) &&
                resultTable[3].Equals(resultTable[7]) &&
                !resultTable[3].Equals(state.VIDE)
                )
            {
                Console.WriteLine(resultTable[3] + " wins");
                // Colors
                buttonsDictionary[3].BackColor = Color.OrangeRed;
                buttonsDictionary[5].BackColor = Color.OrangeRed;
                buttonsDictionary[7].BackColor = Color.OrangeRed;

                winner = resultTable[3];
            }
            #endregion

            return winner;
        }

        /// <summary>
        /// Ce qui arrive au changement du joueur
        /// </summary>
        private void ChangeGamer(int? gamer = null)
        {
            // Check current Gamer
            if (gamer.HasValue)
            {
                currentGamer = gamer.Value;
            }
            else
            {
                currentGamer = currentGamer == 1 ? 2 : 1;
            }
            

            // Updates Form Text
            this.Text = "TicTacToe - Joueur " + currentGamer;
        }
    }
}
