using Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using WPRssReader.Common;

namespace WPRssReader.ViewModel
{
    public class VMArticle : INotifyPropertyChanged
    {

        private const string CURRENT_ARTICLE = "CurrentArticle";
        private Article currentArticle;
        public Article CurrentArticle
        {
            get
            {
                return currentArticle;
            }
            set
            {
                currentArticle = value;
                OnPropertyChanged(CURRENT_ARTICLE);
                OnPropertyChanged(ARTICLE_IMAGE);
            }
        }

        private const string ARTICLE_IMAGE = "ArticleImage";
        public BitmapImage ArticleImage
        {
            get
            {
                if (CurrentArticle == null || CurrentArticle.IconUrl == null)
                    return null;
                return new BitmapImage(new Uri(CurrentArticle.IconUrl, UriKind.Absolute));
            }
        }

        private RelayCommand cmdAddToFavori;
        public ICommand CmdAddToFavori
        {
            get
            {
                if(cmdAddToFavori == null)
                {
                }
                return cmdAddToFavori;
            }
        }

        private void AddToFavori(Article article)
        {
            FluxManager.getInstance().AddArticleFavori(article);
        }

        private RelayCommand cmdAddTag;
        public ICommand CmdAddTag
        {
            get
            {
                if(cmdAddTag == null)
                {
                }
                return cmdAddTag;
            }
        }

        private void AddTag(Article article)
        {

        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
