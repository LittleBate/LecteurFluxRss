using Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace LecteurFluxRss
{
    public class Program
    {
        static void Main(string[] args)
        {
            //test_Flux_And_Article();
            //test_FluxManager_And_LoaderFluxRss();
            test_Tag_EnregistrementFovarisEnBdd_LectureDesFluxEnBdd();
        }

        /// <summary>
        /// Manaer de flux utilisé pour les testes
        /// </summary>
        private static FluxManager manager;

        /// <summary>
        /// Application Concole permettant :
        /// -> la navigation dans les flux
        /// -> la recherche en fonction d'un tag
        /// -> l'ajout de tag à un artcle ou à un flux
        /// -> la sauvegarde d'un article en tant que favori
        /// </summary>
        private static void test_Tag_EnregistrementFovarisEnBdd_LectureDesFluxEnBdd()
        {
            manager = FluxManager.getInstance();
            manager.SauvegardeManager = new SauvegardeManagerBdd();

            manager.AddFluxLink(@"http://www.developpez.com/index/rss");
            //manager.AddFluxTitle(@"http://radiofrance-podcast.net/podcast09/rss_13100.xml");
            //manager.AddFluxTitle(@"http://lesjoiesducode.fr/rss");
            manager.ChargerDonnées();
            manager.Load();

            Menu();
        }

        /// <summary>
        /// Test l'affichage d'un flux chargé à partir de internet
        /// </summary>
        private static void test_FluxManager_And_LoaderFluxRss()
        {
            FluxManager manager = FluxManager.getInstance();
            //manager.LoadFluxByFile();

            manager.AddFluxLink(@"http://www.developpez.com/index/rss");
            //manager.AddFluxTitle(@"http://radiofrance-podcast.net/podcast09/rss_13100.xml");
            manager.AddFluxLink(@"http://lesjoiesducode.fr/rss");
            manager.Load();

            manager.ListeFlux.ForEach(f =>
                {
                    Console.WriteLine(f);
                    Console.WriteLine("\n\n\n******************************************************\n\n\n");
                });        
        }

        /// <summary>
        /// Test des classe Article et flux
        /// </summary>
        private static void test_Flux_And_Article()
        {
            Flux flux = new Flux()
            {
                Title = "Total Voili Voilou",
                Link = "http://VoiliVoilou",
                Description = "tezrzefdfvdfezfefgvrdsdvc cv"
            };
            for (int i = 0; i < 10; i++)
            {
                flux.AddArticle(new Article() 
                {
                    Author = "Toto" + i,
                    DatePublication = DateTime.Now,
                    Description = "Voili Voilou" + i * 3,
                    Link = "http://VoiliVoilou/" + i,
                    Title = "VOILI VOILOU " + i
                });
            }

            Console.WriteLine(flux);
        }

        /// <summary>
        /// Menu de l'application console.
        /// Propose :
        /// -> la navigation dans les flux
        /// -> la recherche d'un tag
        /// -> quitter l'application
        /// </summary>
        private static void Menu()
        {
            int index = -1;
            do
            {

                Console.Clear();
                Console.WriteLine("1/ Naviguer dans les flux");
                Console.WriteLine("2/ Rechercher un Tag");
                Console.WriteLine("3/ Quitter");
                Console.WriteLine("\n Saisir le numéro du choix : ");
                do
                {
                    string val = Console.ReadLine();
                    if (!Int32.TryParse(val, out index))
                    {
                        index = -1;
                    }
                } while (index < 1 || index > 3);

                if (index == 1)
                {
                    NaviguerDansLesFlux(manager.ListeFlux);
                }
                else if (index == 2)
                {
                    FindTag();
                }

            } while (index != 3);
        }

        /// <summary>
        /// Demande le tag à chercher.
        /// afficher l'ensemble des flux et des articles porteurs du tag
        /// </summary>
        private static void FindTag()
        {
            Tag t = SelectTag(manager.listeTagExistants);
            PrintListeFlux(manager.getFluxByTag(t));
            Console.ReadLine();
            PrintListeArticles(manager.getArticleByTag(t));
            Console.ReadLine();
        }

        /// <summary>
        /// Navigation dans lesflux.
        /// Selectiond'un flux.
        /// Sélection d'un article.
        /// Affichage de l'article.
        /// Possibilité de revenir à tous moment au menu
        /// </summary>
        /// <param name="listeFlux">liste des flux</param>
        private static void NaviguerDansLesFlux(List<Flux> listeFlux)
        {
            Flux f = SelectFlux(listeFlux);
            Article a = SelectArticle(f.ListArticle, f);
            PrintArticle(a);
        }

        private static Tag SelectTag(IEnumerable<Tag> listeTag)
        {
            PrintListeTag(listeTag, true);
            Console.Write(listeTag.Count());
            Console.Write(" Menu\n\n");
            int index = -1;
            Console.WriteLine("Saisir le numéro de tag: ");
            do
            {
                string val = Console.ReadLine();
                if (!Int32.TryParse(val, out index))
                {
                    index = -1;
                }
            } while (index < 0 || index > listeTag.Count());
            if (index == listeTag.Count())
            {
                Menu();
            }
            return listeTag.ElementAt(index);
        }

        /// <summary>
        /// Affichage d'un article
        /// </summary>
        /// <param name="article">article à afficher</param>
        private static void PrintArticle(Article article)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Titre : " + article.Title);
            Console.WriteLine("Auteur : " + article.Author);
            Console.WriteLine("Lien : " + article.Link);
            Console.WriteLine("Date de publication : " + article.DatePublication);
            Console.WriteLine("Description : " + article.Description);
            PrintListeTag(article.ListTag, false);


            Console.Write("1/ Ajouter un tag\n\n");
            Console.Write("2/ Ajouter aux favoris\n\n");
            Console.Write("3/ Menu\n\n");


            int index = -1;
            Console.WriteLine("Saisir le numéro d'article: ");
            do
            {
                string val = Console.ReadLine();
                if (!Int32.TryParse(val, out index))
                {
                    index = -1;
                }
            } while (index != 1 && index != 2 && index != 3);

            if (index == 3)
            {
                Menu();
            }
            else if (index == 1)
            {
                string tagName = String.Empty;
                Console.WriteLine("Saisir le nom du Tag : ");
                do
                {
                    tagName = Console.ReadLine();
                } while (String.IsNullOrEmpty(tagName.Trim()));
                article.AddTag(new Tag(tagName));

            }
            else if (index == 2)
            {
                manager.AddArticleFavori(article);
                Menu();

            }

            Console.ReadLine();
        }

        /// <summary>
        /// Affiche une liste de tags
        /// </summary>
        /// <param name="listeTag">liste de tag à afficher</param>
        /// <param name="clear">définit si on doit clear la console</param>
        private static void PrintListeTag(IEnumerable<Tag> listeTag, bool clear)
        {
            if (clear)
            {
                Console.Clear();
            }
            foreach (Tag t in listeTag)
            {
                Console.Write(listeTag.ToList().IndexOf(t));
                Console.Write(t.Name);
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Affichage des articles et propose la selectionde 
        /// l'article à afficher ou une action à effectuer
        /// </summary>
        /// <param name="listeArticles">liste des article à afficher</param>
        /// <param name="f">flux contenant les article</param>
        /// <returns>l'article séléctionné</returns>
        private static Article SelectArticle(IEnumerable<Article> listeArticles, Flux f)
        {
            PrintListeArticles(listeArticles);
            Console.Write(listeArticles.Count());
            Console.Write(" Ajouter un tag\n\n");
            Console.Write(listeArticles.Count() + 1);
            Console.Write(" Menu\n\n");
            int index = -1;
            Console.WriteLine("Saisir le numéro d'article: ");
            do
            {
                string val = Console.ReadLine();
                if (!Int32.TryParse(val, out index))
                {
                    index = -1;
                }
            } while (index < 0 || index > listeArticles.Count() + 1);
            if (index == listeArticles.Count() +1 )
            {
                Menu();
            }
            else if (index == listeArticles.Count())
            {
                string tagName = String.Empty;
                Console.WriteLine("Saisir le nom du Tag : ");
                do
                {
                    tagName = Console.ReadLine();
                } while (String.IsNullOrEmpty(tagName.Trim()));

                f.AddTag(new Tag(tagName));
                Menu();
            }
            return listeArticles.ElementAt(index);
        }

        /// <summary>
        /// Affiche une liste d'articles
        /// </summary>
        /// <param name="listeArticles">liste des articles à afficher</param>
        private static void PrintListeArticles(IEnumerable<Article> listeArticles)
        {
            Console.Clear();
            Console.WriteLine("Articles : ");
            foreach (Article a in listeArticles)
            {
                Console.Write(listeArticles.ToList().IndexOf(a));
                Console.Write(a.Title);
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Afficage des flux et séléction du flux à afficher 
        /// </summary>
        /// <param name="listeFlux">liste de flux à afficher</param>
        /// <returns>le lux séléctionné</returns>
        private static Flux SelectFlux(IEnumerable<Flux> listeFlux)
        {

            PrintListeFlux(listeFlux);
            Console.Write(listeFlux.Count());
            Console.Write(" Menu\n\n");
            int index = -1;
            Console.WriteLine("Saisir le numéro de flux : ");
            do
            {
                string val = Console.ReadLine();
                if (!Int32.TryParse(val, out index))
                {
                    index = -1;
                }
            } while (index < 0 || index > listeFlux.Count());
            if (index == listeFlux.Count())
            {
                Menu();
            }
            return listeFlux.ElementAt(index);
        }

        /// <summary>
        /// Affiche une liste de flux
        /// </summary>
        /// <param name="listeFlux">liste de flux à afficher</param>
        private static void PrintListeFlux(IEnumerable<Flux> listeFlux)
        {
            Console.Clear();
            Console.WriteLine("Flux : ");
            foreach(Flux f in listeFlux)
            {
                Console.Write(listeFlux.ToList().IndexOf(f));
                Console.Write(f.Title);
                Console.Write("\n");
            }

        }
    
    
    }
}
