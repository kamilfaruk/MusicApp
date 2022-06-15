using MusicApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            musicList = GetMusics();
            recentMusic = musicList.Where(x => x.IsRecent == true).FirstOrDefault();
        }

        ObservableCollection<Music> musicList;
        public ObservableCollection<Music> MusicList
        {
            get { return musicList; }
            set
            {
                musicList = value;
                OnPropertyChanged();
            }
        }

        private Music recentMusic;
        public Music RecentMusic
        {
            get { return recentMusic; }
            set
            {
                recentMusic = value;
                OnPropertyChanged();
            }
        }

        private Music selectedMusic;
        public Music SelectedMusic
        {
            get { return selectedMusic; }
            set
            {
                selectedMusic = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(PlayMusic);

        private void PlayMusic()
        {
            if (selectedMusic != null)
            {
                var viewModel = new PlayerViewModel(selectedMusic, musicList);
                var playerPage = new PlayerPage { BindingContext = viewModel };

                var navigation = Application.Current.MainPage as NavigationPage;
                navigation.PushAsync(playerPage, true);
            }
        }

        private ObservableCollection<Music> GetMusics()
        {
            return new ObservableCollection<Music>
            {
              new Music { Title = "Baba Yorgun", Artist = "Ece Ronay", Url = "https://www.mp3indirdur.info/mp3/indirdurArsiv333/Ece-Ronay/Hiyar/Ece-Ronay-Baba-Yorgun-(Alper-Egri-Remix).mp3", CoverImage = "https://www.mp3indirdur.mobi/album/Ece-Ronay-Hiyar.jpg", IsRecent = true},
              new Music { Title = "Bilmem Mi?", Artist = "Sefo", Url = "https://www.mp3indirdur.info/mp3/indirdurArsiv333/Sefo/Bonita/Sefo-Bilmem-Mi.mp3", CoverImage = "https://i.ytimg.com/vi/Q2eFl9kkA_g/sddefault.jpg"},
              new Music { Title = "Dinle Beni Bi", Artist = "Yüz Yüzeyken Konuşuruz", Url = "https://www.mp3indirdur.info/mp3/indirdurArsiv333/Yuzyuzeyken-Konusuruz/Akustik-Travma/Yuzyuzeyken-Konusuruz-Dinle-Beni-Bi.mp3", CoverImage = "https://www.mp3indirdur.mobi/album/Yuzyuzeyken-Konusuruz-Akustik-Travma.jpg"}
            };
        }
    }
}