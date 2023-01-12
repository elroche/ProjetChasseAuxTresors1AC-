using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCAT
{
    class Program
    {
        public static void AfficheJeu(string[,] plateauJeu)
        {

            for (int ligne = 0; ligne < plateauJeu.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < plateauJeu.GetLength(1); colonne++)
                {
                    Console.Write(plateauJeu[ligne, colonne] + " ");
                }

                Console.WriteLine();
            }

        }

        public static int[] TailleTab()
        {
            //Initialise un tableau contenant le nombre de lignes et de colonnes choisies par le joueur et retourne ce tableau

            int[] taille = new int[2];
            do
            {
                Console.WriteLine("Combien de lignes voulez-vous ?");
                taille[0] = int.Parse(Console.ReadLine());
                if (taille[0] <= 1)
                    Console.WriteLine("Saisie incorrecte !");
            } while (taille[0] <= 1);

            do
            {
                Console.WriteLine("Combien de colonnes voulez-vous ?");
                taille[1] = int.Parse(Console.ReadLine());
                if (taille[1] <= 1)
                    Console.WriteLine("Saisie incorrect !");
            } while (taille[1] <= 1);

            return taille;
        }


        public static string[,] GenereTab(int[] taille)
        {
            //Initialise un tableau de chaine de caracteres avec dans chaque case le mot "ND" et retourne le tableau

            string[,] plateauJeu = new string[taille[0], taille[1]];
            for (int i = 0; i < taille[0]; i++)
            {
                for (int j = 0; j < taille[1]; j++)
                {
                    plateauJeu[i, j] = "ND";
                }
            }

            return plateauJeu;
        }

        public static int[] SelectionCase(string[,] plateauJeu)
        {
            // Vérifie que la case séléctionné est bien une case non découverte
            int[] selection = new int[2];
            do
            {
                // Saisie la ligne à séléctionner et vérifie qu'elle existe bien dans le plateau de Jeu
                do
                {
                    Console.WriteLine("Entrez le numéro de la ligne que souhaitez sélectionner");
                    selection[0] = int.Parse(Console.ReadLine()) - 1;
                    if (selection[0] >= plateauJeu.GetLength(0) || selection[0] < 0)
                        Console.WriteLine("Saisie incorrect !");
                } while (selection[0] >= plateauJeu.GetLength(0) || selection[0] < 0);

                // Saisie la colonne à séléctionner et vérifie qu'elle existe bien dans le plateau de Jeu
                do
                {
                    Console.WriteLine("Entrez le numéro de la colonne que souhaitez sélectionner");
                    selection[1] = int.Parse(Console.ReadLine()) - 1;
                    if (selection[1] >= plateauJeu.GetLength(1) || selection[1] < 0)
                        Console.WriteLine("Saisie incorrect !");
                } while (selection[1] >= plateauJeu.GetLength(1) || selection[1] < 0);

            } while (plateauJeu[selection[0], selection[1]] != "ND");
            return selection;

        }


        public static int GenererObjet(int[] selection, string[,] plateauObjets)
        {
            //On ajoute les mines et tresors générés au tableau vide
            Random objets = new Random();
            int nbMines = objets.Next(plateauObjets.GetLength(0) / 2, plateauObjets.Length / 2 + 1);

            int nbTresors;
            if (plateauObjets.Length == 4)
            {
                nbTresors = objets.Next(1, 3);
            }
            else
            {
                nbTresors = objets.Next(1, 4);
            }

            //On génère aléatoirement la position de nos mines, tout en vérifiant qu'elles ne se localisent pas sur la première case que le joueur a séléctionné, ou une mine déjà positionnée
            for (int i = 0; i < nbMines; i++)
            {

                int ligneMine, colonneMine;

                do
                {
                    ligneMine = objets.Next(0, plateauObjets.GetLength(0));
                    colonneMine = objets.Next(0, plateauObjets.GetLength(1));

                } while ((ligneMine == selection[0] && colonneMine == selection[1]) || plateauObjets[ligneMine, colonneMine] == "M");
                plateauObjets[ligneMine, colonneMine] = "M";
            }

            Console.WriteLine("Etape 1");

            for (int i = 0; i < nbTresors; i++)
            {
                int ligneTresor;
                int colonneTresor;
                do
                {
                    ligneTresor = objets.Next(0, plateauObjets.GetLength(0));
                    colonneTresor = objets.Next(0, plateauObjets.GetLength(1));

                } while ((ligneTresor == selection[0] && colonneTresor == selection[1]) || plateauObjets[ligneTresor, colonneTresor] == "T" || plateauObjets[ligneTresor, colonneTresor] == "M");
                plateauObjets[ligneTresor, colonneTresor] = "T";
            }

            Console.WriteLine("Etape 2");
            return nbMines;
        }

        public static void Compteur(string[,] plateauObjets, int[] caseTableau)
        {
            int compteur = 0;


            if (caseTableau[0] - 1 >= 0)
            {
                if (plateauObjets[caseTableau[0] - 1, caseTableau[1]] == "M")
                    compteur++;
                else if (plateauObjets[caseTableau[0] - 1, caseTableau[1]] == "T")
                    compteur += 2;

                if (caseTableau[1] - 1 >= 0)
                {
                    if (plateauObjets[caseTableau[0] - 1, caseTableau[1] - 1] == "M")
                        compteur++;
                    else if (plateauObjets[caseTableau[0] - 1, caseTableau[1] - 1] == "T")
                        compteur += 2;
                }

                if (caseTableau[1] + 1 <= plateauObjets.GetLength(1) - 1)
                {
                    if (plateauObjets[caseTableau[0] - 1, caseTableau[1] + 1] == "M")
                        compteur++;
                    else if (plateauObjets[caseTableau[0] - 1, caseTableau[1] + 1] == "T")
                        compteur += 2;
                }
            }

            if (caseTableau[1] - 1 >= 0)
            {
                if (plateauObjets[caseTableau[0], caseTableau[1] - 1] == "M")
                    compteur++;
                else if (plateauObjets[caseTableau[0], caseTableau[1] - 1] == "T")
                    compteur += 2;
            }

            if (caseTableau[1] + 1 <= plateauObjets.GetLength(1) - 1)
            {
                if (plateauObjets[caseTableau[0], caseTableau[1] + 1] == "M")
                    compteur++;
                else if (plateauObjets[caseTableau[0], caseTableau[1] + 1] == "T")
                    compteur += 2;
            }

            if (caseTableau[0] + 1 <= plateauObjets.GetLength(0) - 1)
            {
                if (plateauObjets[caseTableau[0] + 1, caseTableau[1]] == "M")
                    compteur++;
                else if (plateauObjets[caseTableau[0] + 1, caseTableau[1]] == "T")
                    compteur += 2;

                if (caseTableau[1] - 1 >= 0)
                {
                    if (plateauObjets[caseTableau[0] + 1, caseTableau[1] - 1] == "M")
                        compteur++;
                    else if (plateauObjets[caseTableau[0] + 1, caseTableau[1] - 1] == "T")
                        compteur += 2;
                }

                if (caseTableau[1] + 1 <= plateauObjets.GetLength(1) - 1)
                {
                    if (plateauObjets[caseTableau[0] + 1, caseTableau[1] + 1] == "M")
                        compteur++;
                    else if (plateauObjets[caseTableau[0] + 1, caseTableau[1] + 1] == "T")
                        compteur += 2;
                }
            }

            if ( compteur != 0)
            {
                string compteurAffiche = compteur.ToString();
                plateauObjets[caseTableau[0], caseTableau[1]] = compteurAffiche;
            }
            

        }


        public static void PlacerCompteurPlateauObjet ( string[,] plateauObjets)
        {
            int[] caseTableau = new int[2];

            for (int i = 0; i < plateauObjets.GetLength(0); i++)
            {
                caseTableau[0] = i;
                for (int j = 0; j < plateauObjets.GetLength(0); j++)
                {
                    caseTableau[1] = j;
                    if (plateauObjets[i,j] != "M" && plateauObjets[i,j] != "T")
                    {
                        Compteur(plateauObjets, caseTableau);
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            /*int cpt =  0;
        	string test = cpt.ToString();
        	Console.WriteLine(test);*/

            int[] taille = TailleTab();
            string[,] plateauJeu = GenereTab(taille);
            AfficheJeu(plateauJeu);
            int[] selection = SelectionCase(plateauJeu);
            string[,] plateauObjets = new string[plateauJeu.GetLength(0), plateauJeu.GetLength(1)];

            int nbMines = GenererObjet(selection, plateauObjets);
            PlacerCompteurPlateauObjet(plateauObjets);
            AfficheJeu(plateauObjets);
            Console.ReadLine();

        }
    }

}
