using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using Dapper;
using System.Collections.ObjectModel;
using YellowMelon.Model;
using System.ComponentModel;

namespace YellowMelon.ViewModel
{
    public class PlayListViewModel
    {
        public ObservableCollection<MList> PlayLists;
        public int CurrentIdx = 0;
        
        private User currentUser = null;

        public MList playList { get => CurrentIdx > -1 ? PlayLists[CurrentIdx] : PlayLists[0]; }


        public PlayListViewModel(User user)
        {
            CurrentIdx = 0;
            currentUser = user;
            InitMusicList(user.Index);
        }

        public void InitMusicList(int userid)
        {
            List<MList> playlist = DataAccess.db.Query<MList>("select * from list_tb where u_idx=@id", new { id = userid }).ToList();
            foreach (MList list in playlist)
            {
                list.PlayList = new ObservableCollection<ListMusic>(
                    DataAccess.db.Query<ListMusic, Music, Artist, ListMusic>("select a.lsm_idx, a.lst_idx, a.lsm_pos, " +
                    "b.mus_idx, b.mus_name, b.mus_exp, b.mus_like, b.mus_is_open, b.mus_link, " +
                    "c.* from " +
                    "listmusic_tb as a inner join music_tb as b on a.mus_idx = b.mus_idx join artist_tb as c on b.art_idx = c.art_idx " +
                    "where a.lst_idx = @id order by a.lsm_pos", (lmusic, music, artist) => {
                        music.FKArtist = artist;
                        lmusic.FK_Music = music;
                        return lmusic;
                    }, new { id = list.Index },
                    splitOn: "mus_idx,art_idx"));
            }
            this.PlayLists = new ObservableCollection<MList>(playlist);
        }

        public void AddMusic(Music music, int pos)
        {
            DataAccess.execute("update listmusic_tb set lsm_pos = lsm_pos + 1 " +
                "where lst_idx = "+ playList.Index +" and lsm_pos >= " + pos);
            
            long id = DataAccess.executeQuery("insert into listmusic_tb (lst_idx, mus_idx, lsm_pos) values (" + playList.Index + ", " + music.Index + ", " + pos + ");" +
                "select last_insert_rowid()").GetInt64(0);
            ListMusic lstMusic = new ListMusic()
            {
                Index = (int)id,
                FK_Music = music,
                Music = music.Index,
                Pos = pos
            };
            playList.PlayList.Insert(pos-1, lstMusic);
            //playList.PlayList = new ObservableCollection<ListMusic>(sort());
        }
        public void RemoveMusic(int idx)
        {
            DataAccess.execute("delete from listmusic_tb where lst_idx = " + playList.Index +
                " and lsm_pos = " + idx);
            DataAccess.execute("update listmusic_tb set lsm_pos = lsm_pos - 1 " +
                "where lst_idx = " + playList.Index
                + " and lsm_pos > " + idx);
            playList.PlayList.RemoveAt(idx-1);
            foreach (ListMusic listMusic in playList.PlayList)
            {
                if (listMusic.Pos > idx)
                    listMusic.Pos--;
            }
        }

        public void AddPlayList(string name)
        {
            int id = DataAccess.executeQuery("insert into list_tb (u_idx, lst_title) values (" + currentUser.Index + ", '" + name + "');" +
                "select last_insert_rowid();").GetInt32(0);
            PlayLists.Add(new MList()
            {
                Index = id,
                PlayList = new ObservableCollection<ListMusic>(),
                Title = name,
                User = currentUser.Index
            });
            CurrentIdx = PlayLists.Count-1;
        }

        public void RemovePlayList()
        {
            DataAccess.executeQuery("delete from list_tb where lst_idx = " + playList.Index);
            CurrentIdx--;
            PlayLists.Remove(PlayLists[CurrentIdx+1]);
        }
    }
}
